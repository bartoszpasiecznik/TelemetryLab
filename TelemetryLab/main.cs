using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using TelemetryLab;
using TelemetryLab.Core;
using TelemetryLab.Data;
using TelemetryLab.Math;
using TelemetryLab.Memory;
using TelemetryLab.Models;
using TelemetryLab.Telemetry;
using TelemetryLab.Messages;

using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        if (Array.IndexOf(args, "--sample-json") >= 0)
        {
            WriteSampleJson();
            return;
        }

        try
        {
            var reader = new TelemetryReader(LMUConstants.TELEMETRY_FILE_NAME);

            var provider = new PlayerDataProvider();
            var lapTracker = new LapTracker();
            var sessionProvider = new SessionDataProvider();

            var loop = new TelemetryLoop(reader, provider, lapTracker, sessionProvider);
            loop.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to connect: " + e.Message);
        }
    }

    static void WriteSampleJson()
    {
        var lap = new LapCompletedMessage(
            type: "lapCompleted",
            sessionId: "session-123",
            lapNumber: 1,
            valid: true,
            driverName: "John Doe",
            lapTimeS: 123.45,
            bestLapTimeS: 120.0,
            fuelUsedL: 5.0,
            pitStop: null
        );

        var session = new { lapCompleted = new[] { lap } };
        var options = new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
            };

        var json = JsonSerializer.Serialize(session, options);
        System.IO.File.WriteAllText("src/from-csharp.json", json);    
    }
}
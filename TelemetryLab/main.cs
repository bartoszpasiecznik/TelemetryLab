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


class main
{
    
   static void Main()
    {

        try
        {
            var reader = new TelemetryReader(LMUConstants.TELEMETRY_FILE_NAME);

            var provider = new PlayerDataProvider();
            var lapTracker = new LapTracker();

            var loop = new TelemetryLoop(reader, provider, lapTracker);
            loop.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to connect: " + e.Message);
        }
    }
}
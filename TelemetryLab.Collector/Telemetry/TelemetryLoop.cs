using System.Diagnostics;
using TelemetryLab.Core;
using TelemetryLab.Math;
using TelemetryLab.Memory;

namespace TelemetryLab.Telemetry;

public class TelemetryLoop
{
    private readonly TelemetryReader _reader;
    private readonly PlayerDataProvider _provider;
    private readonly LapTracker _lapTracker;

    public TelemetryLoop(TelemetryReader reader, PlayerDataProvider provider, LapTracker lapTracker)
    {
        _reader = reader;
        _provider = provider;
        _lapTracker = lapTracker;
    }

    public void Run()
    {
        var sw = Stopwatch.StartNew();
        var interval = TimeSpan.FromMilliseconds(1000.0 / 30.0);//30HZ Data Refresh Rate
        Velocity curSpeed = new Velocity();
        curSpeed = Velocity.FromMs(0);


        while (true)
        {
            if (sw.Elapsed <  interval)
                continue;
            sw.Restart();
            
            var data = _reader.Read();
            var playerIndex = _provider.GetPlayerIndex(data);

            if (!_provider.HasValidPlayerIndex(data, playerIndex))
            {
                Console.WriteLine("Player Index not valid yet.");
                Thread.Sleep(250);
                continue;
            }
            
            var playerTelemetry = _provider.GetPlayerTelem(data, playerIndex);
            var playerScoring = _provider.GetPlayerScoring(data, playerIndex);
            
            curSpeed = Velocity.FromMs(System.Math.Sqrt((playerTelemetry.mLocalVel.x * playerTelemetry.mLocalVel.x) +
                                                        (playerTelemetry.mLocalVel.y * playerTelemetry.mLocalVel.y) +
                                                        (playerTelemetry.mLocalVel.z * playerTelemetry.mLocalVel.z)));
            

            if (_lapTracker.IsNewLap(playerTelemetry.mLapNumber))
            {
                Console.WriteLine();
                Console.WriteLine($"=== NEW LAP: {playerTelemetry.mLapNumber} ===");
                Console.WriteLine($"Last lap time: {playerScoring.mLastLapTime:F3}");
                Console.WriteLine($"Best lap time: {playerScoring.mBestLapTime:F3}");
            }
            
            Console.WriteLine(
                $"Lap {playerTelemetry.mLapNumber} | " +
                $"Speed {curSpeed.InKph.ToString("F1")} km/h | " +
                $"RPM {playerTelemetry.mEngineRPM:F0} | " +
                $"Gear {playerTelemetry.mGear} | " +
                $"Pos {playerScoring.mPlace}"
            );
        }
    }
}

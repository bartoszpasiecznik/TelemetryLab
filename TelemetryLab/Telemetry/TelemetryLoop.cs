using System.Diagnostics;
using TelemetryLab.Core;
using TelemetryLab.Math;
using TelemetryLab.Memory;
using TelemetryLab.Models;

namespace TelemetryLab.Telemetry;

public class TelemetryLoop
{
    private readonly TelemetryReader _reader;
    private readonly PlayerDataProvider _provider;
    private readonly LapTracker _lapTracker;

    private readonly SessionDataProvider _sessionProvider;

    public TelemetryLoop(
        TelemetryReader reader,
        PlayerDataProvider provider,
        LapTracker lapTracker,
        SessionDataProvider sessionProvider)
    {
        _reader = reader;
        _provider = provider;
        _lapTracker = lapTracker;
        _sessionProvider = sessionProvider;
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
            var sessionInfo = _provider.GetSessionInfo(data);

            var lap = new Lap(
                playerTelemetry.mLapNumber, 
                playerScoring.mLastLapTime, 
                playerScoring.mBestLapTime);
            
            var session = new Session(sessionInfo.mSession);

            var playerDriver = new Driver(new string(playerScoring.mDriverName).TrimEnd('\0'));

            var carData = new CarData(
                playerScoring.mPlace,
                playerTelemetry.mEngineRPM,
                curSpeed.InKph,
                playerTelemetry.mGear,
                playerTelemetry.mFilteredThrottle,
                playerTelemetry.mFilteredBrake,
                playerTelemetry.mFilteredClutch,
                playerTelemetry.mFilteredSteering,
                playerTelemetry.mFuel,
                new string(playerTelemetry.mFrontTireCompoundName).TrimEnd('\0'),
                new string(playerTelemetry.mRearTireCompoundName).TrimEnd('\0')
            );

            var playerCar = new Car(new string(playerScoring.mVehicleName).TrimEnd('\0'), new string(playerScoring.mVehicleClass).TrimEnd('\0'), carData);

            var track = new Track(new string(sessionInfo.mTrackName).TrimEnd('\0'), sessionInfo.mLapDist);
            
            curSpeed = Velocity.FromMs(System.Math.Sqrt((playerTelemetry.mLocalVel.x * playerTelemetry.mLocalVel.x) +
                                                        (playerTelemetry.mLocalVel.y * playerTelemetry.mLocalVel.y) +
                                                        (playerTelemetry.mLocalVel.z * playerTelemetry.mLocalVel.z)));
            

            if (_lapTracker.IsNewLap(playerTelemetry.mLapNumber))
            {
                Console.WriteLine();
                Console.WriteLine($"=== NEW LAP: {lap.LapNumber} ===");
                Console.WriteLine($"Last lap time: {lap.LastLapTime:F3}");
                Console.WriteLine($"Best lap time: {lap.BestLapTime:F3}");
            }
            
            Console.WriteLine(
                $"Player Name: {track.Name} | " +
                $"Session {track.LapDistance} | " +
                $"Lap {lap.LapNumber} | " +
                $"Speed {playerCar.CarData.Speed.ToString("F1")} km/h | " +
                $"RPM {playerCar.CarData.Rpm:F0} | " +
                $"Gear {playerCar.CarData.Gear} | " +
                $"Pos {playerCar.CarData.Position} | " + 
                $"Throttle {playerCar.CarData.Throttle:F2} | " +
                $"Brake {playerCar.CarData.Brake:F2} | " +
                $"Clutch {playerCar.CarData.Clutch:F2} | " +
                $"Steering {playerCar.CarData.Steering:F2} | "+
                $"Fuel {playerCar.CarData.Fuel:F2} | " + 
                $"Front Tyre Compound {playerCar.CarData.FrontTyreCompound} | " +
                $"Rear Tyre Compound {playerCar.CarData.RearTyreCompound}"
            );
        }
    }
}
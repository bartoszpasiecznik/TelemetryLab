using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using TelemetryLab;
using TelemetryLab.BasicAttributes;
using TelemetryLab.Models;


class main
{
    
   static void Main()
    {

        try
        {
            MappedBuffer<SharedMemoryLayout> _dataOut =
                new MappedBuffer<SharedMemoryLayout>(LMUConstants.TELEMETRY_FILE_NAME);
            
            _dataOut.Connect();

            var fullData = _dataOut.GetMappedDataUnSynchronized();
            var generic_data = fullData.data.generic;
            var scor_data =  fullData.data.scoring;
            var telemetry_data = fullData.data.telemetry;
            var path_data = fullData.data.paths;

            var player_index = telemetry_data.playerVehicleIdx;
            var selected_player_index = 0;
            
            var player_telemetry_data = telemetry_data.telemInfo[selected_player_index];

            string playerName = new string(scor_data.scoringInfo.mPlayerName);
            string vehNamePlayerTelemetry = new string(player_telemetry_data.mVehicleName);
            string vehDataVehName = new string(scor_data.vehScoringInfo[player_index].mVehicleName);
            string vehDataVehClass = new string(scor_data.vehScoringInfo[player_index].mVehicleClass);
            string trackName = new string(scor_data.scoringInfo.mTrackName);
            string rearCompound = new string(player_telemetry_data.mRearTireCompoundName);
            Velocity curSpeed = new Velocity();
            curSpeed = Velocity.FromMs(0);
            
            Console.WriteLine($"Scoring data player name: {playerName}");
            Console.WriteLine($"Player data vehicle name: {vehNamePlayerTelemetry}");
            Console.WriteLine($"Vehicle data vehicle name: {vehDataVehName}");
            Console.WriteLine($"Vehicle data vehicle Class: {vehDataVehClass}");
            Console.WriteLine($"Scoring data Track Name: {trackName}");
            Console.WriteLine($"Rear tyre compound Name: {rearCompound}");
            
            Thread.Sleep(1000);
            
            while (true)
            {
                

                fullData = _dataOut.GetMappedDataUnSynchronized();

                telemetry_data = fullData.data.telemetry;
                player_index = telemetry_data.playerVehicleIdx;

                player_telemetry_data = telemetry_data.telemInfo[player_index];
                curSpeed = Velocity.FromMs(Math.Sqrt(
                    (player_telemetry_data.mLocalVel.x * player_telemetry_data.mLocalVel.x) +
                    (player_telemetry_data.mLocalVel.y * player_telemetry_data.mLocalVel.y) +
                    (player_telemetry_data.mLocalVel.z * player_telemetry_data.mLocalVel.z)));

                Console.WriteLine($"Speed: {curSpeed.InKph.ToString("F0")} km/h");
                Console.WriteLine($"RPM: {player_telemetry_data.mEngineRPM.ToString("F0")}");
                Console.WriteLine($"Gear: {player_telemetry_data.mGear}");

                Thread.Sleep(100);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to connect: " + e.Message);
        }
    }
}
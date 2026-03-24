using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using TelemetryLab;


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
            
            Console.WriteLine($"Scoring data player name: {playerName}");
            Console.WriteLine($"Player data vehicle name: {vehNamePlayerTelemetry}");
            Console.WriteLine($"Vehicle data vehicle name: {vehDataVehName}");
            Console.WriteLine($"Vehicle data vehicle Class: {vehDataVehClass}");
            Console.WriteLine($"Scoring data Track Name: {trackName}");
            Console.WriteLine($"Rear tyre compound Name: {rearCompound}");
            
            Thread.Sleep(1000);
            
            while (true)
            {
                

                Console.WriteLine($"Speed: {player_telemetry_data.mLocalVel}");
                Console.WriteLine($"RPM: {player_telemetry_data.mEngineRPM}");
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
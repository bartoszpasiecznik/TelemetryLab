// namespace TelemetryLab;
//
// class main
// {
//     static void Main(string[] args)
//     {
//         Console.WriteLine("Hello, World!");
//     }
// }

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
            // MappedBuffer<SharedMemoryGeneric> _generic = new MappedBuffer<SharedMemoryGeneric>(LMUConstants.MM_TELEMETRY_FILE_NAME);
            // MappedBuffer<SharedMemoryPathData> _paths = new MappedBuffer<SharedMemoryPathData>(LMUConstants.MM_TELEMETRY_FILE_NAME);
            // MappedBuffer<SharedMemoryScoringData> _scoring = new MappedBuffer<SharedMemoryScoringData>(LMUConstants.MM_TELEMETRY_FILE_NAME);
            // MappedBuffer<SharedMemoryTelemtryData> _telemetry = new MappedBuffer<SharedMemoryTelemtryData>(LMUConstants.MM_TELEMETRY_FILE_NAME);
            // _generic.Connect();
            // _paths.Connect();
            // _scoring.Connect();
            // _telemetry.Connect();
            // LmuFullData fullData = new LmuFullData(_generic.GetMappedDataUnSynchronized(), _paths.GetMappedDataUnSynchronized(), _scoring.GetMappedDataUnSynchronized(), _telemetry.GetMappedDataUnSynchronized());

            MappedBuffer<SharedMemoryLayout> _dataOut =
                new MappedBuffer<SharedMemoryLayout>(LMUConstants.MM_TELEMETRY_FILE_NAME);
            
            _dataOut.Connect();

            var fullData = _dataOut.GetMappedDataUnSynchronized();
            var generic_data = fullData.data.generic;
            var scor_data =  fullData.data.scoring;
            var telemetry_data = fullData.data.telemetry;

            var player_index = telemetry_data.playerVehicleIdx;
            var selected_player_index = 0;
            
            var player_telemetry_data = telemetry_data.telemInfo[selected_player_index];
            
            Console.WriteLine(scor_data.scoringInfo.mAmbientTemp);
            
            Console.WriteLine(scor_data.scoringInfo.mTrackName);
            
            while (true)
            {
                

                // Console.WriteLine($"Speed: {data.mSpeed}");
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
using TelemetryLab.Data;

namespace TelemetryLab.Telemetry;

public class PlayerDataProvider
{
    public int GetPlayerIndex(SharedMemoryLayout data)
    {
        return data.data.telemetry.playerVehicleIdx;
    }

    public bool HasValidPlayerIndex(SharedMemoryLayout data, int player_index)
    {
        return player_index >= 0 &&
               player_index < data.data.telemetry.activeVehicles &&
               player_index < data.data.telemetry.telemInfo.Length &&
               player_index < data.data.scoring.vehScoringInfo.Length &&
               player_index < data.data.scoring.scoringInfo.mNumVehicles;
    }

    public TelemInfoV01 GetPlayerTelem(SharedMemoryLayout data, int player_index)
    {
        return data.data.telemetry.telemInfo[player_index];
    }

    public VehicleScoringInfoV01 GetPlayerScoring(SharedMemoryLayout data, int player_index)
    {
        return data.data.scoring.vehScoringInfo[player_index];
    }
}

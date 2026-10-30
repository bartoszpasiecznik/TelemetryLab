using TelemetryLab.Data;

namespace TelemetryLab.Telemetry;

public class SessionDataProvider
{
    public ScoringInfoV01 GetSessionInfo(SharedMemoryLayout data)
    {
        return data.data.scoring.scoringInfo;
    }
}
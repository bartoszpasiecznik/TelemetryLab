namespace TelemetryLab.Core;

public class LapTracker
{
    private int _lastLap = -1;

    public bool IsNewLap(int lap)
    {
        if (lap != _lastLap)
        {
            _lastLap = lap;
            return true;
        }
        return false;
    }
    
    public int LastLap => _lastLap;
}

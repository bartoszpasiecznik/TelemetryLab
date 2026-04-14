using TelemetryLab.Core;
using TelemetryLab.Data;
using TelemetryLab.Memory;
using TelemetryLab.Telemetry;

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

using System.Collections.Generic;

namespace TelemetryLab.Messages;

public class TickBatchMessage
{
    public TickBatchMessage(string type, string sessionId, int batchSeq, List<TickSample> samples)
    {
        Type = type;
        SessionId = sessionId;
        BatchSeq = batchSeq;
        Samples = samples;
    }

    public string Type { get; set; }
    public string SessionId { get; set; }
    public int BatchSeq { get; set; }
    public List<TickSample> Samples { get; set; }
}

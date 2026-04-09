using TelemetryLab.Data;

namespace TelemetryLab.Memory;

public class TelemetryReader
{
    private readonly MappedBuffer<SharedMemoryLayout> _buffer;

    public TelemetryReader(string pathName)
    {
        _buffer = new MappedBuffer<SharedMemoryLayout>(pathName);
        _buffer.Connect();
    }

    public SharedMemoryLayout Read()
    {
        return _buffer.GetMappedDataUnSynchronized();
    }
}
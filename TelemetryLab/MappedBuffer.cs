using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace TelemetryLab;

public class MappedBuffer<TMappedBufferT> where TMappedBufferT : struct
    {
        /*readonly int RF2_BUFFER_VERSION_BLOCK_SIZE_BYTES = Marshal.SizeOf(typeof(rF2MappedBufferVersionBlock));
        readonly int RF2_BUFFER_VERSION_BLOCK_WITH_SIZE_SIZE_BYTES = Marshal.SizeOf(typeof(rF2MappedBufferVersionBlockWithSize));*/

        private readonly int _bufferSizeBytes;
        private readonly string _bufferName;

        /* Holds the entire byte array that can be marshalled to a MappedBufferT.  Partial updates
         only read changed part of buffer, ignoring trailing uninteresting bytes.  However,
         to marshal we still need to supply entire structure size.  So, on update new bytes are copied
         (outside of the mutex). */
        private MemoryMappedFile _memoryMappedFile;

        public MappedBuffer(string buffName)
        {
            _bufferSizeBytes = Marshal.SizeOf(typeof(TMappedBufferT));
            _bufferName = buffName;
        }

        public bool IsConnected => _memoryMappedFile != null;

        public void Connect()
        {
            _memoryMappedFile = MemoryMappedFile.OpenExisting(_bufferName);
        }

        public void Disconnect()
        {
            _memoryMappedFile?.Dispose();
            _memoryMappedFile = null;
        }

        public TMappedBufferT GetMappedDataUnSynchronized()
        {
            TMappedBufferT mappedData;
            using (var sharedMemoryStreamView = _memoryMappedFile.CreateViewStream(0, _bufferSizeBytes, MemoryMappedFileAccess.Read))
            {
                var sharedMemoryStream = new BinaryReader(sharedMemoryStreamView);
                var sharedMemoryReadBuffer = sharedMemoryStream.ReadBytes(_bufferSizeBytes);

                var handleBuffer = GCHandle.Alloc(sharedMemoryReadBuffer, GCHandleType.Pinned);
                mappedData = (TMappedBufferT)Marshal.PtrToStructure(handleBuffer.AddrOfPinnedObject(), typeof(TMappedBufferT));
                handleBuffer.Free();

            }

            return mappedData;
        }
    }


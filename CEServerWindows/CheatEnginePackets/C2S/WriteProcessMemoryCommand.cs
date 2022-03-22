using CEServerWindows.CheatEnginePackets.S2C;
using System;
using System.IO;

namespace CEServerWindows.CheatEnginePackets.C2S
{
    public class WriterProcessMemoryCommand : CheatEngineCommand<WriteProcessMemoryResponse>
    {
        public IntPtr Handle;
        public UInt64 Address;
        public int Size;
        public byte[] buffer;

        public override CommandType CommandType => CommandType.CMD_WRITEPROCESSMEMORY;

        public WriterProcessMemoryCommand() { }

        public WriterProcessMemoryCommand(IntPtr handle, UInt64 address, int size, bool compress)
        {
            this.Handle = handle;
            this.Address = address;
            this.Size = size;
            this.initialized = true;
        }

        public override void Initialize(BinaryReader reader)
        {
            Handle = (IntPtr)reader.ReadInt32();
            Address = reader.ReadUInt64();
            Size = reader.ReadInt32();

            buffer = reader.ReadBytes(Size);
            initialized = true;
        }

        public override WriteProcessMemoryResponse Process()
        {
            WindowsAPI.MemoryAPI.WriteProcessMemory(this.Handle, (IntPtr)this.Address, buffer, this.Size, out IntPtr dataWritten);

            return new WriteProcessMemoryResponse((int)dataWritten);
        }
    }
}
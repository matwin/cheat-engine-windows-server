﻿using CEServerWindows.CheatEnginePackets.S2C;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CEServerWindows.CheatEnginePackets.C2S
{
    public class VirtualQueryExFullCommand : CheatEngineCommand<VirtualQueryExFullResponse>
    {

        public IntPtr Handle;

        public override CommandType CommandType => CommandType.CMD_VIRTUALQUERYEXFULL;// throw new NotImplementedException();

        public VirtualQueryExFullCommand() { }

        public VirtualQueryExFullCommand(IntPtr handle)
        {
            this.Handle = handle;
            this.initialized = true;
        }

        public override void Initialize(BinaryReader reader)
        {
            this.Handle = (IntPtr)reader.ReadInt32();
            reader.ReadByte();//Cheat engine/linux specific flags?
            this.initialized = true;
        }

        public override VirtualQueryExFullResponse Process()
        {
            var regions = new List<WindowsAPI.MemoryAPI.MEMORY_BASIC_INFORMATION>();
            ulong currentAddress = 0;
            int ret = 0;
            while(true)
            {
                WindowsAPI.MemoryAPI.MEMORY_BASIC_INFORMATION mbi = new WindowsAPI.MemoryAPI.MEMORY_BASIC_INFORMATION();
                ret = WindowsAPI.MemoryAPI.VirtualQueryEx(Handle, (UIntPtr)currentAddress, out mbi, (uint)Marshal.SizeOf(mbi));
                if (ret == 0)
                    break;
                currentAddress += (ulong)mbi.RegionSize;
                regions.Add(mbi);
            }

            return new VirtualQueryExFullResponse(regions);
        }
    }
}
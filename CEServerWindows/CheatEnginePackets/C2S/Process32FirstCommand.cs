﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using CEServerWindows.CheatEnginePackets.S2C;

namespace CEServerWindows.CheatEnginePackets.C2S
{
    public class Process32FirstCommand : CheatEngineCommand<S2C.Process32Response>
    {
        public override CommandType CommandType => CommandType.CMD_PROCESS32FIRST;

        public IntPtr Handle;
        public Process32FirstCommand()
        {
        }

        public Process32FirstCommand(IntPtr handle)
        {
            this.Handle = handle;
            this.initialized = true;
        }

        public sealed override void Initialize(BinaryReader reader)
        {
            this.Handle = (IntPtr)reader.ReadUInt32();
            this.initialized = true;
        }

        public override Process32Response Process()
        {
            WindowsAPI.ToolHelp.PROCESSENTRY32 processEntry = new WindowsAPI.ToolHelp.PROCESSENTRY32();
            processEntry.dwSize = (uint) Marshal.SizeOf(processEntry);

            var result = WindowsAPI.ToolHelp.Process32First(this.Handle, ref processEntry);

            return new Process32Response(result, processEntry);
        }
    }
}

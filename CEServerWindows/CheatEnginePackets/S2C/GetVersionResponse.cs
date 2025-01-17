﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CEServerWindows.CheatEnginePackets.S2C
{
    public class GetVersionResponse : ICheatEngineResponse
    {
        public const string VERSION = "CHEATENGINE Network 2.1";
        public string version;

        public GetVersionResponse()
        {
            this.version = VERSION;
        }

        public GetVersionResponse(string version)
        {
            this.version = version;
        }

        public byte[] Serialize()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter br = new BinaryWriter(ms);
            br.Write(version.Length);
            br.Write(Encoding.UTF8.GetBytes(version));

            br.Close();
            return ms.ToArray();
        }
    }
}
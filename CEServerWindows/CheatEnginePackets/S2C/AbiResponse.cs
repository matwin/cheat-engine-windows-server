using System.IO;

namespace CEServerWindows.CheatEnginePackets.S2C
{
    public class AbiResponse : ICheatEngineResponse
    {
        private byte Result;

        public AbiResponse(byte result)
        {
            this.Result = result;
        }

        public byte[] Serialize()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter br = new BinaryWriter(ms);

            br.Write(Result);
            br.Close();
            return ms.ToArray();
        }
    }
}
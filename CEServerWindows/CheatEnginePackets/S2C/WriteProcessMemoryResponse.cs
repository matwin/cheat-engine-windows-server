using System.IO;

namespace CEServerWindows.CheatEnginePackets.S2C
{
    public class WriteProcessMemoryResponse : ICheatEngineResponse
    {
        public int Size;
        public WriteProcessMemoryResponse(int size)
        {
            Size = size;
        }

        public byte[] Serialize()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter br = new BinaryWriter(ms);
            br.Write(Size);
            br.Close();
            return ms.ToArray();
        }
    }
}
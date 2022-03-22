using CEServerWindows.CheatEnginePackets.S2C;
using System.IO;

namespace CEServerWindows.CheatEnginePackets.C2S
{
    public class AbiCommand : CheatEngineCommand<AbiResponse>
    {
        public override CommandType CommandType => CommandType.CND_ABI;

        public AbiCommand()
        {
        }

        public override void Initialize(BinaryReader reader)
        {
            this.initialized = true;
        }

        public override AbiResponse Process()
        {
            return new AbiResponse(0);
        }
    }
}
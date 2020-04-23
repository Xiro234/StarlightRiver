using System.IO;
using Terraria.ModLoader;

namespace StarlightRiver
{
    public partial class StarlightRiver : Mod
    {
        public enum SLRPacketType : byte
        {
            ability = 0
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            switch (reader.ReadByte())
            {
                case 0: break;
            }
        }
    }
}

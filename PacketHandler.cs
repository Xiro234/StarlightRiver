using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using StarlightRiver.Keys;
using System.IO;
using Microsoft.Xna.Framework;

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

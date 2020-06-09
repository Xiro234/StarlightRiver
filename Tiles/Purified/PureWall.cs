using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Purified
{
    class WallStonePure : ModWall
    {
        public override void SetDefaults()
        {
            dustType = ModContent.DustType<Dusts.Purify>();
        }
    }
    class WallGrassPure : ModWall
    {
        public override void SetDefaults()
        {
            dustType = ModContent.DustType<Dusts.Purify>();
        }
    }
}

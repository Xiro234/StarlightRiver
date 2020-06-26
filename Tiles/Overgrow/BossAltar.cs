using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Overgrow
{
    class BossAltar : ModTile
    {
        public override void SetDefaults() => QuickBlock.QuickSetFurniture(this, 15, 7, DustType<Dusts.Stone>(), SoundID.Tink, false, new Color(200, 200, 180), false, true, "[PH] OG Boss Altar");
    }
}

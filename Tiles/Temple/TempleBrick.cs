using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using StarlightRiver.Items;

namespace StarlightRiver.Tiles.Temple
{
    class TempleBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileBlockLight[Type] = true;
            Main.tileSolid[Type] = true;
            soundType = SoundID.Tink;
            dustType = DustID.Stone;
        }
    }
    public class TempleBrickItem : QuickTileItem
    {
        public TempleBrickItem() : base("Ancient Temple Bricks", "", ModContent.TileType<TempleBrick>(), 0) { }
    }

}

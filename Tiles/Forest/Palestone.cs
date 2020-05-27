using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace StarlightRiver.Tiles.Forest
{
    class PalestoneItem : Items.QuickTileItem { public PalestoneItem() : base("Palestone", "", ModContent.TileType<Palestone>(), 0) { } }
    class Palestone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileStone[Type] = true;
            soundType = Terraria.ID.SoundID.Tink;

            dustType = Terraria.ID.DustID.Stone;
            drop = ModContent.ItemType<PalestoneItem>();

            AddMapEntry(new Color(167, 180, 191));
        }
    }
}

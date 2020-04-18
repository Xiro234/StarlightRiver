using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace StarlightRiver.Tiles.Vitric
{
    class AncientSandstone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            minPick = 1;
            AddMapEntry(new Color(150, 105, 65));

            Main.tileMerge[Type][ModContent.TileType<VitricSand>()] = true;
        }
    }
    class AncientSandstonePlatform : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileBlockLight[Type] = false;
            minPick = 1;
            AddMapEntry(new Color(150, 105, 65));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace StarlightRiver.Tiles.Vitric
{
    class AncientSandstone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            minPick = int.MaxValue;

            Main.tileMerge[Type][ModContent.TileType<VitricSand>()] = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace spritersguildwip.Tiles
{
    public class GrassJungleEvil : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            TileID.Sets.Grass[Type] = true;
            TileID.Sets.GrassSpecial[Type] = true;
            TileID.Sets.ChecksForMerge[Type] = true;
            drop = ItemID.MudBlock;
            AddMapEntry(new Color(98, 82, 148));
        }
    }
}
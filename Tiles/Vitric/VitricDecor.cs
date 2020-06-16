using Microsoft.Xna.Framework;
using StarlightRiver.Tiles.Vitric.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Vitric
{
    class VitricDecor : ModTile
    {
        public override void SetDefaults()
        {
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorValidTiles = new int[] { ModContent.TileType<VitricSand>(), ModContent.TileType<VitricSoftSand>() };
            TileObjectData.newTile.RandomStyleRange = 4;
            TileObjectData.newTile.StyleHorizontal = true;

            QuickBlock.QuickSetFurniture(this, 2, 2, ModContent.DustType<Dusts.Glass>(), SoundID.Shatter, false, -1, new Color(172, 131, 105));
        }
    }

    class VitricDecorLarge : ModTile
    {
        public override void SetDefaults()
        {
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorValidTiles = new int[] { ModContent.TileType<VitricSand>(), ModContent.TileType<VitricSoftSand>() };
            TileObjectData.newTile.RandomStyleRange = 6;
            TileObjectData.newTile.StyleHorizontal = true;

            QuickBlock.QuickSetFurniture(this, 3, 2, ModContent.DustType<Dusts.Glass>(), SoundID.Shatter, false, -1, new Color(172, 131, 105));
        }
    }
}
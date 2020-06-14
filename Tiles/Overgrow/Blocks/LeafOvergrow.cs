using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Items;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Overgrow.Blocks
{
    internal class LeafOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 210, ModContent.DustType<Dusts.Leaf>(), SoundID.Grass, new Color(221, 211, 67), ModContent.ItemType<LeafOvergrowItem>(), true, true);
            Main.tileMerge[Type][ModContent.TileType<LeafOvergrow>()] = true;
            Main.tileMerge[Type][ModContent.TileType<BrickOvergrow>()] = true;
            Main.tileMerge[Type][ModContent.TileType<GlowBrickOvergrow>()] = true;
        }
    }
    internal class LeafOvergrowItem : QuickTileItem { public LeafOvergrowItem() : base("Faerie Leaves", "", ModContent.TileType<LeafOvergrow>(), 0) { } }
}

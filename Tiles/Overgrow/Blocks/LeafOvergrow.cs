﻿using Microsoft.Xna.Framework;
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
    internal class LeafOvergrow : QuickBlock
    {
        public LeafOvergrow() : base(210, ModContent.DustType<Dusts.Leaf>(), SoundID.Grass, new Color(221, 211, 67), ModContent.ItemType<LeafOvergrowItem>(), true, true) { }
        public override void SafeSetDefaults()
        {
            Main.tileMerge[Type][mod.GetTile("GrassOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("BrickOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("GlowBrickOvergrow").Type] = true;
        }
    }
    internal class LeafOvergrowItem : QuickTileItem { public LeafOvergrowItem() : base("Faerie Leaves", "", ModContent.TileType<LeafOvergrow>(), 0) { } }
}

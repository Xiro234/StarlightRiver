using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ModLoader;
using StarlightRiver.Items.Vitric;
using StarlightRiver.Items.Debug;
using System;

namespace StarlightRiver.Tiles
{
    class BrickOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileMerge[Type][mod.GetTile("GrassOvergrow").Type] = true;
            drop = mod.ItemType("BrickOvergrowItem");
            AddMapEntry(new Color(79, 76, 71));
        }
    }

    class GrassOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileMerge[Type][mod.GetTile("BrickOvergrow").Type] = true;
            TileID.Sets.Grass[Type] = true;
            drop = mod.ItemType("BrickOvergrowItem");
            AddMapEntry(new Color(202, 157, 49));
        }
    }
}

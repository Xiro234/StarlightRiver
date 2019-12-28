using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ModLoader;
using StarlightRiver.Items.Vitric;
using StarlightRiver.Items.Debug;
using System;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;

namespace StarlightRiver.Tiles.Overgrow
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
            Main.tileMerge[Type][mod.GetTile("CrusherTile").Type] = true;
            Main.tileMerge[Type][mod.GetTile("GlowBrickOvergrow").Type] = true;

            Main.tileMerge[Type][TileID.BlueDungeonBrick] = true;
            Main.tileMerge[Type][TileID.GreenDungeonBrick] = true;
            Main.tileMerge[Type][TileID.PinkDungeonBrick] = true;
            minPick = 210;
            drop = mod.ItemType("BrickOvergrowItem");
            AddMapEntry(new Color(79, 76, 71));
        }
    }
    class GlowBrickOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileMerge[Type][mod.GetTile("BrickOvergrow").Type] = true;
            minPick = 210;
            drop = mod.ItemType("BrickOvergrowItem");
            AddMapEntry(new Color(79, 76, 71));

            animationFrameHeight = 270;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 5)
            {
                frameCounter = 0;
                if (++frame >= 5)
                {
                    frame = 0;
                }
            }
        }
    }
    class LeafOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileMerge[Type][mod.GetTile("GrassOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("BrickOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("GlowBrickOvergrow").Type] = true;
            minPick = 210;
            drop = mod.ItemType("BrickOvergrowItem");
            AddMapEntry(new Color(221, 211, 67));
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
            Main.tileMerge[Type][mod.GetTile("CrusherTile").Type] = true;
            TileID.Sets.Grass[Type] = true;
            drop = mod.ItemType("BrickOvergrowItem");
            AddMapEntry(new Color(202, 157, 49));
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Main.tile[i, j - 1].active())
            {
                if (Main.rand.Next(1) == 0)
                {
                    WorldGen.PlaceTile(i, j - 1, ModContent.TileType<TallgrassOvergrow>(), true);
                }
            }
        }
    }

    class VineOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileCut[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                ModContent.TileType<GrassOvergrow>(),
                ModContent.TileType<VineOvergrow>()
            };
            TileObjectData.addTile(Type);
            soundType = 6;
            dustType = 14;
            AddMapEntry(new Color(202, 157, 49));
        }

        public override void RandomUpdate(int i, int j)
        {

        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            float sway = 0;
            for(int k = 1; k > 0; k++)
            {
                if (Main.tile[i, j - k].type == Type && sway <= 2.6f) { sway += 0.3f; }
                else { break; }
            }
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/VineOvergrowFlow"), new Vector2(i + 12, j + 12) * 16 - Main.screenPosition + new Vector2((float)Math.Sin((i % 4 * 1.57f)+LegendWorld.rottime + j % 6.28f) * sway, 0), new Rectangle((j % 2) * 16, 0, 16, 16), drawColor);
        }
    }

    class TallgrassOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileCut[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                ModContent.TileType<GrassOvergrow>(),
                ModContent.TileType<VineOvergrow>()
            };
            TileObjectData.addTile(Type);
            soundType = 6;
            dustType = 14;
            AddMapEntry(new Color(202, 157, 49));
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/TallgrassOvergrowFlow"), new Rectangle(((i + 12) * 16) - (int)Main.screenPosition.X + 8, ((j + 13) * 16) - (int)Main.screenPosition.Y + 2, 16, 16), new Rectangle((i % 2) * 16, 0, 16, 16), drawColor, (float)Math.Sin(LegendWorld.rottime + i % 6.28f) * 0.25f, new Vector2(8, 16), 0, 0);
        }
    }
}

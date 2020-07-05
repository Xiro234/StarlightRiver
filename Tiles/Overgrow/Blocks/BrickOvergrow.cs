using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Items;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Overgrow.Blocks
{
    internal class BrickOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 210, DustID.Stone, SoundID.Tink, new Color(79, 76, 71), ItemType<BrickOvergrowItem>(), true, true);
            Main.tileMerge[Type][TileType<GrassOvergrow>()] = true;
            Main.tileMerge[Type][mod.GetTile("CrusherTile").Type] = true;
            Main.tileMerge[Type][TileType<GlowBrickOvergrow>()] = true;
            Main.tileMerge[Type][TileType<LeafOvergrow>()] = true;

            Main.tileMerge[Type][TileID.BlueDungeonBrick] = true;
            Main.tileMerge[Type][TileID.GreenDungeonBrick] = true;
            Main.tileMerge[Type][TileID.PinkDungeonBrick] = true;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            if (i > Main.screenPosition.X / 16 && i < Main.screenPosition.X / 16 + Main.screenWidth / 16 && j > Main.screenPosition.Y / 16 && j < Main.screenPosition.Y / 16 + Main.screenHeight / 16)
            {
                Random rand = new Random(i * j * j);
                Tile tile = Main.tile[i, j];
                if (rand.Next(60) == 0 && i != 1 && j != 1 && tile.frameX > 10 && tile.frameX < 70 && tile.frameY == 18)
                {
                    Main.specX[nextSpecialDrawIndex] = i;
                    Main.specY[nextSpecialDrawIndex] = j;
                    nextSpecialDrawIndex++;
                }
            }
        }
        public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Texture2D tex = GetTexture("StarlightRiver/Tiles/Overgrow/Blob");
            spriteBatch.Draw(tex, (Helper.TileAdj + new Vector2(i, j)) * 16 + Vector2.One * 8 - Main.screenPosition, new Rectangle(i * j % 4 * 40, 0, 40, 50), Lighting.GetColor(i, j), 0, new Vector2(20, 25), 1, 0, 0);
        }
    }
    internal class BrickOvergrowItem : QuickTileItem { public BrickOvergrowItem() : base("Runic Bricks", "", TileType<BrickOvergrow>(), 0) { } }
}
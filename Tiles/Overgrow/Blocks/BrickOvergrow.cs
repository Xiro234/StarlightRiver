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
    internal class BrickOvergrow : QuickBlock
    {
        public BrickOvergrow() : base(210, DustID.Stone, SoundID.Tink, new Color(79, 76, 71), ModContent.ItemType<BrickOvergrowItem>(), true, true) { }
        public override void SafeSetDefaults()
        {
            Main.tileMerge[Type][mod.GetTile("GrassOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("CrusherTile").Type] = true;
            Main.tileMerge[Type][mod.GetTile("GlowBrickOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("LeafOvergrow").Type] = true;

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
            Texture2D tex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/Blob");
            spriteBatch.Draw(tex, (Helper.TileAdj + new Vector2(i, j)) * 16 + Vector2.One * 8 - Main.screenPosition, new Rectangle(i * j % 4 * 40, 0, 40, 50), Lighting.GetColor(i, j), 0, new Vector2(20, 25), 1, 0, 0);
        }
    }
    internal class BrickOvergrowItem : QuickTileItem { public BrickOvergrowItem() : base("Runic Bricks", "", ModContent.TileType<BrickOvergrow>(), 0) { } }
}
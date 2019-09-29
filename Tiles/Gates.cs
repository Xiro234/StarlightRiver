using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Ability;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles
{
    class HellGate : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 13;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0);

            TileObjectData.addTile(Type);

            drop = mod.ItemType("Bounce");
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (Main.tile[i,j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                if (Main.hardMode)
                {
                    Main.tileSolid[Type] = false;
                }
                else
                {
                    Main.tileSolid[Type] = true;
                }
            }
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                if (!Main.hardMode)
                {
                    spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/Flesh"), new Vector2((i + 12) * 16, (j + 12) * 16) - Main.screenPosition, Color.White);
                }
            }
        }
    }
}

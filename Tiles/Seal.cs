using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles
{
    class Seal : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.Width = 22;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0);

            TileObjectData.addTile(Type);

            drop = mod.ItemType("Bounce");
            AddMapEntry(new Color(50, 50, 50));
        }

        /*public override void NearbyEffects(int i, int j, bool closer)
        {
            if (Main.tile[i,j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                foreach (Player player in Main.player)
                {
                    if (Collision.CheckAABBvAABBCollision(player.position, player.Size, new Vector2(i * 16, j * 16 - 45), new Vector2(368, 64)) && player.GetModPlayer<AbilityHandler>().ability is Smash && !LegendWorld.SealOpen)
                    {
                        LegendWorld.SealOpen = true;
                        player.GetModPlayer<AbilityHandler>().ability.OnExit();
                        player.GetModPlayer<AbilityHandler>().ability = null;
                        player.velocity.Y = -20;
                    }
                }
                if (LegendWorld.SealOpen)
                {
                    Main.tileSolid[Type] = false;
                }
                else
                {
                    Main.tileSolid[Type] = true;
                }
            }
        }*/
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                Vector2 Seal = new Vector2((i + 12) * 16, (j + 12) * 16);
                if (!LegendWorld.SealOpen)
                {
                    spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/SealClosed"), Seal - Main.screenPosition, Color.White);
                }
                else
                {
                    spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/SealOpen"), Seal - Main.screenPosition, Color.White);
                }
            }
        }
    }
}

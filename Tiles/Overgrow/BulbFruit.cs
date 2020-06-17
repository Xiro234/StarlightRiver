using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Overgrow
{
    internal class BulbFruit : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;

            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Orb Fruit");//Map name
            AddMapEntry(new Color(255, 255, 200), name);
            dustType = DustType<Dusts.Gold2>();
            disableSmartCursor = true;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            if (tile.frameY == 0 && tile.frameX % 34 == 0)
            {
                if (tile.frameX == 0)
                {
                    Texture2D tex = GetTexture("StarlightRiver/Tiles/Overgrow/BulbFruitGlow");
                    spriteBatch.Draw(tex, (new Vector2(i, j) + Helper.TileAdj) * 16 + new Vector2(3, 12) - Main.screenPosition, tex.Frame(), Color.White * (float)Math.Sin(StarlightWorld.rottime));
                }

                for (int k = 1; k <= 30; k++)
                {
                    if (Main.tile[i, j - k].active()) break;
                    Texture2D tex = GetTexture("StarlightRiver/Tiles/Overgrow/VineOvergrowFlow");
                    float sway = (float)Math.Sin(StarlightWorld.rottime + (15 - k) / 4) * (15 - k) / 150;
                    spriteBatch.Draw(tex, (new Vector2(i + 0.5f + sway, j - k) + Helper.TileAdj) * 16 - Main.screenPosition, new Rectangle(16 * k % 3, 0, 16, 16), Lighting.GetColor(i, j - k));

                    if (tile.frameX == 0 && Main.rand.Next(5) == 0)
                        Dust.NewDust(new Vector2(i + 0.5f, j - k) * 16, 16, 16, DustType<Dusts.Gold2>(), 0, -3, 0, default, 0.3f);
                }
            }
            return true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (!Main.projectile.Any(p => p.active && p.type == ProjectileType<Projectiles.Dummies.BulbDummy>() && p.Hitbox.Contains((new Vector2(i, j) * 16).ToPoint())))
            {
                Projectile.NewProjectile(new Vector2(i, j) * 16, Vector2.Zero, ProjectileType<Projectiles.Dummies.BulbDummy>(), 0, 0);
            }

            if (Main.tile[i, j].frameX < 34)
                Dust.NewDust(new Vector2(i, j) * 16, 16, 16, DustType<Dusts.Gold2>(), 0, 0, 0, default, 0.5f);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.frameY == 0 && tile.frameX == 0) { r = 0.8f; g = 0.7f; b = 0.4f; }
            else { r = 0; g = 0; b = 0; }
        }

        public override void RandomUpdate(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            if (tile.frameY == 0 && tile.frameX == 34)
            {
                if (Main.rand.Next(8) == 0)
                {
                    for (int x = i; x <= i + 1; x++)
                        for (int y = j; y <= j + 1; y++)
                            Main.tile[x, y].frameX -= 34;
                }
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new Vector2(i * 16, j * 16), 32, 48, ItemType<Items.Debug.DebugPotion>());
        }
    }
}
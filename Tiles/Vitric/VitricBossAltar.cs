using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Projectiles.Dummies;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Vitric
{
    internal class VitricBossAltar : ModTile
    {
        private Projectile Dummy = new Projectile();
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;

            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.Height = 7;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            dustType = DustID.Stone;
            disableSmartCursor = true;
            minPick = int.MaxValue;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Vitric Altar");
            AddMapEntry(new Color(113, 113, 113), name);
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            if (tile.frameX % 90 == 0 && tile.frameY == 0)
            {
                if (!(Dummy.modProjectile is VitricAltarDummy && Main.projectile.Any(n => n == Dummy) && Dummy.active && Dummy.Hitbox.Contains(new Point(i * 16 + 30, j * 16 + 30))))
                {
                    Dummy = Main.projectile[Projectile.NewProjectile(new Vector2(i, j) * 16 + new Vector2(40, 56), Vector2.Zero, ModContent.ProjectileType<VitricAltarDummy>(), 0, 0)];
                }
            }
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.tile[i, j].frameX == 90 && Main.tile[i, j].frameY == 0)
            {
                //Texture2D tex = ModContent.GetTexture("StarlightRiver/Symbol");
                //spriteBatch.Draw(tex, (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition + new Vector2(40 - tex.Width / 2, (float)Math.Sin(LegendWorld.rottime) * 5), new Color(150, 220, 255) * 0.5f);
            }
            return true;
        }
        public override bool NewRightClick(int i, int j)
        {
            //debug stuff
            if (Main.LocalPlayer.HeldItem.modItem is Items.Debug.DebugPotion && Main.tile[i, j].frameX >= 90)
            {
                Tile tile = Framing.GetTileSafely(i, j);
                for (int x = i - tile.frameX % 90 / 16; x < (i - tile.frameX % 90 / 16) + 5; x++)
                {
                    for (int y = j - tile.frameY / 16; y < (j - tile.frameY / 16) + 7; y++)
                    {
                        Framing.GetTileSafely(x, y).frameX -= 90;
                    }
                }
                LegendWorld.GlassBossOpen = false;
                return true;
            }
            //end debug
            if (LegendWorld.GlassBossOpen && Dummy.modProjectile is VitricAltarDummy)
            {
                (Dummy.modProjectile as VitricAltarDummy).SpawnBoss();
            }
            return true;
        }
    }
}
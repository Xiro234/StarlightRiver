using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Interactive
{
    internal class StaminaGem : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = true;

            drop = mod.ItemType("StaminaGemItem");
            dustType = mod.DustType("Stamina");
            AddMapEntry(new Color(255, 186, 66));
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.236f / 1.1f;
            g = 0.144f / 1.1f;
            b = 0.071f / 1.1f;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Vector2 pos = new Vector2(i * 16, j * 16) - new Vector2(-8, -11);
            if (!Main.projectile.Any(proj => proj.Hitbox.Intersects(new Rectangle(i * 16 + 4, j * 16 + 4, 1, 1)) && proj.type == ModContent.ProjectileType<Projectiles.Dummies.StaminaGemDummy>() && proj.active))
            {
                Projectile.NewProjectile(pos, Vector2.Zero, ModContent.ProjectileType<Projectiles.Dummies.StaminaGemDummy>(), 0, 0);
            }
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.projectile.Any(proj => proj.Hitbox.Intersects(new Rectangle(i * 16 + 4, j * 16 + 4, 1, 1)) && proj.type == ModContent.ProjectileType<Projectiles.Dummies.StaminaGemDummy>() && proj.active && proj.ai[0] == 0))
            {
                Color color = new Color(255, 255, 255) * (float)Math.Sin(LegendWorld.rottime * 3f);
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/Interactive/StaminaGemGlow"), (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition, color);
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/Interactive/StaminaGemOn"), (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition, Color.White);
            }
        }
    }
}

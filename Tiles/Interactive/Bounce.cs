using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Interactive
{
    class Bounce : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            drop = mod.ItemType("Bounce");
            dustType = mod.DustType("Air");
            AddMapEntry(new Color(115, 182, 158));
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Vector2 pos = new Vector2(i * 16, j * 16) - new Vector2(-8, -11);
            if (!Main.projectile.Any(proj => proj.Hitbox.Intersects(new Rectangle(i*16 + 4, j*16 + 4, 1,1)) && proj.type == ModContent.ProjectileType<Projectiles.Dummies.BouncerDummy>() && proj.active ))
            {
                Projectile.NewProjectile(pos, Vector2.Zero, ModContent.ProjectileType<Projectiles.Dummies.BouncerDummy>(), 0, 0);
            }
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color color = new Color(255, 255, 255) * (float)Math.Sin(LegendWorld.rottime);
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/Interactive/BounceGlow"), (new Vector2(i, j) + Helper.TileAdj) * 16 - Vector2.One - Main.screenPosition, color);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.WeaponProjectiles.HexStaff
{
    class HomingHexBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.timeLeft = 1200;
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 4; i++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 27, 0f, 0f, 175, default, 1.2f);
                Main.dust[dust].velocity *= 1.1f;
                Main.dust[dust].noGravity = true;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(projectile.width / 2, projectile.height / 2);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale - (k * 0.15f), SpriteEffects.None, 0f);
            }
            return true;
        }
        NPC target = Main.npc[0];
        bool picked = false;
        public override void AI()
        {
            if (!picked)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Main.npc[i].active && !Main.npc[i].friendly)
                    {
                        if (Vector2.Distance(Main.npc[i].Center, projectile.Center) < Vector2.Distance(target.Center, projectile.Center))
                        {
                            target = Main.npc[i];
                        }
                    }
                }
            }
            projectile.velocity += Vector2.Normalize(target.Center - projectile.Center) * 0.5f;
            projectile.velocity = Vector2.Normalize(projectile.velocity) * 7;
            projectile.scale = 1 + (float)Math.Sin((float)projectile.timeLeft / 16) / 12f;
            projectile.rotation += 0.0314f;
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 27, 0f, 0f, 175, default, 1f);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}

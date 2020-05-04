using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.WeaponProjectiles.HexStaff
{
    class HexRift : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.aiStyle = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.timeLeft = 1200;
            Main.projFrames[projectile.type] = 18;
        }
        public override void AI()
        {
            projectile.scale = 1 + projectile.ai[0] + (float)Math.Sin((float)projectile.timeLeft / 12) / 8f;
            projectile.rotation += 0.0157f;
            if (projectile.ai[0] > 0)
            {
                projectile.ai[0] -= 0.1f;
            }
            if (++projectile.frameCounter >= 7)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 18)
                {
                    projectile.frame = 0;
                }
            }
            Vector2 dustPos = projectile.Center + Main.rand.NextVector2CircularEdge(80, 80);
            Dust dust = Dust.NewDustPerfect(dustPos, 27,
                (dustPos - projectile.Center).SafeNormalize(Vector2.Zero) * -4,
                0, default, 1.2f);
            dust.velocity *= 1.1f;
            dust.noGravity = true;
            dust.alpha = 175;
            for (int k = 0; k < Main.projectile.Length; k++)
            {
                if (Main.projectile[k].active && Main.projectile[k].friendly)
                {
                    if (Main.projectile[k].modProjectile is HexBolt)
                    {
                        if (Helper.CheckCircularCollision(projectile.Center, projectile.height / 2, Main.projectile[k].Hitbox))
                        {
                            Main.projectile[k].Kill();
                            projectile.ai[0] = 0.8f;
                            Vector2 speed = Vector2.One * 8f;
                            float numberProjectiles = 4 + Main.rand.Next(2); // 3, 4, or 5 shots
                            float rotation = MathHelper.ToRadians(360);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = speed.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 0.2f;
                                Projectile.NewProjectile(projectile.Center, perturbedSpeed, ModContent.ProjectileType<HomingHexBolt>(), projectile.damage / 3, projectile.knockBack, projectile.owner);
                            }
                        }
                    }
                }
            }
        }
    }
}

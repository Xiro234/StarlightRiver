using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Projectiles
{
    public class FireStaffThing : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.friendly = true;
            projectile.penetrate = 4;
            projectile.timeLeft = 180;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VortexPenisStaff moment 2");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (!player.channel)
            {
                projectile.timeLeft -= 12;
            }
            if (projectile.height <= 26)
            {
                projectile.width += 1;
                projectile.height += 1;
            }
            for (int counter = 0; counter <= 4; counter++)
            {
                int dustType = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                        269,
                        203,
                        6
                });
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f)];
                dust.velocity = new Vector2(0, 1f);
                dust.velocity += new Vector2(Main.rand.NextFloat(0.8f, 1.6f), Main.rand.NextFloat(0.8f, 1.6f));
                dust.velocity = dust.velocity / 3f;
                dust.noGravity = true;
                dust.scale = 1.2f;
                if (dustType == 269)
                {
                    dust.position = projectile.Center;
                }
                dust.noLight = true;
            }

            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 25f)
            {
                projectile.velocity.Y += 0.15f;
                projectile.velocity.X *= 0.98f;
            }
            Vector2 vectorToCursor = projectile.Center - player.Center;
            if (projectile.Center.X < player.Center.X)
            {
                vectorToCursor = -vectorToCursor;
            }
            player.itemRotation = vectorToCursor.ToRotation();
            player.itemTime = 20;
            player.itemAnimation = 20;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 14); //boom
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 74); //fork boom
            Projectile.NewProjectile(projectile.Center, new Vector2(0f, 0f), mod.ProjectileType("AOEExplosion"), projectile.damage, projectile.knockBack, player.whoAmI);
            for (int counter = 0; counter <= 21; counter++)
            {
                int dustType = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                        269,
                        6,
                        36
                });
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustType, 1f, 1f, 100, default(Color), 1.2f);
                if (dustType != 36)
                {
                    Main.dust[dust].velocity *= 6f;
                    Main.dust[dust].scale *= 2.2f;
                }
                else
                {
                    Main.dust[dust].velocity *= 1.4f;
                    Main.dust[dust].scale *= 1.2f;
                }
                Main.dust[dust].scale += Main.rand.NextFloat(0.8f, 0.16f);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
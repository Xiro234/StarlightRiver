using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Projectiles
{
    public class FrostStaffThing : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.friendly = true;
            projectile.penetrate = 2;
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
            for (int k = 0; k <= 200; k += 1)
            {
                float maxDistance = 90f;
                NPC npc = Main.npc[k];

                Vector2 vectorToNPC = npc.Center - projectile.Center;
                float distanceToNPC = vectorToNPC.Length();

                if (distanceToNPC <= maxDistance)
                {
                    projectile.timeLeft -= 25;
                }
            }
            if (projectile.height <= 26)
            {
                projectile.width += 1;
                projectile.height += 1;
            }
            for (int counter = 0; counter <= 5; counter++)
            {
                int dustType = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                        113,
                        135,
                        132
                });
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, projectile.velocity.X, projectile.velocity.Y, 160, default(Color), 1f)];
                dust.velocity = new Vector2(0, 1f);
                dust.velocity += new Vector2(Main.rand.NextFloat(0.8f, 1.6f), Main.rand.NextFloat(0.8f, 1.6f));
                dust.velocity = dust.velocity / 4f;
                dust.noGravity = true;
                dust.scale = 0.9f;
                if (dustType == 132)
                {
                    dust.position = projectile.Center;
                }
                else
                {
                    dust.scale = 0.2f;
                    dust.fadeIn = 1.1f;
                }
                dust.noLight = true;
            }

            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 25f)
            {
                projectile.velocity.Y += 0.12f;
                projectile.velocity.X *= 0.99f;
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
            int explosion = Projectile.NewProjectile(projectile.Center, new Vector2(0f, 0f), mod.ProjectileType("AOEExplosion"), projectile.damage, projectile.knockBack, player.whoAmI);
            Main.projectile[explosion].ai[0] = 80;
            for (int counter = 0; counter <= 25; counter++)
            {
                int dustType = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                        113,
                        135,
                        132
                });
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, projectile.velocity.X, projectile.velocity.Y, 160, default(Color), 1f)];
                dust.velocity += new Vector2(Main.rand.NextFloat(0.8f, 1.6f), Main.rand.NextFloat(0.8f, 1.6f));
                dust.velocity = dust.velocity / 4f;
                dust.noGravity = true;
                dust.scale = 0.9f;
                dust.scale = 0.2f;
                dust.fadeIn = 1.1f;

                dust.noLight = true;
            }
        }
    }
}
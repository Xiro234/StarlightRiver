using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using spritersguildwip.NPCs;

namespace spritersguildwip.Projectiles.WeaponProjectiles
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
                float maxDistance = 80f;
                NPC npc = Main.npc[k];
                Vector2 vectorToNPC = npc.Center - projectile.Center;
                float distanceToNPC = vectorToNPC.Length();
                if (npc.active)
                {
                    if (distanceToNPC <= maxDistance)
                    {
                        for (int counter = 0; counter <= 2; counter++)
                        {
                            Dust.NewDust(Vector2.Lerp(projectile.Center, npc.Center, 0.4f), 16, 16, 132, (Vector2.Normalize(projectile.Center - npc.Center) * 8f).X, (Vector2.Normalize(projectile.Center - npc.Center) * 8f).Y);
                        }
                        projectile.velocity = (Vector2.Normalize(projectile.Center - npc.Center) * -12f);
                    }
                }
            }
            if (projectile.height <= 18)
            {
                projectile.width += 1;
                projectile.height += 1;
            }
            for (int counter = 0; counter <= 3; counter++)
            {
                int dustType = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                        113,
                        135,
                        132
                });
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, projectile.velocity.X, projectile.velocity.Y, 160, default(Color), 1f)];
                dust.velocity += new Vector2(Main.rand.NextFloat(-1.6f, 1.6f), Main.rand.NextFloat(-1.6f, 1.6f));
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
            if (!player.channel)
            {
                projectile.timeLeft -= 6;
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 25f)
            {
                projectile.velocity.Y += 0.08f;
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
                dust.velocity += new Vector2(Main.rand.NextFloat(-1.6f, 1.6f), Main.rand.NextFloat(-1.6f, 1.6f));
                dust.velocity = dust.velocity / 4f;
                dust.noGravity = true;
                dust.scale = 0.2f;
                dust.fadeIn = 1.1f;

                dust.noLight = true;
            }
        }
    }
    public class FrostStaffThing2 : ModProjectile
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.GetGlobalNPC<DebuffHandler>(mod).frozenTime = 40;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            for (int k = 0; k <= 200; k += 1)
            {
                float maxDistance = 140f;
                NPC npc = Main.npc[k];
                Vector2 vectorToNPC = npc.Center - projectile.Center;
                float distanceToNPC = vectorToNPC.Length();
                if (npc.active)
                {
                    if (distanceToNPC <= maxDistance)
                    {
                        for (int counter = 0; counter <= 5; counter++)
                        {
                            Dust.NewDust(Vector2.Lerp(projectile.Center, npc.Center, 0.4f), 16, 16, 132, (Vector2.Normalize(projectile.Center - npc.Center) * 8f).X, (Vector2.Normalize(projectile.Center - npc.Center) * 8f).Y);
                        }
                        projectile.velocity = (Vector2.Normalize(projectile.Center - npc.Center) * -12f);
                    }
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
                dust.velocity += new Vector2(Main.rand.NextFloat(-1.6f, 1.6f), Main.rand.NextFloat(-1.6f, 1.6f));
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
            if (!player.channel)
            {
                projectile.timeLeft -= 8;
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 25f)
            {
                projectile.velocity.Y += 0.05f;
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
            int explosion = Projectile.NewProjectile(projectile.Center, new Vector2(0f, 0f), mod.ProjectileType("AOEExplosion"), projectile.damage, projectile.knockBack, player.whoAmI);
            Main.projectile[explosion].ai[0] = 200;
            for (float k = 0; k <= Math.PI * 2; k += (float)Math.PI / 20)
            {
                int dustType = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                        113,
                        135,
                        132
                });
                Dust dust = Dust.NewDustPerfect(projectile.Center, dustType, new Vector2((float)Math.Cos(k), (float)Math.Sin(k)), 0, default, 0.5f);
                dust.noGravity = true;
                dust.scale = 0.9f;
                dust.fadeIn = 1.1f;
                dust.velocity *= 8f;
                dust.noLight = true;
            }
            for (int counter = 0; counter <= 9; counter++)
            {
                int dustType = 132;
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 5f, 5f, 160, default(Color), 1f)];
                dust.noGravity = true;
                dust.scale = 0.9f;
                dust.noLight = true;
            }
        }
    }
}
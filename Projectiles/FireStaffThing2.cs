using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Projectiles
{
    public class FireStaffThing2 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.friendly = true;
            projectile.penetrate = 6;
            projectile.timeLeft = 280;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VortexPenisStaff moment 2");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (projectile.ai[0] == 1)
            {
                projectile.velocity = new Vector2(0f, 0f);
                projectile.ai[1] -= 1;
                if (projectile.ai[1] == 0)
                {
                    projectile.Kill();
                }
                for (int num1 = 0; num1 <= 20; num1++)
                {
                    int dustType = Utils.SelectRandom<int>(Main.rand, new int[]
                    {
                        269,
                        203,
                        6
                    });
                    Vector2 dustPos = projectile.Center + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((8f * projectile.ai[1]) - (num1 * 2));
                    int dust = Dust.NewDust(dustPos - Vector2.One * 8f, 16, 16, dustType, 0f, 0f, 0, default, 0.6f);
                    Main.dust[dust].velocity = Vector2.Normalize(projectile.Center - dustPos) * 1.5f * (10f - num1 * 2f) / 10f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1.1f;
                }
                for (int k = 0; k <= 200; k += 1)
                {
                    float maxDistance = 9f * projectile.ai[1];
                    NPC npc = Main.npc[k];

                    Vector2 vectorToNPC = npc.Center - projectile.Center;
                    float distanceToNPC = vectorToNPC.Length();

                    if (distanceToNPC <= maxDistance)
                    {
                        Vector2 Direction = new Vector2(projectile.Center.X - npc.Center.X, projectile.Center.Y - npc.Center.Y);
                        Direction.Normalize();
                        npc.velocity = Direction * 4f;
                    }
                }
            }
            if (projectile.ai[0] == 0)
            {
                if (!player.channel)
                {
                    projectile.ai[0] = 1;
                    projectile.ai[1] = 20;
                }
                if (projectile.height <= 34)
                {
                    projectile.width += 2;
                    projectile.height += 2;
                }
                for (int counter = 0; counter <= 5; counter++)
                {
                    int dustType = Utils.SelectRandom<int>(Main.rand, new int[]
                    {
                        269,
                        203,
                        6
                    });
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f)];
                    dust.velocity = new Vector2(0, 2f);
                    dust.velocity += new Vector2(Main.rand.NextFloat(0.8f, 1.6f), Main.rand.NextFloat(0.8f, 1.6f));
                    dust.velocity = dust.velocity / 2f;
                    dust.noGravity = true;
                    dust.scale = 1.4f;
                    if (dustType == 269)
                    {
                        dust.position = projectile.Center;
                    }
                    dust.noLight = true;
                }

                projectile.ai[1] += 1f;
                if (projectile.ai[1] >= 25f)
                {
                    projectile.velocity.Y += 0.15f;
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
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 14); //boom
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 74); //fork boom
            for (int counter = 0; counter <= 21; counter++)
            {
                Vector2 velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));

                int dustType = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                        269,
                        6
                });
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustType, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[dust].velocity = velocity * 8f;
                Main.dust[dust].scale *= 1.4f;
                Main.dust[dust].scale += Main.rand.NextFloat(0.8f, 1.6f);
                Main.dust[dust].noGravity = true;
            }
            for (int counter2 = 0; counter2 <= 10; counter2++) //sparks
            {
                Vector2 velocity = new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f));
                velocity *= 2f * (counter2 / 3);
                int dustType = 133;
                int dust = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, dustType, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[dust].velocity = velocity;
            }
        }
    }
}
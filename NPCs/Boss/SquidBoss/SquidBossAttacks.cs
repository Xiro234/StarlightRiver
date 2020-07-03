using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.SquidBoss
{
    public partial class SquidBoss : ModNPC
    {
        private void RandomizeTarget()
        {
            List<int> possible = new List<int>();
            foreach (Player player in Main.player.Where(n => Vector2.Distance(n.Center, npc.Center) < 1500))
            {
                possible.Add(player.whoAmI);
            }
            if (possible.Count == 0) { npc.active = false; return; }
            npc.target = possible[Main.rand.Next(possible.Count - 1)];
        }

        private void ResetAttack() => npc.ai[3] = 0;

        private void ShufflePlatforms()
        {
            int n = platforms.Count(); //fisher yates
            while (n > 1)
            {
                n--;
                int k = Main.rand.Next(n + 1);
                NPC value = platforms[k];
                platforms[k] = platforms[n];
                platforms[n] = value;
            }
        }

        #region phase 1
        private void TentacleSpike()
        {
            RandomizeTarget();
            for (int k = 0; k < 4; k++)
            {
                Tentacle tentacle = tentacles[k].modNPC as Tentacle;

                if (npc.ai[3] == k * 100 || (k == 0 && npc.ai[3] == 1)) //teleport where needed
                {
                    int adj = (int)Main.player[npc.target].velocity.X * 60; if (adj > 200) adj = 200;
                    tentacles[k].Center = new Vector2(Main.player[npc.target].Center.X + adj, tentacles[k].Center.Y);
                    tentacle.SavedPoint = tentacles[k].Center;
                    tentacle.MovePoint = tentacles[k].Center + new Vector2(0, -1000);

                    for (int n = 0; n < 50; n++)
                    {
                        Dust.NewDustPerfect(tentacles[k].Center + new Vector2(0, -n * 25 + Main.rand.NextFloat(5)), DustID.Fireworks, Vector2.Zero, 0, default, 0.5f);
                    }
                    Main.PlaySound(SoundID.Drown, npc.Center);
                }

                if (npc.ai[3] > k * 100 + 30 && npc.ai[3] < k * 100 + 90) //shooting up, first 30 frames are for tell
                {
                    if (npc.ai[3] == k * 100 + 40)
                    {
                        Main.PlaySound(SoundID.Splash, npc.Center);
                        Main.PlaySound(SoundID.Item81, npc.Center);
                    }
                    int time = (int)npc.ai[3] - (k * 100 + 30);
                    tentacles[k].Center = Vector2.SmoothStep(tentacle.SavedPoint, tentacle.MovePoint, time / 60f);
                    tentacles[k].ai[1] += 5f; //make it squirm faster
                }

                if (npc.ai[3] > k * 100 + 90 && npc.ai[3] < k * 100 + 300) //retracting
                {
                    int time = (int)npc.ai[3] - (k * 100 + 90);
                    tentacles[k].Center = Vector2.SmoothStep(tentacle.MovePoint, tentacle.SavedPoint, time / 210f);
                }
            }

            if (npc.ai[3] == 600) ResetAttack();
        }

        private void InkBurst()
        {
            for (float k = 0; k <= 3.14f; k += 3.14f / 5f)
            {
                if (npc.ai[3] % 3 == 0) Projectile.NewProjectile(npc.Center + new Vector2(0, 100), new Vector2(-10, 0).RotatedBy(k), ModContent.ProjectileType<InkBlob>(), 10, 0.2f, 255, 0, Main.rand.NextFloat(6.28f));
                if (npc.ai[3] % 10 == 0) Main.PlaySound(SoundID.Item95, npc.Center);
                if (npc.ai[3] == 60) ResetAttack();
            }
        }

        private void PlatformSweep()
        {
            if (npc.ai[3] == 1) //start by randomizing the platform order and assigning targets
            {
                ShufflePlatforms();
                for (int k = 0; k < 4; k++)
                {
                    Tentacle tentacle = tentacles[k].modNPC as Tentacle;
                    tentacles[k].Center = new Vector2(platforms[k].Center.X, tentacles[k].Center.Y);
                    tentacle.SavedPoint = tentacles[k].Center;
                    tentacle.MovePoint = platforms[k].Center + new Vector2(0, -70);
                }
                Main.PlaySound(SoundID.Drown, npc.Center);
            }

            if (npc.ai[3] > 60 && npc.ai[3] < 120) //rising
            {
                if (npc.ai[3] == 61)
                {
                    Main.PlaySound(SoundID.Splash, npc.Center);
                    Main.PlaySound(SoundID.Item81, npc.Center);
                }

                for (int k = 0; k < 4; k++)
                {
                    Tentacle tentacle = tentacles[k].modNPC as Tentacle;
                    tentacles[k].Center = Vector2.SmoothStep(tentacle.SavedPoint, tentacle.MovePoint, (npc.ai[3] - 60) / 60f);
                }
            }

            if (npc.ai[3] > 120 && npc.ai[3] < 360) //waving around
            {
                for (int k = 0; k < 4; k++)
                {
                    tentacles[k].position.X += (float)Math.Sin(npc.ai[3] / 10f + k) * 2;
                    tentacles[k].position.Y += (float)Math.Cos(npc.ai[3] / 10f + k) * 4;
                }
            }

            if (npc.ai[3] > 360 && npc.ai[3] < 420) //going back
            {
                for (int k = 0; k < 4; k++)
                {
                    Tentacle tentacle = tentacles[k].modNPC as Tentacle;
                    tentacles[k].Center = Vector2.SmoothStep(tentacle.MovePoint, tentacle.SavedPoint, (npc.ai[3] - 360) / 60f);
                }
            }

            if (npc.ai[3] == 420) ResetAttack();

        }

        private void ArenaSweep()
        {
            for (int k = 0; k < 4; k++)
            {
                Tentacle tentacle = tentacles[k].modNPC as Tentacle;

                if (AttackTimer == 1)
                {
                    tentacles[k].Center = spawnPoint + new Vector2(850, 0);
                    tentacle.SavedPoint = tentacles[k].Center;
                }

                if (AttackTimer > 30 + k * 60 && AttackTimer < (30 + k * 60) + 400)
                {
                    if (AttackTimer % 60 == 0)
                    {
                        Main.PlaySound(SoundID.Splash, npc.Center);
                        Main.PlaySound(SoundID.Item81, npc.Center);
                    }

                    tentacles[k].position.X -= 4.25f;
                    tentacle.SavedPoint.X -= 4.25f;
                    tentacles[k].position.Y = tentacle.SavedPoint.Y - (1 + (float)Math.Cos(AttackTimer / 20f + k * -60)) * 500;
                }

                if (AttackTimer == (30 + k * 60) + 400) tentacle.MovePoint = tentacles[k].Center;

                if (AttackTimer > (30 + k * 60) + 400 && AttackTimer < ((30 + k * 60) + 400) + 40)
                {
                    float rel = (AttackTimer - ((30 + k * 60) + 400)) / 40f;
                    tentacles[k].Center = Vector2.SmoothStep(tentacle.MovePoint, tentacle.SavedPoint, rel);
                }
            }

            if (AttackTimer >= 660) ResetAttack();
        }
        #endregion

        #region phase 2
        private void Spew()
        {
            if (npc.ai[3] % 100 == 0)
            {
                Main.PlaySound(SoundID.Item9, npc.Center);

                if (Main.expertMode) //spawn more + closer together on expert
                {
                    for (int k = 0; k < 14; k++)
                        Projectile.NewProjectile(npc.Center + new Vector2(0, 100), new Vector2(-100 + k * 14, 0), ModContent.ProjectileType<SpewBlob>(), 10, 0.2f);
                }
                else
                {
                    for (int k = 0; k < 10; k++)
                        Projectile.NewProjectile(npc.Center + new Vector2(0, 100), new Vector2(-100 + k * 20, 0), ModContent.ProjectileType<SpewBlob>(), 10, 0.2f);
                }
            }

            if (npc.ai[3] == 300) ResetAttack();
        }

        private void Laser()
        {
            npc.ai[1]++;

            if (npc.ai[3] == 1) //set movement points
            {
                savedPoint = npc.Center;
                npc.velocity *= 0;
                npc.rotation = 0;
            }

            if (npc.ai[3] < 60) //move to left of the arena
            {
                npc.Center = Vector2.SmoothStep(savedPoint, spawnPoint + new Vector2(-800, -500), npc.ai[3] / 60f);
                npc.rotation += 3.14f / 59f;
            }

            if (npc.ai[3] == 60)
            {
                savedPoint = npc.Center; //leftmost point of laser
                Projectile.NewProjectile(npc.Center + new Vector2(0, -200), Vector2.Zero, ModContent.ProjectileType<Laser>(), 10, 0.2f, 255, 0, npc.ai[3] * 0.1f);
            }

            int laserTime = Main.expertMode ? 450 : 600; //faster in expert

            if (npc.ai[3] > 60 && npc.ai[3] < 60 + laserTime) //lasering
            {
                if (npc.ai[3] % 10 == 0) Main.PlaySound(SoundID.NPCHit53, npc.Center);
                npc.Center = Vector2.Lerp(savedPoint, spawnPoint + new Vector2(800, -500), (npc.ai[3] - 60) / laserTime);
            }

            if (npc.ai[3] == 60 + laserTime) savedPoint = npc.Center; //end of laser

            if (npc.ai[3] > 60 + laserTime && npc.ai[3] < 120 + laserTime) //return to center of arena
            {
                npc.Center = Vector2.SmoothStep(savedPoint, spawnPoint + new Vector2(0, -300), (npc.ai[3] - (laserTime + 60)) / 60f);
                npc.rotation -= 3.14f / 59f;
            }

            if (npc.ai[3] >= 120 + laserTime) ResetAttack();
        }

        private void Leap()
        {
            if (npc.ai[3] == 1)
            {
                savedPoint = npc.Center;
                npc.velocity *= 0;
                npc.rotation = 0;

                for (int k = 0; k < 4; k++) //tentacles
                {
                    Tentacle tentacle = tentacles[k].modNPC as Tentacle;
                    int off;

                    switch (k)
                    {
                        case 0: off = -430; break;
                        case 1: off = -150; break;
                        case 2: off = 150; break;
                        case 3: off = 430; break;
                        default: off = 0; break;
                    }

                    tentacles[k].Center = new Vector2(spawnPoint.X + off, tentacles[k].Center.Y);
                    tentacle.SavedPoint = tentacles[k].Center;
                    tentacle.MovePoint = tentacles[k].Center + new Vector2(off * 0.45f, -900);

                    for (int n = 0; n < 40; n++)
                        Dust.NewDustPerfect(Vector2.Lerp(tentacle.SavedPoint, tentacle.MovePoint, n / 30f), DustID.Fireworks, Vector2.Zero);
                }
            }

            if (npc.ai[3] < 120) //go to center
            {
                npc.Center = Vector2.SmoothStep(savedPoint, spawnPoint + new Vector2(0, -500), npc.ai[3] / 120f);

                for (int k = 0; k < 4; k++) //tentacles
                {
                    Tentacle tentacle = tentacles[k].modNPC as Tentacle;
                    tentacles[k].Center = Vector2.SmoothStep(tentacle.SavedPoint, tentacle.MovePoint, npc.ai[3] / 120f);
                }
            }

            if (npc.ai[3] == 120) npc.velocity.Y = -15; //jump

            if (npc.ai[3] == 150) //spawn projectiles
            {
                Main.PlaySound(SoundID.NPCDeath24, npc.Center);

                for (float k = 0; k <= 3.14f; k += 3.14f / 4f)
                    Projectile.NewProjectile(npc.Center + new Vector2(0, 100), new Vector2(-10, 0).RotatedBy(k), ModContent.ProjectileType<InkBlob>(), 10, 0.2f, 255, 0, Main.rand.NextFloat(6.28f));
            }

            if (npc.ai[3] > 120 && npc.ai[3] < 220) npc.velocity.Y += 0.16f; //un-jump

            if (npc.ai[3] > 120)
            {
                for (int k = 0; k < 4; k++) //tentacles
                {
                    Tentacle tentacle = tentacles[k].modNPC as Tentacle;
                    tentacles[k].Center = new Vector2(tentacles[k].Center.X + (float)Math.Sin(npc.ai[3] / 10f + k) * 4f, tentacles[k].Center.Y + (float)Math.Cos(npc.ai[3] / 10f + k) * 2f);
                }
            }

            if (npc.ai[3] > 540)
            {
                for (int k = 0; k < 4; k++) //tentacles
                {
                    Tentacle tentacle = tentacles[k].modNPC as Tentacle;
                    tentacles[k].Center = Vector2.SmoothStep(tentacle.MovePoint, tentacle.SavedPoint, (npc.ai[3] - 540) / 60f);
                }
            }

            if (npc.ai[3] == 600) ResetAttack();
        }

        private void Eggs()
        {
            if (AttackTimer == 1)
            {
                npc.velocity *= 0;
                savedPoint = npc.Center;
            }

            if (AttackTimer < 60) npc.Center = Vector2.SmoothStep(savedPoint, spawnPoint + Vector2.UnitY * -800, AttackTimer / 60);

            if (npc.ai[3] == 60)
            {
                Main.PlaySound(SoundID.Item9, npc.Center);
                for (int k = 0; k < 5; k++)
                    Projectile.NewProjectile(npc.Center + new Vector2(0, 100), new Vector2(-50 + k * 25, 0), ModContent.ProjectileType<SquidEgg>(), 10, 0.2f);
            }

            if (npc.ai[3] == 300) ResetAttack();
        }
        #endregion

        #region phase 3
        private void TentacleSpike2()
        {
            RandomizeTarget();
            for (int k = 0; k < 4; k++)
            {
                Tentacle tentacle = tentacles[k].modNPC as Tentacle;
                if (npc.ai[3] == k * 80 || (k == 0 && npc.ai[3] == 1)) //teleport where needed
                {
                    tentacles[k].Center = new Vector2(Main.npc.FirstOrDefault(n => n.active && n.modNPC is ArenaActor).Center.X + (k % 2 == 0 ? -600 : 600), npc.Center.Y + Main.rand.Next(-200, 200));
                    tentacle.SavedPoint = tentacles[k].Center;
                    tentacle.MovePoint = Main.player[npc.target].Center;

                    for (int n = 0; n < 50; n++)
                        Dust.NewDustPerfect(Vector2.Lerp(Main.player[npc.target].Center, tentacle.SavedPoint, n / 50f), DustID.Fireworks, Vector2.Zero, 0, default, 0.5f);

                    Main.PlaySound(SoundID.Drown, npc.Center);
                }

                if (npc.ai[3] > k * 80 + 30 && npc.ai[3] < k * 80 + 90) //shooting up, first 30 frames are for tell
                {
                    if (npc.ai[3] == k * 80 + 40)
                    {
                        Main.PlaySound(SoundID.Splash, npc.Center);
                        Main.PlaySound(SoundID.Item81, npc.Center);
                    }

                    int time = (int)npc.ai[3] - (k * 80 + 30);
                    tentacles[k].Center = Vector2.SmoothStep(tentacle.SavedPoint, tentacle.MovePoint, time / 50f);
                    tentacles[k].ai[1] += 5f; //make it squirm faster
                }

                if (npc.ai[3] > k * 80 + 90 && npc.ai[3] < k * 80 + 150) //retracting
                {
                    int time = (int)npc.ai[3] - (k * 80 + 90);
                    tentacles[k].Center = Vector2.SmoothStep(tentacle.MovePoint, tentacle.SavedPoint, time / 60f);
                }
            }

            if (npc.ai[3] == 400 && !Main.expertMode) ResetAttack(); //stop on normal mode only

            for (int k = 0; k < 4; k++)
            {
                Tentacle tentacle = tentacles[k].modNPC as Tentacle;

                if (AttackTimer == 401)
                {
                    RandomizeTarget();
                    Player player = Main.player[npc.target];

                    tentacles[k].Center = player.Center + new Vector2(k % 2 == 0 ? -800 : 800, k > 1 ? 0 : -400);
                    tentacle.SavedPoint = tentacles[k].Center;
                    tentacle.MovePoint = Main.player[npc.target].Center;

                    for (int n = 0; n < 50; n++)
                        Dust.NewDustPerfect(Vector2.Lerp(Main.player[npc.target].Center, tentacle.SavedPoint, n / 50f), DustID.Fireworks, Vector2.Zero, 0, default, 0.5f);

                    Main.PlaySound(SoundID.Drown, npc.Center);
                }

                if (npc.ai[3] > 420 && npc.ai[3] < 460) //shooting out
                {
                    if (npc.ai[3] == 401)
                    {
                        Main.PlaySound(SoundID.Splash, npc.Center);
                        Main.PlaySound(SoundID.Item81, npc.Center);
                    }

                    tentacles[k].Center = Vector2.SmoothStep(tentacle.SavedPoint, tentacle.MovePoint, (AttackTimer - 420) / 40f);
                    tentacles[k].ai[1] += 5f; //make it squirm faster
                }

                if (npc.ai[3] > 460 && npc.ai[3] < 520) //retracting
                {
                    tentacles[k].Center = Vector2.SmoothStep(tentacle.MovePoint, tentacle.SavedPoint, (AttackTimer - 460) / 60f);
                }
            }

            if (AttackTimer > 550) ResetAttack();
        }

        private void StealPlatform()
        {
            if (npc.ai[3] == 1)
            {
                ShufflePlatforms();

                Tentacle tentacle = tentacles[0].modNPC as Tentacle;
                tentacles[0].Center = new Vector2(platforms[0].Center.X, spawnPoint.Y - 100);
                tentacle.SavedPoint = tentacles[0].Center;
            }

            if (npc.ai[3] < 90)
            {
                Dust.NewDust(platforms[0].position, 200, 16, DustID.Fireworks, 0, 0, 0, default, 0.7f);

                Tentacle tentacle = tentacles[0].modNPC as Tentacle;
                tentacles[0].Center = Vector2.SmoothStep(tentacle.SavedPoint, platforms[0].Center, npc.ai[3] / 90f);
            }

            if (npc.ai[3] == 90)
            {
                Tentacle tentacle = tentacles[0].modNPC as Tentacle;
                tentacle.MovePoint = tentacles[0].Center;
                platforms[0].ai[3] = 450; //sets it into fall mode
            }

            if (npc.ai[3] > 90)
            {
                Tentacle tentacle = tentacles[0].modNPC as Tentacle;
                tentacles[0].Center = Vector2.SmoothStep(tentacle.MovePoint, tentacle.SavedPoint, (npc.ai[3] - 90) / 90f);
            }

            if (npc.ai[3] == 180) ResetAttack();
        }

        private void InkBurst2()
        {
            if (npc.ai[3] == 1)
            {
                npc.velocity *= 0;
                npc.velocity.Y = -10;
            }

            if (npc.ai[3] <= 61) npc.velocity.Y += 10 / 60f;

            if (npc.ai[3] > 61)
            {
                for (float k = 0; k <= 3.14f; k += 2.14f / 3f)
                {
                    if (npc.ai[3] % 3 == 0) Projectile.NewProjectile(npc.Center + new Vector2(0, 100), new Vector2(10, 0).RotatedBy(k), ModContent.ProjectileType<InkBlob>(), 10, 0.2f, 255, 0, Main.rand.NextFloat(6.28f));
                    if (npc.ai[3] % 10 == 0) Main.PlaySound(SoundID.Item95, npc.Center);
                }
            }

            if (npc.ai[3] == 76) ResetAttack();
        }
        #endregion
    }
}

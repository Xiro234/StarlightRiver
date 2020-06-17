﻿using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.OvergrowBoss
{
    public partial class OvergrowBoss : ModNPC
    {
        private void Phase1Spin()
        {
            if (npc.ai[3] <= 60)
            {
                npc.Center = Vector2.SmoothStep(npc.Center, spawnPoint, npc.ai[3] / 60f);
                flail.npc.Center = Vector2.SmoothStep(flail.npc.Center, spawnPoint, npc.ai[3] / 60f);
                if (npc.Center == spawnPoint) npc.ai[3] = 61;
            }
            if (npc.ai[3] == 61)
            {
                npc.TargetClosest();
                targetPoint = Main.player[npc.target].Center;
                Main.NewText(targetPoint);
            }
            float size = Vector2.Distance(targetPoint, npc.Center);
            if (size > 400) size = 400;
            //Main.NewText(size);

            if (npc.ai[3] <= 120)
                flail.npc.Center = Vector2.Lerp(flail.npc.Center, npc.Center, (npc.ai[3] - 60) / 40);

            if (npc.ai[3] > 120 && npc.ai[3] <= 160)
                flail.npc.Center = Vector2.SmoothStep(npc.Center, npc.Center + new Vector2(0, size), (npc.ai[3] - 120) / 40f);

            if (npc.ai[3] > 160 && npc.ai[3] <= 280)
            {
                int x = (int)npc.ai[3] - 160;
                float rot = 0.314f * -0.042f * x + 0.003f * (float)Math.Pow(x, 2) - 0.00002f * (float)Math.Pow(x, 3);
                //function to model the desired rotation, thanks wolfram alpha :3
                flail.npc.Center = npc.Center + new Vector2(0, 1).RotatedBy(rot) * size;

                if (npc.ai[3] > 165 && npc.ai[3] < 250)
                {
                    for (int k = 0; k < 3; k++)
                        Dust.NewDust(flail.npc.position, flail.npc.width, flail.npc.height, DustType<Dusts.Gold2>());
                    for (int k = 0; k < 8; k++)
                        Dust.NewDustPerfect(Vector2.Lerp(flail.npc.Center, flail.npc.oldPosition + flail.npc.Size / 2, k / 8f), DustType<Dusts.Gold2>(), Vector2.One.RotatedByRandom(6.28f) * 0.5f);
                }
            }
            if (npc.ai[3] == 280)
            {
                flail.npc.velocity = flail.npc.position - flail.npc.oldPosition;
                flail.npc.velocity.X *= 0.2f;
            }
            if (npc.ai[3] > 280 && npc.ai[3] <= 450)
            {
                if (Vector2.Distance(flail.npc.Center, npc.Center) < size) flail.npc.velocity.Y += 0.8f;
                else
                {
                    float cos = (float)Math.Cos((npc.Center - flail.npc.Center).ToRotation());
                    flail.npc.velocity.X += flail.npc.velocity.Y * cos;
                    flail.npc.velocity.Y *= -0.1f;
                }

                flail.npc.velocity.X += (npc.Center.X - flail.npc.Center.X) * 0.01f;
                flail.npc.velocity *= 0.96f;
            }
            if (npc.ai[3] == 451) ResetAttack();
        }

        private void Phase1Pendulum()
        {
            if (npc.ai[3] > 1 && npc.ai[3] <= 60)
            {
                flail.npc.Center = Vector2.SmoothStep(flail.npc.Center, npc.Center, npc.ai[3] / 60f);
            }

            if (npc.ai[3] == 60) targetPoint = Main.player[npc.target].Center;
            int direction = -Math.Sign(targetPoint.X - spawnPoint.X);

            if (npc.ai[3] > 60 && npc.ai[3] <= 90)
            {
                if (targetPoint.Y > npc.Center.Y)
                {
                    flail.npc.Center = Vector2.SmoothStep(flail.npc.Center, npc.Center + new Vector2(0, targetPoint.Y - npc.Center.Y), (npc.ai[3] - 60) / 30f);
                }
                else
                {
                    flail.npc.Center = Vector2.SmoothStep(flail.npc.Center, npc.Center + new Vector2(0, 150), (npc.ai[3] - 60) / 30f);
                }
            }
            if (npc.ai[3] > 90 && npc.ai[3] <= 160)
            {
                npc.Center = Vector2.SmoothStep(npc.Center, spawnPoint + new Vector2((500) * -direction, 0), (npc.ai[3] - 90) / 70f);
                flail.npc.Center = Vector2.SmoothStep(flail.npc.Center, spawnPoint + new Vector2((500) * -direction, flail.npc.Center.Y - npc.Center.Y), (npc.ai[3] - 90) / 60f);
            }
            if (npc.ai[3] == 210) ResetAttack();
        }

        private void Phase1Bolts()
        {
            Vector2 handpos = npc.Center; //used as a basepoint for this attack to match the animation

            if (npc.ai[3] <= 30)
            {
                float rot = Main.rand.NextFloat(6.28f); //random rotation for the dust
                Dust.NewDustPerfect(handpos + Vector2.One.RotatedBy(rot) * 50, DustType<Dusts.Gold2>(), -Vector2.One.RotatedBy(rot) * 2); //"suck in" charging effect
            }
            if (npc.ai[3] == 30)
            {
                RandomTarget(); //pick a random target
                if (Main.player[npc.target] == null) //safety check
                {
                    ResetAttack();
                    return;
                }
            }
            if (npc.ai[3] == 60) targetPoint = Main.player[npc.target].Center;
            if (npc.ai[3] >= 60 && npc.ai[3] <= 120 && npc.ai[3] % 30 == 0) //3 rounds of projectiles
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/ProjectileLaunch1"), npc.Center);
                for (float k = -0.6f; k <= 0.6f; k += 0.3f) //5 projectiles in even spread
                {
                    Vector2 trajectory = Vector2.Normalize(targetPoint - handpos).RotatedBy(k + (npc.ai[3] == 90 ? 0.15f : 0)) * 1.6f; //towards the target, alternates on the second round
                    Projectile.NewProjectile(handpos, trajectory, ProjectileType<OvergrowBossProjectile.Phase1Bolt>(), 20, 0.2f);
                }
            }
            if (npc.ai[3] == 200) ResetAttack();
        }

        private void Phase1Toss()
        {
            if (npc.ai[3] <= 60)
                flail.npc.Center = Vector2.Lerp(flail.npc.Center, npc.Center, npc.ai[3] / 50);
            if (npc.ai[3] == 60)
            {
                npc.TargetClosest();
                targetPoint = Main.player[npc.target].Center + Main.player[npc.target].velocity * 30; //sets the target to the closest player
                if (Vector2.Distance(Main.player[npc.target].Center, targetPoint) > 300) targetPoint = Main.player[npc.target].Center + Vector2.Normalize(Main.player[npc.target].Center + targetPoint) * 300; //clamp to 3d00 pixels away
            }

            if (Main.player[npc.target] == null && npc.ai[3] == 60) ResetAttack(); //defensive programminginging!!

            Vector2 trajectory = -Vector2.Normalize(npc.Center - targetPoint); //boss' toss direction
            if (npc.ai[3] > 60 && npc.ai[3] < 120)
            {
                flail.npc.Center = Vector2.Lerp(npc.Center, npc.Center + trajectory * -20, (npc.ai[3] - 60) / 120f); //pull it back
            }
            if (npc.ai[3] == 120) flail.npc.velocity = trajectory * 20;
            if ((flail.npc.velocity.Y == 0 || flail.npc.velocity.X == 0 || Main.tile[(int)flail.npc.Center.X / 16, (int)flail.npc.Center.Y / 16 + 1].collisionType == 1) && !(flail.npc.velocity.Y == 0 && flail.npc.velocity.X == 0)) //hit the ground
            {
                //updates
                flail.npc.velocity *= 0;
                npc.ai[3] = 180;

                //visuals
                for (int k = 0; k < 50; k++)
                {
                    Dust.NewDust(flail.npc.position, flail.npc.width, flail.npc.height, DustType<Dusts.Stone>(), Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3));
                    Dust.NewDustPerfect(flail.npc.Center, DustType<Dusts.Gold2>(), Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(5), 0, default, 1);
                }

                //audio
                Main.PlaySound(SoundID.Item70, flail.npc.Center);
                Main.PlaySound(SoundID.NPCHit42, flail.npc.Center);

                //screenshake
                int distance = (int)Vector2.Distance(Main.LocalPlayer.Center, flail.npc.Center);
                Main.LocalPlayer.GetModPlayer<StarlightPlayer>().Shake += distance < 100 ? distance / 20 : 5;
            }

            if (npc.ai[3] == 240) ResetAttack();
        }

        private void DrawTossTell(SpriteBatch sb)
        {
            float glow = npc.ai[3] > 90 ? (1 - (npc.ai[3] - 90) / 30f) : ((npc.ai[3] - 60) / 30f);
            Color color = new Color(255, 70, 70) * glow;
            Texture2D tex = GetTexture("StarlightRiver/Gores/TellBeam");
            sb.End();
            sb.Begin(default, BlendState.Additive, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);
            for (float k = 0; 1 == 1; k++)
            {
                Vector2 point = Vector2.Lerp(npc.Center, npc.Center + Vector2.Normalize(targetPoint - npc.Center) * tex.Frame().Width, k);
                sb.Draw(tex, point - Main.screenPosition, tex.Frame(), color, (targetPoint - npc.Center).ToRotation(), tex.Frame().Size() / 2, 1, 0, 0);

                if (!WorldGen.InWorld((int)point.X / 16, (int)point.Y / 16)) break;
                Tile tile = Framing.GetTileSafely(point / 16);
                if (tile.active()) break;
            }
            sb.End();
            sb.Begin(default, default, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);
        }

        private void Phase1Trap()
        {
            Main.NewText(npc.ai[3]);
            if (npc.ai[3] == 1)
            {
                RandomTarget();
                targetPoint = Main.player[npc.target].Center + new Vector2(0, -50);
            }
            if (npc.ai[3] == 90)
            {
                foreach (Player player in Main.player.Where(p => p.active && Helper.CheckCircularCollision(targetPoint, 100, p.Hitbox))) //circular collision
                {
                    player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " was strangled..."), 50, 0); //hurt em
                    //debuff em
                }

                //dusts
                for (float k = 0; k < 6.28f; k += 0.1f)
                {
                    Dust.NewDustPerfect(targetPoint + Vector2.One.RotatedBy(k) * 90, DustType<Dusts.Leaf>(), null, 0, default, 1.5f);
                    Dust.NewDustPerfect(targetPoint + Vector2.One.RotatedBy(k) * Main.rand.NextFloat(95, 105), DustType<Dusts.Gold2>(), null, 0, default, 0.6f);
                    if (Main.rand.Next(4) == 0) Dust.NewDustPerfect(targetPoint + Vector2.One.RotatedBy(k) * Main.rand.Next(100), DustType<Dusts.Leaf>());
                }
            }
            if (npc.ai[3] >= 180) ResetAttack();
        }

        private void DrawTrapTell(SpriteBatch sb)
        {
            float glow = npc.ai[3] > 45 ? (1 - (npc.ai[3] - 45) / 45f) : ((npc.ai[3]) / 45f);
            Color color = new Color(255, 40, 40) * glow;
            Texture2D tex = GetTexture("StarlightRiver/Gores/TellCircle");
            sb.End();
            sb.Begin(default, BlendState.Additive, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);

            if (npc.ai[3] <= 90) sb.Draw(tex, targetPoint - Main.screenPosition, tex.Frame(), color, 0, tex.Frame().Size() / 2, 2, 0, 0);
            else if (npc.ai[3] <= 100) sb.Draw(tex, targetPoint - Main.screenPosition, tex.Frame(), new Color(255, 200, 30) * (1 - (npc.ai[3] - 90) / 10f), 0, tex.Frame().Size() / 2, 2, 0, 0);

            sb.End();
            sb.Begin(default, default, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);
        }

        private void RapidToss()
        {
            if (npc.ai[3] <= 15)
                flail.npc.Center = Vector2.Lerp(flail.npc.Center, npc.Center, npc.ai[3] / 15);
            if (npc.ai[3] == 15)
            {
                npc.TargetClosest();
                targetPoint = Main.player[npc.target].Center + Main.player[npc.target].velocity * 10; //sets the target to the closest player
                if (Vector2.Distance(Main.player[npc.target].Center, targetPoint) > 300) targetPoint = Main.player[npc.target].Center + Vector2.Normalize(Main.player[npc.target].Center + targetPoint) * 300; //clamp to 3d00 pixels away
                npc.ai[3] = 60; //i am lazy
            }

            if (Main.player[npc.target] == null && npc.ai[3] == 20) ResetAttack(); //defensive programminginging!!

            Vector2 trajectory = -Vector2.Normalize(npc.Center - targetPoint); //boss' toss direction
            if (npc.ai[3] > 60 && npc.ai[3] < 120)
            {
                flail.npc.Center = Vector2.Lerp(npc.Center, npc.Center + trajectory * -10, (npc.ai[3] - 60) / 120f); //pull it back
                npc.ai[3]++; //double time! im lazy.
            }
            if (npc.ai[3] == 120) flail.npc.velocity = trajectory * 24;
            if ((flail.npc.velocity.Y == 0 || flail.npc.velocity.X == 0 || Main.tile[(int)flail.npc.Center.X / 16, (int)flail.npc.Center.Y / 16 + 1].collisionType == 1) && !(flail.npc.velocity.Y == 0 && flail.npc.velocity.X == 0)) //hit the ground
            {
                //updates
                flail.npc.velocity *= 0;
                npc.ai[3] = 160;

                //visuals
                for (int k = 0; k < 50; k++)
                {
                    Dust.NewDust(flail.npc.position, flail.npc.width, flail.npc.height, DustType<Dusts.Stone>(), Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3));
                    Dust.NewDustPerfect(flail.npc.Center, DustType<Dusts.Gold2>(), Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(5), 0, default, 1);
                }

                //audio
                Main.PlaySound(SoundID.Item70, flail.npc.Center);
                Main.PlaySound(SoundID.NPCHit42, flail.npc.Center);

                //screenshake
                int distance = (int)Vector2.Distance(Main.LocalPlayer.Center, flail.npc.Center);
                Main.LocalPlayer.GetModPlayer<StarlightPlayer>().Shake += distance < 100 ? distance / 20 : 5;
            }

            if (npc.ai[3] == 180) ResetAttack();
        }

        private void RandomTarget()
        {
            List<int> players = new List<int>();
            foreach (Player player in Main.player.Where(p => Vector2.Distance(npc.Center, p.Center) < 2000)) players.Add(player.whoAmI);
            if (players.Count == 0) return;
            npc.target = players[Main.rand.Next(players.Count)];

            Main.NewText("Random target chosen!");
        }

        public void ResetAttack()
        {
            flail.npc.velocity *= 0;
            npc.ai[3] = 0;
            npc.ai[2] = 0;
        }
    }
}
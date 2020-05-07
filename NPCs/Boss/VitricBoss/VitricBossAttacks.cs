
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    sealed partial class VitricBoss : ModNPC
    {
        public void ResetAttack()
        {
            npc.ai[3] = 0;
        }
        private void RandomizeTarget()
        {
            List<int> players = new List<int>();
            foreach(Player player in Main.player.Where(n => n.active))
            {
                players.Add(player.whoAmI);
            }
            npc.target = players[Main.rand.Next(players.Count)];
        }

        private void NukePlatforms()
        {
            if (npc.ai[3] == 1)
            {
                List<Vector2> possibleLocations = new List<Vector2>(CrystalLocations);
                for (int k = 0; k < Crystals.Count; k++)
                {
                    NPC npc = Crystals[k];
                    VitricBossCrystal crystal = npc.modNPC as VitricBossCrystal;

                    crystal.StartPos = npc.Center;
                    Vector2 target = possibleLocations.OrderBy(n => Vector2.Distance(npc.Center, n)).ToList()[0]; //sort the remaining possible unique platforms by distance, this ensures crystals go to theirn earest unique platform
                    crystal.TargetPos = target;
                    possibleLocations.Remove(target);
                    npc.ai[1] = 0; //reset the crystal's timers
                    npc.ai[2] = 1; //set them into this attack's mode
                }
            }
            if (npc.ai[3] == 180) Crystals.FirstOrDefault(n => n.ai[0] == 2).ai[0] = 0;
            if(npc.ai[3] >= 720)
            {
                ResetAttack();
            }
        }
        private void CrystalCage()
        {
            for (int k = 0; k < 4; k++) //each crystal
            {
                NPC crystal = Crystals[k];
                VitricBossCrystal crystalModNPC = crystal.modNPC as VitricBossCrystal;
                if (npc.ai[3] == 1) //set the crystal's home position to where they are
                {
                    crystalModNPC.StartPos = crystal.Center;
                    FavoriteCrystal = Main.rand.Next(4); //randomize which crystal will have the opening
                }

                if (npc.ai[3] > 1 && npc.ai[3] <= 60) //suck the crystals in
                {
                    crystal.Center = npc.Center + (Vector2.SmoothStep(crystalModNPC.StartPos, npc.Center, npc.ai[3] / 60) - npc.Center).RotatedBy(npc.ai[3] / 60f * 3.14f);
                }

                if (npc.ai[3] == 61)  //Set the crystal's new endpoints. !! actual endpoints are offset by pi !!
                {
                    crystalModNPC.StartPos = crystal.Center;
                    crystalModNPC.TargetPos = npc.Center + new Vector2(0, -800).RotatedBy(1.57f * k);
                    crystal.ai[2] = 2; //set them into this mode to get the rotational effect
                }

                if(npc.ai[3] >= 120 && npc.ai[3] < 360) //spiral outwards slowly
                {
                    crystal.Center = npc.Center + (Vector2.SmoothStep(crystalModNPC.StartPos, crystalModNPC.TargetPos, (npc.ai[3] - 120) / 240) - npc.Center).RotatedBy((npc.ai[3] - 120) / 240 * 3.14f);
                }

                if (npc.ai[3] >= 360 && npc.ai[3] < 840) //come back in
                {
                    crystal.Center = npc.Center + (Vector2.SmoothStep(crystalModNPC.TargetPos, crystalModNPC.StartPos, (npc.ai[3] - 360) / 480) - npc.Center).RotatedBy(-(npc.ai[3] - 360) / 480 * 4.72f);

                    //the chosen "favorite" or master crystal is the one where our opening should be
                    if (k != FavoriteCrystal) for (int i = 0; i < 8; i++)
                        {
                            Dust d = Dust.NewDustPerfect(npc.Center + (crystal.Center - npc.Center).RotatedBy(Main.rand.NextFloat(1.57f)), ModContent.DustType<Dusts.Electric>(), Vector2.Zero, 0, default, 2);
                        }
                }

                if (npc.ai[3] >= 840 && npc.ai[3] < 880) //reset to ready position
                {
                    crystal.Center = Vector2.SmoothStep(npc.Center, npc.Center + new Vector2(0, -120).RotatedBy(1.57f * k), (npc.ai[3] - 840) / 40);
                }

                if (npc.ai[3] == 880) //end of the attack
                {
                    crystal.ai[2] = 0; //reset our crystals
                    ResetAttack(); //all done!
                }
            }
            if (npc.ai[3] >= 360 && npc.ai[3] < 840) //the collision handler for this attack. out here so its not done 4 times
            {
                foreach(Player player in Main.player.Where(n => n.active))
                {
                    float dist = Vector2.Distance(player.Center, npc.Center); //distance the player is from the boss
                    float angleOff = (player.Center - npc.Center).ToRotation() % 6.28f; //where the player is versus the boss angularly. used to check if the player is in the opening
                    NPC crystal = Crystals[FavoriteCrystal]; 
                    float crystalDist = Vector2.Distance(crystal.Center, npc.Center); //distance from the boss to the ring
                    float crystalOff = (crystal.Center - npc.Center).ToRotation() % 6.28f; //crystal's rotation
                    float crystal2Off = ((crystal.Center - npc.Center).ToRotation() + 1.57f) % 6.28f; //second crystals rotation to calculate the opening

                    // if the player's distance from the boss is within 2 player widths of the ring and if the player isnt in the gab where they would be safe
                    if ((dist <= crystalDist + player.width && dist >= crystalDist - player.width) && !(angleOff >= crystalOff  && angleOff <= crystal2Off))
                    {
                        player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByNPC(npc.whoAmI), 65, 0); //do big damag
                        player.velocity += Vector2.Normalize(player.Center - npc.Center) * -5; //knock into boss
                        Main.PlaySound(Terraria.ID.SoundID.DD2_LightningAuraZap); //bzzt!
                    }
                }
            }
        }
        private void CrystalSmash()
        {
            //boss during the attack
            if (npc.ai[3] == 1) endPos = npc.Center; //set the ending point to the center of the arena so we can come back later

            //actual movement
            if (npc.ai[3] < 270)
            {
                npc.position.Y += (float)Math.Sin(npc.ai[3] / 90 * 6.28f) * 2;
                float vel = ((npc.ai[3] % 68) / 17 - (float)Math.Pow(npc.ai[3] % 68, 2) / 1156) * 9;
                npc.position.X += (npc.ai[3] < 68 || npc.ai[3] > 68*3) ? vel : -vel ;
            }


            if (npc.ai[3] == 270) { startPos = npc.Center; npc.velocity *= 0; } //where we start our return trip
            if (npc.ai[3] > 270) npc.Center = Vector2.SmoothStep(startPos, endPos, (npc.ai[3] - 270) / 90); //smoothstep back to the center


            //Crystals during the attack
            for(int k = 0; k < 4; k++)
            {
                NPC crystal = Crystals[k];
                VitricBossCrystal crystalModNPC = crystal.modNPC as VitricBossCrystal;
                if (npc.ai[3] == 60 + k * 60) //set motion points correctly
                {
                    RandomizeTarget(); //pick a random target to smash a crystal down

                    Player player = Main.player[npc.target]; 
                    crystal.ai[2] = 0; //set the crystal into normal mode
                    crystalModNPC.StartPos = crystal.Center;
                    crystalModNPC.TargetPos = new Vector2(player.Center.X + player.velocity.X * 50, player.Center.Y - 250); //endpoint is above the player
                }
                if(npc.ai[3] >= 60 + k * 60 && npc.ai[3] <= 60 + (k + 1) * 60) //move the crystal there
                {
                    crystal.Center = Vector2.SmoothStep(crystalModNPC.StartPos, crystalModNPC.TargetPos, (npc.ai[3] - (60 + k * 60)) / 60);
                }
                if(npc.ai[3] == 60 + (k + 1) * 60) //set the crystal into falling mode after moving
                {
                    Player player = Main.player[npc.target];
                    crystal.ai[2] = 3;
                    crystalModNPC.TargetPos = player.Center;
                }
            }

            //ending the attack
            if (npc.ai[3] > 360) ResetAttack();
        }
        private void RandomSpikes()
        {
            List<Vector2> points = new List<Vector2>();
            CrystalLocations.ForEach(n => points.Add(n + new Vector2(Main.rand.NextFloat(-40, 40), -20)));
            points.OrderBy(n => Main.rand.Next(50));
            for(int k = 0; k < 1 + Crystals.Count(n => n.ai[0] == 3); k++)
            {
                Projectile.NewProjectile(points[k], Vector2.Zero, ModContent.ProjectileType<BossSpike>(), 25, 0);
            }
            ResetAttack();
        }
        private void PlatformDash()
        {
            if(npc.ai[3] == 1) CrystalLocations.OrderBy(n => n.Y); //orders the points the boss should go to by height off the ground
            for (int k = 0; k < CrystalLocations.Count; k++)
            {
                if (npc.ai[3] >= 120 + k * 120 && npc.ai[3] < 120 + (k + 1) * 120) //move between each platform
                {
                    int timer = (int)npc.ai[3] - (120 + k * 120); //0 to 240, grabs the relative timer for ease of writing code
                    if (timer == 0) { startPos = npc.Center; endPos = CrystalLocations[k] + new Vector2(0, -30); RandomizeTarget(); } //set positions and randomize the target
                    if (timer < 60)
                    {
                        npc.Center = Vector2.SmoothStep(startPos, endPos, timer / 60f); //move our big sandy boi into the position of a platform
                    }

                    if (k % 2 == 0) //pick one of these 2 projectile-based attacks, alternating every other platform
                    {
                        if (timer >= 80 && timer % 10 == 0) //burst of 4 spikes
                        {
                            Main.PlaySound(Terraria.ID.SoundID.DD2_WitherBeastCrystalImpact);
                            Projectile.NewProjectile(npc.Center, Vector2.Normalize(npc.Center - Main.player[npc.target].Center) * -8, ModContent.ProjectileType<Projectiles.GlassSpike>(), 15, 0);
                        }
                    }
                    else
                    {
                        if(timer == 60) //sand cone
                        {
                            int index = Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<SandCone>(), 1, 0);
                            Main.projectile[index].rotation = (npc.Center - Main.player[npc.target].Center).ToRotation(); //sand cones always need their rotation set on spawn
                        }
                    }
                }
            }
            if (npc.ai[3] == 120 + 120 * 6) startPos = npc.Center; //set where we are to the start
            if (npc.ai[3] > 120 + 120 * 6) //going home
            {
                int timer = (int)npc.ai[3] - (120 + 6 * 120);
                npc.Center = Vector2.SmoothStep(startPos, homePos, timer / 120f);
                if (timer == 121) ResetAttack(); //reset attack
            }
            
        }

        private void AngerAttack()
        {
            if(Crystals.Count(n => n.ai[0] == 2) == 0)
            {
                npc.ai[1] = (int)AIStates.FirstToSecond; //this is where we phase the boss
                npc.ai[0] = 0;
            }
            for (int i = 0; i < Crystals.Count(n => n.ai[0] == 1 || n.ai[0] == 3); i++)
            {
                if (npc.ai[3] == 30 + i * 45)
                {
                    for (float k = 0; k < 6.28f; k += 6.28f / 12) //ring of glass spikes
                    {
                        Projectile.NewProjectile(npc.Center, Vector2.One.RotatedBy(k + (i % 2 == 0 ? 6.28f / 24 : 0)) * 3.5f, ModContent.ProjectileType<Projectiles.GlassSpike>(), 15, 0.2f);
                    }
                }
            }
            if (npc.ai[3] >= 240)
            {
                Crystals.FirstOrDefault(n => n.ai[0] == 1).ai[0] = 3;
                npc.ai[1] = (int)AIStates.FirstPhase; //go back to normal attacks after this is all over
                npc.immortal = false;
                ResetAttack();
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Core;

namespace StarlightRiver.NPCs.Boss.SquidBoss
{
    public partial class SquidBoss : ModNPC
    {
        public List<NPC> Tentacles = new List<NPC>(); //the tentacle NPCs which this boss controls
        public List<NPC> Platforms = new List<NPC>(); //the big platforms the boss' arena has
        Vector2 Spawn;
        Vector2 SavedPoint;

        internal ref float Phase => ref npc.ai[0];
        internal ref float GlobalTimer => ref npc.ai[1];
        internal ref float AttackPhase => ref npc.ai[2];
        internal ref float AttackTimer => ref npc.ai[3];

        #region TML hooks
        public override void SetStaticDefaults() => DisplayName.SetDefault("Auroracle");

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale) => npc.lifeMax = (int)(6500 * bossLifeScale);

        public override bool CheckActive() => false;

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor) => false;

        public override void SetDefaults()
        {
            npc.lifeMax = 3500;
            npc.width = 80;
            npc.height = 80;
            npc.boss = true;
            npc.damage = 1;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.npcSlots = 15f;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SquidBoss");
            npc.noTileCollide = true;
            npc.knockBackResist = 0;
            npc.dontTakeDamage = true;
        }

        public void DrawUnderWater(SpriteBatch spriteBatch)
        {
            for (int k = 3; k > 0; k--)
            {
                Texture2D tex2 = ModContent.GetTexture("IceKracken/Boss/BodyRing");
                Vector2 pos = npc.Center + new Vector2(0, 70 + k * 35).RotatedBy(npc.rotation) - Main.screenPosition;
                int squish = k * 10 + (int)(Math.Sin(npc.ai[1] / 10f - k / 4f * 6.28f) * 20);
                Rectangle rect = new Rectangle((int)pos.X, (int)pos.Y, tex2.Width + (3 - k) * 20 - squish, tex2.Height + (int)(squish * 0.4f) + (3 - k) * 5);

                float sin = 1 + (float)Math.Sin(npc.ai[1] / 10f - k);
                float cos = 1 + (float)Math.Cos(npc.ai[1] / 10f + k);
                Color color = new Color(0.5f + cos * 0.2f, 0.8f, 0.5f + sin * 0.2f) * 0.7f;
                if (npc.ai[0] == (int)AIStates.ThirdPhase) color = new Color(0.8f + sin * 0.1f, 0.3f + sin * -0.25f, 0.05f) * 0.7f;

                spriteBatch.Draw(tex2, rect, tex2.Frame(), color, npc.rotation, tex2.Size() / 2, 0, 0);
            }

            Texture2D tex = ModContent.GetTexture("IceKracken/Boss/BodyUnder");
            spriteBatch.Draw(tex, npc.Center - Main.screenPosition, tex.Frame(), Color.White, npc.rotation, tex.Size() / 2, 1, 0, 0);
            if(npc.ai[0] >= (int)AIStates.SecondPhase)
            {
                Texture2D tex2 = ModContent.GetTexture(Texture);
                spriteBatch.Draw(tex2, npc.Center - Main.screenPosition, tex2.Frame(), Color.White, npc.rotation, tex2.Size() / 2, 1, 0, 0);
            }
        }     
        #endregion

        #region AI
        public enum AIStates
        {
            SpawnEffects = 0,
            SpawnAnimation = 1,
            FirstPhase = 2,
            FirstPhaseTwo = 3,
            SecondPhase = 4,
            ThirdPhase = 5
        }

        public override void AI()
        {
            GlobalTimer++;

            if (Phase == (int)AIStates.SpawnEffects)
            {
                Phase = (int)AIStates.SpawnAnimation;

                npc.damage = 0;
                foreach (NPC npc in Main.npc.Where(n => n.active && n.modNPC is IcePlatform)) Platforms.Add(npc);

                Spawn = npc.Center;

                StarlightRiver.Instance.textcard.Display("Auroracle", "Aurora Calamari", null, 600);
                Main.LocalPlayer.GetModPlayer<StarlightPlayer>().ScreenMoveTarget = npc.Center;
                Main.LocalPlayer.GetModPlayer<StarlightPlayer>().ScreenMovePan = npc.Center + new Vector2(0, -600);
                Main.LocalPlayer.GetModPlayer<StarlightPlayer>().ScreenMoveTime = 600;
            }

            if (Phase == (int)AIStates.SpawnAnimation)
            {
                if (GlobalTimer < 200) npc.Center = Vector2.SmoothStep(Spawn, Spawn + new Vector2(0, -600), GlobalTimer / 200f); //rise up from the ground

                for (int k = 0; k < 4; k++) //each tenticle
                {
                    if (GlobalTimer == 200 + k * 50)
                    {
                        int x;
                        int y;
                        int xb;

                        switch (k) //I handle these manually to get them to line up with the window correctly
                        {
                            case 0: x = -370; y = 0; xb = -50; break;
                            case 1: x = -420; y = -100; xb = -20; break;
                            case 3: x = 370; y = 0; xb = 50; break;
                            case 2: x = 420; y = -100; xb = 20; break;
                            default: x = 0; y = 0; xb = 0; break;
                        }

                        int i = NPC.NewNPC((int)npc.Center.X + x, (int)npc.Center.Y + 550, ModContent.NPCType<Tentacle>(), 0, k == 1 || k == 2 ? 1 : 0); //middle 2 tentacles should be vulnerable
                        (Main.npc[i].modNPC as Tentacle).Parent = this;
                        (Main.npc[i].modNPC as Tentacle).MovePoint = new Vector2((int)npc.Center.X + x, (int)npc.Center.Y - y);
                        (Main.npc[i].modNPC as Tentacle).OffBody = xb;
                        Tentacles.Add(Main.npc[i]);
                    }
                }

                if (GlobalTimer > 600) //tentacles returning back underwater
                {
                    foreach (NPC tentacle in Tentacles)
                    {
                        Tentacle mt = tentacle.modNPC as Tentacle;
                        tentacle.Center = Vector2.SmoothStep(mt.MovePoint, mt.SavedPoint, (GlobalTimer - 600) / 100f);
                    }
                }

                if (GlobalTimer > 700) Phase = (int)AIStates.FirstPhase;
            }

            if (Phase == (int)AIStates.FirstPhase) //first phase, part 1. Tentacle attacks and ink.
            {
                AttackTimer++;

                if (AttackTimer == 1)
                {
                    if (Tentacles.Count(n => n.ai[0] == 2) == 2) //phasing logic
                    {
                        Phase = (int)AIStates.FirstPhaseTwo;
                        GlobalTimer = 0;
                        return;
                    }
                    else //else advance the attack pattern
                    {
                        AttackPhase++;
                        if (AttackPhase > 3) AttackPhase = 1;
                    }
                }

                switch (AttackPhase)
                {
                    case 1: TentacleSpike(); break;                      
                    case 2: InkBurst(); break;
                    case 3: PlatformSweep(); break;
                }
            }

            if (Phase == (int)AIStates.FirstPhaseTwo) //first phase, part 2. Tentacle attacks and ink. Raise water first.
            {
                if (GlobalTimer < 325) //water rising up
                {
                    Main.npc.FirstOrDefault(n => n.active && n.modNPC is ArenaActor).ai[0]++;
                    npc.Center = Vector2.SmoothStep(Spawn + new Vector2(0, -600), Spawn + new Vector2(0, -750), GlobalTimer / 325f);
                    if (GlobalTimer % 10 == 0) Main.PlaySound(SoundID.Splash, npc.Center);
                }

                if(GlobalTimer == 325) //make the remaining tentacles vulnerable
                {
                    foreach (NPC tentacle in Tentacles.Where(n => n.ai[0] == 1)) tentacle.ai[0] = 0; 
                }

                if(GlobalTimer > 325) //continue attacking otherwise
                {
                    AttackTimer++;

                    if (AttackTimer == 1)
                    {
                        if (Tentacles.Count(n => n.ai[0] == 2) == 4) //phasing logic
                        {
                            Phase = (int)AIStates.SecondPhase;
                            GlobalTimer = 0;
                            return;
                        }
                        else //else advance the attack pattern
                        {
                            AttackPhase++;
                            if (AttackPhase > 3) AttackPhase = 1;
                        }
                    }

                    switch (AttackPhase)
                    {
                        case 1: TentacleSpike(); break;
                        case 2: InkBurst(); break;
                        case 3: PlatformSweep(); break;
                    }
                }
            }

            if (Phase == (int)AIStates.SecondPhase) //second phase
            {
                if (GlobalTimer < 300) //water rising
                {
                    Main.npc.FirstOrDefault(n => n.active && n.modNPC is ArenaActor).ai[0]++;
                    if (GlobalTimer % 10 == 0) Main.PlaySound(SoundID.Splash, npc.Center);
                }

                if(GlobalTimer == 300) //reset
                {
                    npc.dontTakeDamage = false;
                    ResetAttack();
                    AttackPhase = 0;
                }

                if(GlobalTimer > 300)
                {
                    if (npc.life < npc.lifeMax / 7) npc.dontTakeDamage = true; //health gate

                    AttackTimer++;

                    if (AttackPhase != 2 && AttackPhase != 4) //when not lasering, passive movement
                    {
                        npc.velocity += Vector2.Normalize(npc.Center - (Main.player[npc.target].Center + new Vector2(0, 250))) * -0.2f;
                        if (npc.velocity.Length() > 5) npc.velocity = Vector2.Normalize(npc.velocity) * 5;
                        npc.rotation = npc.velocity.X * 0.05f;
                    }

                    if (AttackTimer == 1)
                    {
                        if (npc.life < npc.lifeMax / 7) //phasing logic
                        {
                            Phase = (int)AIStates.ThirdPhase;
                            GlobalTimer = 0;
                            AttackPhase = 0;
                            ResetAttack();

                            Platforms.RemoveAll(n => Math.Abs(n.Center.X - Main.npc.FirstOrDefault(l => l.active && l.modNPC is ArenaActor).Center.X) >= 550);
                            return;
                        }

                        AttackPhase++;
                        if (AttackPhase > 4) AttackPhase = 1;
                    }

                    switch (AttackPhase)
                    {
                        case 1: Spew(); break;
                        case 2: Laser(); break;
                        case 3: Spew(); break;
                        case 4: Leap(); break;
                    }
                }
            }    
            if(Phase == (int)AIStates.ThirdPhase)
            {
                if(GlobalTimer == 1) //reset velocity + set movement points
                {
                    npc.velocity *= 0;
                    npc.rotation = 0;
                    SavedPoint = npc.Center;
                }

                if(GlobalTimer < 240) npc.Center = Vector2.SmoothStep(SavedPoint, Spawn + new Vector2(0, -1400), GlobalTimer / 240f); //move to the top of the arena

                if(GlobalTimer == 240) //roar and activate
                {
                    npc.dontTakeDamage = false;
                    foreach(Player player in Main.player.Where(n => n.active)) player.GetModPlayer<StarlightPlayer>().Shake += 40;
                    Main.PlaySound(SoundID.Roar, npc.Center, 0);
                }

                if (GlobalTimer > 240) //following unless using ink attack
                {
                    if (AttackPhase != 3)
                    {
                        npc.velocity += Vector2.Normalize(npc.Center - (Main.player[npc.target].Center + new Vector2(0, -350))) * -0.3f;
                        if (npc.velocity.Length() > 7) npc.velocity = Vector2.Normalize(npc.velocity) * 7;
                        npc.rotation = npc.velocity.X * 0.05f;
                    }

                    GlobalTimer++;

                    if (GlobalTimer % 8 == 0) Main.npc.FirstOrDefault(n => n.active && n.modNPC is ArenaActor).ai[0]++; //rising water

                    AttackTimer++;

                    if (AttackTimer == 1)
                    {
                        AttackPhase++;
                        if (AttackPhase > 3) AttackPhase = 1;
                    }

                    switch (AttackPhase)
                    {
                        case 1: TentacleSpike2(); break;
                        case 2: StealPlatform(); break;
                        case 3: InkBurst2(); break;
                    }
                }
            }
        }
        #endregion
    }
}

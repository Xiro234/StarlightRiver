﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    sealed partial class VitricBoss : ModNPC
    {
        #region tml hooks
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[PH] Vitric Boss");
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 3500;
            npc.damage = 30;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 140;
            npc.height = 140;
            npc.value = Item.buyPrice(0, 20, 0, 0);
            npc.npcSlots = 15f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GlassBoss");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
        }

        public override bool CheckDead()
        {
            LegendWorld.GlassBossDowned = true;

            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.frame.Width = npc.width;
            npc.frame.Height = npc.height;
            spriteBatch.Draw(ModContent.GetTexture(Texture), npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, 0, 0);
            return false;
        }
        #endregion

        #region helper methods
        //Used for the various differing passive animations of the different forms
        private void SetFrameX(int frame)
        {
            npc.frame.X = npc.width * frame;
        }

        //Easily animate a phase with custom framerate and frame quantity
        private void Animate(int ticksPerFrame, int maxFrames)
        {
            if (npc.frameCounter++ >= ticksPerFrame) { npc.frame.Y += npc.height; npc.frameCounter = 0; }
            if ((npc.frame.Y / npc.height) > maxFrames - 1) npc.frame.Y = 0;
        }

        //resets animation and changes phase
        private void ChangePhase(AIStates phase, bool resetTime = false)
        {
            npc.frame.Y = 0;
            npc.ai[1] = (int)phase;
            if (resetTime) npc.ai[0] = 0;
        }
        #endregion

        #region AI

        List<NPC> Crystals = new List<NPC>();
        List<Vector2> CrystalLocations = new List<Vector2>();
        enum AIStates
        {
            SpawnEffects = 0,
            SpawnAnimation = 1,
            FirstPhase = 2,
            FirstToSecond = 3,
            SecondPhase = 4
        }

        public override void AI()
        {
            /*
             * AI slots:
             * 0: Timer
             * 1: Phase
             * 2: Attack state
             * 3: Attack timer
             */

            //Ticks the timer
            npc.ai[0]++;
            npc.ai[3]++;

            switch (npc.ai[1])
            {
                //on spawn effects
                case (int)AIStates.SpawnEffects:
                    StarlightPlayer mp = Main.LocalPlayer.GetModPlayer<StarlightPlayer>();
                    mp.ScreenMoveTarget = npc.Center;
                    mp.ScreenMoveTime = 360;
                    StarlightRiver.Instance.abilitytext.Display(npc.FullName, Main.rand.Next(10000) == 0 ? "Glass tax returns" : "Shattered Sentinel", null, 210); //Screen pan + intro text

                    for(int k = 0; k < Main.maxNPCs; k++)
                    {
                        NPC npc = Main.npc[k];
                        if (npc != null && npc.active && (npc.type == ModContent.NPCType<VitricBossPlatformUp>() || npc.type == ModContent.NPCType<VitricBossPlatformDown>())) CrystalLocations.Add(npc.Center + new Vector2(0, -48));
                    }
                    Main.NewText(CrystalLocations.Count);
                    for(int k = 0; k < 4; k++)
                    {
                        Vector2 target = npc.Center + Vector2.One.RotatedBy(k) * 64;
                        int index = NPC.NewNPC((int)target.X, (int)target.Y, ModContent.NPCType<VitricBossCrystal>(), 0, 2); //spawn in state 2: sandstone forme
                        (Main.npc[index].modNPC as VitricBossCrystal).Parent = this;
                        Crystals.Add(Main.npc[index]);
                    }

                    ChangePhase(AIStates.SpawnAnimation, true);
                    break;

                case (int)AIStates.SpawnAnimation:
                    if (npc.ai[0] > 360) ChangePhase(AIStates.FirstPhase, true);
                    break;

                case (int)AIStates.FirstPhase:
                    switch (npc.ai[2]) //switch for crystal behavior
                    {
                        case 0: NukePlatforms(); break;
                    }
                    break;

            }
        }
        #endregion
    }
}

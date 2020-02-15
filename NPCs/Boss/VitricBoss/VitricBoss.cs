using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    sealed partial class VitricBoss : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glass Tax Returns");
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
            if (!LegendWorld.AnyBossDowned) LegendWorld.ForceStarfall = true;
            LegendWorld.GlassBossDowned = true;
            LegendWorld.AnyBossDowned = true;

            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.frame.Width = npc.width;
            npc.frame.Height = npc.height;
            spriteBatch.Draw(ModContent.GetTexture(Texture), npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, 0, 0 );
            return false;
        }
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
        private void ChangePhase(int phase, bool resetTime = false)
        {
            npc.frame.Y = 0;
            npc.ai[1] = phase;
            if (resetTime) npc.ai[0] = 0;
        }

        public override void AI()
        {
            /*
            AI slots:
            0: Timer
            1: Phase          
            */

            //Ticks the timer
            npc.ai[0]++;

            switch (npc.ai[1])
            {
                //on spawn effects
                case 0:
                    int count = 2 + Main.player.Count(p => Vector2.Distance(p.Center, npc.Center) <= 1000) * 2; //counts players in the fight
                    if (count > 6) count = 6; //caps at 6

                    for (int k = 0; k < count; k++) //spawns an appropriate amount of crystals for the players
                    {
                        int index = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<VitricBossCrystal>(), 0, 0, 0, 0, k);
                        Main.npc[index].velocity = new Vector2(-1, 0).RotatedBy((k / ((float)count - 1)) * 3.14f) * 2;
                    }
                    ChangePhase(1);
                break;

                //First phase
                case 1:

                    //Attacks
                    if(npc.ai[0] >= 120)
                    {
                        Main.NewText("foo!");
                        npc.ai[0] = 0;
                    }

                    //vulnerability check
                    bool vulnerable = !Main.npc.Any(n => n.active && n.type == ModContent.NPCType<VitricBossCrystal>());

                    //Dash + vulnerability detection
                    if (Main.player.Any(p => AbilityHelper.CheckDash(p, npc.Hitbox)) && vulnerable) ChangePhase(2);

                    //Animation
                    if (vulnerable)
                    {
                        SetFrameX(1);
                        Animate(5, 3);
                    }
                    else Animate(5, 3);

                break;

                //First => Second phase transition
                case 2:
                    Main.PlaySound(SoundID.Shatter);

                    for (int k = 0; k <= 100; k++) //reforming glass shards
                    {
                        Dust dus = Dust.NewDustPerfect(npc.Center + Vector2.One.RotatedBy(k) * 50,
                            ModContent.DustType<Dusts.Glass>(), Vector2.One.RotatedBy(k + Main.rand.NextFloat(-2, 2)) * Main.rand.Next(10, 20), 0, default, k / 40f);
                        dus.customData = npc.Center;
                    }
                    npc.scale = 0;
                    SetFrameX(2);
                    ChangePhase(3, true);

                    music = mod.GetSoundSlot(SoundType.Music, "VortexIsACunt");

                    break;

                //Second phase
                case 3:
                    music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/WhipAndNaenae");
                    //Visuals
                    float rot = Main.rand.NextFloat(6.28f);
                    Dust.NewDustPerfect(npc.Center + Vector2.One.RotatedBy(rot) * 80, ModContent.DustType<Dusts.Glass3>(), Vector2.One.RotatedBy(rot + 1.58f));

                    //Formation scaling
                    if (npc.ai[0] >= 20 && npc.ai[0] <= 60)
                    {
                        npc.scale = (npc.ai[0] - 20) / 40f;
                    }

                    //Animation
                    Animate(10, 3);

                break;

            }
        }
    }  
}

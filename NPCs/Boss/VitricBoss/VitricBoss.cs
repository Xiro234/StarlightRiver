using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    internal sealed partial class VitricBoss : ModNPC, IDynamicMapIcon
    {
        #region tml hooks

        public override bool CheckActive()
        {
            return npc.ai[1] == (int)AIStates.Leaving;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ceiros");
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 5000;
            npc.damage = 30;
            npc.defense = 18;
            npc.knockBackResist = 0f;
            npc.width = 256;
            npc.height = 256;
            npc.value = Item.buyPrice(0, 20, 0, 0);
            npc.npcSlots = 15f;
            npc.dontTakeDamage = true;
            npc.friendly = false;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.dontTakeDamageFromHostiles = true;
            npc.scale = 0.5f;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GlassBoss1");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(7500 * bossLifeScale);
            npc.damage = 40;
            npc.defense = 21;
        }

        public override bool CheckDead()
        {
            if (Vector2.Distance(Main.LocalPlayer.Center, npc.Center) < 1500) Helper.UnlockEntry<Codex.Entries.CeirosEntry>(Main.LocalPlayer); //unlocks the entry if the local player is close enough. codex is clientside so this is fine.
            foreach (NPC npc in Main.npc.Where(n => n.modNPC is VitricBackdropLeft || n.modNPC is VitricBossPlatformUp)) npc.active = false; //reset arena
            LegendWorld.GlassBossDowned = true;
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.frame.Width = 128;
            npc.frame.Height = 128;
            spriteBatch.Draw(ModContent.GetTexture(Texture), npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, 0, 0);

            //debug drawing

            Utils.DrawBorderString(spriteBatch, "AI: " + npc.ai[0] + " / " + npc.ai[1] + " / " + npc.ai[2] + " / " + npc.ai[3], new Vector2(40, Main.screenHeight - 100), Color.Red);
            Utils.DrawBorderString(spriteBatch, "Vel: " + npc.velocity, new Vector2(40, Main.screenHeight - 120), Color.Red);
            Utils.DrawBorderString(spriteBatch, "Pos: " + npc.position, new Vector2(40, Main.screenHeight - 140), Color.Red);
            Utils.DrawBorderString(spriteBatch, "Next Health Gate: " + (npc.lifeMax - (1 + Crystals.Count(n => n.ai[0] == 3)) * 500), new Vector2(40, Main.screenHeight - 160), Color.Red);
            for (int k = 0; k < 4; k++)
            {
                if (Crystals.Count == 4) Utils.DrawBorderString(spriteBatch, "Crystal " + k + " Distance: " + Vector2.Distance(Crystals[k].Center, npc.Center) + " State: " + Crystals[k].ai[2], new Vector2(40, Main.screenHeight - 180 - k * 20), Color.Yellow);
            }

            return false;
        }

        private readonly List<VitricBossEye> Eyes = new List<VitricBossEye>()
        {
            new VitricBossEye(new Vector2(24, 32), 0),
            new VitricBossEye(new Vector2(58, 28), 1),
            new VitricBossEye(new Vector2(36, 52), 2),
            new VitricBossEye(new Vector2(20, 70), 3),
            new VitricBossEye(new Vector2(12, 78), 4),
            new VitricBossEye(new Vector2(38, 96), 5),
            new VitricBossEye(new Vector2(66, 102), 6),
            new VitricBossEye(new Vector2(80, 80), 7),
            new VitricBossEye(new Vector2(106, 66), 8),
            new VitricBossEye(new Vector2(64, 60), 9)
        };

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (Eyes.Any(n => n.Parent == null)) Eyes.ForEach(n => n.Parent = this);
            if (npc.frame.X == 0) Eyes.ForEach(n => n.Draw(spriteBatch));

            if (npc.ai[1] == (int)AIStates.FirstPhase && npc.dontTakeDamage) //draws the npc's shield when immune and in the first phase
            {
                Texture2D tex = ModContent.GetTexture("StarlightRiver/NPCs/Boss/VitricBoss/Shield");
                spriteBatch.Draw(tex, npc.Center - Main.screenPosition, tex.Frame(), Color.White * (0.55f + ((float)Math.Sin(LegendWorld.rottime * 2) * 0.15f)), 0, tex.Size() / 2, 1, 0, 0);
            }
        }

        #endregion tml hooks

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

        #endregion helper methods

        #region AI

        public Vector2 startPos;
        public Vector2 endPos;
        public Vector2 homePos;
        public List<NPC> Crystals = new List<NPC>();
        public List<Vector2> CrystalLocations = new List<Vector2>();

        public enum AIStates
        {
            SpawnEffects = 0,
            SpawnAnimation = 1,
            FirstPhase = 2,
            Anger = 3,
            FirstToSecond = 4,
            SecondPhase = 5,
            Leaving = 6
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

            if (npc.ai[1] != (int)AIStates.Leaving && !Main.player.Any(n => n.active && n.statLife > 0 && Vector2.Distance(n.Center, npc.Center) <= 1500)) //if no valid players are detected
            {
                npc.ai[0] = 0;
                npc.ai[1] = (int)AIStates.Leaving; //begone thot!
                Crystals.ForEach(n => n.ai[2] = 4);
                Crystals.ForEach(n => n.ai[1] = 0);
            }
            switch (npc.ai[1])
            {
                //on spawn effects
                case (int)AIStates.SpawnEffects:
                    StarlightPlayer mp = Main.LocalPlayer.GetModPlayer<StarlightPlayer>();
                    mp.ScreenMoveTarget = npc.Center + new Vector2(0, -850);
                    mp.ScreenMoveTime = 600;
                    StarlightRiver.Instance.abilitytext.Display(npc.FullName, Main.rand.Next(10000) == 0 ? "Glass tax returns" : "Shattered Sentinel", null, 500); //Screen pan + intro text

                    for (int k = 0; k < Main.maxNPCs; k++) //finds all the large platforms to add them to the list of possible locations for the nuke attack
                    {
                        NPC npc = Main.npc[k];
                        if (npc?.active == true && (npc.type == ModContent.NPCType<VitricBossPlatformUp>() || npc.type == ModContent.NPCType<VitricBossPlatformDown>())) CrystalLocations.Add(npc.Center + new Vector2(0, -48));
                    }

                    ChangePhase(AIStates.SpawnAnimation, true);
                    break;

                case (int)AIStates.SpawnAnimation: //the animation that plays while the boss is spawning and the title card is shown

                    if (npc.ai[0] == 2)
                    {
                        npc.friendly = true; //so he wont kill you during the animation
                        RandomizeTarget(); //pick a random target so the eyes will follow them
                    }
                    if (npc.ai[0] <= 200) //rise up
                    {
                        npc.Center += new Vector2(0, -4f);
                    }
                    if (npc.ai[0] > 200 && npc.ai[0] <= 300) //grow
                    {
                        npc.scale = 0.5f + (npc.ai[0] - 200) / 200f;
                    }
                    if (npc.ai[0] > 280) //summon crystal babies
                    {
                        for (int k = 0; k <= 4; k++)
                        {
                            if (npc.ai[0] == 280 + k * 30)
                            {
                                Vector2 target = new Vector2(npc.Center.X + (-100 + k * 50), LegendWorld.VitricBiome.Top * 16 + 1100);
                                int index = NPC.NewNPC((int)target.X, (int)target.Y, ModContent.NPCType<VitricBossCrystal>(), 0, 2); //spawn in state 2: sandstone forme
                                (Main.npc[index].modNPC as VitricBossCrystal).Parent = this;
                                (Main.npc[index].modNPC as VitricBossCrystal).StartPos = target;
                                (Main.npc[index].modNPC as VitricBossCrystal).TargetPos = npc.Center + new Vector2(0, -120).RotatedBy(6.28f / 4 * k);
                                Crystals.Add(Main.npc[index]); //add this crystal to the list of crystals the boss controls
                            }
                        }
                    }
                    if (npc.ai[0] > 460) //start the fight
                    {
                        npc.dontTakeDamage = false; //make him vulnerable
                        npc.friendly = false; //and hurt when touched
                        homePos = npc.Center; //set the NPCs home so it can return here after attacks
                        int index = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<ArenaBottom>());
                        (Main.npc[index].modNPC as ArenaBottom).Parent = this;
                        ChangePhase(AIStates.FirstPhase, true);
                    }
                    break;

                case (int)AIStates.FirstPhase:
                    int healthGateAmount = npc.lifeMax / 7;
                    if (npc.life <= npc.lifeMax - (1 + Crystals.Count(n => n.ai[0] == 3 || n.ai[0] == 1)) * healthGateAmount && !npc.dontTakeDamage)
                    {
                        npc.dontTakeDamage = true; //boss is immune at phase gate
                        npc.life = npc.lifeMax - ((1 + Crystals.Count(n => n.ai[0] == 3 || n.ai[0] == 1)) * healthGateAmount) - 1; //set health at phase gate
                        Main.PlaySound(SoundID.ForceRoar, npc.Center);
                    }

                    if (npc.ai[3] == 1) //switching out attacks
                    {
                        if (npc.dontTakeDamage) npc.ai[2] = 0; //nuke attack once the boss turns immortal for a chance to break a crystal
                        else //otherwise proceed with attacking pattern
                        {
                            npc.ai[2]++;
                            if (npc.ai[2] > 4) npc.ai[2] = 1;
                        }
                    }
                    switch (npc.ai[2]) //switch for crystal behavior
                    {
                        case 0: NukePlatforms(); break;
                        case 1: CrystalCage(); break;
                        case 2: CrystalSmash(); break;
                        case 3: RandomSpikes(); break;
                        case 4: PlatformDash(); break;
                    }
                    //TODO: rework this. It needs to be better.
                    /*if(npc.ai[2] != 1 && npc.ai[0] % 90 == 0 && Main.rand.Next(2) == 0) //summon crystal spikes when not using the cage attack, every 90 seconds half the time on a player thats standing on the ground
                    {
                        List<int> players = new List<int>();
                        foreach (Player player in Main.player.Where(n => n.active && n.statLife > 0 && n.velocity.Y == 0 && Vector2.Distance(n.Center, npc.Center) <= 1000))
                        {
                            players.Add(player.whoAmI);
                        }
                        if(players.Count != 0)
                        {
                            Player player = Main.player[players[Main.rand.Next(players.Count)]];
                            Projectile.NewProjectile(player.Center + new Vector2(-24, player.height / 2 - 64), Vector2.Zero, ModContent.ProjectileType<BossSpike>(), 10, 0);
                        }
                    }*/
                    break;

                case (int)AIStates.Anger: //the short anger phase attack when the boss loses a crystal
                    AngerAttack();
                    break;

                case (int)AIStates.FirstToSecond:
                    if (npc.ai[0] == 2)
                    {
                        foreach (NPC crystal in Crystals)
                        {
                            crystal.ai[0] = 0;
                            crystal.ai[2] = 5; //turn the crystals to transform mode
                        }
                    }
                    if (npc.ai[0] == 120)
                    {
                        SetFrameX(1);
                        foreach (NPC crystal in Crystals) //kill all the crystals
                        {
                            crystal.Kill();
                        }
                        npc.friendly = true; //so we wont get contact damage
                    }
                    if (npc.ai[0] > 120)
                    {
                        foreach (Player player in Main.player)
                        {
                            if (Abilities.AbilityHelper.CheckDash(player, npc.Hitbox)) //boss should be dashable now, when dashed:
                            {
                                SetFrameX(2);
                                ChangePhase(AIStates.SecondPhase, true); //go on to the next phase
                                ResetAttack(); //reset attack
                                foreach (NPC wall in Main.npc.Where(n => n.modNPC is VitricBackdropLeft)) wall.ai[1] = 3; //make the walls scroll
                                foreach (NPC plat in Main.npc.Where(n => n.modNPC is VitricBossPlatformUp)) plat.ai[0] = 1; //make the platforms scroll

                                break;
                            }
                        }
                    }
                    if (npc.ai[0] > 900) //after waiting too long, wipe all players
                    {
                        foreach (Player player in Main.player.Where(n => Vector2.Distance(n.Center, npc.Center) < 1000))
                        {
                            player.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason(player.name + " was shattered..."), double.MaxValue, 0);
                        }
                        ChangePhase(AIStates.Leaving, true);
                    }
                    break;

                case (int)AIStates.SecondPhase:
                    npc.dontTakeDamage = false; //damagable again
                    npc.friendly = false;
                    if (npc.ai[0] == 1) music = mod.GetSoundSlot(SoundType.Music, "VortexHasASmallPussy"); //handles the music transition
                    if (npc.ai[0] == 2) music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GlassBossTransition");

                    if (npc.ai[0] == 701) music = mod.GetSoundSlot(SoundType.Music, "VortexHasASmallPussy");
                    if (npc.ai[0] == 702)
                    {
                        music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GlassBoss2");
                    }

                    if (npc.ai[0] > 702 && npc.ai[0] < 760) //no fadein
                    {
                        for (int k = 0; k < Main.musicFade.Length; k++)
                        {
                            if (k == Main.curMusic)
                            {
                                Main.musicFade[k] = 1;
                            }
                        }
                    }

                    Volley();
                    break;

                case (int)AIStates.Leaving:
                    npc.position.Y += 3;
                    if (npc.ai[0] >= 180)
                    {
                        npc.active = false; //leave
                        foreach (NPC npc in Main.npc.Where(n => n.modNPC is VitricBackdropLeft || n.modNPC is VitricBossPlatformUp)) npc.active = false; //arena reset
                    }
                    break;
            }
        }

        #endregion AI

        #region Networking

        private int FavoriteCrystal = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(FavoriteCrystal);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            FavoriteCrystal = reader.ReadInt32();
        }

        #endregion Networking

        private int IconFrame = 0;
        private int IconFrameCounter = 0;

        public void DrawOnMap(SpriteBatch spriteBatch, Vector2 center, float scale, Color color)
        {
            if (IconFrameCounter++ >= 5) { IconFrame++; IconFrameCounter = 0; }
            if (IconFrame > 3) IconFrame = 0;
            Texture2D tex = ModContent.GetTexture("StarlightRiver/NPCs/Boss/VitricBoss/VitricBoss_Head_Boss");
            spriteBatch.Draw(tex, center, new Rectangle(0, IconFrame * 30, 30, 30), color, npc.rotation, Vector2.One * 15, scale, 0, 0);
        }
    }
}
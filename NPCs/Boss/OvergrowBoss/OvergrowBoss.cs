using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace StarlightRiver.NPCs.Boss.OvergrowBoss
{
    public partial class OvergrowBoss : ModNPC
    {
        public OvergrowBossFlail flail; //the flail which the boss owns, allows direct manipulation in the boss' partial attack class, much nicer than trying to sync between two NPCs

        private bool usedBolts = false; //tracks if the boss ahs used it's bolt attack, so it cant spam it. projectile spam bad kids!
        private bool usedPendulum = false; //tracks use of the pendulum attack

        private Vector2 spawnPoint = Vector2.Zero; //the Boss' spawn point, used for returning during the guardian phase and some animations
        private Vector2 targetPoint = Vector2.Zero; //the Boss' stored targeting point, for things like bolt and flail toss. SHOULD be deterministic I hope?
        public enum OvergrowBossPhase : int //Enum for boss phases so I dont get lost later. wee!
        {
            Struggle = 0,
            spawnAnimation = 1,
            Setup = 2,
            FirstAttack = 3,
            FirstToss = 4,
            FirstStun = 5,
            FirstBurn = 6,
            FirstGuard = 7
        };
        public override void SetDefaults()
        {
            npc.lifeMax = 6000;
            npc.width = 86;
            npc.height = 176;
            npc.immortal = true;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.knockBackResist = 0;
            npc.noGravity = true;
            music = default;
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, new Vector3(1, 1, 0.8f));
            /* AI fields:
             * 0: phase
             * 1: timer
             * 2: attack phase
             * 3: attack timer
             */
            npc.ai[1]++; //tick our timer up constantly
            npc.ai[3]++; //tick up our attack timer

            if (npc.ai[0] == (int)OvergrowBossPhase.Struggle)
            {
                if(spawnPoint == Vector2.Zero) spawnPoint = npc.Center; //sets the boss' home

                npc.velocity.Y = (float)Math.Sin((npc.ai[1] % 120) / 120f * 6.28f) * 0.6f;

                if (!Main.npc.Any(n => n.active && n.type == ModContent.NPCType<OvergrowBossAnchor>())) //once the chains are broken
                {
                    npc.velocity *= 0;
                    npc.Center = spawnPoint;
                    npc.ai[1] = 0;

                    StarlightPlayer mp = Main.LocalPlayer.GetModPlayer<StarlightPlayer>();
                    mp.ScreenMoveTime = 320;
                    mp.ScreenMoveTarget = npc.Center;

                    StarlightRiver.Instance.abilitytext.Display("[PH] Overgrow Boss", "[PH] Boss of the Overgrow", null, 200);

                    LegendWorld.OvergrowBossFree = true;
                    npc.ai[0] = (int)OvergrowBossPhase.spawnAnimation;
                }

            }
            if(npc.ai[0] == (int)OvergrowBossPhase.spawnAnimation)
            {
                if (npc.ai[1] >= 500) npc.ai[0] = (int)OvergrowBossPhase.Setup;
            }

            if(npc.ai[0] == (int)OvergrowBossPhase.Setup)
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GlassBoss");

                int index = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<OvergrowBossFlail>()); //spawn the flail after intro
                (Main.npc[index].modNPC as OvergrowBossFlail).parent = this; //set the flail's parent
                flail = Main.npc[index].modNPC as OvergrowBossFlail; //tells the boss what flail it owns

                npc.ai[0] = (int)OvergrowBossPhase.FirstAttack; //move on to the first attack phase
                npc.ai[1] = 0; //reset our timer
                npc.ai[3] = 0; //reset our attack timer
            }

            if (flail == null) return; //at this point, our boss should have her flail. if for some reason she dosent, this is a safety check

            Main.NewText(npc.ai[0] + "/" + npc.ai[1] + "/" + npc.ai[2] + "/" + npc.ai[3] + "/" + usedBolts + "/" + usedPendulum + "/" + Vector2.Distance(spawnPoint, Main.player[npc.target].Center));
            if (npc.ai[0] == (int)OvergrowBossPhase.FirstAttack)
            {
                //attacks here
                if (npc.ai[2] == 0)
                {
                    RandomTarget(); //pendulum attack is based on a RANDOM target's position
                    if ((Math.Abs(spawnPoint.X - Main.player[npc.target].Center.X) > 500 || Main.rand.Next(3) == 0) && !usedPendulum) npc.ai[2] = 5; //if the player is near the edge or randomly

                    if (npc.ai[2] == 0) //if the random checks fail to pick an attack
                    {
                        npc.TargetClosest();
                        if (usedBolts && Vector2.Distance(spawnPoint, Main.player[npc.target].Center) < 500) npc.ai[2] = 1; //if the player is near the center, use a swing if bolts has been used, else move on
                        else if (!usedBolts && Main.rand.Next(2) == 0) npc.ai[2] = 2; //otherwise use another attack, even though bolts takes a random target, the logic dictating the chance of this attack does not
                        else if (Main.rand.Next(2) == 0) npc.ai[2] = 3;
                        else npc.ai[2] = 4;
                    }

                    if (npc.ai[2] != 2) usedBolts = false; //reset bolt restriction
                    if (npc.ai[2] == 1) usedPendulum = false; //reset pendulum restriction after being spun
                }
                switch (npc.ai[2])
                {
                    case 1: Phase1Spin(); break; //I should make an enum for this too
                    case 2: Phase1Bolts(); usedBolts = true; break; //Bolts! make sure to set usedBolts to true!
                    case 3: Phase1Toss(); break;
                    case 4: Phase1Trap(); break;
                    case 5: Phase1Pendulum(); usedPendulum = true; break;
                }

                if (flail.npc.life <= 1)
                {
                    npc.ai[0] = (int)OvergrowBossPhase.FirstToss; //move to next phase once the flail is depleated
                    ResetAttack();
                    foreach (Projectile proj in Main.projectile.Where(p => p.type == ModContent.ProjectileType<Projectiles.Dummies.OvergrowBossPitDummy>())) proj.ai[1] = 1; //opens the pits
                }
            }

            if(npc.ai[0] == (int)OvergrowBossPhase.FirstToss)
            {
                RapidToss();
            }

            if(npc.ai[0] == (int)OvergrowBossPhase.FirstStun)
            {
                foreach (Player player in Main.player)
                    if (Abilities.AbilityHelper.CheckDash(player, npc.Hitbox))
                    {
                        npc.ai[0] = (int)OvergrowBossPhase.FirstGuard;
                        npc.ai[1] = 0;

                        flail.npc.ai[0] = 1; //turn the flail into a pick-upable thing
                        flail.npc.noGravity = false; //obey the laws of physics!
                    }
            }

            if (npc.ai[0]  == (int)OvergrowBossPhase.FirstBurn)
            {

            }

            if(npc.ai[0] == (int)OvergrowBossPhase.FirstGuard)
            {
                if (npc.ai[1] == 0) //at the start of the phase, spawn in our mechanics!
                {
                    npc.position = spawnPoint;
                    music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GlassPassive");
                    //spawn moving platforms
                    NPC.NewNPC((int)npc.Center.X + 500, (int)npc.Center.Y + 200, ModContent.NPCType<OvergrowBossVerticalPlatform>());
                    NPC.NewNPC((int)npc.Center.X - 500, (int)npc.Center.Y + 200, ModContent.NPCType<OvergrowBossVerticalPlatform>());

                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, ModContent.NPCType<OvergrowBossCircularPlatform>());

                    //spawn guardians
                    NPC.NewNPC((int)npc.Center.X + 600, (int)npc.Center.Y - 300, ModContent.NPCType<OvergrowBossGuardian>());
                    NPC.NewNPC((int)npc.Center.X - 600, (int)npc.Center.Y - 300, ModContent.NPCType<OvergrowBossGuardian>());
                    NPC.NewNPC((int)npc.Center.X + 600, (int)npc.Center.Y + 300, ModContent.NPCType<OvergrowBossGuardian>());
                    NPC.NewNPC((int)npc.Center.X - 600, (int)npc.Center.Y + 300, ModContent.NPCType<OvergrowBossGuardian>());

                    //make platforms appear
                    for (int x = (int)npc.Center.X / 16 - 100; x <= (int)npc.Center.X / 16 + 100; x++)
                    {
                        for (int y = (int)npc.Center.Y / 16 - 100; y <= (int)npc.Center.Y / 16 + 100; y++)
                        {
                            if (Main.tile[x, y].type == ModContent.TileType<Tiles.Overgrow.AppearingBrick>() && Main.tile[x, y].frameX == 0)
                            {
                                Main.tile[x, y].frameX = 20;
                            }
                        }
                    }
                }
                else if (!Main.npc.Any(n => n.active && n.type == ModContent.NPCType<OvergrowBossGuardian>()))
                {
                    npc.ai[0] = 7;
                    ResetIntermission();
                }
                

            }
        }
        private void ResetIntermission()
        {
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GlassBoss");

            foreach (NPC npc in Main.npc.Where(n => n.active && (n.type == ModContent.NPCType<OvergrowBossCircularPlatform>() || n.type == ModContent.NPCType<OvergrowBossVerticalPlatform>())))
            {
                npc.active = false;
            }

            //make platforms disappear
            for (int x = (int)npc.Center.X / 16 - 100; x <= (int)npc.Center.X / 16 + 100; x++)
            {
                for (int y = (int)npc.Center.Y / 16 - 100; y <= (int)npc.Center.Y / 16 + 100; y++)
                {
                    if (Main.tile[x, y].type == ModContent.TileType<Tiles.Overgrow.AppearingBrick>() && Main.tile[x, y].frameX == 20)
                    {
                        Main.tile[x, y].frameX = 0;
                    }
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[3] > 60 && npc.ai[3] < 120 && (npc.ai[2] == 3 || npc.ai[0] == (int)OvergrowBossPhase.FirstToss)) //if the boss is using a flail toss 
                DrawTossTell(spriteBatch);

            if (npc.ai[2] == 4) DrawTrapTell(spriteBatch);

            return npc.ai[0] != (int)OvergrowBossPhase.FirstGuard;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[0] == (int)OvergrowBossPhase.Struggle)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

                for (int k = 0; k < 3; k++)
                {
                    float sin = (float)Math.Sin(LegendWorld.rottime + k * (6.28f / 6));

                    DrawData data = new DrawData(TextureManager.Load("Images/Misc/Perlin"), npc.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 300, 200)), new Color(255, 255, 200) * 0.6f, npc.rotation, new Vector2(150, 100), 2 + sin * 0.1f, 0, 0);

                    GameShaders.Misc["ForceField"].UseColor(new Vector3(1.1f - (sin * 0.4f)));
                    GameShaders.Misc["ForceField"].Apply(new DrawData?(data));
                    data.Draw(spriteBatch);
                }

                spriteBatch.End();
                spriteBatch.Begin(default, default, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);
            }
        }
        public override bool CheckDead()
        {
            LegendWorld.OvergrowBossDowned = true;
            LegendWorld.AnyBossDowned = true;
            return true;
        }
    }
}

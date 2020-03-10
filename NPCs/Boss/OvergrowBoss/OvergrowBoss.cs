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

namespace StarlightRiver.NPCs.Boss.OvergrowBoss
{
    public partial class OvergrowBoss : ModNPC
    {
        public OvergrowBossFlail flail; //the flail which the boss owns, allows direct manipulation in the boss' partial attack class, much nicer than trying to sync between two NPCs

        private bool usedBolts = false; //tracks if the boss ahs used it's bolt attack, so it cant spam it. projectile spam bad kids!

        private Vector2 spawnPoint; //the Boss' spawn point, used for returning during the guardian phase and some animations
        private Vector2 targetPoint; //the Boss' stored targeting point, for things like bolt and flail toss. SHOULD be deterministic I hope?
        public enum OvergrowBossPhase : int //Enum for boss phases so I dont get lost later. wee!
        {
            spawnAnimation = 0,
            Setup = 1,
            FirstAttack = 2,
            FirstToss = 3,
            FirstStun = 4,
            FirstGuard = 5
        };
        public override string Texture => "StarlightRiver/MarioCumming"; //mario make big cream
        public override void SetDefaults()
        {
            npc.lifeMax = 6000;
            npc.width = 120;
            npc.height = 120;
            npc.immortal = true;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.knockBackResist = 0;
            npc.noGravity = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GlassBoss"); //changes throughout the fight. Reminder to myself to also change this in the ResetIntermission method also          
        }
        public override void AI()
        {
            /* AI fields:
             * 0: phase
             * 1: timer
             * 2: attack phase
             * 3: attack timer
             */
            npc.ai[1]++; //tick our timer up constantly
            npc.ai[3]++; //tick up our attack timer

            if(npc.ai[0] == (int)OvergrowBossPhase.spawnAnimation)
            {
                //Whatever intro cutscene type thing goes here

                if (npc.ai[1] > 120) npc.ai[0] = 1; //after the intro "cutscene", our boss summons her flail
            }

            if(npc.ai[0] == (int)OvergrowBossPhase.Setup)
            {
                int index = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<OvergrowBossFlail>()); //spawn the flail after intro
                (Main.npc[index].modNPC as OvergrowBossFlail).parent = this; //set the flail's parent
                flail = Main.npc[index].modNPC as OvergrowBossFlail; //tells the boss what flail it owns
                spawnPoint = npc.position; //sets the boss' home

                npc.ai[0] = 2; //move on to the first attack phase
                npc.ai[1] = 0; //reset our timer
                npc.ai[3] = 0; //reset our attack timer
            }

            if (flail == null) return; //at this point, our boss should have her flail. if for some reason she dosent, this is a safety check

            if (npc.ai[0] == (int)OvergrowBossPhase.FirstAttack)
            {
                //attacks here
                if (npc.ai[2] == 0)
                {
                    npc.ai[2] = Main.rand.Next(4, 5);
                    if (usedBolts && npc.ai[2] == 2) { npc.ai[2] = 3; usedBolts = false; }//use a toss instead if you just used bolts!
                }
                switch (npc.ai[2])
                {
                    case 1: Phase1Spin(1.1f); break; //I should make an enum for this too
                    case 2: Phase1Bolts(); usedBolts = true; break; //Bolts! make sure to set usedBolts to true!
                    case 3: Phase1Toss(); break;
                    case 4: Phase1Trap(); break;
                }

                if (flail.npc.life <= 1)
                {
                    npc.ai[0] = 3; //move to next phase once the flail is depleated
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
                        npc.ai[0] = 5;
                        npc.ai[1] = 0;
                        npc.life -= 2000;
                        CombatText.NewText(npc.Hitbox, Color.Orange, 2000);

                        flail.npc.ai[0] = 1; //turn the flail into a pick-upable thing
                        flail.npc.noGravity = false; //obey the laws of physics!
                    }
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
                    npc.ai[0] = 6;
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
            if (npc.ai[3] > 60 && npc.ai[3] < 120 && (npc.ai[2] == 3 || npc.ai[0] == 3)) //if the boss is using a flail toss 
                DrawTossTell(spriteBatch);

            if (npc.ai[2] == 4) DrawTrapTell(spriteBatch);

            return npc.ai[0] != 5;
        }
    }
}

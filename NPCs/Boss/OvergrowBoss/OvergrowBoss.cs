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
    class OvergrowBoss : ModNPC
    {
        public OvergrowBossFlail flail;
        private Vector2 spawnPoint;
        public override string Texture => "StarlightRiver/MarioCumming";
        public override void SetDefaults()
        {
            npc.lifeMax = 6000;
            npc.width = 120;
            npc.height = 120;
            npc.immortal = true;
            npc.defense = int.MaxValue / 2;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.knockBackResist = 0;
            npc.noGravity = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GlassBoss");            
        }
        public override void AI()
        {
            /* AI fields:
             * 0: phase
             * 1: timer
             * 2: attack phase
             */
            npc.ai[1]++; //tick our timer up constantly

            if(npc.ai[0] == 0)
            {
                //Whatever intro cutscene type thing goes here

                if (npc.ai[1] > 120) npc.ai[0] = 1; //after the intro "cutscene", our boss summons her flail
            }

            if(npc.ai[0] == 1)
            {
                int index = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<OvergrowBossFlail>()); //spawn the flail after intro
                (Main.npc[index].modNPC as OvergrowBossFlail).parent = this; //set the flail's parent
                flail = Main.npc[index].modNPC as OvergrowBossFlail; //tells the boss what flail it owns
                spawnPoint = npc.position; //sets the boss' home

                npc.ai[0] = 2; //move on to the first attack phase
                npc.ai[1] = 0; //reset our timer
            }

            if (flail == null) return; //at this point, our boss should have her flail. if for some reason she dosent, this is a safety check

            if (npc.ai[0] == 2)
            {
                //attacks here
                //Temporary attacks
                if(npc.ai[2] == 0) flail.npc.velocity.Y += 0.1f;
                if(flail.npc.velocity.Y == 0.1f && npc.ai[2] == 0)
                {
                    npc.ai[2] = 1;
                    flail.npc.velocity.Y = -5f;
                    Helper.DoTilt((flail.npc.Center.X - Main.LocalPlayer.Center.X) / 1680f);
                }
                if (npc.ai[2] == 1 && npc.Hitbox.Contains(flail.npc.Hitbox)) npc.ai[2] = 0;

                if (flail.npc.life <= 1)
                {
                    npc.ai[0] = 3; //move to next phase once the flail is depleated
                    foreach (Projectile proj in Main.projectile.Where(p => p.type == ModContent.ProjectileType<Projectiles.Dummies.OvergrowBossPitDummy>())) proj.ai[1] = 1; //opens the pits
                }
            }

            if(npc.ai[0] == 3)
            {
                //repeated flail toss here
            }

            if(npc.ai[0] == 4)
            {
                foreach (Player player in Main.player)
                    if (Abilities.AbilityHelper.CheckDash(player, npc.Hitbox))
                    {
                        npc.ai[0] = 5;
                        npc.life -= 2000;
                        CombatText.NewText(npc.Hitbox, Color.Orange, 2000);

                        flail.npc.ai[0] = 1; //turn the flail into a pick-upable thing
                        flail.npc.noGravity = false; //obey the laws of physics!
                    }
            }

            if(npc.ai[0] == 5)
            {
                npc.position = spawnPoint;
                if (npc.Hitbox.Intersects(flail.npc.Hitbox) && flail.holder == null)
                {
                    npc.ai[0] = 2;
                    npc.ai[1] = 0;

                    //resets the flail
                    flail.npc.ai[0] = 0;
                    flail.npc.ai[3] = 1;
                    flail.npc.Center = npc.Center;
                    flail.npc.velocity *= 0;
                    flail.npc.rotation = 0;
                    flail.npc.dontTakeDamage = false;
                    flail.npc.friendly = false;
                    flail.npc.lifeMax += 250;
                    flail.npc.life = flail.npc.lifeMax;
                    npc.noGravity = false;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            return npc.ai[0] != 5;
        }
    }
}

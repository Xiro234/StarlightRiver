using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using StarlightRiver.Abilities;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    class VitricBossCrystal : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Resonant Crystal");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 2;
            npc.damage = 20;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 32;
            npc.height = 48;
            npc.value = 0;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.immortal = true;
        }
        public override void AI()
        {
            /*
             * AI slots:
             * 0: vulnerability
             * 1: timer
             * 2: phase
             * 3: index
             */

            //The boss NPC that this crystal is tied to
            NPC parent;

            //Finds the active sentinel, NPC kills itself if zero or >1 is found.
            if (Main.npc.Count(n => n.active && n.type == ModContent.NPCType<VitricBoss>()) != 1) { npc.Kill(); return; }
            else parent = Main.npc.FirstOrDefault(n => n.active && n.type == ModContent.NPCType<VitricBoss>());

            //Harmlessness
            if (npc.ai[0] == 1) npc.damage = 0;
            else npc.damage = 20;

            //Damage Detection
            foreach (Player player in Main.player)
            {
                if(AbilityHelper.CheckDash(player, npc.Hitbox))
                {
                    npc.life--;
                    npc.frame.Y += npc.height;
                    npc.ai[0] = 0;

                    for(int k = 0; k <= 20; k++)
                    Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Dusts.Glass2>());

                    Main.PlaySound(SoundID.Shatter, npc.Center);

                    player.GetModPlayer<AbilityHandler>().dash.Active = false;
                    player.velocity *= -0.2f;
                }
            }
            
            //Timer
            npc.ai[1]++;

            //Behaviors
            switch (npc.ai[2])
            {
                //OnSpawn
                case 0:
                    npc.scale = 0;
                    npc.alpha = 255;
                    npc.ai[2] = 1;
                break;

                //First Phase
                case 1:
                    npc.rotation += (6.28f * 2) / 51f;
                    npc.scale += 1 / 51f;
                    npc.alpha -= 5;

                    if (npc.ai[1] > 51) { npc.ai[2] = 2; npc.ai[1] = 0; npc.velocity *= 0; }
                break;

                //Attacks
                case 2:
                    if(npc.ai[1] >= 120)
                    {
                        npc.ai[1] = 0;
                        npc.ai[2] = 3;
                    }
                break;

                //Lineup 
                case 3:
                    Vector2 target = parent.Center + new Vector2((npc.ai[3] + 1) * 150, -100);
                    if (Vector2.Distance(npc.Center, target) >= 20) npc.velocity = Vector2.Normalize(npc.Center - target) * -4;
                    else { npc.ai[2] = 4; npc.velocity *= 0; }
                break;

                //Fall
                case 4:
                    npc.velocity.Y += 0.5f;
                    if (Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16 + 1].collisionType == 1) { npc.ai[2] = 5; npc.ai[1] = 0; npc.velocity *= 0; }
                break;

                //Stuck
                case 5:
                    npc.ai[0] = 1;
                    if(npc.ai[1] >= 360) { npc.ai[0] = 0; npc.ai[1] = 0; npc.ai[2] = 6; }
                break;

                //Return
                case 6:
                    npc.velocity.Y = -1;
                    if (npc.ai[1] >= 60)
                    {
                        npc.ai[1] = 0;
                        npc.ai[2] = 2;
                        npc.velocity *= 0;
                        npc.position = parent.Center +
                            new Vector2(-1, 0).RotatedBy((npc.ai[3] / ((float)Main.npc.Count(n => n.active && n.type == ModContent.NPCType<VitricBossCrystal>()) - 1)) * 3.14f) * 100 -
                            npc.Hitbox.Size() / 2;
                    }
                break;
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D tex = ModContent.GetTexture("StarlightRiver/NPCs/Boss/VitricBoss/CrystalGlow");
            if(npc.ai[0] == 1)
            spriteBatch.Draw(tex, npc.Center - Main.screenPosition + new Vector2(0, 4), tex.Frame(), Color.White * (float)Math.Sin(LegendWorld.rottime), npc.rotation, tex.Frame().Size() / 2, npc.scale, 0, 0);
        }
    }
}

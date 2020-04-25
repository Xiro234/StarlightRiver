using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    class VitricBossCrystal : ModNPC
    {
        public Vector2 StartPos;
        public Vector2 TargetPos;
        public VitricBoss Parent;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Resonant Crystal");
            Main.npcFrameCount[npc.type] = 4;
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
            npc.friendly = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.immortal = true;
        }
        public override void AI()
        {
            /* AI fields:
             * 0: state, lazy so: 0 = vulnerable, 1 = vulnerable broken, 2 = invulnerable, 3 = invulnerable broken
             * 1: timer
             * 2: phase
             * 3: alt timer
             */
            if (Parent == null) npc.Kill();
            npc.frame = new Rectangle(0, npc.height * (int)npc.ai[0], npc.width, npc.height); //frame finding based on state

            npc.ai[1]++; //ticks the timers
            npc.ai[3]++;

            foreach(Player player in Main.player)
            if(npc.ai[0] == 0 && Abilities.AbilityHelper.CheckDash(player, npc.Hitbox))
                {
                    npc.ai[0] = 1; //It's all broken and on the floor!
                    npc.ai[2] = 0; //go back to doing nothing
                    npc.ai[1] = 0; //reset timer

                    Parent.npc.ai[2] = 1; //boss should go into it's angery phase
                }

            switch (npc.ai[2])
            {
                case 0: //nothing / spawning animation, sensitive to friendliness
                    if (npc.friendly && npc.ai[0] != 0)
                    {
                        if (npc.ai[3] > 0 && npc.ai[3] <= 90)
                        {
                            npc.Center = Vector2.SmoothStep(StartPos, TargetPos, npc.ai[3] / 90);
                        }
                        if (npc.ai[3] == 90)
                        {
                            npc.friendly = false;
                            ResetTimers();
                        }
                    }
                    break;

                case 1: //nuke attack
                    if(npc.ai[1] > 60 && npc.ai[1] <= 180)
                    {
                        npc.Center = Vector2.SmoothStep(StartPos, TargetPos, (npc.ai[1] - 60) / 120f); //go to the platform
                    }
                    if (npc.ai[1] >= 720) //when time is up... uh oh
                    {
                        npc.ai[0] = 2; //make invulnerable again
                        npc.ai[2] = 0; //go back to doing nothing
                        npc.ai[1] = 0; //reset timer
                    }
                    break;

            }
        }
        private void ResetTimers()
        {
            npc.ai[1] = 0;
            npc.ai[3] = 0;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D tex = ModContent.GetTexture("StarlightRiver/NPCs/Boss/VitricBoss/CrystalGlow"); //glowy outline
            if (npc.ai[0] == 0)
                spriteBatch.Draw(tex, npc.Center - Main.screenPosition + new Vector2(0, 4), tex.Frame(), Color.White * (float)Math.Sin(LegendWorld.rottime), npc.rotation, tex.Frame().Size() / 2, npc.scale, 0, 0);

            if(npc.ai[2] == 1 && npc.ai[1] < 180) //tell line for going to a platform in the nuke attack
            {
                DrawLine(spriteBatch, npc.Center, TargetPos);
            }
        }

        private void DrawLine(SpriteBatch sb, Vector2 p1, Vector2 p2) //helper method to draw a tell line between two points.
        {
            sb.End();
            sb.Begin(default, BlendState.Additive);
            Texture2D tex = ModContent.GetTexture("StarlightRiver/Gores/TellBeam");
            for(float k = 0; k < 1; k += 1/Vector2.Distance(p1, p2) * tex.Width)
            {
                sb.Draw(tex, Vector2.Lerp(p1, p2, k) - Main.screenPosition, tex.Frame(), new Color(180, 220, 250) * 0.8f, (p1 - p2).ToRotation(), tex.Frame().Size() / 2, 1, 0, 0);
            }
            sb.End();
            sb.Begin();
        }
    }
}

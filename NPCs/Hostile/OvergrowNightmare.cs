using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace StarlightRiver.NPCs.Hostile
{
    class OvergrowNightmare : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("?!?!?!");
            Main.npcFrameCount[npc.type] = 22;
        }

        const int runFramesLoop = 11;

        public override void SetDefaults()
        {
            npc.height = 42;
            npc.width = 48;
            npc.lifeMax = 110;
            npc.damage = 40;
            npc.aiStyle = -1;
            npc.noGravity = false;
            npc.direction = Main.rand.Next(2) == 0 ? 1 : -1;
            npc.spriteDirection = -npc.direction;
        }

        public override void AI()
        {
            /*AI fields:
             * 0: state
             * 1: timer
             */
            Player target = Main.player[npc.target];
            switch (npc.ai[0])
            {          
                case 0://waiting
                    npc.immortal = true;
                    if (Main.player.Any(n => Vector2.Distance(n.Center, npc.Center) <= 100))
                    {
                        npc.ai[0] = 1;
                        npc.immortal = false;
                    }
                    break;

                case 1://popping up from ground
                    if (npc.ai[1]++ >= 50) npc.ai[0] = 2;
                    npc.TargetClosest();
                    break;

                case 2://oh god oh fuck
                    if (npc.velocity.Y == 0)//jumping. note: (the could be moved to just before it sets the velocity high in MoveVertical())
                    {
                        Helper.NpcVertical(this.npc, false);
                    }

                    npc.velocity.X += npc.position.X - target.Center.X > 0 ? -0.2f : 0.2f;
                    if (Math.Abs(npc.velocity.X) >= 10) npc.velocity.X = (npc.velocity.X > 0) ? 10 : -10;

                    npc.direction = npc.velocity.X > 0 ? 1 : -1;
                    npc.spriteDirection = -npc.direction;

                    int x = (int)npc.Center.X / 16;
                    int y = (int)npc.Center.Y / 16;

                    //cross gaps/jump over obstacles
                    if ((!WorldGen.TileEmpty(x + 1, y - 1) || WorldGen.TileEmpty(x + 1, y + 1)) && npc.velocity.Y == 0) npc.velocity.Y -= 10;
                    //lunge at the player
                    if (Main.player.Any(n => n.Hitbox.Contains(new Point((int)npc.Center.X + 64, (int)npc.Center.Y))) && npc.velocity.Y == 0) npc.velocity.Y -= 6;

                    break;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            npc.velocity *= 0.1f;
        }

        public override void FindFrame(int frameHeight)
        {
            switch (npc.ai[0])
            {
                case 0:
                    npc.frame.Y = 0;
                    break;

                case 1:
                    npc.frame.Y = frameHeight + frameHeight * (int)(npc.ai[1] / 5);
                    break;

                case 2:
                    npc.frameCounter += Math.Abs(npc.velocity.X);
                    if ((int)(npc.frameCounter * 0.1) >= Main.npcFrameCount[npc.type])//replace the 0.1 with a float to control animation speed
                        npc.frameCounter = (Main.npcFrameCount[npc.type] - runFramesLoop) * 10;//accounting for the offset makes this a bit jank, might be able to optimize this.
                    npc.frame.Y = (int)(npc.frameCounter * 0.1) * frameHeight;
                    break;
            }
        }
    }
}

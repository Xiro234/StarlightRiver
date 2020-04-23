using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Hostile
{
    class OvergrowNightmare : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("?!?!?!");
            Main.npcFrameCount[npc.type] = 11;
        }
        public override void SetDefaults()
        {
            npc.height = 16;
            npc.width = 16;
            npc.lifeMax = 110;
            npc.damage = 40;
            npc.aiStyle = -1;
            npc.noGravity = false;
            npc.direction = Main.rand.Next(2) == 0 ? 1 : -1;
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
                case 0:
                    npc.immortal = true;
                    if (Main.player.Any(n => Vector2.Distance(n.Center, npc.Center) <= 100))
                    {
                        npc.ai[0] = 1;
                        npc.immortal = false;
                    }
                    break;

                case 1:
                    if (npc.ai[1]++ >= 50) npc.ai[0] = 2;
                    break;

                case 2:
                    npc.TargetClosest();
                    npc.direction = npc.velocity.X > 0 ? 1 : -1;
                    npc.velocity.X += npc.position.X - target.Center.X > 0 ? -0.2f : 0.2f;
                    if (Math.Abs(npc.velocity.X) >= 10) npc.velocity.X = (npc.velocity.X > 0) ? 10 : -10;

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

        int frame = 0;
        public override void FindFrame(int frameHeight)
        {
            if (npc.frameCounter++ >= 3) { frame++; npc.frameCounter = 0; }
            if (frame > 5) frame = 0;

            switch (npc.ai[0])
            {
                case 0: npc.frame.Y = 0; break;

                case 1:
                    npc.frame.Y = frameHeight + frameHeight * (int)(npc.ai[1] / 5);
                    break;

                case 2:
                    if (Math.Abs(npc.velocity.X) >= Math.Abs(npc.oldVelocity.X))
                    {
                        npc.frame.Y = frameHeight * 10 + frameHeight * frame; //speeding up & running
                    }
                    else npc.frame.Y = frameHeight * 13; //slowing down

                    if (npc.velocity.Y > 0) npc.frame.Y = frameHeight * 14; //Jumping/falling

                    break;
            }
        }

    }
}

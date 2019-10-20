using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Hostile
{
    class CrystalPopper : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandbat");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.width = 50;
            npc.height = 42;
            npc.knockBackResist = 0f;
            npc.lifeMax = 80;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.damage = 10;
            npc.aiStyle = -1;           
        }

        public override bool CheckDead()
        {
            return true;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            if(npc.ai[0] == 0)
            {
                if(Vector2.Distance(Main.player[npc.target].Center, npc.Center)<= 180)
                {
                    npc.ai[0] = 1;
                }
            }

            if(npc.ai[0] == 1)
            {
                npc.ai[1]++;

                if(npc.ai[1] == 1)
                {
                    npc.velocity.Y = -20;
                }

                npc.velocity.Y += (.6f);
                
                for (int k = 0; k <= 10; k++)
                {
                    Dust.NewDust(npc.position, 32, 32, DustID.Sandstorm);
                }

                if(npc.ai[1] >= 5)
                {
                    npc.noTileCollide = false;
                }

                if(npc.ai[1] >= 30)
                {
                    npc.velocity.Y = 0;
                    npc.ai[1] = 0;
                    npc.ai[0] = 2;
                    for (int k = -1; k <= 1; k++)
                    {
                        Projectile.NewProjectile(npc.Center, Vector2.Normalize(Main.player[npc.target].Center - npc.Center).RotatedBy(k * 0.5f) * 2, 2, 10, 0);
                    }
                }
            }

            if(npc.ai[0] == 2)
            {
                npc.velocity = Vector2.Normalize(Main.player[npc.target].Center - npc.Center) * 2.5f;
                npc.spriteDirection = (Main.player[npc.target].Center.X - npc.Center.X < 0) ? -1 : 1;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneRockLayerHeight && Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].active() && spawnInfo.player.GetModPlayer<BiomeHandler>().ZoneGlass) ? 0.75f : 0f;
        }
        /*public override int SpawnNPC(int tileX, int tileY)
        {
            return NPC.NewNPC(tileX, tileY + 1, ModContent.NPCType<CrystalPopper>());
        }*/

        public int Framecounter = 0;
        public int Gameframecounter = 0;
        public override void FindFrame(int frameHeight)
        {
            if (Gameframecounter++ == 4)
            {
                Framecounter++;
                Gameframecounter = 0;
            }
            npc.frame.Y = 42 * Framecounter;
            if (Framecounter >= 5)
            {
                Framecounter = 0;
            }
            switch(npc.ai[0])
            {
                case 0: npc.frame.Y = npc.height * 5; break;
                case 1: npc.frame.Y = npc.height * 0; break;
            }
        }
    }
}

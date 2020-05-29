
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
            Main.npcFrameCount[npc.type] = 7;
        }
        const int animFramesLoop = 6; //amount of frames in the main loop

        public override void SetDefaults()
        {
            npc.width = 50;
            npc.height = 42;
            npc.knockBackResist = 0.8f;
            npc.lifeMax = 80;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.damage = 10;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath4;

            npc.direction = Main.rand.Next(2) == 0 ? 1 : -1;
            npc.spriteDirection = npc.direction;
        }

        float AnimSpeedMult = 0.3f;

        public override void AI()
        {
            npc.TargetClosest(true);
            switch (npc.ai[0])
            {
                case 0://in ground checking for player
                    if (Vector2.Distance(Main.player[npc.target].Center, npc.Center) <= 180)
                    {
                        npc.ai[0] = 1;
                        npc.noGravity = true;
                    }
                    break;

                case 1://shoot out of ground and attack
                    npc.ai[1]++;

                    if (npc.ai[1] == 1)
                    {
                        npc.velocity.Y = -20;
                    }
                    npc.velocity.Y += (.6f);
                    for (int k = 0; k <= 10; k++)
                    {
                        Dust.NewDust(npc.position, 32, 32, DustID.Sandstorm);
                    }
                    /*if(npc.ai[1] >= 5) //disabled to make this in-line with the overgrowth nightmare
                    {
                        npc.noTileCollide = false;
                    }*/
                    if (npc.ai[1] >= 30)
                    {
                        npc.velocity.Y = 0;
                        npc.ai[1] = 0;
                        npc.ai[0] = 2;
                        for (int k = -1; k <= 1; k++)
                        {
                            Projectile.NewProjectile(npc.Center, Vector2.Normalize(Main.player[npc.target].Center - npc.Center).RotatedBy(k * 0.5f) * 6, ModContent.ProjectileType<Projectiles.GlassSpike>(), 10, 0);
                        }
                        npc.velocity = Vector2.Normalize(Main.player[npc.target].Center - npc.Center) * -5.5f;
                    }
                    break;

                case 2://seek and destroy
                    npc.velocity += Vector2.Normalize(Main.player[npc.target].Center - npc.Center) * 0.08f;
                    if (npc.velocity.Length() > 5.5f && ((npc.velocity - npc.oldVelocity).ToRotation() == (Main.player[npc.target].Center - npc.Center).ToRotation()))
                    { npc.velocity = Vector2.Normalize(npc.velocity) * 5.5f; }

                    Main.NewText(Vector2.Normalize(npc.velocity) + " Normalize");
                    Main.NewText(npc.velocity.Length() + " Length");

                    if (npc.collideX && Math.Abs(npc.velocity.X) > 1f)
                    { npc.velocity.X = Vector2.Normalize(-npc.velocity).X * 1.5f; }
                    if (npc.collideY && Math.Abs(npc.velocity.Y) > 1f)
                    { npc.velocity.Y = Vector2.Normalize(-npc.velocity).Y * 1.5f; }

                    npc.spriteDirection = (Main.player[npc.target].Center.X - npc.Center.X < 0) ? -1 : 1;
                    break;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneRockLayerHeight && Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].active() && spawnInfo.player.GetModPlayer<BiomeHandler>().ZoneGlass) ? 0.75f : 0f;
        }
        /*public override int SpawnNPC(int tileX, int tileY)
        {
            return 
            (tileX, tileY + 1, ModContent.NPCType<CrystalPopper>());
        }*/

        public override void NPCLoot()
        {
            if (Main.rand.NextFloat() < 0.5f) { Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Vitric.VitricOre>(), Main.rand.Next(1, 3)); }
            if (Main.rand.NextFloat() < 0.8f) { Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Vitric.VitricSandItem>(), Main.rand.Next(10, 12)); }
        }

        public override void FindFrame(int frameHeight)
        {
            switch (npc.ai[0])
            {
                case 0: npc.frame.Y = frameHeight * 6; break;
                case 1: npc.frame.Y = frameHeight * 0; break;
                case 2:
                    npc.frameCounter++;//skele frame-code
                    if ((int)(npc.frameCounter * AnimSpeedMult) >= animFramesLoop)
                        npc.frameCounter = 0;
                    npc.frame.Y = (int)(npc.frameCounter * AnimSpeedMult) * frameHeight; break;
            }
        }
    }
}
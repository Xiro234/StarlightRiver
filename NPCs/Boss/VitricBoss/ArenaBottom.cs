﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    internal class ArenaBottom : ModNPC
    {
        public VitricBoss Parent;
        public override string Texture => "StarlightRiver/Invisible";

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return false;
        }

        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return false;
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }

        public override void SetDefaults()
        {
            npc.height = 16;
            npc.width = 1260;
            npc.aiStyle = -1;
            npc.lifeMax = 2;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.dontTakeDamage = true;
            npc.dontCountMe = true;
        }

        public override void AI()
        {
            /* AI fields:
             * 0: timer
             * 1: state
             */
            if (Parent?.npc.active != true) { npc.active = false; return; }
            if (Parent.npc.ai[1] == (int)VitricBoss.AIStates.FirstToSecond) npc.ai[1] = 2;
            switch (npc.ai[1])
            {
                case 0:
                    if (Main.player.Any(n => n.Hitbox.Intersects(npc.Hitbox))) npc.ai[0]++; //ticks the enrage timer when players are standing on the ground. Naughty boys.
                    if (npc.ai[0] > 120) //after standing there for too long a wave comes by to fuck em up.
                    {
                        npc.ai[1] = 1; //wave mode
                        npc.ai[0] = 0; //reset timer so it can be reused
                    }
                    break;

                case 1:
                    npc.ai[0] += 8; //timer is now used to track where we are in the crystal wave
                    if (npc.ai[0] % 32 == 0) //summons a crystal at every tile covered by the NPC
                    {
                        Projectile.NewProjectile(new Vector2(npc.position.X + npc.ai[0], npc.position.Y + 48), Vector2.Zero, ModContent.ProjectileType<CrystalWave>(), 20, 1);
                    }
                    if (npc.ai[0] > npc.width)
                    {
                        npc.ai[1] = 0; //go back to waiting for enrage time
                        npc.ai[0] = 0; //reset timer
                    }
                    break;

                case 2: //only happens when the boss goes into phase 2
                    if (npc.ai[0] < 120) npc.ai[0]++; //cap timer at 120
                    if (npc.ai[0] < 90) //dust before rising
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Dusts.Air>());
                    }
                    if (npc.ai[0] == 90) //make the NPC damage players now
                    {
                        npc.friendly = false;
                        npc.damage = 50;
                    }
                    break;
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[1] == 2 && npc.ai[0] > 90) //in the second phase after the crystals have risen
            {
                float off = (npc.ai[0] - 90) / 30 * 32;
                for (int k = 0; k < npc.width; k += 16)
                {
                    Vector2 pos = npc.position + new Vector2(k, 32 - off) - Main.screenPosition; //actually draw the crystals lol
                    Texture2D tex = ModContent.GetTexture("StarlightRiver/NPCs/Boss/VitricBoss/CrystalWave");
                    spriteBatch.Draw(tex, pos, Color.White);
                }
            }
        }
    }

    internal class CrystalWave : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 16;
            projectile.height = 48;
            projectile.timeLeft = 30;
            projectile.hide = true;
        }

        private float startY;

        public override void AI()
        {
            float off = 128 * projectile.timeLeft / 15 - 64 * (float)Math.Pow(projectile.timeLeft, 2) / 225;
            if (projectile.timeLeft == 30)
            {
                Main.PlaySound(Terraria.ID.SoundID.DD2_WitherBeastCrystalImpact, projectile.Center);
                startY = projectile.position.Y;
            }
            projectile.position.Y = startY - off;
        }

        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.Draw(ModContent.GetTexture(Texture), projectile.position - Main.screenPosition, Color.White);
        }
    }
}
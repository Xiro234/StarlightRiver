﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.NPCs.Boss.SquidBoss
{
    class ArenaBlocker : ModNPC
    {
        public override string Texture => "STarlightRiver/Invisible";

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor) => false;

        public override bool CanHitPlayer(Player target, ref int cooldownSlot) => npc.ai[1] == 0;

        public override bool CheckActive() => NPC.AnyNPCs(NPCType<SquidBoss>());

        public override void SetDefaults()
        {
            npc.width = 1600;
            npc.height = 32;
            npc.damage = 1;
            npc.lifeMax = 1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.dontTakeDamage = true;
        }

        public override void AI()
        {
            if(npc.ai[1] == 1 && npc.ai[0] > 0) { npc.ai[0] -= 4; }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[0] > 150)
            {
                Texture2D top = GetTexture("StarlightRiver/NPCs/Boss/SquidBoss/TentacleTop");
                Texture2D glow = GetTexture("StarlightRiver/NPCs/Boss/SquidBoss/TentacleGlow");
                Texture2D body = GetTexture("StarlightRiver/NPCs/Boss/SquidBoss/TentacleBody");

                for (int k = 0; k < (npc.ai[0] - top.Height); k += (body.Height + 2))
                {
                    Vector2 pos2 = npc.position + new Vector2(k, 4 + (float)Math.Sin(StarlightWorld.rottime + (k / 25f)) * 10);
                    spriteBatch.Draw(body, pos2 - Main.screenPosition, body.Frame(), Lighting.GetColor((int)pos2.X / 16, (int)pos2.Y / 16), 1.57f, body.Size() / 2, 1, 0, 0);

                    Vector2 pos3 = npc.position + new Vector2(npc.width - k, 4 + (float)Math.Sin(StarlightWorld.rottime + (k / 25f)) * 10);
                    spriteBatch.Draw(body, pos3 - Main.screenPosition, body.Frame(), Lighting.GetColor((int)pos3.X / 16, (int)pos3.Y / 16), 1.57f, body.Size() / 2, 1, 0, 0);
                }

                float sin = (float)Math.Sin(StarlightWorld.rottime + 3.6f) * 10;
                Color color = new Color(255, 50, 50);

                Vector2 pos = npc.position + new Vector2(npc.ai[0] - top.Height + 36, 32 - top.Width / 2 + sin);
                spriteBatch.Draw(top, pos - Main.screenPosition, top.Frame(), Lighting.GetColor((int)pos.X / 16, (int)pos.Y / 16), 1.57f, top.Size() / 2, 1, 0, 0);
                spriteBatch.Draw(glow, pos - Main.screenPosition, top.Frame(), color, 1.57f, top.Size() / 2, 1, 0, 0);
                Lighting.AddLight(pos, color.ToVector3() * 0.4f);

                Vector2 pos4 = npc.position + new Vector2(1600 - npc.ai[0] + top.Height - 36, 32 - top.Width / 2 + sin);
                spriteBatch.Draw(top, pos4 - Main.screenPosition, top.Frame(), Lighting.GetColor((int)pos4.X / 16, (int)pos4.Y / 16), 4.73f, top.Size() / 2, 1, 0, 0);
                spriteBatch.Draw(glow, pos4 - Main.screenPosition, top.Frame(), color, 4.73f, top.Size() / 2, 1, 0, 0);
                Lighting.AddLight(pos4, color.ToVector3() * 0.4f);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.position.Y = npc.position.Y + npc.height;
            target.velocity.Y += 3;
        }
    }
}

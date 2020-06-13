﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.OvergrowBoss
{
    internal class OvergrowBossGuardian : ModNPC
    {
        public override void SetDefaults()
        {
            npc.dontTakeDamage = true;
            npc.lifeMax = 1;
            npc.width = 56;
            npc.height = 56;
            npc.aiStyle = -1;
            npc.noGravity = true;
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override void AI()
        {
            if (Main.npc.Any(n => n.active && n.type == ModContent.NPCType<OvergrowBossFlail>() && n.ai[0] == 1 && n.Hitbox.Intersects(npc.Hitbox) && (n.modNPC as OvergrowBossFlail).holder == null))
            {
                for (int k = 0; k < 100; k++)
                    Dust.NewDustPerfect(npc.Center, ModContent.DustType<Dusts.Gold2>(), Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(2), default, default, 6.4f);

                npc.Kill();
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

            for (int k = 0; k < 3; k++)
            {
                float sin = (float)Math.Sin(StarlightWorld.rottime + k * (6.28f / 6));

                DrawData data = new DrawData(TextureManager.Load("Images/Misc/Perlin"), npc.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 150, 100)), new Color(255, 255, 200) * 0.6f, npc.rotation, new Vector2(75f, 50f), 2 + sin * 0.18f, 0, 0);

                GameShaders.Misc["ForceField"].UseColor(new Vector3(1.1f - (sin * 0.4f)));
                GameShaders.Misc["ForceField"].Apply(new DrawData?(data));
                data.Draw(spriteBatch);
            }

            spriteBatch.End();
            spriteBatch.Begin(default, default, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);
        }
    }
}
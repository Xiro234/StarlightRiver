using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.NPCs.Boss.SquidBoss
{
    class SquidEgg : ModProjectile
    {
        public override bool PreDraw(SpriteBatch spriteBatch, Color color) => false;

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.timeLeft = 120;
            projectile.hostile = true;
            projectile.damage = 15;
        }

        public override void AI()
        {
            projectile.ai[0] += 0.1f;

            projectile.velocity.Y += (projectile.timeLeft > 90 ? -0.14f : 0.035f);
            projectile.velocity.X *= 0.9f;

            projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            float sin = 1 + (float)Math.Sin(projectile.ai[0] / 10f);
            float cos = 1 + (float)Math.Cos(projectile.ai[0] / 10f);
            float scale = 1 + sin * 0.04f;

            Color color = new Color(0.5f + cos * 0.2f, 0.8f, 0.5f + sin * 0.2f) * 0.8f;
            spriteBatch.Draw(GetTexture(Texture), projectile.Center - Main.screenPosition, null, color, projectile.rotation, projectile.Size / 2, 1, 0, 0);
        }

        public override void Kill(int timeLeft)
        {
            NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, Main.expertMode ? NPCType<Auroraborn>() : NPCType<Auroraling>());
        }
    }
}

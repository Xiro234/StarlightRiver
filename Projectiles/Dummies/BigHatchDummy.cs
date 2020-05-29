using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.Dummies
{
    internal class BigHatchDummy : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override string Texture => "StarlightRiver/Invisible";
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.aiStyle = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            projectile.timeLeft = 2;
            projectile.ai[0] += 0.01f;
            if (projectile.ai[0] >= 6.28f) projectile.ai[0] = 0;
            if (Main.rand.Next(5) == 0)
            {
                float rot = Main.rand.NextFloat(-0.7f, 0.7f);
                Dust.NewDustPerfect(projectile.Center + new Vector2(24, -32), ModContent.DustType<Dusts.Gold4>(),
                    new Vector2(0, 0.4f).RotatedBy(rot + 0.7f), 0, default, 0.4f - Math.Abs(rot) / 0.7f * 0.2f);
            }

            if (Main.tile[(int)projectile.Center.X / 16, (int)projectile.Center.Y / 16].type != ModContent.TileType<Tiles.Overgrow.BigHatchOvergrow>())
            {
                projectile.timeLeft = 0;
            }
        }
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 pos = projectile.Center + new Vector2(24, -32) - Main.screenPosition;
            Texture2D tex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/Shine");
            Color col = new Color(160, 160, 120);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive);

            for (int k = 0; k <= 5; k++)
            {
                spriteBatch.Draw(tex, pos, tex.Frame(), col * 0.4f, (float)Math.Sin(projectile.ai[0] + k) * 0.5f + 0.7f, new Vector2(8, 0), 2.6f, 0, 0);
                spriteBatch.Draw(tex, pos, tex.Frame(), col * 0.3f, (float)Math.Sin(projectile.ai[0] + k + 0.5f) * 0.6f + 0.7f, new Vector2(8, 0), 4f, 0, 0);
                spriteBatch.Draw(tex, pos, tex.Frame(), col * 0.5f, (float)Math.Sin(projectile.ai[0] + k + 0.9f) * 0.3f + 0.7f, new Vector2(8, 0), 3.2f, 0, 0);
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            return false;
        }
    }
}

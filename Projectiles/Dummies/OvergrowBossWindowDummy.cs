using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.Dummies
{
    class OvergrowBossWindowDummy : ModProjectile
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

            for(float k = 0; k <= 6.28f; k+= 0.2f)
            {
                Lighting.AddLight(projectile.Center + Vector2.One.RotatedBy(k) * 23 * 16, new Vector3(1, 1, 0.7f));
            }
        }

        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 pos = projectile.Center;
            Vector2 dpos = pos - Main.screenPosition;

            Texture2D frametex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/WindowFrame");
            spriteBatch.Draw(frametex, dpos, frametex.Frame(), Color.White, 0, frametex.Frame().Size() / 2, 1, 0, 0);

            return false;
        }
    }
}

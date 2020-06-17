using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Core;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver
{
    public partial class StarlightRiver : Mod
    {
        static ParticleSystem.Update UpdateForeground => UpdateForegroundBody;
        static ParticleSystem.Update UpdateBackground => UpdateBackgroundBody;

        internal ParticleSystem ForegroundParticles;
        internal ParticleSystem BackgroundParticles;

        private void LoadVitricBGSystems()
        {
            ForegroundParticles = new ParticleSystem("StarlightRiver/GUI/Assets/LightBig", UpdateForeground, 3);
            BackgroundParticles = new ParticleSystem("StarlightRiver/GUI/Assets/LightBig", UpdateBackground, 3);
        }

        private static void UpdateForegroundBody(Particle particle)
        {
            particle.Timer--;
            particle.StoredPosition += particle.Velocity;
            particle.Position.X = particle.StoredPosition.X - Main.screenPosition.X + GetParallaxOffset(particle.StoredPosition.X, 0.25f) + (float)Math.Sin(particle.Timer / 400f * 6.28f) * 20;
            particle.Position.Y = particle.StoredPosition.Y - Main.screenPosition.Y + GetParallaxOffsetY(particle.StoredPosition.Y, 0.1f);

            int factor = (int)(particle.Timer / 1800f * 100f);
            particle.Color = new Color(155 + factor, 155 + factor, 255) * (particle.Timer / 1800f * 0.8f);
            particle.Scale = (particle.Timer / 1800f * 1.2f);
            particle.Rotation += 0.02f;
            //particle.Position -= Main.screenPosition;
        }

        private static void UpdateBackgroundBody(Particle particle)
        {
            particle.Timer--;
            particle.StoredPosition += particle.Velocity;
            particle.Position.X = particle.StoredPosition.X - Main.screenPosition.X + GetParallaxOffset(particle.StoredPosition.X, 0.5f) + (float)Math.Sin(particle.Timer / 400f * 6.28f) * 10;
            particle.Position.Y = particle.StoredPosition.Y - Main.screenPosition.Y + GetParallaxOffsetY(particle.StoredPosition.Y, 0.2f);
            particle.Color = Color.White * (particle.Timer / 1800f * 0.5f);
            particle.Scale = (particle.Timer / 1800f * 0.8f);
            particle.Rotation += 0.02f;
            //particle.Position -= Main.screenPosition;
        }

        private void DrawVitricBackground(On.Terraria.Main.orig_DrawBackgroundBlackFill orig, Main self)
        {
            orig(self);

            if (Main.gameMenu) return;

            Player player = null;
            if (Main.playerLoaded) { player = Main.LocalPlayer; }

            if (player != null && StarlightWorld.VitricBiome.Contains((player.Center / 16).ToPoint()))
            {
                Vector2 basepoint = (StarlightWorld.VitricBiome != null) ? StarlightWorld.VitricBiome.TopLeft() * 16 + new Vector2(-2000, 0) : Vector2.Zero;

                DrawLayer(basepoint, ModContent.GetTexture("StarlightRiver/Backgrounds/Glass5"), 0, 300); //the background

                DrawLayer(basepoint, ModContent.GetTexture("StarlightRiver/Backgrounds/Glass1"), 5, 170, new Color(150, 175, 190)); //the back sand
                DrawLayer(basepoint, ModContent.GetTexture("StarlightRiver/Backgrounds/Glass1"), 5.5f, 400, new Color(120, 150, 170), true); //the back sand on top

                BackgroundParticles.DrawParticles(Main.spriteBatch);

                for (int k = 4; k >= 0; k--)
                {
                    int off = 140 + (440 - k * 110);
                    if (k == 4) off = 400;
                    DrawLayer(basepoint, ModContent.GetTexture("StarlightRiver/Backgrounds/Glass" + k), k + 1, off); //the crystal layers and front sand
                    if (k == 0) DrawLayer(basepoint, ModContent.GetTexture("StarlightRiver/Backgrounds/Glass1"), 0.5f, 100, new Color(180, 220, 235), true); //the sand on top
                    if (k == 2)
                    {
                        ForegroundParticles.DrawParticles(Main.spriteBatch);
                    }
                }

                int screenCenterX = (int)(Main.screenPosition.X + Main.screenWidth / 2);
                for (int k = (int)(screenCenterX - basepoint.X) - (int)(Main.screenWidth * 1.5f); k <= (int)(screenCenterX - basepoint.X) + (int)(Main.screenWidth * 1.5f); k += 30)
                {
                    Vector2 spawnPos = basepoint + new Vector2(2000 + Main.rand.Next(8000), 1800);
                    if (Main.rand.Next(1200) == 0)
                    {
                        BackgroundParticles.AddParticle(new Particle(new Vector2(0, basepoint.Y + 1550), new Vector2(0, Main.rand.NextFloat(-1.6f, -0.6f)), 0, 0, Color.White, 1800, spawnPos));
                    }

                    if (Main.rand.Next(1000) == 0)
                    {
                        ForegroundParticles.AddParticle(new Particle(new Vector2(0, basepoint.Y + 1550), new Vector2(0, Main.rand.NextFloat(-1.6f, -0.6f)), 0, 0, Color.White, 1800, spawnPos));
                    }
                }

                for (int i = -2 + (int)(Main.screenPosition.X) / 16; i <= 2 + (int)(Main.screenPosition.X + Main.screenWidth) / 16; i++)
                {
                    for (int j = -2 + (int)(Main.screenPosition.Y) / 16; j <= 2 + (int)(Main.screenPosition.Y + Main.screenHeight) / 16; j++)
                    {
                        if (Lighting.Brightness(i, j) == 0 || ((Main.tile[i, j].active() && Main.tile[i, j].collisionType == 1) || Main.tile[i, j].wall != 0))
                        {
                            Color color = Color.Black * (1 - Lighting.Brightness(i, j) * 2);
                            Main.spriteBatch.Draw(Main.blackTileTexture, new Vector2(i * 16, j * 16) - Main.screenPosition, color);
                        }
                    }
                }
            }
        }

        public static void DrawLayer(Vector2 basepoint, Texture2D texture, float parallax, int offY = 0, Color color = default, bool flip = false)
        {
            if (color == default) color = Color.White;
            for (int k = 0; k <= 5; k++)
            {
                float x = basepoint.X + (k * 739 * 4) + GetParallaxOffset(basepoint.X, parallax * 0.1f) - (int)Main.screenPosition.X;
                float y = basepoint.Y + offY - (int)Main.screenPosition.Y + GetParallaxOffsetY(basepoint.Y + StarlightWorld.VitricBiome.Height * 8, parallax * 0.04f);
                if (x > -texture.Width && x < Main.screenWidth + 30)
                {
                    Main.spriteBatch.Draw(texture, new Vector2(x, y), new Rectangle(0, 0, 2956, 1528), color, 0f, Vector2.Zero, 1f, flip ? SpriteEffects.FlipVertically : 0, 0);
                }
            }
        }

        public static int GetParallaxOffset(float startpoint, float factor)
        {
            float vanillaParallax = 1 - (Main.caveParallax - 0.8f) / 0.2f;
            return (int)((Main.screenPosition.X + Main.screenWidth / 2 - startpoint) * factor * vanillaParallax);
        }

        public static int GetParallaxOffsetY(float startpoint, float factor)
        {
            //float vanillaParallax = 1 - (Main.caveParallax - 0.8f) / 0.2f;
            return (int)((Main.screenPosition.Y + Main.screenHeight / 2 - startpoint) * factor /* vanillaParallax*/);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Configs;
using StarlightRiver.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver
{
    public static partial class Helper
    {
        public static void DrawWithLighting(Vector2 pos, Texture2D tex)
        {
            if (!OnScreen(new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height))) return;

            int coarseness = GetInstance<Config>().Coarseness;
            int coarse16 = coarseness * 16;

            VertexPositionColorTexture[] verticies = new VertexPositionColorTexture[(tex.Width / coarse16 + 1) * (tex.Height / coarse16 * 6 + 1)];

            Color[,] colorCache = new Color[tex.Width / coarse16 + 1, tex.Height / coarse16 + 2];

            for (int x = 0; x < tex.Width / coarse16 + 1; x++)
                for (int y = 0; y < tex.Height / coarse16 + 2; y++)
                {
                    Vector2 target = pos + new Vector2(x, y) * coarse16;
                    colorCache[x, y] = Lighting.GetColor((int)(target.X + Main.screenPosition.X) / 16, (int)(target.Y + Main.screenPosition.Y) / 16);
                }

            int targetIndex = 0;
            for (int x = 0; x < tex.Width; x += coarse16)
                for (int y = 0; y < tex.Height; y += coarse16)
                {
                    Vector2 target = pos + new Vector2(x, y);

                    Color topLeft = colorCache[x / coarse16, y / coarse16];
                    Color topRight = colorCache[x / coarse16 + 1, y / coarse16];
                    Color bottomLeft = colorCache[x / coarse16, y / coarse16 + 1];
                    Color bottomRight = colorCache[x / coarse16 + 1, y / coarse16 + 1];

                    verticies[targetIndex] = (new VertexPositionColorTexture(new Vector3(ConvertX(target.X), ConvertY(target.Y), 0), topLeft, ConvertTex(new Vector2(x, y), tex))); targetIndex++;
                    verticies[targetIndex] = (new VertexPositionColorTexture(new Vector3(ConvertX(target.X + coarse16), ConvertY(target.Y), 0), topRight, ConvertTex(new Vector2(x + coarse16, y), tex))); targetIndex++;
                    verticies[targetIndex] = (new VertexPositionColorTexture(new Vector3(ConvertX(target.X + coarse16), ConvertY(target.Y + coarse16), 0), bottomRight, ConvertTex(new Vector2(x + coarse16, y + coarse16), tex))); targetIndex++;

                    verticies[targetIndex] = (new VertexPositionColorTexture(new Vector3(ConvertX(target.X), ConvertY(target.Y + coarse16), 0), bottomLeft, ConvertTex(new Vector2(x, y + coarse16), tex))); targetIndex++;
                    verticies[targetIndex] = (new VertexPositionColorTexture(new Vector3(ConvertX(target.X), ConvertY(target.Y), 0), topLeft, ConvertTex(new Vector2(x, y), tex))); targetIndex++;
                    verticies[targetIndex] = (new VertexPositionColorTexture(new Vector3(ConvertX(target.X + coarse16), ConvertY(target.Y + coarse16), 0), bottomRight, ConvertTex(new Vector2(x + coarse16, y + coarse16), tex))); targetIndex++;
                }
                
            if (verticies.Length >= 3) //cant draw a triangle with < 3 points fucktard
            {
                VertexBuffer buffer = new VertexBuffer(Main.instance.GraphicsDevice, typeof(VertexPositionColorTexture), verticies.Length, BufferUsage.WriteOnly);
                buffer.SetData(verticies);

                Main.instance.GraphicsDevice.SetVertexBuffer(buffer);

                BasicEffect basicEffect = new BasicEffect(Main.instance.GraphicsDevice)
                {
                    VertexColorEnabled = true,
                    TextureEnabled = true,
                    Texture = tex
                };

                foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    Main.instance.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, verticies.Length / 3);
                }
            }
        }

        private static float ConvertX(float input) => input / (Main.screenWidth / 2) - 1;

        private static float ConvertY(float input) => -1 * (input / (Main.screenHeight / 2) - 1);

        private static Vector2 ConvertTex(Vector2 input, Texture2D tex)
        {
            float x = input.X / tex.Width;
            float y = input.Y / tex.Height;
            return new Vector2(x, y);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace StarlightRiver.GUI
{
    public enum OverlayState : int
    {
        Rift = 1,
        CorruptJungle = 2,
        BloodyJungle = 3,
        HolyJungle = 4,
        Overgrow = 5
    }
    public class Overlay : UIState
    {
        public static bool visible = true;
        public static int state = 0;
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Bootlegdust.ForEach(VoidDust => VoidDust.Draw(spriteBatch));
            Recalculate();
        }

        internal static readonly List<BootlegDust> Bootlegdust = new List<BootlegDust>();
        public override void Update(GameTime gameTime)
        {
            BiomeHandler player = Main.LocalPlayer.GetModPlayer<BiomeHandler>();
            Bootlegdust.ForEach(dust => dust.Update());
            Bootlegdust.RemoveAll(dust => dust.time <= 0);

            if (visible)
            {
                if(!(player.ZoneVoidPre || player.ZoneJungleCorrupt || player.ZoneJungleBloody || player.ZoneJungleHoly || player.ZoneOvergrow))
                {
                    state = 0;
                }

                if (state == (int)OverlayState.Rift)
                {
                    for (int k = 0; k <= Main.screenHeight; k++)
                    {
                        if (k % Main.rand.Next(3, 13) == 0 && Main.rand.Next(30) == 0)
                        {
                            VoidDust dus = new VoidDust(ModContent.GetTexture("StarlightRiver/GUI/Fire"), new Vector2(-15, k), new Vector2(Main.rand.NextFloat(1, 2), 0));
                            VoidDust dus2 = new VoidDust(ModContent.GetTexture("StarlightRiver/GUI/Fire"), new Vector2(Main.screenWidth, k), new Vector2(-Main.rand.NextFloat(1, 2), 0));
                            Bootlegdust.Add(dus);
                            Bootlegdust.Add(dus2);
                        }
                    }
                }

                if (state == (int)OverlayState.CorruptJungle)
                {
                    for (int k = 0; k <= Main.screenWidth; k++)
                    {
                        if (k % Main.rand.Next(5, 15) == 0 && Main.rand.Next(1000) == 0)
                        {
                            EvilDust dus = new EvilDust(ModContent.GetTexture("StarlightRiver/GUI/Corrupt"), new Vector2(k, Main.screenHeight), new Vector2(0, -1.4f));
                            Bootlegdust.Add(dus);
                        }
                    }
                }

                if (state == (int)OverlayState.BloodyJungle)
                {
                    for (int k = 0; k <= Main.screenWidth; k++)
                    {
                        if (k % Main.rand.Next(5, 15) == 0 && Main.rand.Next(1500) == 0)
                        {
                            BloodDust dus = new BloodDust(ModContent.GetTexture("StarlightRiver/GUI/Blood"), new Vector2(k, 0), new Vector2(0, 2f));
                            Bootlegdust.Add(dus);
                        }
                    }
                }

                if (state == (int)OverlayState.HolyJungle)
                {
                    for (int k = 0; k <= Main.screenWidth; k++)
                    {
                        if (k % Main.rand.Next(20, 40) == 0 && Main.rand.Next(750) == 0)
                        {
                            HolyDust dus = new HolyDust(ModContent.GetTexture("StarlightRiver/GUI/Light"), new Vector2(k, Main.rand.Next(Main.screenHeight)), Vector2.Zero);
                            Bootlegdust.Add(dus);
                        }
                    }
                }

                /*if (state == (int)OverlayState.Overgrow)
                {
                    for (int k = 0; k <= Main.screenWidth; k++)
                    {
                        if (k % Main.rand.Next(20, 40) == 0 && Main.rand.Next(550) == 0)
                        {
                            HolyDust dus = new HolyDust(ModContent.GetTexture("StarlightRiver/GUI/Holy"), new Vector2(k, Main.rand.Next(Main.screenHeight)), Vector2.Zero);
                            Bootlegdust.Add(dus);
                        }
                    }
                }*/
            }
        }
    }

    public class VoidDust : BootlegDust
    {
        public VoidDust(Texture2D texture, Vector2 position, Vector2 velocity) :
            base(texture, position, velocity, Color.Black, Main.rand.NextFloat(1, 10), 140)
        {
        }

        public override void Update()
        {
            col *= 0.99f;
            pos += vel;
            scl *= 0.99f;
            time--;
        }
    }

    public class EvilDust : BootlegDust
    {
        public EvilDust(Texture2D texture, Vector2 position, Vector2 velocity) :
            base(texture, position, velocity, Color.White * 0.9f, 1.5f, 550)
        {
        }

        public override void Update()
        {
            col *= 0.999999972f;
            pos += vel;
            scl *= 0.996f;
            time--;
            pos.X += (float)Math.Sin((float)(time / 550f * 12.56f));
        }
    }
    public class BloodDust : BootlegDust
    {
        public BloodDust(Texture2D texture, Vector2 position, Vector2 velocity) :
            base(texture, position, velocity, Color.White, 2.5f, 600)
        {
        }

        public override void Update()
        {

            col *= 0.99999948f;
            pos += vel;
            vel += new Vector2(0, 0.07f);
            scl *= 0.987f;

            time--;
            pos.X += (float)Math.Sin((float)(time / 550f * 31.4f)) * 0.25f;
        }
    }
    public class HolyDust : BootlegDust
    {
        public HolyDust(Texture2D texture, Vector2 position, Vector2 velocity) :
            base(texture, position, velocity, Color.White * 0.1f, 0.1f, 200)
        {
        }

        public override void Update()
        {
            if(time >= 100)
            {
                col *= 1.05f;
                scl *= 1.025f;
            }
            else
            {
                col *= 0.98f;
                scl *= 0.99f;
            }

            time--;
            pos.X += vel.X + (float)Math.Cos((time / 550f * 31.4f)) * 0.5f;
            pos.Y += vel.Y + (float)Math.Sin((time / 550f * 31.4f)) * 0.5f;
        }
    }
    public class VitricDust : BootlegDust
    {
        Vector2 Basepos = Vector2.Zero;
        int Offset = 0;
        float Parallax;
        float Velocity;
        float rot = Main.rand.NextFloat(6.28f);
        public VitricDust(Texture2D texture, Vector2 basepos, int offset, float scale, float alpha, float parallax) :
            base(texture, basepos, new Vector2(0, -1), new Color(130, 205, 215) * alpha, scale + Main.rand.NextFloat(0, 0.6f), 1500)
        {
            Basepos = basepos;
            Offset = offset;
            Parallax = parallax;
            Velocity = Main.rand.NextFloat(3.4f, 6.2f);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, new Rectangle(0, 0, 32, 22), col, rot, default, scl, default, 0);
        }

        public override void Update()
        {
            col *= 0.9999999994f;
            //pos += vel;
            float veloff = (Parallax > 0.2) ? 0.2f : 0.1f;
            float off = Basepos.X + Offset + (StarlightRiver.Instance.GetParallaxOffset(Basepos.X, 0.5f) - Parallax*(Main.screenPosition.X + Main.screenWidth / 2 - Basepos.X));
            pos.X = (off) - Main.screenPosition.X;
            pos.Y = ((Basepos.Y + 256) - (1500 * veloff * Velocity - time * veloff * Velocity) - Main.screenPosition.Y);
            scl *= (Parallax > 0.2) ? 0.997f : 0.999f;
            rot += 0.015f;
            time--;
        }
    }
}
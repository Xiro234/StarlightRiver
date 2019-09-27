using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Ability;
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
    public class Overlay : UIState
    {
        public static bool visible = false;
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
            Bootlegdust.ForEach(VoidDust => VoidDust.Update());
            Bootlegdust.RemoveAll(VoidDust => VoidDust.time <= 0);

            if (visible)
            {
                if (state == 1)
                {
                    if (Main.LocalPlayer.GetModPlayer<AbilityHandler>(StarlightRiver.Instance).unlock[2] == 1)
                    {
                        for (int k = 0; k <= Main.screenWidth; k++)
                        {
                            if (k % Main.rand.Next(5, 15) == 0 && Main.rand.Next(4) == 0)
                            {
                                VoidDust dus = new VoidDust(ModContent.GetTexture("StarlightRiver/GUI/Fire"), new Vector2(k, -10), new Vector2(0, 2));
                                VoidDust dus2 = new VoidDust(ModContent.GetTexture("StarlightRiver/GUI/Fire"), new Vector2(k, Main.screenHeight), new Vector2(0, -2));
                                Bootlegdust.Add(dus);
                                Bootlegdust.Add(dus2);
                            }
                        }
                        for (int k = 0; k <= Main.screenHeight; k++)
                        {
                            if (k % Main.rand.Next(5, 15) == 0 && Main.rand.Next(4) == 0)
                            {
                                VoidDust dus = new VoidDust(ModContent.GetTexture("StarlightRiver/GUI/Fire"), new Vector2(-15, k), new Vector2(2, 0));
                                VoidDust dus2 = new VoidDust(ModContent.GetTexture("StarlightRiver/GUI/Fire"), new Vector2(Main.screenWidth, k), new Vector2(-2, 0));
                                Bootlegdust.Add(dus);
                                Bootlegdust.Add(dus2);
                            }
                        }
                    }
                    else
                    {
                        for (int k = 0; k <= Main.screenWidth; k++)
                        {
                            if (k % Main.rand.Next(3, 13) == 0 && Main.rand.Next(4) == 0)
                            {
                                VoidDust2 dus = new VoidDust2(ModContent.GetTexture("StarlightRiver/GUI/Fire"), new Vector2(k, -10), new Vector2(0, 2));
                                VoidDust2 dus2 = new VoidDust2(ModContent.GetTexture("StarlightRiver/GUI/Fire"), new Vector2(k, Main.screenHeight), new Vector2(0, -2));
                                Bootlegdust.Add(dus);
                                Bootlegdust.Add(dus2);
                            }
                        }
                        for (int k = 0; k <= Main.screenHeight; k++)
                        {
                            if (k % Main.rand.Next(3, 13) == 0 && Main.rand.Next(4) == 0)
                            {
                                VoidDust2 dus = new VoidDust2(ModContent.GetTexture("StarlightRiver/GUI/Fire"), new Vector2(-15, k), new Vector2(2, 0));
                                VoidDust2 dus2 = new VoidDust2(ModContent.GetTexture("StarlightRiver/GUI/Fire"), new Vector2(Main.screenWidth, k), new Vector2(-2, 0));
                                Bootlegdust.Add(dus);
                                Bootlegdust.Add(dus2);
                            }
                        }
                    }
                }

                if (state == 2)
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

                if (state == 3)
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

                if (state == 4)
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
            }
            if 
                (
                !Main.LocalPlayer.GetModPlayer<BiomeHandler>(StarlightRiver.Instance).ZoneVoidPre &&
                !Main.LocalPlayer.GetModPlayer<BiomeHandler>(StarlightRiver.Instance).ZoneJungleCorrupt &&
                !Main.LocalPlayer.GetModPlayer<BiomeHandler>(StarlightRiver.Instance).ZoneJungleBloody &&
                !Main.LocalPlayer.GetModPlayer<BiomeHandler>(StarlightRiver.Instance).ZoneJungleHoly
                )
            {
                Bootlegdust.Clear();
            }

            if(state == 0)
            {
                Bootlegdust.Clear();
            }
        }
    }

    public class VoidDust : BootlegDust
    {
        public VoidDust(Texture2D texture, Vector2 position, Vector2 velocity) :
            base(texture, position, velocity, Color.Black, 8f, 80)
        {
        }

        public override void Update()
        {
            col *= 0.98f;
            pos += vel;
            scl *= 0.97f;
            time--;
        }
    }
    public class VoidDust2 : BootlegDust
    {
        public VoidDust2(Texture2D texture, Vector2 position, Vector2 velocity) :
            base(texture, position, velocity, Color.Black, 8f, 160)
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
            pos.X += (float)Math.Cos((float)(time / 550f * 31.4f)) * 0.5f;
            pos.Y += (float)Math.Sin((float)(time / 550f * 31.4f)) * 0.5f;
        }
    }
}
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
            }
            if (!Main.LocalPlayer.GetModPlayer<BiomeHandler>(StarlightRiver.Instance).ZoneVoidPre && !Main.LocalPlayer.GetModPlayer<BiomeHandler>(StarlightRiver.Instance).ZoneJungleCorrupt)
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
            base(texture, position, velocity, Color.White, 2f, 550)
        {
        }

        public override void Update()
        {
            col *= 0.999999978f;
            pos += vel;
            scl *= 0.996f;
            time--;
            pos.X += (float)Math.Sin((float)(time / 550f * 12.56f));
        }
    }
}
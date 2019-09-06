using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace spritersguildwip.GUI
{
    public class Overlay : UIState
    {
        public static bool visible = false;
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Bootlegdust.ForEach(VoidDust => VoidDust.Draw(spriteBatch));
            Recalculate();
        }

        internal static readonly List<VoidDust> Bootlegdust = new List<VoidDust>();
        public override void Update(GameTime gameTime)
        {
            Bootlegdust.ForEach(VoidDust => VoidDust.Update());
            Bootlegdust.RemoveAll(VoidDust => VoidDust.time <= 0);

            if (visible)
            {
                for (int k = 0; k <= Main.screenWidth; k++)
                {
                    if (k % Main.rand.Next(5,15) == 0 && Main.rand.Next(4) == 0)
                    {
                        VoidDust dus = new VoidDust(ModContent.GetTexture("spritersguildwip/GUI/Fire"), new Vector2(k, -10), new Vector2(0, 2));
                        VoidDust dus2 = new VoidDust(ModContent.GetTexture("spritersguildwip/GUI/Fire"), new Vector2(k, Main.screenHeight), new Vector2(0, -2));
                        Bootlegdust.Add(dus);
                        Bootlegdust.Add(dus2);
                    }
                }
                for (int k = 0; k <= Main.screenHeight; k++)
                {
                    if (k % Main.rand.Next(5, 15) == 0 && Main.rand.Next(4) == 0)
                    {
                        VoidDust dus = new VoidDust(ModContent.GetTexture("spritersguildwip/GUI/Fire"), new Vector2(-15, k), new Vector2(2, 0));
                        VoidDust dus2 = new VoidDust(ModContent.GetTexture("spritersguildwip/GUI/Fire"), new Vector2(Main.screenWidth, k), new Vector2(-2, 0));
                        Bootlegdust.Add(dus);
                        Bootlegdust.Add(dus2);
                    }
                }
            }
            if (!Main.LocalPlayer.GetModPlayer<BiomeHandler>(spritersguildwip.Instance).ZoneVoidPre)
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
}
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

        internal static readonly List<BootlegDust> Bootlegdust = new List<BootlegDust>();
        public override void Update(GameTime gameTime)
        {
            Bootlegdust.ForEach(VoidDust => VoidDust.Update());
            Bootlegdust.RemoveAll(VoidDust => VoidDust.time <= 0);

            if (visible)
            {
                for (int k = 0; k <= Main.screenWidth; k++)
                {
                    new VoidDust(ModContent.GetTexture("spritersguildwip/GUI/Fire"), new Vector2(k, 0), new Vector2(0, 4), new Color(0, 0, 0), 8f, 180);
                    new VoidDust(ModContent.GetTexture("spritersguildwip/GUI/Fire"), new Vector2(k, Main.screenHeight), new Vector2(0, -4), new Color(0, 0, 0), 8f, 180);
                }
                for (int k = 0; k <= Main.screenHeight; k++)
                {
                    new VoidDust(ModContent.GetTexture("spritersguildwip/GUI/Fire"), new Vector2(0, k), new Vector2(4, 0), new Color(0, 0, 0), 8f, 180);
                    new VoidDust(ModContent.GetTexture("spritersguildwip/GUI/Fire"), new Vector2(Main.screenWidth, k) , new Vector2(-4, 0), new Color(0, 0, 0), 8f, 180);
                }
            }
        }
    }

    public class VoidDust : BootlegDust
    {
        Texture2D tex;
        Vector2 pos;
        Vector2 vel;
        Color col;
        float scl;
        new public int time;
        public VoidDust(Texture2D texture, Vector2 position, Vector2 velocity, Color color, float scale, int timeleft) : base(null, Vector2.Zero, Vector2.Zero, Color.Black, 0, 0)
        {
            tex = texture;
            pos = position;
            vel = velocity;
            col = color;
            scl = scale;
            time = timeleft;
        }

        public override void Update()
        {
            pos += vel;
            scl *= 0.94f;
            time--;
        }
    }
}

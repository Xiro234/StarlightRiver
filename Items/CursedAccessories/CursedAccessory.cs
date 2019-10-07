using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.CursedAccessories
{
    class CursedAccessory : ModItem
    {
        Texture2D Glow = null;
        public CursedAccessory(Texture2D glow)
        {
            Glow = glow;
        }

        internal static readonly List<BootlegDust> Bootlegdust = new List<BootlegDust>();
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Color color = Color.White * (float)Math.Sin(LegendWorld.rottime);
            spriteBatch.Draw(Glow, position, new Rectangle(0,0,32,32), color, 0, origin, scale, SpriteEffects.None, 0);

            Bootlegdust.ForEach(BootlegDust => BootlegDust.Draw(spriteBatch));


                BootlegDust dus = new CurseDust(ModContent.GetTexture("StarlightRiver/GUI/Dark"), position + new Vector2(Main.rand.Next(0, frame.Width - 4), Main.rand.Next(0, frame.Height - 4)), new Vector2(0, -0.4f), Color.White * 0.1f, 1.5f, 60);
                Bootlegdust.Add(dus);

        }
    }

    public class CurseDust : BootlegDust
    {
        public CurseDust(Texture2D texture, Vector2 position, Vector2 velocity, Color color, float scale, int time) :
            base(texture, position, velocity, color, scale, time)
        {
        }

        public override void Update()
        {
            if (time > 20 && col.R < 255)
            {
                col *= 1.2f;
            }
            if (time <= 20)
            {
                col *= 0.78f;
            }

            scl *= 0.97f;
            pos += vel;
            
            time--;
        }
    }
}

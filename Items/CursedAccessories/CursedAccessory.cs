using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.GUI;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.CursedAccessories
{
    public class CursedAccessory : ModItem
    {
        private readonly Texture2D Glow = null;
        private Vector2 drawpos = Vector2.Zero;

        public CursedAccessory(Texture2D glow)
        {
            Glow = glow;
        }

        //internal static readonly List<BootlegDust> Bootlegdust = new List<BootlegDust>();

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Color color = Color.White * (float)Math.Sin(LegendWorld.rottime);
            spriteBatch.Draw(Glow, position, new Rectangle(0, 0, 32, 32), color, 0, origin, scale, SpriteEffects.None, 0);
            /*
            Bootlegdust.ForEach(BootlegDust => BootlegDust.Draw(spriteBatch));

            BootlegDust dus = new CurseDust(ModContent.GetTexture("StarlightRiver/GUI/Dark"), (position) + frame.Size() / 4 - Vector2.One + (Vector2.One * Main.rand.Next(12)).RotatedBy(Main.rand.NextFloat(0, 6.28f)), new Vector2(0, -0.4f), Color.White * 0.1f, 1.5f, 60);
            Bootlegdust.Add(dus);
            */

            drawpos = position - new Vector2((frame.Width / 2), (frame.Width / 2));
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            Main.PlaySound(SoundID.NPCHit55);
            Main.PlaySound(SoundID.Item123);
            for (int k = 0; k <= 175; k++)
            {
                //BootlegDust dus = new CurseDust2(ModContent.GetTexture("StarlightRiver/GUI/Dark"), drawpos, Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(0, 0.8f), Color.White * 0.4f, 3.8f, 180);
                //Bootlegdust.Add(dus);
            }

            return true;
        }
    }

    /*public class CurseDust : BootlegDust
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

    public class CurseDust2 : BootlegDust
    {
        public CurseDust2(Texture2D texture, Vector2 position, Vector2 velocity, Color color, float scale, int time) :
            base(texture, position, velocity, color, scale, time)
        {
        }

        public override void Update()
        {
            if (time <= 20)
            {
                col *= 0.94f;
            }

            scl *= 0.98f;
            pos += vel;

            time--;
        }
    }
    */
}
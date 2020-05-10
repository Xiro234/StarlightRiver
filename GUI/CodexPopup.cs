using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;


namespace StarlightRiver.GUI
{
    public class CodexPopup : UIState
    {
        private string Text;
        public int Timer;
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.GetTexture("StarlightRiver/GUI/Book1Closed");
            string str = "New Entry: " + Text;
            float xOff = Main.screenWidth / 2 - Main.fontDeathText.MeasureString(str).X / 4;

            Vector2 pos = Timer > 120 ? new Vector2(xOff, Main.screenHeight - 60) : new Vector2(xOff, (Main.screenHeight - 60) + (120 - Timer));
            Color col = Timer > 120 ? Color.White : Color.White * (Timer / 120f);
            spriteBatch.Draw(tex, pos, col);
            Utils.DrawBorderString(spriteBatch, str, pos + new Vector2(40, 8), col);
            Timer--;
        }
        public void TripEntry(string text)
        {
            Text = text;
            Timer = 240;
        }
    }


}

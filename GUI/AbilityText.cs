using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.UI;
using Terraria;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace StarlightRiver.GUI
{
    public class AbilityText : UIState
    {
        public static bool Visible = false;

        private Ability Thisability;
        private string Title;
        private string Message;
        private int Timer = 0;
        private bool used = false;

        private int tempTime = 0;
        private int tempTimeMax = 0;

        public void Display(string title, string message, Ability ability = null, int time = 0)
        {
            Thisability = ability;
            Title = title;
            Message = message;
            Visible = true;
            used = false;
            tempTimeMax = time;
            tempTime = 0;
            Timer = 1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int TitleLength = (int)(Main.fontDeathText.MeasureString(Title).X * 0.65f) / 2;
            int MessageLength = (int)(Main.fontDeathText.MeasureString(Message).X * 0.4f) / 2;
            int Longest = MessageLength > TitleLength ? MessageLength : TitleLength;
            Color color = Color.White * (Timer / 120f);

            spriteBatch.End();
            spriteBatch.Begin(default, BlendState.AlphaBlend);

            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Keys/Glow"), new Rectangle(Main.screenWidth / 2 - Longest * 2, 125, Longest * 4, 150), Color.Black * 0.6f * (Timer / 120f));

            spriteBatch.End();
            spriteBatch.Begin();

            spriteBatch.DrawString(Main.fontDeathText, Title, new Vector2(Main.screenWidth / 2 - TitleLength, 160), color, 0f, Vector2.Zero, 0.65f, 0, 0);
            spriteBatch.DrawString(Main.fontDeathText, Message, new Vector2(Main.screenWidth / 2 - MessageLength, 200), color, 0f, Vector2.Zero, 0.4f, 0, 0);

            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/LineMid"), 
                new Rectangle(Main.screenWidth / 2 - (int)(Longest * 1.2f), 225, (int)(Longest * 2.4f), 6),
                color);
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/LineSide"), new Vector2(Main.screenWidth / 2 - (int)(Longest * 1.2f) - 34, 195), color);
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/LineSide"), new Rectangle(Main.screenWidth / 2 + (int)(Longest * 1.2f), 195, 34, 36), new Rectangle(0,0,34,36), color, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);

            if (Thisability != null)
            {
                if (Thisability.Active) { used = true; }

                if (used) { Timer--; }
                else if (Timer < 120) { Timer++; }

                if (Timer == 0) { Visible = false; }
            }

            else
            {
                if (tempTime < tempTimeMax) tempTime++;
                if (tempTime >= tempTimeMax) { Timer--; }
                else if (Timer < 120) { Timer++; }

                if (Timer == 0) { Visible = false; }
            }
        }
    }
}

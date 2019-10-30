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

        public void Display(Ability ability, string title, string message)
        {
            Thisability = ability;
            Title = title;
            Message = message;
            Visible = true;
            used = false;
            Timer = 1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int TitleLength = (int)(Main.fontDeathText.MeasureString(Title).X * 0.65f) / 2;
            int Length = (int)(Main.fontDeathText.MeasureString(Message).X * 0.4f) / 2;
            Color color = Color.White * (Timer / 120f);

            spriteBatch.DrawString(Main.fontDeathText, Title, new Vector2(Main.screenWidth / 2 - TitleLength, 160), color, 0f, Vector2.Zero, 0.65f, 0, 0);
            spriteBatch.DrawString(Main.fontDeathText, Message, new Vector2(Main.screenWidth / 2 - Length, 200), color, 0f, Vector2.Zero, 0.4f, 0, 0);

            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/LineMid"), 
                new Rectangle(Main.screenWidth / 2 - (int)(Length * 1.2f), 225, (int)(Length * 2.4f), 6),
                color);
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/LineSide"), new Vector2(Main.screenWidth / 2 - (int)(Length * 1.2f) - 34, 195), color);
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/LineSide"), new Rectangle(Main.screenWidth / 2 + (int)(Length * 1.2f), 195, 34, 36), new Rectangle(0,0,34,36), color, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);

            if (Thisability.Active) { used = true; }

            if (used) { Timer--; }
            else if (Timer < 120) { Timer++; }

            if (Timer == 0) { Visible = false; }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;
using StarlightRiver.Abilities;
using System.Collections.Generic;
using StarlightRiver.Codex;

namespace StarlightRiver.GUI
{
    public class Codex : UIState
    {
        public static bool Visible;
        public static bool Open;

        public UIImageButton BookButton = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/Book1Closed"));

        public override void OnInitialize()
        {
            BookButton.Left.Set(400, 0);
            BookButton.Top.Set(100, 0);
            BookButton.Width.Set(26, 0);
            BookButton.Height.Set(32, 0);
            BookButton.OnClick += OpenCodex;
            Append(BookButton);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            BookButton.Left.Set(502, 0);
            BookButton.Top.Set(276, 0);
            if (BookButton.IsMouseHovering || Open) { BookButton.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Book1Open")); }
            else { BookButton.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Book1Closed")); }

            if (Main.LocalPlayer.GetModPlayer<CodexHandler>().CodexState == 0) { BookButton.SetImage(ModContent.GetTexture("StarlightRiver/GUI/BookLocked")); };

            if (Open)
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Items/Debug/Kill"), new Vector2(600, 400), Color.White);
            }
            Recalculate();
        }

        private void OpenCodex(UIMouseEvent evt, UIElement listeningElement)
        {
            if (Main.LocalPlayer.GetModPlayer<CodexHandler>().CodexState != 0)
            {
                Open = true;
                Main.PlaySound(SoundID.MenuOpen);
            }
        }
    }
}

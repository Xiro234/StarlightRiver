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
using StarlightRiver.Codex.Entries;

namespace StarlightRiver.GUI
{
    public class Codex : UIState
    {
        public static bool Visible;
        public static bool Open;
        public CodexEntry ActiveEntry;

        public UIImageButton BookButton = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/Book1Closed"));
        public List<EntryButton> Buttons = new List<EntryButton>();
        public List<EntryButton> ShownButtons = new List<EntryButton>();

        public override void OnInitialize()
        {
            BookButton.Left.Set(400, 0);
            BookButton.Top.Set(100, 0);
            BookButton.Width.Set(26, 0);
            BookButton.Height.Set(32, 0);
            BookButton.OnClick += OpenCodex;
            Append(BookButton);

            for (int k = 0; k < 5; k++)
            {
                CategoryButton button = new CategoryButton(k);
                button.Left.Set(k * 60 - 240, 0.5f);
                button.Top.Set(-150, 0.5f);
                button.Width.Set(50, 0);
                button.Height.Set(24, 0);
                Append(button);
            }
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
                if (ActiveEntry != null) { ActiveEntry.Draw(new Vector2(Main.screenWidth / 2, Main.screenHeight / 2) - new Vector2(200, 100), spriteBatch); }
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

            if (!Elements.Any(element => element is EntryButton))
            {
                foreach (CodexEntry entry in Main.LocalPlayer.GetModPlayer<CodexHandler>().Entries)
                {
                    EntryButton button = new EntryButton(entry);
                    button.Visible = false;
                    Buttons.Add(button);
                    Append(button);
                }
            }
        }

        public void Refresh()
        {
            foreach(UIElement element in Elements.Where(element => element is EntryButton))
            {
                (element as EntryButton).Visible = false;
            }

            for (int k = 0; k < ShownButtons.Count; k++)
            {
                EntryButton button = ShownButtons.ElementAt(k);
                button.Left.Set(60, 0.5f);
                button.Top.Set(k * 30 - 120, 0.5f);
                button.Width.Set(120, 0);
                button.Height.Set(24, 0);
                button.Visible = true;
            }
        }
    }

    public class EntryButton : UIElement
    {
        public CodexEntry Entry;
        public bool Visible = true;
        public EntryButton(CodexEntry entry)
        {
            Entry = entry;
        }
        public override void Click(UIMouseEvent evt)
        {
            if (!Entry.Locked)
            {
                (Parent as Codex).ActiveEntry = Entry;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Codex.Open && Visible)
            {
                Vector2 pos = GetDimensions().ToRectangle().TopLeft();

                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/box"), new Rectangle((int)pos.X, (int)pos.Y, 120, 27), Color.White);

                spriteBatch.Draw(Entry.Locked ? ModContent.GetTexture("StarlightRiver/GUI/blank") : Entry.Icon,
                    new Rectangle((int)pos.X + 5, (int)pos.Y + 5, 16, 16), new Rectangle(0,0,32,32), Color.White);

                Utils.DrawBorderString(spriteBatch, Entry.Locked ? "???" : Entry.Title, pos + new Vector2(24, 7), (Parent as Codex).ActiveEntry == Entry ? Color.Yellow : Color.White, 0.6f);
            }
        }
    }

    public class CategoryButton : UIElement
    {
        int Category = 0;
        public CategoryButton(int category)
        {
            Category = category;
        }

        public override void Click(UIMouseEvent evt)
        {
            (Parent as Codex).ShownButtons.Clear();
            Main.NewText((Parent as Codex).Buttons.Count);
            foreach(EntryButton entry in (Parent as Codex).Buttons.Where(entry => entry.Entry.Category == Category))
            {
                (Parent as Codex).ShownButtons.Add(entry);
            }
            (Parent as Codex).Refresh();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Codex.Open)
            {
                Vector2 pos = GetDimensions().ToRectangle().TopLeft();
                String name = "";
                switch (Category)
                {
                    case 0: name = "Abilities"; break;
                    case 1: name = "Biomes"; break;
                    case 2: name = "Relics"; break;
                    case 3: name = "Bosses"; break;
                    case 4: name = "Misc"; break;
                }

                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/box"), new Rectangle((int)pos.X, (int)pos.Y, 50, 27), Color.White);
                Utils.DrawBorderString(spriteBatch, name, pos + new Vector2(7, 7), (Parent as Codex).ShownButtons.Any(button => button.Entry.Category == Category) ? Color.Yellow : Color.White, 0.6f);
            }
        }
    }
}

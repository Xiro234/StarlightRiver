using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using StarlightRiver.Codex;
using StarlightRiver.RiftCrafting;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace StarlightRiver.GUI
{
    public class Codex : UIState
    {
        public static bool Visible;
        public static bool Open;
        public static bool Crafting;
        public CodexEntry ActiveEntry;
        public RiftRecipe ActiveRecipe;
        public Vector2 Basepos = new Vector2(Main.screenWidth, Main.screenHeight) / 2;

        public UIImage DragButton = new UIImage(ModContent.GetTexture("StarlightRiver/GUI/DragButton"));
        public UIImageButton BookButton = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/Book1Closed"));
        public UIImageButton CraftButton = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/RiftButton"));
        public UIImageButton BackButton = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/BackButton"));
        public UIImageButton ExitButton = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/ExitButton"));
        public QuerySlot QuerySlot = new QuerySlot();
        public List<EntryButton> Buttons = new List<EntryButton>();
        public List<EntryButton> ShownButtons = new List<EntryButton>();
        public List<RecipeButton> Recipes = new List<RecipeButton>();
        public List<RecipeButton> ShownRecipes = new List<RecipeButton>();

        public List<BootlegDust> Dust = new List<BootlegDust>();

        public override void OnInitialize()
        {
            DragButton.Left.Set(-210, 0);
            DragButton.Top.Set(-210, 0);
            DragButton.Width.Set(38, 0);
            DragButton.Height.Set(38, 0);
            Append(DragButton);

            ExitButton.Left.Set(150, 0);
            ExitButton.Top.Set(-200, 0);
            ExitButton.Width.Set(28, 0);
            ExitButton.Height.Set(28, 0);
            ExitButton.OnClick += Exit;
            ExitButton.SetVisibility(0.8f, 0.8f);
            Append(ExitButton);

            BookButton.Left.Set(502, 0);
            BookButton.Top.Set(276, 0);
            BookButton.Width.Set(26, 0);
            BookButton.Height.Set(32, 0);
            BookButton.OnClick += OpenCodex;
            BookButton.SetVisibility(1, 1);
            Append(BookButton);

            CraftButton.Left.Set(-160, 0);
            CraftButton.Top.Set(-235, 0);
            CraftButton.Width.Set(76, 0);
            CraftButton.Height.Set(28, 0);
            CraftButton.OnClick += OpenCrafting;
            CraftButton.SetVisibility(0.8f, 0.8f);
            Append(CraftButton);

            BackButton.Left.Set(-210, 0);
            BackButton.Top.Set(-150, 0);
            BackButton.Width.Set(32, 0);
            BackButton.Height.Set(32, 0);
            BackButton.OnClick += CloseCrafting;
            BackButton.SetVisibility(0.8f, 0.8f);
            Append(BackButton);

            QuerySlot.Left.Set(160, 0);
            QuerySlot.Top.Set(-150, 0);
            QuerySlot.Width.Set(48, 0);
            QuerySlot.Height.Set(48, 0);
            Append(QuerySlot);

            for (int k = 0; k < 5; k++)
            {
                CategoryButton button = new CategoryButton(k);
                button.Left.Set(k * 60 - 160, 0);
                button.Top.Set(-200, 0);
                button.Width.Set(50, 0);
                button.Height.Set(24, 0);
                Append(button);
            }
        }

        private void Exit(UIMouseEvent evt, UIElement listeningElement)
        {
            Open = false;
            Crafting = false;
        }

        private bool Moving = false;
        public override void Update(GameTime gameTime)
        {
            if (DragButton.IsMouseHovering && Main.mouseLeft) Moving = true;
            if (!Main.mouseLeft) Moving = false;

            if (Moving) Basepos = Main.MouseScreen + Vector2.One * 200;
            if (Basepos.X < 20) Basepos.X = 20;
            if (Basepos.Y < 270) Basepos.Y = 270;
            if (Basepos.X > Main.screenWidth - 20 - 260) Basepos.X = Main.screenWidth - 20 - 260;
            if (Basepos.Y > Main.screenHeight - 20 - 200) Basepos.Y = Main.screenHeight - 20 - 200;

            foreach(UIElement element in Elements.Where(n => n != BookButton && n.Left.Percent != 2))
            {
                element.Left.Percent = Basepos.X / Main.screenWidth;
                element.Top.Percent = Basepos.Y / Main.screenHeight;
            }

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //regular codex open
            if (Open)
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/CodexBack"), Basepos - new Vector2(170, 168), Color.White * 0.8f);
                if (ActiveEntry != null) { ActiveEntry.Draw(Basepos - new Vector2(120, 150), spriteBatch); }

                BackButton.Left.Set(0, 2);
                CraftButton.Left.Set(-160, Basepos.X / Main.screenWidth);
                QuerySlot.Left.Set(0, 2);
            }
            //RC open
            else if (Crafting)
            {
                Texture2D tex = (ActiveRecipe == null) ? ModContent.GetTexture("StarlightRiver/GUI/RecipeBack") : ModContent.GetTexture("StarlightRiver/GUI/RecipeBackDark");
                spriteBatch.Draw(tex, Basepos - new Vector2(170, 168), Color.White * 0.8f);

                CraftButton.Left.Set(0, 2);
                BackButton.Left.Set(-210, Basepos.X / Main.screenWidth);
                QuerySlot.Left.Set(160, Basepos.X / Main.screenWidth);
            }
            //Nothing open
            else
            {
                DragButton.Left.Set(0, 2);
                ExitButton.Left.Set(0, 2);
                CraftButton.Left.Set(0, 2);
                BackButton.Left.Set(0, 2);
                QuerySlot.Left.Set(0, 2);
            }

            //Draw Bootlegdusts
            foreach (BootlegDust dust in Dust)
            {
                dust.Update();
                dust.Draw(spriteBatch);
            }
            Dust.RemoveAll(dust => dust.time <= 0);

            //Draw actual UI elements
            base.Draw(spriteBatch);

            //Swap the button textures
            if (BookButton.IsMouseHovering || Open || Crafting) { BookButton.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Book1Open")); }
            else { BookButton.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Book1Closed")); }

            //Glow for new entry
            if (Main.LocalPlayer.GetModPlayer<CodexHandler>().Entries.Any(n => n.New))
            {
                spriteBatch.Draw((BookButton.IsMouseHovering || Open || Crafting) ? ModContent.GetTexture("StarlightRiver/GUI/BookGlowOpen") : ModContent.GetTexture("StarlightRiver/GUI/BookGlowClosed"),
                    BookButton.GetDimensions().ToRectangle().TopLeft() + new Vector2(-1, 0), Color.White * (0.5f + (float)Math.Sin(LegendWorld.rottime * 2) * 0.25f));
            }

            //if locked
            if (Main.LocalPlayer.GetModPlayer<CodexHandler>().CodexState == 0) { BookButton.SetImage(ModContent.GetTexture("StarlightRiver/GUI/BookLocked")); };

            //bootlegdust for crafing 
            if (Crafting && ShownRecipes.Count > 0 && ActiveRecipe == null)
            {
                Vector2 pos = Basepos + new Vector2(-10, -10);
                Texture2D tex = ModContent.GetTexture("StarlightRiver/GUI/Light");
                Dust.Add(new HolyDust(tex, pos + Vector2.One.RotatedByRandom(6.28f) * 80, Vector2.Zero));
            }

            if (Crafting && ActiveRecipe != null)
            {
                Texture2D tex = ModContent.GetTexture("StarlightRiver/GUI/Holy");
                Vector2 pos = Basepos + new Vector2(-15, -15);
                DrawItem(spriteBatch, pos, ActiveRecipe.Result);

                List<int> DrawnItems = new List<int>();
                DrawnItems.Add(ModContent.ItemType<Items.RiftCore1>() + (ActiveRecipe.Tier - 1));
                for (int k = 0; k < ActiveRecipe.Ingredients.Count; k++)
                {
                    for (int l = 0; l < ActiveRecipe.Ingredients[k].count; l++)
                    {
                        DrawnItems.Add(ActiveRecipe.Ingredients[k].type);
                    }
                }

                for (int k = 0; k < DrawnItems.Count; k++)
                {
                    float rot = k / (float)DrawnItems.Count * 6.28f;
                    Vector2 pos2 = pos + new Vector2(0, 110).RotatedBy(rot);
                    DrawItem(spriteBatch, pos2, DrawnItems[k]);

                    float offset = (6.28f - LegendWorld.rottime) / 6.28f * 100;
                    Dust.Add(new ExpertDust(tex, pos + new Vector2((float)Math.Sin(LegendWorld.rottime * 5) * 3, offset).RotatedBy(rot), Vector2.Zero, Color.White, 1, 30));
                    Dust.Add(new ExpertDust(tex, pos + new Vector2(0, Main.rand.NextFloat(100)).RotatedBy(rot), Vector2.Zero, Color.White * 0.6f, 0.6f, 30));
                }
                Dust.Add(new ExpertDust(tex, pos, Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(1), Color.White * 0.7f, 1.5f, 30));
            }

            Recalculate();
        }
        private void DrawItem(SpriteBatch spriteBatch, Vector2 pos, int type)
        {
            Item item = new Item();
            item.type = type;
            item.SetDefaults(item.type);
            Texture2D tex = (item.modItem != null) ? ModContent.GetTexture(item.modItem.Texture) : ModContent.GetTexture("Terraria/Item_" + item.type);
            Rectangle frame = Main.itemAnimations[item.type] != null ? Main.itemAnimations[item.type].GetFrame(tex) : tex.Frame();

            spriteBatch.Draw(tex, pos, frame, Color.White * (0.8f + (float)Math.Sin(LegendWorld.rottime) * 0.2f),
                0 + (float)Math.Sin(LegendWorld.rottime * 2) * 0.1f, tex.Size() / 2, 1 + (float)Math.Sin(LegendWorld.rottime) * 0.1f, 0, 0);

            Rectangle rect = new Rectangle((int)pos.X - tex.Frame().Width / 2, (int)pos.Y - tex.Frame().Height / 2, tex.Frame().Width, tex.Frame().Height);
            if (rect.Contains(Main.MouseScreen.ToPoint()))
            {
                spriteBatch.DrawString(Main.fontMouseText, item.Name, rect.Center() + new Vector2(-Main.fontMouseText.MeasureString(item.Name).X * 0.7f / 2, -tex.Frame().Height / 2 - 14),
                    Main.mouseTextColorReal, 0, Vector2.Zero, 0.7f, 0, 0);
            }
        }
        private void OpenCodex(UIMouseEvent evt, UIElement listeningElement)
        {
            if (Main.LocalPlayer.GetModPlayer<CodexHandler>().CodexState != 0)
            {
                Open = true;
                DragButton.Left.Set(-210, 0);
                ExitButton.Left.Set(150, 0);
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

            if (!Elements.Any(element => element is RecipeButton))
            {
                foreach (RiftRecipe recipe in ModContent.GetInstance<StarlightRiver>().RiftRecipes)
                {
                    RecipeButton button = new RecipeButton(recipe);
                    button.Visible = false;
                    Recipes.Add(button);
                    Append(button);
                }
            }
        }

        public void Refresh()
        {
            foreach (UIElement element in Elements.Where(element => element is EntryButton))
            {
                (element as EntryButton).Visible = false;
                element.Width.Set(0, 0);
            }

            for (int k = 0; k < ShownButtons.Count; k++)
            {
                EntryButton button = ShownButtons.ElementAt(k);
                button.Left.Set(140, 0);
                button.Top.Set(k * 30 - 150, 0);
                button.Width.Set(120, 0);
                button.Height.Set(24, 0);
                button.Visible = true;
            }
        }

        public void RefreshRecipes()
        {
            foreach (UIElement element in Elements.Where(element => element is RecipeButton))
            {
                (element as RecipeButton).Visible = false;
                element.Width.Set(0, 0);
            }

            for (int k = 0; k < ShownRecipes.Count; k++)
            {
                RecipeButton button = ShownRecipes.ElementAt(k);
                Vector2 offset = new Vector2(0, -110).RotatedBy(k / (float)ShownRecipes.Count * 6.28f);
                button.Left.Set(offset.X - 40, 0);
                button.Top.Set(offset.Y - 40, 0);
                button.Width.Set(48, 0);
                button.Height.Set(48, 0);
                button.Visible = true;
            }
        }

        public void OpenCrafting(UIMouseEvent evt, UIElement listeningElement)
        {
            Open = false;
            Crafting = true;
        }

        public void CloseCrafting(UIMouseEvent evt, UIElement listeningElement)
        {
            if (ActiveRecipe != null)
            {
                ActiveRecipe = null;
                return;
            }
            else
            {
                Crafting = false;
                ShownRecipes.Clear();
                RefreshRecipes();
                OpenCodex(evt, listeningElement);
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
                Entry.New = false;
            }
            Main.PlaySound(SoundID.MenuTick);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Codex.Open && Visible)
            {
                Color col = Color.White;
                Vector2 pos = GetDimensions().ToRectangle().TopLeft();
                if (Entry.New)
                {
                    col = new Color(255, 255, 127 + (int)((float)Math.Sin(LegendWorld.rottime * 2) * 127f));
                }
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/EntryButton"), new Rectangle((int)pos.X, (int)pos.Y, 120, 27), col * 0.8f);

                spriteBatch.Draw(Entry.Locked ? ModContent.GetTexture("StarlightRiver/GUI/blank") : Entry.Icon,
                    new Rectangle((int)pos.X + 5, (int)pos.Y + 5, 16, 16), new Rectangle(0, 0, 32, 32), Color.White);

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
            foreach (EntryButton entry in (Parent as Codex).Buttons.Where(entry => entry.Entry.Category == Category))
            {
                (Parent as Codex).ShownButtons.Add(entry);
            }
            (Parent as Codex).Refresh();

            Main.PlaySound(SoundID.MenuTick);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Codex.Open)
            {
                Color col = Color.White;
                Vector2 pos = GetDimensions().ToRectangle().TopLeft();
                String name = "";
                switch (Category)
                {
                    case 0: name = "Abilities"; break;
                    case 1: name = "Biomes"; break;
                    case 2: name = " Relics"; break;
                    case 3: name = "Bosses"; break;
                    case 4: name = " Misc"; break;
                }
                if (Main.LocalPlayer.GetModPlayer<CodexHandler>().Entries.Any(n => n.New && n.Category == Category))
                {
                    col = new Color(255, 255, 127 + (int)((float)Math.Sin(LegendWorld.rottime * 2) * 127f));
                }

                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/CategoryButton"), new Rectangle((int)pos.X, (int)pos.Y, 50, 27), col * 0.8f);
                Utils.DrawBorderString(spriteBatch, name, pos + new Vector2(7, 7), (Parent as Codex).ShownButtons.Any(button => button.Entry.Category == Category) ? Color.Yellow : Color.White, 0.6f);
            }
        }
    }

    public class RecipeButton : UIElement
    {
        public RiftRecipe Recipe;
        public bool Visible = false;
        public RecipeButton(RiftRecipe recipe)
        {
            Recipe = recipe;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible && Codex.Crafting && (Parent as Codex).ActiveRecipe == null)
            {
                Item item = new Item();
                item.type = Recipe.Result;
                item.SetDefaults(item.type);
                Texture2D tex = (item.modItem != null) ? ModContent.GetTexture(item.modItem.Texture) : ModContent.GetTexture("Terraria/Item_" + item.type);

                spriteBatch.Draw(tex, GetDimensions().ToRectangle().Center(), tex.Frame(), Color.White * (0.8f + (float)Math.Sin(LegendWorld.rottime) * 0.2f),
                    0 + (float)Math.Sin(LegendWorld.rottime * 2) * 0.1f, tex.Size() / 2, 1 + (float)Math.Sin(LegendWorld.rottime) * 0.1f, 0, 0);

                Rectangle rect = GetDimensions().ToRectangle();
                if (rect.Contains(Main.MouseScreen.ToPoint()))
                {
                    spriteBatch.DrawString(Main.fontMouseText, item.Name, rect.Center() + new Vector2(-Main.fontMouseText.MeasureString(item.Name).X * 0.7f / 2, -GetDimensions().Height / 2 - 8),
                        Main.mouseTextColorReal, 0, Vector2.Zero, 0.7f, 0, 0);
                }
            }
        }

        public override void Click(UIMouseEvent evt)
        {
            Codex codex = Parent as Codex;
            codex.ActiveRecipe = Recipe;
            codex.Dust.Clear();
        }
    }

    public class QuerySlot : UIElement
    {
        public Item item = null;

        public override void Update(GameTime gameTime)
        {
            if (IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                Main.isMouseLeftConsumedByUI = true;
            }
            if (!Codex.Crafting)
            {
                Codex codex = (Parent as Codex);
                item = null;
                codex.ShownRecipes.Clear();
                codex.RefreshRecipes();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = Main.inventoryBackTexture;

            //Draws the slot
            spriteBatch.Draw(tex, GetDimensions().ToRectangle(), new Rectangle(0, 0, (int)tex.Size().X, (int)tex.Size().Y), Color.White * 0.75f);

            if (item != null)
            {
                //Draws the item itself
                Texture2D tex2 = (item.modItem != null) ? ModContent.GetTexture(item.modItem.Texture) : ModContent.GetTexture("Terraria/Item_" + item.type);
                spriteBatch.Draw(tex2, GetDimensions().Center(), tex2.Frame(), Color.White, 0f, tex2.Frame().Center(), 0.8f, 0, 0);

                if (IsMouseHovering && Main.mouseItem.IsAir)
                {
                    //Grabs the items tooltip
                    string ToolTip = "";
                    for (int k = 0; k < item.ToolTip.Lines; k++)
                    {
                        ToolTip += item.ToolTip.GetLine(k);
                        ToolTip += "\n";
                    }

                    //Draws the name and tooltip at the mouse
                    Utils.DrawBorderStringBig(spriteBatch, item.Name, Main.MouseScreen + new Vector2(22, 22), ItemRarity.GetColor(item.rare).MultiplyRGB(Main.mouseTextColorReal), 0.39f);
                    Utils.DrawBorderStringBig(spriteBatch, ToolTip, Main.MouseScreen + new Vector2(22, 48), Main.mouseTextColorReal, 0.39f);
                }
            }
        }

        public override void Click(UIMouseEvent evt)
        {
            if (item == null && Main.mouseItem.IsAir)
                return;

            //if the player isnt holding anything but something is equipped, unequip it
            Codex codex = (Parent as Codex);
            if (item != null && Main.mouseItem.IsAir)
            {
                item = null;
                Main.PlaySound(SoundID.Grab);

                codex.ShownRecipes.Clear();
                codex.RefreshRecipes();
                return;
            }
            //if nothing is equipped, equip the held item
            if (item == null)
            {
                item = Main.mouseItem.Clone();
                Main.PlaySound(SoundID.Grab);

                UpdateParent(item);
            }
            //if something is equipped, swap that for the held item
            else
            {
                item = Main.mouseItem.Clone();
                Main.PlaySound(SoundID.Grab);

                UpdateParent(item);
            }
            codex.RefreshRecipes();
        }

        private void UpdateParent(Item item)
        {
            Codex codex = (Parent as Codex);
            codex.ShownRecipes.Clear();
            foreach (RecipeButton recipe in codex.Recipes)
            {
                if (recipe.Recipe.Ingredients.Any(i => i.type == item.type))
                    codex.ShownRecipes.Add(recipe);
            }
        }
    }
}

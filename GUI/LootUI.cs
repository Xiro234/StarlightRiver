using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using StarlightRiver.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace StarlightRiver.GUI
{
    public class LootUI : UIState
    {
        public static bool Visible = false;
        private Item BigItem = new Item();
        internal Item[] Selections = new Item[2];
        internal List<string> Quotes;
        private int QuoteID;

        public override void OnInitialize()
        {
            Quotes = new List<string>()
            {
                "Loot?",
                "Loot!",
                "Shiny treasures!",
                "Shinies!",
                "Treasure!",
                "For your troubles...",
                "This looks valuable...",
                "Not a mimmic!"
            };
        }
        public override void Update(GameTime gameTime)
        {
            if(Selections[1] != null)
            {
                Visible = false;
                Main.LocalPlayer.QuickSpawnItem(BigItem);
                Main.LocalPlayer.QuickSpawnItem(Selections[0], Selections[0].stack);
                Main.LocalPlayer.QuickSpawnItem(Selections[1], Selections[1].stack);
            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.GetTexture("StarlightRiver/GUI/CookSlotY");

            Utils.DrawBorderStringBig(spriteBatch, Quotes[QuoteID], GetDimensions().Center() + new Vector2(0, -80) - 2.2f * Main.fontItemStack.MeasureString(Quotes[QuoteID]) / 2, Color.White, 0.75f);

            string str = "You get:";
            string str2 = "Pick two:";

            Utils.DrawBorderString(spriteBatch, str, GetDimensions().Center() + new Vector2(0, -40) - Main.fontItemStack.MeasureString(str) / 2, Color.White, 0.8f);
            Utils.DrawBorderString(spriteBatch, str2, GetDimensions().Center() + new Vector2(0, + 70) - Main.fontItemStack.MeasureString(str2) / 2, Color.White, 0.8f);

            spriteBatch.Draw(tex, GetDimensions().Center(), tex.Frame(), Color.White, 0, tex.Size() / 2, 1, 0, 0);
            if (!BigItem.IsAir)
            {
                Texture2D tex2 = BigItem.type > ItemID.Count ? ModContent.GetTexture(BigItem.modItem.Texture) : ModContent.GetTexture("Terraria/Item_" + BigItem.type);
                float scale = tex2.Frame().Size().Length() < 52 ? 1 : 52f / tex2.Frame().Size().Length();

                spriteBatch.Draw(tex2, GetDimensions().Center(), tex2.Frame(), Color.White, 0, tex2.Frame().Size() / 2, scale, 0, 0);
                if (BigItem.stack > 1) spriteBatch.DrawString(Main.fontItemStack, BigItem.stack.ToString(), GetDimensions().Position() + Vector2.One * 28, Color.White);
            }
            base.Draw(spriteBatch);
            Recalculate();
        }
        public void SetItems(Loot bigItemID, Loot[] smallItemIDs)
        {
            Elements.Clear();
            Selections = new Item[2];

            Item item = new Item();
            item.SetDefaults(bigItemID.Type);
            item.stack = bigItemID.GetCount();
            BigItem = item;

            for(int k = 0; k < smallItemIDs.Length; k++)
            {
                Item item2 = new Item();
                item2.SetDefaults(smallItemIDs[k].Type);
                item2.stack = smallItemIDs[k].GetCount();
                AppendSlot(item2, (-2 + k) * 80);
            }
            QuoteID = Main.rand.Next(Quotes.Count);
        }
        private void AppendSlot(Item item, int offX)
        {
            LootSelection slot = new LootSelection(item);
            slot.Left.Set(offX - 30, 0.5f);
            slot.Top.Set(80, 0.5f);
            slot.Width.Set(60, 0);
            slot.Height.Set(60, 0);
            base.Append(slot);
        }
    }
    class LootSelection : UIElement
    {
        internal Item Item;
        public LootSelection(Item item) { Item = item; }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Parent is LootUI)
            {
                LootUI parent = Parent as LootUI;
                Texture2D tex = parent.Selections.Any(n => n == Item) ? ModContent.GetTexture("StarlightRiver/GUI/CookSlotY") : ModContent.GetTexture("StarlightRiver/GUI/CookSlotB");

                spriteBatch.Draw(tex, GetDimensions().Position(), tex.Frame(), Color.White, 0, Vector2.Zero, 1, 0, 0);
                if (!Item.IsAir)
                {
                    Texture2D tex2 = Item.type > ItemID.Count ? ModContent.GetTexture(Item.modItem.Texture) : ModContent.GetTexture("Terraria/Item_" + Item.type);
                    float scale = tex2.Frame().Size().Length() < 52 ? 1 : 52f / tex2.Frame().Size().Length();

                    spriteBatch.Draw(tex2, GetDimensions().Center(), tex2.Frame(), Color.White, 0, tex2.Frame().Size() / 2, 1, 0, 0);
                    if (Item.stack > 1) Utils.DrawBorderString(spriteBatch, Item.stack.ToString(), GetDimensions().Position() + Vector2.One * 36, Color.White, 0.75f);
                }
            }
        }
        public override void Click(UIMouseEvent evt)
        {
            if(Parent is LootUI)
            {
                LootUI parent = Parent as LootUI;
                if (parent.Selections[0] == null) parent.Selections[0] = Item;
                else parent.Selections[1] = Item;
            }
        }
    }
}

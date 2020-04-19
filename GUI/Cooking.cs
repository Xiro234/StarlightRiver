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
using StarlightRiver.Food;
using ReLogic.Graphics;
using System.Collections.Generic;

namespace StarlightRiver.GUI
{
    public class Cooking : UIState
    {
        public static bool Visible = false;
        CookingSlot MainSlot = new CookingSlot(IngredientType.Main);
        CookingSlot SideSlot0 = new CookingSlot(IngredientType.Side);
        CookingSlot SideSlot1 = new CookingSlot(IngredientType.Side);
        CookingSlot SeasonSlot = new CookingSlot(IngredientType.Seasoning);
        UIImageButton CookButton = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/CookPrep"));
        UIImageButton ExitButton = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/CookExit"));
        UIImage StatBack = new UIImage(ModContent.GetTexture("StarlightRiver/GUI/CookStatWindow"));
        UIImage TopBar = new UIImage(ModContent.GetTexture("StarlightRiver/GUI/CookTop"));

        Vector2 Basepos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
        public override void OnInitialize()
        {
            CookButton.OnClick += CookFood;
            CookButton.SetVisibility(1, 1);
            ExitButton.OnClick += Exit;
            ExitButton.SetVisibility(1, 1);
        }
        public override void Update(GameTime gameTime)
        {
            Basepos = new Vector2(Main.screenWidth / 2 - 173, Main.screenHeight / 2 - 122);

            SetPosition(MainSlot, 44, 44);
            SetPosition(SideSlot0, 10, 112);
            SetPosition(SideSlot1, 78, 112);
            SetPosition(SeasonSlot, 44, 180);
            SetPosition(StatBack, 170, 40);
            SetPosition(CookButton, 170, 202);
            SetPosition(ExitButton, 314, 0);
            SetPosition(TopBar, 0, 2);

            Append(MainSlot);
            Append(SideSlot0);
            Append(SideSlot1);
            Append(SeasonSlot);
            Append(CookButton);
            Append(ExitButton);
            Append(StatBack);
            Append(TopBar);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Utils.DrawBorderString(spriteBatch, "Ingredients", Basepos + new Vector2(38, 8), Color.White, 0.8f);
            Utils.DrawBorderString(spriteBatch, "Info/Stats", Basepos + new Vector2(202, 8), Color.White, 0.8f);
            Utils.DrawBorderString(spriteBatch, "Prepare", Basepos + new Vector2(212, 210), Color.White, 1.1f);

        }
        private void SetPosition(UIElement element, int x, int y)
        {
            element.Left.Set(Basepos.X + x, 0);
            element.Top.Set(Basepos.Y + y, 0);
        }
        private void CookFood(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!MainSlot.Item.IsAir) //make sure were cooking SOMETHING!
            {
                Item item = new Item();
                item.SetDefaults(ModContent.ItemType<Meal>()); //let TML hanlde making the item properly
                (item.modItem as Meal).Ingredients = new List<Item>();
                CookIngredient(item, MainSlot);
                CookIngredient(item, SideSlot0);
                CookIngredient(item, SideSlot1);
                CookIngredient(item, SeasonSlot);
                item.position = Main.LocalPlayer.Center;
                Main.LocalPlayer.QuickSpawnClonedItem(item);
            }
        }
        private void CookIngredient(Item target, CookingSlot source)
        {
            if (!source.Item.IsAir && source.Item.modItem is Ingredient)
            {
                (target.modItem as Meal).Ingredients.Add(source.Item.Clone());
                (target.modItem as Meal).Fullness += (MainSlot.Item.modItem as Ingredient).Fill;
                if (source.Item.stack == 1) source.Item.TurnToAir();
                else source.Item.stack--;
            }
        }
        private void Exit(UIMouseEvent evt, UIElement listeningElement)
        {
            Visible = false;
        }
    }
    public class CookingSlot : UIElement
    {
        public Item Item = new Item();
        IngredientType Type;
        public CookingSlot(IngredientType type) { Type = type; }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.GetTexture("StarlightRiver/GUI/CookSlotY");
            switch (Type)
            {
                case IngredientType.Main: tex = ModContent.GetTexture("StarlightRiver/GUI/CookSlotY"); break;
                case IngredientType.Side: tex = ModContent.GetTexture("StarlightRiver/GUI/CookSlotG"); break;
                case IngredientType.Seasoning: tex = ModContent.GetTexture("StarlightRiver/GUI/CookSlotB"); break;
            }

            spriteBatch.Draw(tex, GetDimensions().Position(), tex.Frame(), Color.White, 0, Vector2.Zero, 1, 0, 0);
            if (!Item.IsAir)
            {
                Texture2D tex2 = ModContent.GetTexture(Item.modItem.Texture);
                spriteBatch.Draw(tex2, new Rectangle((int)GetDimensions().X + 16, (int)GetDimensions().Y + 16, 28, 28), tex2.Frame(), Color.White);
                if (Item.stack > 1) spriteBatch.DrawString(Main.fontItemStack, Item.stack.ToString(), GetDimensions().Position() + Vector2.One * 28, Color.White);
            }
        }
        public override void Click(UIMouseEvent evt)
        {
            Player player = Main.LocalPlayer;

            if (Main.mouseItem.IsAir && !Item.IsAir) //if the cursor is empty and there is something in the slot, take the item out
            {
                Main.mouseItem = Item.Clone();
                Item.TurnToAir();
                Main.PlaySound(SoundID.Grab);
            }
            if (Item.IsAir && player.HeldItem.type == Item.type) //if the cursor is the same type as the item already in the slot, add to the slot
            {
                Item.stack += player.HeldItem.stack;
                player.HeldItem.TurnToAir();
                Main.PlaySound(SoundID.Grab);
            }
            if(player.HeldItem.modItem is Ingredient && (player.HeldItem.modItem as Ingredient).ThisType == Type && Item.IsAir) //if the slot is empty and the cursor has an item, put it in the slot
            {
                Item = player.HeldItem.Clone();
                player.HeldItem.TurnToAir();
                Main.PlaySound(SoundID.Grab);
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (Item.type == 0 || Item.stack <= 0) Item.TurnToAir();
            Width.Set(60, 0);
            Height.Set(60, 0);
        }
    }

}

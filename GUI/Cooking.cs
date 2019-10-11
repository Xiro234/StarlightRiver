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

namespace StarlightRiver.GUI
{
    public class Cooking : UIState
    {
        public UIPanel back;
        public IngredientSlot mainSlot;
        public IngredientSlot side1Slot;
        public IngredientSlot side2Slot;
        public IngredientSlot seasoningSlot;
        public UIImageButton cookButton;
        public static bool visible = false;

        public override void OnInitialize()
        {
            back = new UIPanel();
            mainSlot = new IngredientSlot();
            side1Slot = new IngredientSlot();
            side2Slot = new IngredientSlot();
            seasoningSlot = new IngredientSlot();
            cookButton = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/blank"));

            back.Left.Set(-150, 0.5f);
            back.Top.Set(-175, 0.5f);
            back.Width.Set(300, 0);
            back.Height.Set(350, 0);
            base.Append(back);

            mainSlot.Left.Set(75, 0);
            mainSlot.Top.Set(20, 0);
            mainSlot.Width.Set(64, 0);
            mainSlot.Height.Set(64, 0);
            mainSlot.OnClick += new MouseEvent(mainSlot.CheckInsertMains);
            back.Append(mainSlot);

            side1Slot.Left.Set(40, 0);
            side1Slot.Top.Set(120, 0);
            side1Slot.Width.Set(64, 0);
            side1Slot.Height.Set(64, 0);
            side1Slot.OnClick += new MouseEvent(side1Slot.CheckInsertSide);
            back.Append(side1Slot);

            side2Slot.Left.Set(110, 0);
            side2Slot.Top.Set(120, 0);
            side2Slot.Width.Set(64, 0);
            side2Slot.Height.Set(64, 0);
            side2Slot.OnClick += new MouseEvent(side2Slot.CheckInsertSide);
            back.Append(side2Slot);

            seasoningSlot.Left.Set(75, 0);
            seasoningSlot.Top.Set(220, 0);
            seasoningSlot.Width.Set(64, 0);
            seasoningSlot.Height.Set(64, 0);
            seasoningSlot.OnClick += new MouseEvent(seasoningSlot.CheckInsertSeasoning);
            back.Append(seasoningSlot);

            cookButton.Left.Set(75, 0);
            cookButton.Top.Set(300, 0);
            cookButton.Width.Set(32, 0);
            cookButton.Height.Set(16, 0);
            cookButton.OnClick += new MouseEvent(Cook);
            back.Append(cookButton);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Recalculate();
        }

        public void Cook(UIMouseEvent evt, UIElement listeningElement)
        {
            if (mainSlot.Item != null)
            {
                Player player = Main.LocalPlayer;
                int item = Item.NewItem(player.Center, StarlightRiver.Instance.ItemType<Meal>());
                Meal meal = Main.item[item].modItem as Meal;

                meal.mains = mainSlot.Item.modItem as MainCourse;
                if (side1Slot.Item != null) { meal.side1 = side1Slot.Item.modItem as SideCourse; }
                if (side2Slot.Item != null) { meal.side2 = side2Slot.Item.modItem as SideCourse; }
                if (seasoningSlot.Item != null) { meal.seasoning = seasoningSlot.Item.modItem as Seasoning; }
            }
        }

    }

    public class IngredientSlot : UIElement
    {
        public Item Item;

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(Main.inventoryBackTexture, dimensions.Position(), Color.White * 0.75f);
            if (Item != null && Item.modItem != null)
            {
                Texture2D tex = ModContent.GetTexture(Item.modItem.Texture);
                spriteBatch.Draw(tex, dimensions.Center() - (tex.Size() * 0.75f), Color.White);
            }
        }
        public void CheckInsertMains(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            if (player.HeldItem.modItem != null && player.HeldItem.modItem is MainCourse && Item == null)
            {             
                Item = player.HeldItem.Clone();
                Item.stack = 1;
                if (player.HeldItem.stack > 1) { player.HeldItem.stack--; } else { player.HeldItem.TurnToAir(); }
            }

            else if (Item != null)
            {
                for (int i = 0; i < 400; i++)
                {
                    if (!Main.item[i].active)
                    {
                        Main.item[i] = Item;
                        Main.item[i].position = player.Center;
                        break;
                    }
                }
                Item = null;
            }
        }

        public void CheckInsertSide(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            if (player.HeldItem.modItem != null && player.HeldItem.modItem is SideCourse && Item == null)
            {
                Item = player.HeldItem.Clone();
                Item.stack = 1;
                if (player.HeldItem.stack > 1) { player.HeldItem.stack--; } else { player.HeldItem.TurnToAir(); }
            }

            else if (Item != null)
            {
                for (int i = 0; i < 400; i++)
                {
                    if (!Main.item[i].active)
                    {
                        Main.item[i] = Item;
                        Main.item[i].position = player.Center;
                        break;
                    }
                }
                Item = null;
            }
        }

        public void CheckInsertSeasoning(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            if (player.HeldItem.modItem != null && player.HeldItem.modItem is Seasoning && Item == null)
            {
                Item = player.HeldItem.Clone();
                Item.stack = 1;
                if (player.HeldItem.stack > 1) { player.HeldItem.stack--; } else { player.HeldItem.TurnToAir(); }
            }

            else if (Item != null)
            {
                for (int i = 0; i < 400; i++)
                {
                    if (!Main.item[i].active)
                    {
                        Main.item[i] = Item;
                        Main.item[i].position = player.Center;
                        break;
                    }
                }
                Item = null;
            }
        }
    }

}

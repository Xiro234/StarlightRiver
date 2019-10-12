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
        public UIPanel statback;
        public IngredientSlot mainSlot;
        public IngredientSlot side1Slot;
        public IngredientSlot side2Slot;
        public IngredientSlot seasoningSlot;
        public UIImageButton cookButton;
        public static bool visible = false;

        public override void OnInitialize()
        {
            back = new UIPanel();
            statback = new UIPanel();
            mainSlot = new IngredientSlot();
            side1Slot = new IngredientSlot();
            side2Slot = new IngredientSlot();
            seasoningSlot = new IngredientSlot();
            cookButton = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/Cook"));

            back.Left.Set(-172, 0.5f);
            back.Top.Set(-100, 0.5f);
            back.Width.Set(144, 0);
            back.Height.Set(250, 0);
            base.Append(back);

            statback.Left.Set(149 - 172, 0.5f);
            statback.Top.Set(-100, 0.5f);
            statback.Width.Set(166, 0);
            statback.Height.Set(100, 0);
            base.Append(statback);

            mainSlot.Left.Set(34, 0);
            mainSlot.Top.Set(31, 0);
            mainSlot.Width.Set(64, 0);
            mainSlot.Height.Set(64, 0);
            mainSlot.OnClick += new MouseEvent(mainSlot.CheckInsertMains);
            back.Append(mainSlot);

            side1Slot.Left.Set(0, 0);
            side1Slot.Top.Set(105, 0);
            side1Slot.Width.Set(64, 0);
            side1Slot.Height.Set(64, 0);
            side1Slot.OnClick += new MouseEvent(side1Slot.CheckInsertSide);
            back.Append(side1Slot);

            side2Slot.Left.Set(69, 0);
            side2Slot.Top.Set(105, 0);
            side2Slot.Width.Set(64, 0);
            side2Slot.Height.Set(64, 0);
            side2Slot.OnClick += new MouseEvent(side2Slot.CheckInsertSide);
            back.Append(side2Slot);

            seasoningSlot.Left.Set(34, 0);
            seasoningSlot.Top.Set(179, 0);
            seasoningSlot.Width.Set(64, 0);
            seasoningSlot.Height.Set(64, 0);
            seasoningSlot.OnClick += new MouseEvent(seasoningSlot.CheckInsertSeasoning);
            back.Append(seasoningSlot);

            cookButton.Left.Set(149 - 172, 0.5f);
            cookButton.Top.Set(5, 0.5f);
            cookButton.Width.Set(166, 0);
            cookButton.Height.Set(32, 0);
            cookButton.OnClick += new MouseEvent(Cook);
            base.Append(cookButton);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Vector2 origin = back.GetDimensions().Position();
            Utils.DrawBorderString(spriteBatch, "Main Course", origin + new Vector2(20, 21), Color.White);
            Utils.DrawBorderString(spriteBatch, "Side Courses", origin + new Vector2(18, 95), Color.White);
            Utils.DrawBorderString(spriteBatch, "Seasoning", origin + new Vector2(32, 169), Color.White);

            float strmod = (seasoningSlot.Item != null) ? (seasoningSlot.Item.modItem as Seasoning).StrengthMod : 1;
            string boost1 = (mainSlot.Item != null) ? "+ " + (int)((mainSlot.Item.modItem as MainCourse).Strength * strmod) + (mainSlot.Item.modItem as MainCourse).ITooltip : "";
            string boost2 = (side1Slot.Item != null) ? "+ " + (int)((side1Slot.Item.modItem as SideCourse).Strength * strmod) + (side1Slot.Item.modItem as SideCourse).ITooltip : "";
            string boost3 = (side2Slot.Item != null) ? "+ " + (int)((side2Slot.Item.modItem as SideCourse).Strength * strmod) + (side2Slot.Item.modItem as SideCourse).ITooltip : "";
            string durboost = 60 + ((seasoningSlot.Item != null) ? (seasoningSlot.Item.modItem as Seasoning).Modifier / 60 : 0) + " Second Duration";

            string fill = (mainSlot.Item != null) ? ("Fullness: " + (((mainSlot.Item.modItem as MainCourse).Fill) + 
                ((side1Slot.Item != null) ? (side1Slot.Item.modItem as SideCourse).Fill : 0) + 
                ((side2Slot.Item != null) ? (side2Slot.Item.modItem as SideCourse).Fill : 0) + 
                ((seasoningSlot.Item != null) ? (seasoningSlot.Item.modItem as Seasoning).Fill : 0)
                )/60) + " Seconds" : "";

            if (mainSlot.Item != null)
            {
                Utils.DrawBorderString(spriteBatch, boost1, origin + new Vector2(155, 10), new Color(255, 220, 140), 0.75f);
                Utils.DrawBorderString(spriteBatch, boost2, origin + new Vector2(155, 25), new Color(140, 255, 140), 0.75f);
                Utils.DrawBorderString(spriteBatch, boost3, origin + new Vector2(155, 40), new Color(140, 255, 140), 0.75f);
                Utils.DrawBorderString(spriteBatch, durboost, origin + new Vector2(155, 65), (seasoningSlot.Item != null) ? new Color(140, 200, 255) : Color.White, 0.75f);
                Utils.DrawBorderString(spriteBatch, fill, origin + new Vector2(155, 80), new Color(255, 163, 153), 0.75f);
            }

            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/Line"), new Rectangle((int)origin.X + 154, (int)origin.Y + 60, 156, 4), Color.White * 0.25f);
            Utils.DrawBorderString(spriteBatch, "Prepare", origin + new Vector2(190, 106), Color.White, 1.1f);

            Recalculate();
        }

        public void Cook(UIMouseEvent evt, UIElement listeningElement)
        {
            if (mainSlot.Item != null)
            {
                Player player = Main.LocalPlayer;
                int item = Item.NewItem(player.Center, ModContent.ItemType<Meal>());
                Meal meal = Main.item[item].modItem as Meal;

                meal.mains = mainSlot.Item.modItem as MainCourse; mainSlot.Item = null;
                if (side1Slot.Item != null) { meal.side1 = side1Slot.Item.modItem as SideCourse; side1Slot.Item = null; }
                if (side2Slot.Item != null) { meal.side2 = side2Slot.Item.modItem as SideCourse; side2Slot.Item = null; }
                if (seasoningSlot.Item != null) { meal.seasoning = seasoningSlot.Item.modItem as Seasoning; seasoningSlot.Item = null; }
            }
        }

    }

    public class IngredientSlot : UIElement
    {
        public Item Item;

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(Main.inventoryBackTexture, dimensions.Position(), Color.White * 0.6f);
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

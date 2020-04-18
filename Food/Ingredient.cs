using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Food
{
    public enum IngredientType
    {
        Main = 0,
        Side = 1,
        Seasoning = 2
    };
    public abstract class Ingredient : ModItem
    {
        public string ItemName;
        public string ItemTooltip;
        public int Fill = 0;
        public IngredientType ThisType {get; set;}

        public Ingredient(string name, string tooltip, int filling, IngredientType type)
        {
            Fill = filling;
            ItemName = name;
            ItemTooltip = tooltip;
            ThisType = type;
        }

         ///<summary>Where the effects of this food item's buff will go. use the multiplier param for any effect that should be multiplier-sensitive</summary>
        public virtual void BuffEffects(Player player, float multipler)
        {

        }

        public override bool CloneNewInstances => true;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\n\n");
        }
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.width = 32;
            item.height = 32;
            item.useStyle = 1;
            item.useTime = 30;
            item.useAnimation = 30;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string description;
            Color nameColor;
            Color descriptionColor;
            switch (ThisType)
            {
                case IngredientType.Main: description = "Main Course"; nameColor = new Color(255, 220, 140); descriptionColor = new Color(255, 220, 80); break;
                case IngredientType.Side: description = "Side Dish"; nameColor = new Color(); descriptionColor = new Color(); break;
                case IngredientType.Seasoning: description = "Seasonings"; nameColor = new Color(); descriptionColor = new Color(); break;
                default: description = "ERROR"; nameColor = Color.Black; descriptionColor = Color.Black; break;
            }
            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName") { line.text = ItemName; }
                if (line.mod == "Terraria" && line.Name == "Tooltip0") { line.text = description; line.overrideColor = nameColor; }
                if (line.mod == "Terraria" && line.Name == "Tooltip1") { line.text = ItemTooltip; line.overrideColor = descriptionColor; }
            }
        }
    }
}

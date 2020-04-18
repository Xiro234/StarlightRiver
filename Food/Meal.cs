using Microsoft.Xna.Framework;
using StarlightRiver.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Food
{
    class Meal : ModItem
    {
        public List<Item> Ingredients { get; set; }
        public override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meal");
            Tooltip.SetDefault("Food that shouldn't exist. You probably shouldnt eat this. Please report me to the devs!\n\n\n\n");
        }

        public override void SetDefaults()
        {
            item.consumable = true;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 1;
            item.width = 32;
            item.height = 32;
        }
        public override bool CanUseItem(Player player)
        {
            FoodBuffHandler mp = player.GetModPlayer<FoodBuffHandler>();

            if (player.HasBuff(ModContent.BuffType<Full>())) { return false; }

            if (Ingredients.Count > 0)
            {
                player.AddBuff(ModContent.BuffType<FoodBuff>(), 1);
                mp.Consumed.AddRange(Ingredients);

                player.AddBuff(ModContent.BuffType<Full>(), 1);
            }
            else Main.NewText("Bad food! Please report me to the mod devs.", Color.Red);

            item.stack--;
            return true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach(Item item in Ingredients)
            {
                tooltips.Add(new TooltipLine(mod, "StarlightRiver: Ingredient", (item.modItem as Ingredient).ItemTooltip));
            }
            
        }
    }
}

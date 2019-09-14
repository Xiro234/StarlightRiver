using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{
    public class Bottle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A bottle made of thick vitric glass");
            DisplayName.SetDefault("Vitric Bottle");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 250;
            item.rare = 2;
        }
    }
    public class PotionStamina : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases stamina regeneration");
            DisplayName.SetDefault("Invigorating Brew");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = 3;
            item.value = Item.buyPrice(gold: 1);
            item.buffType = mod.BuffType("StaminaRegen"); 
            item.buffTime = 10800; 
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Bottle"), 1);
            recipe.AddIngredient(mod.ItemType("BlendForest"), 1);
            recipe.AddIngredient(ItemID.GlowingMushroom, 5);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }

    public class PotionFury : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Gain damage based on damage taken");
            DisplayName.SetDefault("Fallen Warrior's Brew");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = 3;
            item.value = Item.buyPrice(gold: 1);
            item.buffType = mod.BuffType("DamageBoost");
            item.buffTime = 10800;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Bottle"), 1);
            recipe.AddIngredient(mod.ItemType("BlendEvil"), 1);
            recipe.AddIngredient(mod.ItemType("OreIvoryItem"), 5);
            recipe.AddIngredient(ItemID.Fireblossom, 3);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}

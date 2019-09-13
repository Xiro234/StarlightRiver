using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{
    public class BlendForest : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Powdered herbs from the Forest");
            DisplayName.SetDefault("Forest Blend");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Daybloom, 1);
            recipe.AddIngredient(ItemID.GrassSeeds, 1);
            recipe.AddIngredient(mod.ItemType("Ivy"), 5);
            recipe.AddTile(mod.TileType<Tiles.HerbStation>());
            recipe.SetResult(this, 3);
            recipe.AddRecipe();
        }
    }
    public class BlendEvil : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Powdered herbs from dark places");
            DisplayName.SetDefault("Twisted Blend");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(ItemID.Shiverthorn, 1);
            recipe.AddIngredient(mod.ItemType("Deathstalk"), 5);
            recipe.AddTile(mod.TileType<Tiles.HerbStation>());
            recipe.SetResult(this, 3);
            recipe.AddRecipe();
        }
    }
}

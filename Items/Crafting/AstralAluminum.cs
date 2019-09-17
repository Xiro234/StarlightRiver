using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Crafting
{
    public class AluminumOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Aluminum Chunk");
            Tooltip.SetDefault("Smelt into bars at an Oven"); //is that too much? yes
        }
        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 12;
            item.maxStack = 999;
            item.value = 100;
            item.rare = 1;
        }
    }
    public class AluminumBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Aluminum Bar");
            Tooltip.SetDefault("It shimmers with beautiful light"); //is that too much? yes
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 999;
            item.value = 100;
            item.rare = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<AluminumOre>(), 5);
            recipe.AddTile(mod.TileType<Tiles.Oven>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

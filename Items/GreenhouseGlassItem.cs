using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
	public class GreenhouseGlassItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greenhouse Glass");
			Tooltip.SetDefault("Speeds up the growth the plant below it"
				+ "\nNeeds a clear area above it");
		}
		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("GreenhouseGlass");
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddIngredient(ModContent.ItemType<Items.Crafting.AluminumBar>(), 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }
}

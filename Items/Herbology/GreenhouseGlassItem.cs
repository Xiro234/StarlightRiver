using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{
    public class GreenhouseGlassItem : QuickTileItem
    {
        public GreenhouseGlassItem() : base("Greenhouse Glass", "Speeds up the growth the plant below it\nNeeds a clear area above it", TileType<Tiles.Herbology.GreenhouseGlass>(), 1)
        {
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddIngredient(ItemType<Crafting.AluminumBar>(), 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }
}
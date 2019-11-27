using StarlightRiver.Tiles.Crafting;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items.TileItems.CraftingTables
{
    public sealed class CookStationItem : StandardTileItem
    {
        public CookStationItem() : base("Prep Station", "Right click to prepare meals", 30, 20, ModContent.TileType<CookStation>())
        {
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddIngredient(RecipeGroupID.IronBar, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
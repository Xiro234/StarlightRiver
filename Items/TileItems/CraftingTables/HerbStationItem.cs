using StarlightRiver.Tiles.Crafting;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items.TileItems.CraftingTables
{
    public sealed class HerbStationItem : StandardTileItem
    {
        public HerbStationItem() : base("Herbologist's Bench", "Used to refine herbs", 22, 14, ModContent.TileType<HerbStation>())
        {
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddIngredient(ModContent.ItemType<Herbology.Ivy>());
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
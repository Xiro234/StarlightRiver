using StarlightRiver.Tiles.Crafting;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items.TileItems.CraftingTables
{
    public sealed class OvenItem : StandardTileItem
    {
        public OvenItem() : base("Oven", "Used to bake items", 30, 20, ModContent.TileType<Oven>())
        {
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.StoneBlock, 20);
            recipe.AddIngredient(ItemID.Gel, 5);
            recipe.AddIngredient(ItemID.Wood, 10);
            
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            
            recipe.AddRecipe();
        }
    }
}
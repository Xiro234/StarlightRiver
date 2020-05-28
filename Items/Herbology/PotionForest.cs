using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{
    internal class PotionForest : QuickPotion
    {
        public PotionForest() : base("Forest Tonic", "Provides regenration and immunity to poision", 1800, ModContent.BuffType<Buffs.ForestTonic>(), 2) { }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ModContent.ItemType<ForestBerries>(), 5);
            recipe.AddIngredient(ModContent.ItemType<Ivy>(), 20);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.HerbStation>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.EbonyIvory
{
    public class OreEbonyItem : QuickTileItem { public OreEbonyItem() : base("Ebony Ore", "Heavy and Impure", ModContent.TileType<Tiles.OreEbony>(), 1) { } }
    public class OreIvoryItem : QuickMaterial { public OreIvoryItem() : base("Ivory Ore", "Light and Pure", 999, 1000, 4) { } }
    public class BarEbony : QuickTileItem
    {
        public BarEbony() : base("Ebony Bar", "Soft and Heavy", ModContent.TileType<Tiles.EbonyBar>(), 1) { }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<OreEbonyItem>(), 4);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.Oven>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class BarIvory : QuickTileItem
    {
        public BarIvory() : base("Ivory Bar", "Hard and Light", ModContent.TileType<Tiles.IvoryBar>(), 1) { }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<OreIvoryItem>(), 4);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.OvenAstral>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

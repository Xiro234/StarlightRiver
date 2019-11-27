using StarlightRiver.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items.EbonyIvory
{
    public class OreEbonyItem : StandardTileItem { public OreEbonyItem() : base("Ebony Ore", "Heavy and Impure", 14, 14, ModContent.TileType<Tiles.OreEbony>(), ItemRarityID.Blue) { } }
    public class OreIvoryItem : StandardTileItem { public OreIvoryItem() : base("Ivory Ore", "Light and Pure", 14, 14, ModContent.TileType<OreIvory>(), rarity: ItemRarityID.LightRed, value: 1000) { } }

    public class BarEbony : QuickMaterial
    {
        public BarEbony() : base("Ebony Bar", "Soft and Heavy", 999, 1000, 1) { }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<OreEbonyItem>(), 4);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.Oven>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class BarIvory : QuickMaterial
    {
        public BarIvory() : base("Ivory Bar", "Hard and Light", 999, 5000, 4) { }
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

using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Aluminum
{
    public class AluminumOre : QuickTileItem
    {
        public AluminumOre() : base("Astral Aluminum", "", TileType<Tiles.OreAluminum>(), ItemRarityID.White) { }

        public override void SafeSetDefaults() => item.value = Item.sellPrice(0, 0, 2, 0);
    }

    public class AluminumBar : QuickTileItem
    {
        public AluminumBar() : base("Astral Aluminum Bar", "'Shimmering with Beautiful Light'", TileType<Tiles.AluminumBar>(), ItemRarityID.Blue) { }

        public override void SafeSetDefaults() => item.value = Item.sellPrice(0, 0, 14, 0);

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<AluminumOre>(), 3);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
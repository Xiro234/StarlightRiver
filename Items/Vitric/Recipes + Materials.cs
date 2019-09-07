using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Items.Vitric
{
    public class VitricGem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitric Gem");
            Tooltip.SetDefault("'Many facets shimmer within'"); //is that too much? yes
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 250;
            item.value = 100;
            item.rare = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Items.Vitric.Glassore>(), 8); //recipe not final
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.FossilOre, 10); //recipe also not final
            recipe.AddIngredient(mod.ItemType<Items.Vitric.VitricGem>(), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(mod.ItemType<Items.Vitric.VitricSword>());
            recipe.AddRecipe();
        }
    }
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Items.Vitric
{
    public class VitricBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitric Bar");
            Tooltip.SetDefault("Gazing into this ingot reveals thousands of facets, glimmering in the light."); //is that too much?
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.value = 100;
            item.rare = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Items.Vitric.Glassore>(), 3); //recipe not final
            recipe.AddIngredient(mod.ItemType<Items.Vitric.Sandglass>(), 6);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType<Items.Vitric.OverseerCore>()); //recipe also not final
            recipe.AddIngredient(mod.ItemType<Items.Vitric.VitricBar>(), 7);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(mod.ItemType<Items.Vitric.VitricSword>());
            recipe.AddRecipe();
        }
    }
    public class OverseerCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Overseer Core");
            Tooltip.SetDefault("You can feel it pulling you closer."); //is that also too much?
        }
        public override void SetDefaults()
        {
            item.width = 16; //still trying to figure out what difference changing these two values makes
            item.height = 16;
            item.maxStack = 999;
            item.value = 100;
            item.rare = 2;
        }

        public override void AddRecipes() //not sure if i should handle all vitric recipes in the above method or just the ones that include vitric bars, going to leave this here anyway
        {

        }
    }
}
//well that was fun
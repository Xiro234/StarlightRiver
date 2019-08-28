using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Items
{
	public class Vortexs_peepee : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Vortexs_peepee"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("doin your mom doin doin your mom");
		}

		public override void SetDefaults() 
		{
			item.damage = 50000;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 1;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 1;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

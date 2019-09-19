using StarlightRiver.Items.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Ammo
{
	public class AluminumBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Astral Tracer");
			Tooltip.SetDefault("Weakly homes in on foes");
		}

		public override void SetDefaults()
		{
			item.damage = 8;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;         
			item.knockBack = 0.5f;
			item.value = 10;
			item.rare = 2;
			item.shoot = mod.ProjectileType<Projectiles.Ammo.AluminumBullet>();  
			item.shootSpeed = 0.01f;                  
			item.ammo = AmmoID.Bullet;            
		}

		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<AluminumBar>(), 1);
            recipe.AddTile(mod.TileType<Tiles.Oven>());
            recipe.SetResult(this, 25);
            recipe.AddRecipe();
        }
	}
}

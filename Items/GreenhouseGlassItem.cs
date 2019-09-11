using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Items
{
	public class GreenhouseGlassItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greenhouse Glass");
			Tooltip.SetDefault("Speeds up the growth the plant below it"
				+ "Needs a clear area above it");
		}
		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("GreenhouseGlass");
		}
	}
}

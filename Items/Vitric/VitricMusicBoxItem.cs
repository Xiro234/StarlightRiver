using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace StarlightRiver.Items.Vitric
{
	public class VitricMusicBoxItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (Vitric Desert)");
		}

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = TileType<Tiles.Vitric.VitricMusicBox>();
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Green;
			item.value = 10000;
			item.accessory = true;
		}
	}
}
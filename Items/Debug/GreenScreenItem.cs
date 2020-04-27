using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Debug
{
    public class GreenScreenItem : QuickTileItem
    {
        public GreenScreenItem() : base("Green Screen", "For all your movie making needs", ModContent.TileType<Tiles.Debug.GreenScreen>(), 1) { }
    }
	
	public class GreenScreenWallItem : ModItem
    {
        public override string Texture => "StarlightRiver/Items/Debug/GreenScreenItem";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Screen Wall");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createWall = ModContent.WallType<Tiles.Debug.GreenScreenWall>();
        }
    }
}

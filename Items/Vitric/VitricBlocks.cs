using Terraria.ModLoader;

namespace StarlightRiver.Items.Vitric
{
    public class VitricSandItem : QuickTileItem { public VitricSandItem() : base("Glassy Sand", "", ModContent.TileType<Tiles.Vitric.VitricSand>(), 0) { } }
    public class VitricGlassItem : QuickTileItem { public VitricGlassItem() : base("Fuseglass", "", ModContent.TileType<Tiles.Vitric.VitricGlass>(), 0) { } }
    public class VitricGlassCrystalItem : QuickTileItem { public VitricGlassCrystalItem() : base("Crystaline Glass", "", ModContent.TileType<Tiles.Vitric.VitricGlassCrystal>(), 0) { } }
    public class VitricBrickItem : QuickTileItem { public VitricBrickItem() : base("Vitric Brick", "", ModContent.TileType<Tiles.Vitric.VitricBrick>(), 0) { } }
    public class AncientSandstoneItem : QuickTileItem { public AncientSandstoneItem() : base("Ancient Sandstone Brick", "", ModContent.TileType<Tiles.Vitric.AncientSandstone>(), 0) { } }
    public class AncientSandstonePlatformItem : QuickTileItem { public AncientSandstonePlatformItem() : base("Ancient Sandstone Platform", "", ModContent.TileType<Tiles.Vitric.AncientSandstonePlatform>(), 0) { } }

    public class Bounce : ModItem //TODO Migrate to itneractable tiles
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Dash into this to bounce!");
            DisplayName.SetDefault("Crystal Bouncer");
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
            item.createTile = mod.TileType("Bounce");
        }
    }
}

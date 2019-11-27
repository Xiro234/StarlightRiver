using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items.Vitric
{
    public class VitricSandItem : StandardTileItem { public VitricSandItem() : base("Glassy Sand", "", 16, 16, ModContent.TileType<Tiles.Vitric.VitricSand>()) { } }
    public class VitricGlassItem : StandardTileItem { public VitricGlassItem() : base("Fuseglass", "", 16, 16, ModContent.TileType<Tiles.Vitric.VitricGlass>()) { } }
    public class VitricGlassCrystalItem : StandardTileItem { public VitricGlassCrystalItem() : base("Crystaline Glass", "", 16, 16, ModContent.TileType<Tiles.Vitric.VitricGlassCrystal>()) { } }
    public class VitricBrickItem : StandardTileItem { public VitricBrickItem() : base("Vitric Brick", "", 16, 16, ModContent.TileType<Tiles.Vitric.VitricBrick>()) { } }

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

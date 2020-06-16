using StarlightRiver.Tiles.Vitric.Blocks;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Items.Vitric
{
    public class AncientSandstoneItem : QuickTileItem { public AncientSandstoneItem() : base("Ancient Sandstone Brick", "", TileType<Tiles.Vitric.AncientSandstone>(), 0) { } }

    public class AncientSandstonePlatformItem : QuickTileItem { public AncientSandstonePlatformItem() : base("Ancient Sandstone Platform", "", TileType<Tiles.Vitric.AncientSandstonePlatform>(), 0) { } }
}
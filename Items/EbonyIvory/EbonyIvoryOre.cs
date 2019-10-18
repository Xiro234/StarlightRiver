using Terraria.ModLoader;

namespace StarlightRiver.Items.EbonyIvory
{
    public class OreEbonyItem : QuickTileItem { public OreEbonyItem() : base("Ebony Ore", "Heavy and Impure", ModContent.TileType<Tiles.OreEbony>(), 1) { } }
    public class OreIvoryItem : QuickMaterial { public OreIvoryItem() : base("Ivory Ore", "Light and Pure", 999, 1000, 4) { } }
}

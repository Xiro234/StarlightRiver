using StarlightRiver.Tiles.Crafting;
using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items.TileItems.CraftingTables
{
    public sealed class OvenAstralItem : StandardTileItem
    {
        public OvenAstralItem() : base("Astral Oven", "Used to bake advanced items", 30, 20, ModContent.TileType<OvenAstral>())
        {
        }
    }
}
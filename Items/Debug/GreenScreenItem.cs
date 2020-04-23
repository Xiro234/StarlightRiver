using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Debug
{
    public class GreenScreenItem : QuickTileItem
    {
        public GreenScreenItem() : base("Green Screen", "For all your movie making needs", ModContent.TileType<Tiles.Debug.GreenScreen>(), 1) { }
    }
}

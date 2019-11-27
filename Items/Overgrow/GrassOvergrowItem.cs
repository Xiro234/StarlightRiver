using StarlightRiver.Tiles.Overgrow;
using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items.Overgrow
{
    public sealed class GrassOvergrowItem : StandardTileItem
    {
        public GrassOvergrowItem() : base("Teeming with tiny life", "Overgrow Grass", 12, 12, ModContent.TileType<GrassOvergrow>())
        {
        }


        public override void SetDefaults()
        {
            base.SetDefaults();

            item.noUseGraphic = true;
        }
    }
}
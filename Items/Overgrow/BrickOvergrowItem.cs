using StarlightRiver.Tiles.Overgrow;
using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items.Overgrow
{
    public sealed class BrickOvergrowItem : StandardTileItem
    {
        public BrickOvergrowItem() : base("Teeming with tiny life", "Overgrow Brick", 16, 16, ModContent.TileType<BrickOvergrow>())
        {
        }


        public override void SetDefaults()
        {
            base.SetDefaults();

            item.noUseGraphic = true;
        }
    }
}
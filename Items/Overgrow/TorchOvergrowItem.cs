using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items.Overgrow
{
    public class TorchOvergrowItem : StandardTileItem
    {
        public TorchOvergrowItem() : base("Faerie Torch", "Sparkly!", 16, 16, ModContent.TileType<Tiles.Overgrow.TorchOvergrow>())
        {
        }
    }
}
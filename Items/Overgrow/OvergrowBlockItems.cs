using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Overgrow
{
    public class TorchOvergrowItem : QuickTileItem { public TorchOvergrowItem() : base("Faerie Torch", "Sparkly!", ModContent.TileType<Tiles.Overgrow.TorchOvergrow>(), 0) { } }
    public class HatchOvergrowItem : QuickTileItem { public HatchOvergrowItem() : base("Skyview Vent", "", ModContent.TileType<Tiles.Overgrow.HatchOvergrow>(), 0) { } }
    public class DartOvergrowItem : QuickTileItem { public DartOvergrowItem() : base("Overgrow Dart Trap", "", ModContent.TileType<Tiles.Overgrow.DartTile>(), 0) { } }
    public class CrusherOvergrowItem : QuickTileItem { public CrusherOvergrowItem() : base("Crusher Trap", "", ModContent.TileType<Tiles.Overgrow.CrusherTile>(), 0) { } }
    public class SetpieceOvergrowItem : QuickTileItem
    {
        public SetpieceOvergrowItem() : base("Overgrow Altar", "", ModContent.TileType<Tiles.Overgrow.SetpieceAltar>(), 0) { }
        public override string Texture => "StarlightRiver/Items/Overgrow/BrickOvergrowItem";
    }
    public class BigHatchOvergrowItem : QuickTileItem
    {
        public BigHatchOvergrowItem() : base("Overgrow Godrays", "", ModContent.TileType<Tiles.Overgrow.BigHatchOvergrow>(), 0) { }
        public override string Texture => "StarlightRiver/Items/Overgrow/BrickOvergrowItem";
    }
}
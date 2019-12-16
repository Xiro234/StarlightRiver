using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Overgrow
{
    public class TorchOvergrowItem : QuickTileItem { public TorchOvergrowItem() : base("Faerie Torch", "Sparkly!", ModContent.TileType<Tiles.Overgrow.TorchOvergrow>(), 0) { } }
    public class BrickOvergrowItem : QuickTileItem { public BrickOvergrowItem() : base("Runic Bricks", "", ModContent.TileType<Tiles.Overgrow.BrickOvergrow>(), 0) { } }
    public class GlowBrickOvergrowItem : QuickTileItem { public GlowBrickOvergrowItem() : base("Awoken Runic Bricks", "They have a pulse...", ModContent.TileType<Tiles.Overgrow.GlowBrickOvergrow>(), 1) { } }
    public class LeafOvergrowItem : QuickTileItem { public LeafOvergrowItem() : base("Faerie Leaves", "", ModContent.TileType<Tiles.Overgrow.LeafOvergrow>(), 0) { } }
    public class HatchOvergrowItem : QuickTileItem { public HatchOvergrowItem() : base("Skyview Vent", "", ModContent.TileType<Tiles.Overgrow.HatchOvergrow>(), 0) { } }

    public class GrassOvergrowItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Teeming with tiny life");
            DisplayName.SetDefault("Overgrow Grass Seeds");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 10;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("GrassOvergrow");
        }
    }
}
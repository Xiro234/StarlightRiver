using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Overgrow
{
    public class TorchOvergrowItem : QuickTileItem { public TorchOvergrowItem() : base("Faerie Torch", "Sparkly!", ModContent.TileType<Tiles.TorchOvergrow>(), 0) { } }
    public class BrickOvergrowItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Teeming with tiny life");
            DisplayName.SetDefault("Overgrow Brick");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.consumable = true;
            item.createTile = mod.TileType("BrickOvergrow");
        }
    }

    public class GrassOvergrowItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Teeming with tiny life");
            DisplayName.SetDefault("Overgrow Grass");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.consumable = true;
            item.createTile = mod.TileType("GrassOvergrow");
        }
    }
}
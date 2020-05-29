using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{
    public class Ivy : QuickMaterial { public Ivy() : base("Forest Ivy", "A common, yet versatile herb", 999, 100, 1) { } }
    public class IvySeeds : QuickMaterial { public IvySeeds() : base("Forest Ivy Seeds", "Can grow in hanging planters", 99, 0, 1) { } }
    public class ForestBerries : QuickMaterial { public ForestBerries() : base("Forest Berries", "Sweet and juicy!", 99, 100, 1) { } }
    public class BerryBush : QuickTileItem { public BerryBush() : base("Berry bush", "Plant to grow your own berries!", ModContent.TileType<Tiles.Herbology.ForestBerryBush>(), 1) { } }

    public class Deathstalk : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grows on Rich Soil");
            DisplayName.SetDefault("Deathstalk");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = ItemRarityID.Green;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("Deathstalk");
        }
    }
}

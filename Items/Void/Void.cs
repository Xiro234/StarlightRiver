using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Void
{
    public class Void1Item : QuickTileItem { public Void1Item() : base("Eldritch Brick", "It's Whispering...", ModContent.TileType<Tiles.Void.Void1>(), 0) { } }
    public class Void2Item : QuickTileItem { public Void2Item() : base("Eldritch Stone", "It's Whispering...", ModContent.TileType<Tiles.Void.Void2>(), 0) { } }
    public class VoidTorch1Item : QuickTileItem { public VoidTorch1Item() : base("Eldritch Torch", "This is backwards...", ModContent.TileType<Tiles.Void.VoidTorch1>(), 0) { } }
    public class VoidTorch2Item : QuickTileItem { public VoidTorch2Item() : base("Eldritch Brazier", "This is backwards...", ModContent.TileType<Tiles.Void.VoidTorch2>(), 0) { } }
    public class VoidPillarBItem : QuickTileItem { public VoidPillarBItem() : base("Eldritch Pillar Base", "", ModContent.TileType<Tiles.Void.VoidPillarB>(), 0) { } }
    public class VoidPillarMItem : QuickTileItem { public VoidPillarMItem() : base("Eldritch Pillar", "", ModContent.TileType<Tiles.Void.VoidPillarM>(), 0) { } }
    public class VoidPillarTItem : QuickTileItem { public VoidPillarTItem() : base("Eldritch Pillar Support", "", ModContent.TileType<Tiles.Void.VoidPillarT>(), 0) { } }
    public class VoidPillarPItem : QuickTileItem { public VoidPillarPItem() : base("Eldritch Pillar Pedestal", "", ModContent.TileType<Tiles.Void.VoidPillarP>(), 0) { } }

    public class VoidWallItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eldritch Brick Wall");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createWall = ModContent.WallType<Tiles.Void.VoidWall>();
        }
    }
    public class VoidWallPillarItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eldritch Pillar Wall");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createWall = ModContent.WallType<Tiles.Void.VoidWallPillar>();
        }
    }
    public class VoidWallPillarSItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Short Eldritch Pillar Wall");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createWall = ModContent.WallType<Tiles.Void.VoidWallPillarS>();
        }
    }
}

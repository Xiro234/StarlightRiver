using Terraria.ModLoader;

namespace StarlightRiver.Items.Void
{
    public class Void1Item : QuickTileItem { public Void1Item() : base("Eldritch Brick", "It's Whispering...", ModContent.TileType<Tiles.Void1>(), 0) { } }
    public class Void2Item : QuickTileItem { public Void2Item() : base("Eldritch Stone", "It's Whispering...", ModContent.TileType<Tiles.Void2>(), 0) { } }
    public class VoidTorch1Item : QuickTileItem { public VoidTorch1Item() : base("Eldritch Torch", "This is backwards...", ModContent.TileType<Tiles.VoidTorch1>(), 0) { } }
    public class VoidTorch2Item : QuickTileItem { public VoidTorch2Item() : base("Eldritch Brazier", "This is backwards...", ModContent.TileType<Tiles.VoidTorch2>(), 0) { } }
    public class VoidPillarBItem : QuickTileItem { public VoidPillarBItem() : base("Eldritch Pillar Base", "", ModContent.TileType<Tiles.VoidPillarB>(), 0) { } }
    public class VoidPillarMItem : QuickTileItem { public VoidPillarMItem() : base("Eldritch Pillar", "", ModContent.TileType<Tiles.VoidPillarM>(), 0) { } }
    public class VoidPillarTItem : QuickTileItem { public VoidPillarTItem() : base("Eldritch Pillar Support", "", ModContent.TileType<Tiles.VoidPillarT>(), 0) { } }
    public class VoidPillarPItem : QuickTileItem { public VoidPillarPItem() : base("Eldritch Pillar Pedestal", "", ModContent.TileType<Tiles.VoidPillarP>(), 0) { } }

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
            item.useStyle = 1;
            item.consumable = true;
            item.createWall = ModContent.WallType<Tiles.VoidWall>();
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
            item.useStyle = 1;
            item.consumable = true;
            item.createWall = ModContent.WallType<Tiles.VoidWallPillar>();
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
            item.useStyle = 1;
            item.consumable = true;
            item.createWall = ModContent.WallType<Tiles.VoidWallPillarS>();
        }
    }
}

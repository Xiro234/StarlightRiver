using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items.Void
{
    public class Void1Item : StandardTileItem { public Void1Item() : base("Eldritch Brick", "It's Whispering...", 16, 16, ModContent.TileType<Tiles.Void.Void1>()) { } }
    public class Void2Item : StandardTileItem { public Void2Item() : base("Eldritch Stone", "It's Whispering...", 16, 16, ModContent.TileType<Tiles.Void.Void2>()) { } }
    public class VoidTorch1Item : StandardTileItem { public VoidTorch1Item() : base("Eldritch Torch", "This is backwards...", 14, 26, ModContent.TileType<Tiles.Void.VoidTorch1>()) { } }
    public class VoidTorch2Item : StandardTileItem { public VoidTorch2Item() : base("Eldritch Brazier", "This is backwards...", 16, 16, ModContent.TileType<Tiles.Void.VoidTorch2>()) { } }
    public class VoidPillarBItem : StandardTileItem { public VoidPillarBItem() : base("Eldritch Pillar Base", "", 36, 24, ModContent.TileType<Tiles.Void.VoidPillarB>()) { } }
    public class VoidPillarMItem : StandardTileItem { public VoidPillarMItem() : base("Eldritch Pillar", "", 36, 24, ModContent.TileType<Tiles.Void.VoidPillarM>()) { } }
    public class VoidPillarTItem : StandardTileItem { public VoidPillarTItem() : base("Eldritch Pillar Support", "", 36, 24, ModContent.TileType<Tiles.Void.VoidPillarT>()) { } }
    public class VoidPillarPItem : StandardTileItem { public VoidPillarPItem() : base("Eldritch Pillar Pedestal", "", 36, 24, ModContent.TileType<Tiles.Void.VoidPillarP>()) { } }

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
            item.useStyle = 1;
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
            item.useStyle = 1;
            item.consumable = true;
            item.createWall = ModContent.WallType<Tiles.Void.VoidWallPillarS>();
        }
    }
}

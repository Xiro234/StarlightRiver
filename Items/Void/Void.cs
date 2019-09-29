using Terraria.ModLoader;

namespace StarlightRiver.Items.Void
{
    public class Void1Item : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("It's Whispering");
            DisplayName.SetDefault("Eldritch Brick");
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
            item.createTile = mod.TileType("Void1");
        }
    }

    public class Void2Item : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("It's Whispering");
            DisplayName.SetDefault("Eldritch Stone");
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
            item.createTile = mod.TileType("Void2");
        }
    }

    public class VoidTorch1Item : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("This is backwards...");
            DisplayName.SetDefault("Eldritch Torch");
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
            item.createTile = mod.TileType("VoidTorch1");
        }
    }
    public class VoidTorch2Item : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("This is backwards...");
            DisplayName.SetDefault("Eldritch Brazier");
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
            item.createTile = mod.TileType("VoidTorch2");
        }
    }
    public class VoidPillarBItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eldritch Pillar Base");
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
            item.createTile = mod.TileType("VoidPillarB");
        }
    }
    public class VoidPillarMItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eldritch Pillar");
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
            item.createTile = mod.TileType("VoidPillarM");
        }
    }
    public class VoidPillarTItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eldritch Pillar Support");
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
            item.createTile = mod.TileType("VoidPillarT");
        }
    }
    public class VoidPillarPItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eldritch Pillar Pedestal");
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
            item.createTile = mod.TileType("VoidPillarP");
        }
    }
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
            item.createWall = mod.WallType<Tiles.VoidWall>();
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
            item.createWall = mod.WallType<Tiles.VoidWallPillar>();
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
            item.createWall = mod.WallType<Tiles.VoidWallPillarS>();
        }
    }
}

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
}

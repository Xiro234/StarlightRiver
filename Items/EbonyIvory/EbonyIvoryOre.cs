using Terraria.ModLoader;

namespace spritersguildwip.Items.EbonyIvory
{
    public class OreEbonyItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Heavy and Impure");
            DisplayName.SetDefault("Ebony Ore");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 14;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("OreEbony");
        }
    }
    public class OreIvoryItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Light and Pure");
            DisplayName.SetDefault("Ivory Ore");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 14;
            item.maxStack = 999;
        }
    }
}

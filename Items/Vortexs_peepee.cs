using Terraria.ModLoader;

namespace spritersguildwip.Items
{
    public class voidgoo : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Can be passed with a shadow dash");
            DisplayName.SetDefault("Void Goo");
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
            item.consumable = true;
            item.createTile = mod.TileType("Dark");
        }
    }
}

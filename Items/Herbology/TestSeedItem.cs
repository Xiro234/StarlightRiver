using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{

    public class TestSeedItem : ModItem
    {
        public override string Texture => "StarlightRiver/Invisible";
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("wip");
            DisplayName.SetDefault("Test Seed Item");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 621;
            item.rare = 2;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
        }
    }
}

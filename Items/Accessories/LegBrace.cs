using Terraria.ModLoader;

namespace StarlightRiver.Items.Accessories
{
    public class LegBrace : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leg Brace");
            Tooltip.SetDefault("Unimplimented Function");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }
    }
}
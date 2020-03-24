using StarlightRiver.Abilities;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Accessories
{
    public class LegBrace : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Leg Brace");
            DisplayName.SetDefault("Unimplimented Function");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }
    }
}
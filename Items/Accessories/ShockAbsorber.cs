using StarlightRiver.Abilities;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Accessories
{
    public class ShockAbsorber : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shock Absorber");
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
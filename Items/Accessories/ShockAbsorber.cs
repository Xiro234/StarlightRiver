using StarlightRiver.Abilities;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Accessories
{
    public class ShockAbsorber : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shock Absorber");
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
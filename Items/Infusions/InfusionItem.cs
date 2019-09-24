using StarlightRiver.Ability;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Infusions
{
    public class DashComet : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;
            item.useStyle = 5;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = 3;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Comet Dash");
            Tooltip.SetDefault("Unlocks the comet dash infusion\nClick an infusion slot to equip");
        }

        public override bool UseItem(Player player)
        {
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            mp.upgradeUnlock[0] = 1;
            return true;
        }
    }
    public class DashFire : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;
            item.useStyle = 5;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = 3;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flaming Slash");
            Tooltip.SetDefault("Unlocks the flaming slash infusion\nClick an infusion slot to equip");
        }

        public override bool UseItem(Player player)
        {
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            mp.upgradeUnlock[1] = 1;
            return true;
        }
    }
}
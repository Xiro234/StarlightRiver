using StarlightRiver.Abilities;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Accessories
{
    public class ResonantVessel : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Adds 1 max stamina");
            DisplayName.SetDefault("Resonant Vessel");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            mp.StatStaminaMaxTemp += 1;
        }
    }

    public class RingofStamina : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases stamina regeneration");
            DisplayName.SetDefault("Band of Stamina");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            mp.StatStaminaRegenMax -= 60;
        }
    }
}
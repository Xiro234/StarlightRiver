using StarlightRiver.Abilities;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Accessories
{
    public class BroochOfRage : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Getting hit has a chance to recover stamina");
            DisplayName.SetDefault("Brooch of Rage");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }
        public override void UpdateEquip(Player player) 
        {
            AccessoryHandler mp = Main.LocalPlayer.GetModPlayer<AccessoryHandler>();
            mp.accessories[0] = 1;
        }
    }
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
            AccessoryHandler mp = Main.LocalPlayer.GetModPlayer<AccessoryHandler>();
            mp.accessories[1] = 1;
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
            AccessoryHandler mp = Main.LocalPlayer.GetModPlayer<AccessoryHandler>();
            mp.accessories[2] = 1;
        }
    }
}

using spritersguildwip.Ability;
using Terraria;
using Terraria.ModLoader;

namespace spritersguildwip.Items.Accessories
{
    public class BloodyScarf : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Getting hit has a chance to recover stamina\n'Soaked with blood'");
            DisplayName.SetDefault("Bloody Scarf");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            
        }
    }
    public class ViteousTimepiece : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Using an ability has a chance to recover a point of stamina\n'Filled with that strange sand'");
            DisplayName.SetDefault("Vitreous Hourglass");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
            for (int k = 0; k < mp.justUsedAbility.Length; k++)
            {
                if (mp.justUsedAbility[k] != 0)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        mp.stamina += 1;
                    }
                }
            }
        }
    }
    public class RingofStamina : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases stamina regeneration significantly");
            DisplayName.SetDefault("Ring of Stamina");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
            mp.staminaTickerMax -= 60;
        }
    }
}

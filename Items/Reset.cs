using spritersguildwip.Ability;
using Terraria;
using Terraria.ModLoader;

namespace spritersguildwip.Items
{
    public class Reset : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;
            item.useStyle = 5;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reset Button");
            Tooltip.SetDefault("Resets all movement abilities");
        }

        public override bool UseItem(Player player)
        {
            AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
            for(int k = 0; k <= 3; k++)
            {
                mp.ability[k] = 0;
            }
            return true;
        }
    }
    public class  plus : Reset
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plus");
            Tooltip.SetDefault("+1 Max Stamina!");
        }
        public override bool UseItem(Player player)
        {
            AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
            if (mp.staminamax < 5)
            {
                mp.staminamax++;
            }
            return true;
        }
    }
    public class fill : Reset
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fill");
            Tooltip.SetDefault("Fills up your stamina!");
        }
        public override bool UseItem(Player player)
        {
            AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
            mp.stamina = mp.staminamax;
            return true;
        }
    }
}


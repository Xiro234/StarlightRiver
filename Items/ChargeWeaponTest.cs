using Terraria;
using Terraria.ModLoader;

namespace spritersguildwip.Items
{
    public class ChargeWeaponTest : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;
            item.useStyle = 5;
            item.useAnimation = 60;
            item.useTime = 60;
            item.shootSpeed = 9f;
            item.knockBack = 2f;
            item.damage = 2;
            item.rare = 1;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.noMelee = true;
            item.noUseGraphic = true;
            item.magic = true;
            item.channel = true;
            item.shoot = mod.ProjectileType("ChargeWeaponTest");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The ChargeWeaponTest");
            Tooltip.SetDefault("ChargeWeaponTest moment");
        }
    }
}

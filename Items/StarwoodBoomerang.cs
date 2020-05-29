using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    public class StarwoodBoomerang : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starwood Boomerang");
            Tooltip.SetDefault("Tooltip");
        }

        public override void SetDefaults()
        {
            item.damage = 20;//also set on the projectile's side because it keeps getting reset to zero
            item.width = 18;
            item.height = 34;
            item.useTime = 10;
            item.useAnimation = 10;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shoot = ModContent.ProjectileType<Projectiles.WeaponProjectiles.StarwoodBoomerangProjectile>();
            item.shootSpeed = 10f;//this is also set on the projectile's side, if this is changed change it there too
        }
    }
}
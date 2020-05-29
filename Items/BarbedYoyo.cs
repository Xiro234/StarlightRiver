using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    public class BarbedYoyo : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.useStyle = 5;
            item.useAnimation = 30;
            item.useTime = 30;
            item.autoReuse = false;
            item.damage = 18;
            item.rare = 2;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.melee = true;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[PH] barbed wire yoyo");
            Tooltip.SetDefault("Links your foes together!");
        }

        public override bool UseItem(Player player)
        {
            player.channel = true;
            Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.WeaponProjectiles.BarbedYoyo>(), item.damage, item.knockBack, player.whoAmI);
            return true;
        }
    }
}

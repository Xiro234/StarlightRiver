using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace spritersguildwip.Items
{
    public class WispSuck : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 42;
            item.useStyle = 5;
            item.useAnimation = 8;
            item.useTime = 8;
            item.shootSpeed = 8f;
            item.shoot = mod.ProjectileType("WispSuck");
            item.rare = 2;
            item.noMelee = true;
            item.autoReuse = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 34); //
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 35f;
            position += muzzleOffset;

            for (int k = 0; k < item.shootSpeed; k++)
            {
                if (k >= 0)
                {
                    Projectile projectile = Main.projectile[Projectile.NewProjectile(position, new Vector2(0f, 0f), item.shoot, damage, knockBack, player.whoAmI)];
                    projectile.width = 16 * (k * 2);
                    projectile.height = projectile.width;
                    if (k < 2)
                    {
                        projectile.ai[1] = 1;
                    }
                    position += muzzleOffset;
                }
            }
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Wisp Suck");
            Tooltip.SetDefault("Succ");
        }
    }
    public class Wisp : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 20;
            item.useStyle = 1;
            item.useAnimation = 8;
            item.useTime = 8;
            item.shootSpeed = 8f;
            item.rare = 1;
            item.noMelee = true;
            item.autoReuse = false;
            item.noUseGraphic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Wisp");
            Tooltip.SetDefault("Don't be cruel to it!");
        }
    }
}

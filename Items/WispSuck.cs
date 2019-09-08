using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            for (int k = 0; k < item.shootSpeed; k++)
            {
                position += muzzleOffset;
                if (k != 0)
                {
                    Projectile projectile = Main.projectile[Projectile.NewProjectile(position, new Vector2(0f, 0f), item.shoot, damage, knockBack, player.whoAmI)];
                    projectile.width = 16 * (k * 2);
                    projectile.height = projectile.width;
                    if (k <= 1)
                    {
                        projectile.ai[1] = 1;
                    }
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
            item.width = 16;
            item.height = 16;
            item.useStyle = 1;
            item.rare = 3;
            item.autoReuse = false;
            item.noUseGraphic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Wisp");
            Tooltip.SetDefault("Don't be cruel to it!");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 6));
        }
    }
}

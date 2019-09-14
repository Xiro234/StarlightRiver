using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Vitric
{
    public class VitricStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 34;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.useAnimation = 20;
            item.useTime = 20;
            item.shootSpeed = 6f;
            item.knockBack = 2f;
            item.damage = 18;
            item.shoot = mod.ProjectileType("VitricIcicleProjectile");
            item.rare = 2;
            item.noMelee = true;
            item.magic = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 35f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4));
            Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, item.shoot, damage, knockBack, player.whoAmI);
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitric Staff");
            Tooltip.SetDefault("Vitric Staff");
        }
    }
}

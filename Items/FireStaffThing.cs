using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace spritersguildwip.Items
{
    public class FireStaffThing : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 34;
            item.useStyle = 5;
            Item.staff[item.type] = true;

            item.useAnimation = 40;
            item.useTime = 40;
            item.shootSpeed = 8f;
            item.knockBack = 2f;
            item.damage = 24;
            item.shoot = mod.ProjectileType("FireStaffThing");
            item.rare = 3;
            item.noMelee = true;
            item.magic = true;
            item.channel = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(18));
            Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, item.shoot, damage, knockBack, player.whoAmI);
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VortexPenisStaff 2");
            Tooltip.SetDefault("VortexPenisStaff moment 2");
        }
    }
}

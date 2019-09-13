using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    public class FireStaffThing2 : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 46;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.useAnimation = 50;
            item.useTime = 50;
            item.shootSpeed = 12f;
            item.knockBack = 2f;
            item.damage = 76;
            item.shoot = mod.ProjectileType("FireStaffThing2");
            item.rare = 6;
            item.noMelee = true;
            item.magic = true;
            item.useTurn = false;
            item.channel = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 14); //boom
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 73); //fork
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 65f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(21));
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

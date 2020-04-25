using Microsoft.Xna.Framework;
using StarlightRiver.Projectiles;
using StarlightRiver.Projectiles.WeaponProjectiles;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    public class ThoriumRod : ModItem
    {
        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 45;
            item.useTime = 45;
            item.shootSpeed = 10f;
            item.knockBack = 2f;
            item.width = 60;
            item.height = 72;
            item.damage = 72;
            Item.staff[item.type] = true;
            item.rare = 4;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.noMelee = true;
            item.autoReuse = true;
            item.mana = 24;
            item.magic = true;
            item.shoot = ModContent.ProjectileType<ThoriumRodProjectile>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thorium Rord");
            Tooltip.SetDefault("You like throw it and like\nIt like has this aura around itself that like, damages enemies\nIt's called thorium cause get it thtats like a terraria mod");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 55f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }
    }
}

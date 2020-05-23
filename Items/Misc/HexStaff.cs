using Microsoft.Xna.Framework;
using StarlightRiver.Projectiles;
using StarlightRiver.Projectiles.Ammo;
using StarlightRiver.Projectiles.WeaponProjectiles.HexStaff;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Misc
{
    public class HexStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 24;
            item.useTime = 24;
            item.shootSpeed = 14f;
            item.knockBack = 2f;
            item.width = 60;
            item.height = 17;
            item.damage = 72;
            Item.staff[item.type] = true;
            item.rare = 4;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.noMelee = true;
            item.autoReuse = true;
            item.mana = 12;
            item.magic = true;
            item.shoot = ModContent.ProjectileType<HexBolt>();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hex staff");
            Tooltip.SetDefault("Shoot hex bolt\nRight to make rift\nIf rift exist, hex bolt home in\nRift eat bolt, shoot out more bolt at enemy");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y), Vector2.Zero, ModContent.ProjectileType<HexRift>(), damage, knockBack, player.whoAmI);
                return false;
            }
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 55f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }
    }
}

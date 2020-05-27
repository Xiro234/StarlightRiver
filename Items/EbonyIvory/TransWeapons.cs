using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.EbonyIvory
{
    public class TransGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("");
            DisplayName.SetDefault("Trans Gun");
        }
        public static int transCharge = 0;
        public override void SetDefaults()
        {
            item.damage = 20;
            item.ranged = true;
            item.width = 40;
            item.height = 20;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 10000;
            item.rare = 2;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        //>= 100 ready to transform, <= -100 transformed, neither is normal
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                transCharge += 1;
                if (transCharge >= 6)
                {
                    transCharge = 6;
                    Main.NewText("Whip and NaeNae ready");
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/WhipAndNaenae"));
                }
                item.autoReuse = false;
                item.useTime = 18;
                item.useAnimation = 18;
            }
            else
            {
                if (transCharge >= 6)
                {
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 14); //boom
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 73); //fork
                    transCharge = 0;
                    item.autoReuse = true;
                    item.useTime = 4;
                    item.useAnimation = 24;
                }
                else
                {
                    return false;
                }
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 11); //gun
            int bullet = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
            return false;
        }
    }
}

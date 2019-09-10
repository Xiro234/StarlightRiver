using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace spritersguildwip.Items.EbonyIvory
{
    public class TransGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Trans Gun");
            DisplayName.SetDefault("");
        }
        public int transCharge = 0;
        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 14;
            item.useTurn = true;
            item.autoReuse = false;
            item.useAnimation = 8;
            item.useTime = 8;
            item.useStyle = 5;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                transCharge += 100;
            }
            else
            {
                if (transCharge >= 100)
                {
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 14); //boom
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 73); //fork
                    transCharge = -100;
                }
                else
                {
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 2);
                }
            }
            return base.UseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            int bullet = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, item.shoot, damage, knockBack, player.whoAmI);
            return false;
        }
    }
}

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Items.EbonyIvory
{
    public class TransHandler : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[projectile.owner];
            ModItem item = owner.HeldItem.modItem;
            if (item is TransGun)
            {
                item.item.TurnToAir();
            }
            base.OnHitNPC(projectile, target, damage, knockback, crit);
        }
    }
}
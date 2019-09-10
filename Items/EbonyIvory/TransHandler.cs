using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Items.EbonyIvory
{
    public class TransHandler : GlobalProjectile
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
      /*  TransGun myClassVariable = ModItem as TransGun;
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[projectile.owner];
            if (TransWeapon != null)
            {
                TransWeapon.transCharge += 1;
            }
            base.OnHitNPC(projectile, target, damage, knockback, crit);
        }*/
    }
}
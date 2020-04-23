using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.EbonyIvory
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
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[projectile.owner];
            base.OnHitNPC(projectile, target, damage, knockback, crit);
        }
    }
}
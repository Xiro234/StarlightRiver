using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Projectiles
{
    public class AOEExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 4;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosion");
        }
    }
}
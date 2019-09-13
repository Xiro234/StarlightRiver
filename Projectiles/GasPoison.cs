using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles
{
    public class GasPoison : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 64;
            projectile.height = 64;
            projectile.penetrate = -1;
            projectile.timeLeft = 180;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.damage = 5;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrosive Spores");
        }
        public override void AI()
        {
            projectile.alpha = 255 - (int)((float)projectile.timeLeft / 180 * 255f);
            projectile.rotation += 0.02f;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
        }
    }
}
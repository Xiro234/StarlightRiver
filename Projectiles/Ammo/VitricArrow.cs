using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.Ammo
{
    class VitricArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 270;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitric Arrow");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 25f)
            {
                projectile.velocity.Y += 0.05f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, projectile.velocity, mod.ProjectileType("VitricArrowShattered"), 15, 0, projectile.owner);
            Projectile.NewProjectile(target.Center, projectile.velocity.RotatedBy(0.3), mod.ProjectileType("VitricArrowShattered"), 15, 0, projectile.owner);
            Projectile.NewProjectile(target.Center, projectile.velocity.RotatedBy(-0.25), mod.ProjectileType("VitricArrowShattered"), 15, 0, projectile.owner);
        }
    }
    class VitricArrowShattered : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 15;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitric Arrow");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2;
        }
    }
}

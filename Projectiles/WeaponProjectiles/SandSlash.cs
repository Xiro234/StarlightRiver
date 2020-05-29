using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    class SandSlash : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.timeLeft = 45;
            projectile.width = 64;
            projectile.height = 64;
            projectile.tileCollide = false;
            projectile.extraUpdates = 2;
            projectile.penetrate = -1;
        }
        public override void AI()
        {
            projectile.ai[0]++;

            if (projectile.ai[0] == 30) projectile.knockBack *= 0;

            Vector2 relativeRot = new Vector2();
            relativeRot.X = (float)Math.Cos(projectile.ai[0] / 60 * 6.28f) * 3f;
            relativeRot.Y = (float)Math.Sin(projectile.ai[0] / 60 * 6.28f) * 10f;
            projectile.velocity = relativeRot.RotatedBy(projectile.rotation - 1.57f);

            Dust.NewDustPerfect(projectile.Center, ModContent.DustType<Dusts.Stamina>(), projectile.velocity * Main.rand.NextFloat(0.2f, 1.1f), 0, default, 1f);
            Dust.NewDustPerfect(projectile.Center, ModContent.DustType<Dusts.Sand>(), projectile.velocity * Main.rand.NextFloat(0.8f, 1.2f), 140, default, 0.7f);

            Lighting.AddLight(projectile.Center, new Vector3(0.3f, 0.2f, 0));
        }
    }
}

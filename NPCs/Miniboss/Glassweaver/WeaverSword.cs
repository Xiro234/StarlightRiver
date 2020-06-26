using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace StarlightRiver.NPCs.Miniboss.Glassweaver
{
    class WeaverSword : ModProjectile
    {
        Player target;

        Vector2 moveTarget;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glass Sword");
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.hostile = true;
            projectile.timeLeft = 600;
        }

        public override void AI()
        {
            int lifeTime = 600 - projectile.timeLeft;

            if(lifeTime == 0) target = Helper.FindNearestPlayer(projectile.Center);

            if (lifeTime <= 60)
            {
                projectile.scale = lifeTime / 60f;
                projectile.rotation = 1.57f * 0.5f + (target.Center - projectile.Center).ToRotation() + lifeTime / 20f * 6.28f;
            }

            if (lifeTime == 61)
            {
                moveTarget = target.Center;
                projectile.velocity = Vector2.Normalize(projectile.Center - moveTarget) * -7;
                Main.PlaySound(SoundID.DD2_WitherBeastAuraPulse, projectile.Center);
            }

            if(lifeTime > 61) Dust.NewDustPerfect(projectile.Center, DustType<Dusts.Air>(), Vector2.Zero, 0, default, 0.5f);
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 20; k++) Dust.NewDustPerfect(projectile.Center, DustType<Dusts.Glass3>());
            Main.PlaySound(SoundID.Shatter, projectile.Center);
        }
    }
}

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.SquidBoss
{
    class SquidEgg : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.timeLeft = 120;
            projectile.hostile = true;
            projectile.damage = 15;
        }

        public override void AI()
        {
            projectile.velocity.Y += (projectile.timeLeft > 90 ? -0.14f : 0.035f);
            projectile.velocity.X *= 0.9f;

            projectile.ai[1] += 0.1f;
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
        }

        public override void Kill(int timeLeft)
        {
            Main.NewText("Squid Birth!", Color.Blue);
        }
    }
}

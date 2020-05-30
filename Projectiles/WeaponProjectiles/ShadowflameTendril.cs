using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles
{
    public class ShadowflameTendril : ModProjectile
    {
        public override string Texture => "StarlightRiver/Invisible";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadowflame");
        }

        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.ranged = true;
            projectile.aiStyle = 91;
            aiType = 496;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 120;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 200);
        }

        private readonly float rand1 = Main.rand.NextFloat(0.1f) - 0.05f;
        private readonly float rand2 = 5f + Main.rand.NextFloat(6f);

        public override void AI()
        {
            projectile.velocity = Vector2.Normalize(projectile.velocity).RotatedBy(rand1) * rand2;
        }
    }
}
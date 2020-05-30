using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    public class ShadowflameGrenade : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.aiStyle = -2;
            projectile.tileCollide = true;
            projectile.timeLeft = 600;
            projectile.extraUpdates = 2;
            projectile.ignoreWater = true;
        }

        public void spawnShadowflame(int angle)
        {
            Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.ToRadians((angle + Main.rand.Next(40) - 20)));
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<ShadowflameTendril>(), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 200);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item62, projectile.position);
            Main.PlaySound(SoundID.Item103, projectile.position);
            int max = 4 + Main.rand.Next(2);
            for (int i = 0; i <= 4 + Main.rand.Next(2); i++)
            {
                spawnShadowflame((360 / max) * i);
            }
            for (int i = 0; i < 24; i++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 27, 0f, 0f, 100, default(Color), 1.6f);
                Main.dust[dust].velocity *= Main.rand.NextFloat(0.8f) + 1.6f;
            }
            //make it actually do the thing
        }

        public override void AI()
        {
            Vector2 dustOffset = (Vector2.One * projectile.height / 2).RotatedBy(Main.time / 3);
            Dust dust1 = Dust.NewDustPerfect(projectile.Center + dustOffset, 27, Vector2.Zero, 100);
            dust1.fadeIn = 100;
            dust1.scale = 1.6f;
            Dust dust2 = Dust.NewDustPerfect(projectile.Center - dustOffset, 27, Vector2.Zero, 100);
            dust2.fadeIn = 100;
            dust2.scale = 1.6f;
            if (projectile.timeLeft <= 565)
            {
                projectile.velocity.Y += 0.04f;
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadowflame Grenade");
        }
    }
}
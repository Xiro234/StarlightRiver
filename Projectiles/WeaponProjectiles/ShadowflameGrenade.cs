using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
            projectile.aiStyle = -1;
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
        public void makeSpirals(int spiralCount, float length, float angleIntensity,float rotationOffset, Dust dust)
        {
            Vector2 cachedPos = dust.position;
            for (float currentSpiral = 0; currentSpiral <= spiralCount; currentSpiral++)
            {
                for (float progress = 0; progress < length; progress++)
                {
                    float rotation = 6.28f * (progress / length) * angleIntensity + rotationOffset;
                    float rot = currentSpiral / spiralCount * 6.28f + rotation * 6.28f;

                    float posX = cachedPos.X + (float)(Math.Cos(rot) * progress);
                    float posY = cachedPos.Y + (float)(Math.Sin(rot) * progress);

                    Vector2 pos = new Vector2(posX + (float)(Math.Cos(rot) * progress), posY + (float)(Math.Sin(rot) * progress));
                    Dust newDust = Dust.NewDustPerfect(pos, dust.type, Vector2.Zero, dust.alpha, default, dust.scale);
                    newDust.velocity = Vector2.Normalize(pos - projectile.Center) * 5 * (progress / length);
                    newDust.fadeIn -= progress / length * dust.scale * 2;
                    newDust.scale -= progress / length * dust.scale;
                }
            }
            dust.active = false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item62, projectile.position);
            Main.PlaySound(SoundID.Item103, projectile.position);
            int max = 4 + Main.rand.Next(2);
            for (int i = 0;i<=max; i++)
            {
                spawnShadowflame((360 / max) * i);
            }
            makeSpirals(5, 40, 0.05f + Main.rand.NextFloat(0.03f), Main.rand.NextFloat(6.28f), Dust.NewDustPerfect(projectile.Center, 27, Vector2.Zero, 100, default, 3.6f));
            for (int i = 0; i < 24; i++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 27, 0f, 0f, 100, default(Color), 1.6f);
                Main.dust[dust].velocity *= Main.rand.NextFloat(0.8f) + 1.6f;
            }
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
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 40;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
    }
}
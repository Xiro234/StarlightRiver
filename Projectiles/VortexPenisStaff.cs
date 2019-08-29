using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Projectiles
{
    public class VortexPenisStaff : ChargeWeaponTest
    {
        public override void SetDefaults()
        {
            projectile.width = 56;
            projectile.height = 106;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ranged = true;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VortexPenisStaff moment");
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool shouldUpdateRotation() //should the staff update its rotation after fireing
        {
            return true;
        }

        public override float maxCharge() //the max amount of ai[1]
        {
            return 16f;
        }
        public override float chargeSpeed() //how fast ai[1] increases
        {
            return 0.1f;
        }
        public override float unchargeSpeed() //how fast ai[1] decreases after releasing the trigger
        {
            return 0.5f;
        }
        public override float baseDamage() //raw damage
        {
            return 0.25f;
        }
        public override void SpawnDustCharging(float num1, float num2)
        {
            Vector2 offset = Vector2.UnitX * (projectile.height);
            offset = offset.RotatedBy((projectile.rotation - 1.57079637f), default);
            Vector2 position = projectile.Center + offset * (num1);
            position -= offset * 0.5f;

            position += -(Vector2.UnitY.RotatedBy((num2 * 3.14159274f / 6f), default) * new Vector2(4f + 1f * (num1 * 4), 8f + 2f * (num1 * 4))).RotatedBy((projectile.rotation - 1.57079637f), default); //actually a circle wow
            int dust = Dust.NewDust(position - Vector2.One * 8f, 0, 0, 6, 0f, 0f, 0, default, 0.6f);
            Main.dust[dust].scale = 1.1f * (0.3f + num1 / 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].customData = projectile.owner;
        }
        public override void SpawnDustCharged(float num1)
        {
            Vector2 offset = Vector2.UnitX * (projectile.height);
            offset = offset.RotatedBy((projectile.rotation - 1.57079637f), default);
            Vector2 position = projectile.Center + offset;
            position -= offset * 0.5f;

            Vector2 dustPos = position + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((11f * projectile.ai[1]) - (num1 * 2));
            int dust = Dust.NewDust(dustPos - Vector2.One * 8f, 16, 16, 6, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 0, default, 0.6f);
            Main.dust[dust].velocity = Vector2.Normalize(position - dustPos) * 1.5f * (10f - num1 * 2f) / 10f;
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.1f;
            Main.dust[dust].customData = projectile.owner;
        }
        public override void Shoot()
        {
            Vector2 offset = Vector2.UnitX * (projectile.height);
            offset = offset.RotatedBy((projectile.rotation - 1.57079637f), default);
            Vector2 position = projectile.Center + offset;
        }
    }
}
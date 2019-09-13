using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.NPCs;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    public class VitricIcicleProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.extraUpdates = 1;
            projectile.timeLeft = 180;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VortexPenisStaff moment 2");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            int dustType = mod.DustType("Air");
            if (Main.rand.Next(3) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 160, default(Color), 1f)];
                dust.noGravity = true;
                dust.scale = 0.8f;
                dust.fadeIn = 0.4f;
                dust.noLight = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            int icicle = Projectile.NewProjectile(projectile.Center, new Vector2(0f, 0f), mod.ProjectileType("VitricCrystalClumpProjectile"), projectile.damage, projectile.knockBack, player.whoAmI);
            Main.projectile[icicle].rotation += Main.rand.NextFloat(0f, 360f);
            for (int counter = 0; counter <= 14; counter++)
            {
                int dustType = mod.DustType("Air");
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, projectile.velocity.X, projectile.velocity.Y, 160, default(Color), 1f)];
                dust.velocity += new Vector2(Main.rand.NextFloat(-1.6f, 1.6f), Main.rand.NextFloat(-1.6f, 1.6f));
                dust.velocity = dust.velocity / 4f;
                dust.noGravity = true;
                dust.fadeIn = 1.1f;

                dust.noLight = true;
            }
        }
    }
    public class VitricCrystalClumpProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.penetrate = 4;
            projectile.tileCollide = false;
            projectile.timeLeft = 280;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VortexPenisStaff moment 2");
        }
        public override void AI()
        {
            projectile.velocity = new Vector2(0f, 0f);
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            for (int counter = 0; counter <= 14; counter++)
            {
                int dustType = mod.DustType("Air");
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, projectile.velocity.X, projectile.velocity.Y, 160, default(Color), 1f)];
                dust.velocity += new Vector2(Main.rand.NextFloat(-1.6f, 1.6f), Main.rand.NextFloat(-1.6f, 1.6f));
                dust.velocity = dust.velocity / 4f;
                dust.noGravity = true;
                dust.fadeIn = 1.1f;

                dust.noLight = true;
            }
        }
    }
}
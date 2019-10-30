using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.NPCs;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    public class BigFuckingDeathRay : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 120;
            projectile.height = 76;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 8000;
            projectile.magic = true;
            Main.projFrames[projectile.type] = 4;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VortexPenisStaff moment 2");
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame > 3)
            {
                projectile.frame = 0;
            }
            Player player = Main.player[projectile.owner];
            if (projectile.ai[0] == 0)
            {
                for (int a = 52; a < 1020; a += 51)
                { 
                    int laser = Projectile.NewProjectile(projectile.Center, new Vector2(0f, 0f), mod.ProjectileType("BigFuckingDeathRay2"), projectile.damage, projectile.knockBack, player.whoAmI);
                    Main.projectile[laser].position.Y -= a;
                    Main.projectile[laser].ai[0] = (int)projectile.whoAmI;
                }
                projectile.ai[0] = 1;
            }
            Vector2 MousePos = new Vector2(Main.MouseWorld.X - projectile.Center.X, Main.MouseWorld.Y - projectile.Center.Y);
            MousePos.Normalize();
            projectile.velocity = MousePos * Vector2.Distance(projectile.Center, Main.MouseWorld) * 0.01f;
            if (!Main.mouseLeft)
            {
                projectile.ai[1] += 2;
            }
            else if (projectile.ai[1] > 0)
            {
                projectile.ai[1] -= 1;
            }
            if (projectile.ai[1] >= 180)
            {
                Projectile.NewProjectile(projectile.Center, new Vector2(0f, 0f), mod.ProjectileType("BigFuckingLaserPointer"), projectile.damage, projectile.knockBack, player.whoAmI);
                projectile.Kill();
            }
            Vector2 vectorToCursor = projectile.Center - player.Center;
            bool projDirection = projectile.Center.X < player.Center.X;
            if (projectile.Center.X < player.Center.X)
            {
                vectorToCursor = -vectorToCursor;
            }
            player.direction = ((projDirection) ? -1 : 1);
            player.itemRotation = vectorToCursor.ToRotation();
            player.itemTime = 20;
            player.itemAnimation = 20;
        }
    }
    public class BigFuckingLaserPointer : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 42;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 8000;
            projectile.magic = true;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VortexPenisStaff moment 2");
        }
        public override void AI()
        {

            projectile.rotation += 0.1f;
            projectile.scale += 0.02f;
            if (projectile.scale >= 1.5f)
            {
                projectile.scale = 1f;
            }
            Player player = Main.player[projectile.owner];
            Vector2 MousePos = new Vector2(Main.MouseWorld.X - projectile.Center.X, Main.MouseWorld.Y - projectile.Center.Y);
            MousePos.Normalize();
            projectile.velocity = MousePos * Vector2.Distance(projectile.Center, Main.MouseWorld) * 0.08f;
            if (Main.mouseLeft)
            {
                projectile.ai[1] += 2;
            }
            else if (projectile.ai[1] > 0)
            {
                projectile.ai[1] -= 1;
            }
            if (projectile.ai[1] >= 180)
            {
                Projectile.NewProjectile(projectile.Center, new Vector2(0f, 0f), mod.ProjectileType("BigFuckingDeathRay"), projectile.damage, projectile.knockBack, player.whoAmI);
                projectile.Kill();
            }
            Vector2 vectorToCursor = projectile.Center - player.Center;
            bool projDirection = projectile.Center.X < player.Center.X;
            if (projectile.Center.X < player.Center.X)
            {
                vectorToCursor = -vectorToCursor;
            }
            player.direction = ((projDirection) ? -1 : 1);
            player.itemRotation = vectorToCursor.ToRotation();
            player.itemTime = 20;
            player.itemAnimation = 20;
        }
    }
    public class BigFuckingDeathRay2 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 120;
            projectile.height = 52;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 8000;
            projectile.magic = true;
            Main.projFrames[projectile.type] = 4;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VortexPenisStaff moment 2");
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame > 3)
            {
                projectile.frame = 0;
            }
            Player player = Main.player[projectile.owner];
            Projectile owner = Main.projectile[(int)projectile.ai[0]];
            if (player.ownedProjectileCounts[mod.ProjectileType("BigFuckingDeathRay")] != 0)
            {
                projectile.velocity = owner.velocity;
            }
            else
            {
                projectile.Kill();
            }
        }
    }
}
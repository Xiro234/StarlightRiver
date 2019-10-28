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
            if (projectile.ai[1] == 0)
            {
                for (int a = 52; a < 1020; a += 51)
                { 
                    int laser = Projectile.NewProjectile(projectile.Center, new Vector2(0f, 0f), mod.ProjectileType("BigFuckingDeathRay2"), projectile.damage, projectile.knockBack, player.whoAmI);
                    Main.projectile[laser].position.Y -= a;
                    Main.projectile[laser].ai[0] = (int)projectile.whoAmI;
                }
                projectile.ai[1] = 1;
            }
            Vector2 MousePos = new Vector2(projectile.Center.X + Main.MouseWorld.X, projectile.Center.Y + Main.MouseWorld.Y);
            MousePos.Normalize();
            projectile.velocity = MousePos * Vector2.Distance(projectile.Center, Main.MouseWorld) * 0.01f;
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
            Projectile owner = Main.projectile[(int)projectile.ai[0]];
            projectile.velocity = owner.velocity;
        }
    }
}
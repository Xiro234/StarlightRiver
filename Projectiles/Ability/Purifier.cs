using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.Ability
{
    class Purifier : ModProjectile
    {
        public Vector2 start;
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 598;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gamer Gas");
            Main.projFrames[projectile.type] = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            Tile target = Main.tile[(int)projectile.Center.X / 16, (int)projectile.Center.Y / 16];
            if (Main.rand.Next(15) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Purify"),0,0,0,default, 1.8f);              
            }
            if (Main.rand.Next(2) == 0)
            {
                Dust.NewDust(projectile.position + new Vector2(8, 8), 16, 16, mod.DustType("Purify"), 0, 0, 0, default, 1.6f);
            }
            if (projectile.timeLeft == 1)
            {
                for (int k = 0; k <= 10; k++)
                {
                    int power = Main.rand.Next(0, 20);
                    Dust.NewDust(projectile.Center, 1, 1, ModContent.DustType<Dusts.Purify2>(), projectile.velocity.X * power, projectile.velocity.Y * power, 0, default, 2 - power / 10f);
                }
            }

            if (projectile.timeLeft >= 555)
            {
                projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2;

                if (target.type == TileID.Stone) { target.type = (ushort)mod.TileType("StonePure"); }
                if (target.type == (ushort)mod.TileType("OreEbony")) { target.type = (ushort)mod.TileType("OreIvory"); }
                if (target.type == (ushort)mod.TileType("VoidDoorOn")) { target.type = (ushort)mod.TileType("VoidDoorOff"); }

            }

            if(projectile.timeLeft == 550)
            {
                projectile.velocity *= -0.087f;
            }

            if (projectile.timeLeft < 550)
            {
                if (target.type == (ushort)mod.TileType("StonePure")) { target.type = TileID.Stone; }
                if (target.type == (ushort)mod.TileType("OreIvory")) { target.type = (ushort)mod.TileType("OreEbony"); }
                if (target.type == (ushort)mod.TileType("VoidDoorOff")) { target.type = (ushort)mod.TileType("VoidDoorOn"); }
            }

            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 8)
                {
                    projectile.frame = 0;
                }
            }
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Projectiles
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
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.ignoreWater = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gamer Gas");
        }

        public override void AI()
        {
            Tile target = Main.tile[(int)projectile.Center.X / 16, (int)projectile.Center.Y / 16];
            if (Main.rand.Next(5) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Gold"));
            }

            if(projectile.timeLeft >= 555)
            {
                if (target.type == TileID.Stone) { target.type = (ushort)mod.TileType("StonePure"); }
                if (target.type == (ushort)mod.TileType("OreEbony")) { target.type = (ushort)mod.TileType("OreIvory"); }

            }

            if(projectile.timeLeft == 550)
            {
                projectile.velocity *= -0.087f;
            }

            if (projectile.timeLeft < 550)
            {
                if (target.type == (ushort)mod.TileType("StonePure")) { target.type = TileID.Stone; }
                if (target.type == (ushort)mod.TileType("OreIvory")) { target.type = (ushort)mod.TileType("OreEbony"); }
            }


        }
    }
}

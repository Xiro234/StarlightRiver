using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles
{
    class Clentam : ModProjectile
    {
        //public Vector2 start;
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 400;
            projectile.tileCollide = false;
            projectile.ignoreWater = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gamer Gas");
        }

        public override void AI()
        {
            for (int y = -3; y <= 3; y++)
            {
                for (int x = -3; x <= 3; x++)
                {
                    Tile target = Main.tile[x + (int)projectile.Center.X / 16, y + (int)projectile.Center.Y / 16];

                    if (target.type == TileID.JungleGrass) { target.type = (ushort)mod.TileType("GrassJungleCorrupt"); }
                    if (target.wall == WallID.JungleUnsafe) { target.wall = (ushort)ModContent.WallType<Tiles.JungleCorrupt.WallJungleCorrupt>(); }
                }
            }
        }
    }
    class Clentam2 : ModProjectile
    {
        //public Vector2 start;
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 400;
            projectile.tileCollide = false;
            projectile.ignoreWater = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gamer Gas");
        }

        public override void AI()
        {
            for (int y = -3; y <= 3; y++)
            {
                for (int x = -3; x <= 3; x++)
                {
                    Tile target = Main.tile[x + (int)projectile.Center.X / 16,y + (int)projectile.Center.Y / 16];

                    if (target.type == TileID.JungleGrass) { target.type = (ushort)mod.TileType("GrassJungleBloody"); }
                }
            }
        }
    }
    class Clentam3 : ModProjectile
    {
        //public Vector2 start;
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 400;
            projectile.tileCollide = false;
            projectile.ignoreWater = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gamer Gas");
        }

        public override void AI()
        {
            for (int y = -3; y <= 3; y++)
            {
                for (int x = -3; x <= 3; x++)
                {
                    Tile target = Main.tile[x + (int)projectile.Center.X / 16, y + (int)projectile.Center.Y / 16];

                    if (target.type == TileID.JungleGrass) { target.type = (ushort)mod.TileType("GrassJungleHoly"); }
                }
            }
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles
{
    class Clentam : ModProjectile
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

            if (target.type == TileID.JungleGrass) { target.type = (ushort)mod.TileType("GrassJungleCorrupt"); }
        }
    }
    class Clentam2 : ModProjectile
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

            if (target.type == TileID.JungleGrass) { target.type = (ushort)mod.TileType("GrassJungleBloody"); }
        }
    }
    class Clentam3 : ModProjectile
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

            if (target.type == TileID.JungleGrass) { target.type = (ushort)mod.TileType("GrassJungleHoly"); }
        }
    }
}

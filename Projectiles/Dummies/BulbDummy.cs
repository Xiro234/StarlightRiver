using Microsoft.Xna.Framework;
using StarlightRiver.Abilities;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.Dummies
{
    class BulbDummy : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");

        }
        public override string Texture => "StarlightRiver/Invisible";
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            int hostX = (int)projectile.position.X / 16;
            int hostY = (int)projectile.position.Y / 16;
            Tile hostTile = Main.tile[hostX, hostY];

            if (Main.player.Any(p => AbilityHelper.CheckWisp(p, projectile.Hitbox)) && hostTile.frameX == 0)
            {
                for (int k = 0; k < 40; k++) Dust.NewDustPerfect(projectile.Center, ModContent.DustType<Dusts.Gold2>(), Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(1.2f, 1.4f));

                for (int x = hostX; x <= hostX + 1; x++)
                    for (int y = hostY; y <= hostY + 1; y++)
                        Main.tile[x, y].frameX += 34;

                Main.NewText("Harvest!");

            }
            projectile.timeLeft = 2;

            if (hostTile.type != ModContent.TileType<Tiles.Overgrow.BulbFruit>())
            {
                projectile.timeLeft = 0;
            }
        }
    }
}

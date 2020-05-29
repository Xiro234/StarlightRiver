using Microsoft.Xna.Framework;
using StarlightRiver.Abilities;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.Dummies
{
    class VitricOreDummy : ModProjectile
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
            foreach (Player player in Main.player.Where(player => Vector2.Distance(player.Center, projectile.Center) <= 100))
            {
                AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

                if (AbilityHelper.CheckDash(player, projectile.Hitbox))
                {
                    WorldGen.KillTile((int)projectile.position.X / 16, (int)projectile.position.Y / 16);

                    for (int k = 0; k <= 20; k++)
                    {
                        Dust.NewDust(projectile.position, 32, 32, ModContent.DustType<Dusts.Glass2>(), 0, 0, 0, default, 1.3f);
                        Dust.NewDust(projectile.position, 32, 32, ModContent.DustType<Dusts.Air>(), 0, 0, 0, default, 0.8f);
                    }
                }
            }
            projectile.timeLeft = 2;

            if (Main.tile[(int)projectile.Center.X / 16, (int)projectile.Center.Y / 16].type != ModContent.TileType<Tiles.Vitric.VitricOre>() &&
                Main.tile[(int)projectile.Center.X / 16, (int)projectile.Center.Y / 16].type != ModContent.TileType<Tiles.Vitric.VitricOreFloat>())
            {
                projectile.timeLeft = 0;
            }
        }
    }
}

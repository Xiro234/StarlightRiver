using Microsoft.Xna.Framework;
using StarlightRiver.Projectiles;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Buffs
{
    public class StarfallCocktailBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Starfall Cocktail");
            Description.SetDefault("Stars are falling much more frequently");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (!Main.dayTime)
            {
                if (Main.rand.Next(1000) < 10f * (Main.maxTilesX / 4200))
                {
                    int xPos = (Main.rand.Next(Main.maxTilesX - 50) + 100) * 16;
                    int yPos = Main.rand.Next((int)(Main.maxTilesY * 0.05)) * 16;
                    Vector2 vector = new Vector2(xPos, yPos);
                    float speedX = Main.rand.Next(-100, 101);
                    float speedY = (Main.rand.Next(200) + 100);
                    float multiplier = (float)Math.Sqrt((speedX * speedX + speedY * speedY));
                    multiplier = 12 / multiplier;
                    speedX *= multiplier;
                    speedY *= multiplier;
                    Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, ModContent.ProjectileType<StarfallCocktailStar>(), 1000, 10f, player.whoAmI);
                }
            }
        }
    }
}

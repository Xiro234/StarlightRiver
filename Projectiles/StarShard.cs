using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles
{
    internal class StarShard : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Fragment");
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k <= 50; k++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Starlight"), Main.rand.NextFloat(-30, 30), Main.rand.NextFloat(-30, 30), 0, default, 1.8f);
            }
            if (Vector2.Distance(projectile.position, Main.LocalPlayer.position) <= 800)
            {
                if (Main.rand.Next(1) == 0)
                {
                    Item.NewItem(projectile.position, ModContent.ItemType<Items.Crafting.AluminumOre>(), Main.rand.Next(4));
                }
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem(projectile.position, ModContent.ItemType<Items.Crafting.Starlight>(), 1);
                }
            }
        }
        public override void AI()
        {
            projectile.timeLeft = 2;
            projectile.velocity = new Vector2(-2, 6);
            Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Starlight"));
            if (Main.dayTime)
            {
                projectile.active = false;
            }
        }
    }
}

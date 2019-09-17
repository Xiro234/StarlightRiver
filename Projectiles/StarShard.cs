using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles
{
    class StarShard : ModProjectile
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
            for(int k = 0; k <= 50; k++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Starlight"), Main.rand.NextFloat(-30, 30), Main.rand.NextFloat(-30, 30), 0, default, 1.8f);               
            }

            if (Main.rand.Next(4) == 0 && Vector2.Distance(projectile.position, Main.LocalPlayer.position) <= 800)
            {
                Item.NewItem(projectile.position, mod.ItemType<Items.Crafting.AstralOre>(), Main.rand.Next(10));              
            }

            if (Main.rand.Next(2) == 0 && Vector2.Distance(projectile.position, Main.LocalPlayer.position) <= 800)
            {
                Item.NewItem(projectile.position, mod.ItemType<Items.Crafting.StardustSoul>(), 1);
            }
        }
        public override void AI()
        {
            projectile.timeLeft = 2;
            projectile.velocity = new Vector2(-2, 8);
            Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Starlight"));
        }      
    }
}

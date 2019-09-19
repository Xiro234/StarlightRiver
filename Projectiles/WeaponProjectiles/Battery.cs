using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.Ammo
{
    class Battery : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 1000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Missile Battery");
        }

        bool picked = false;
        NPC target = Main.npc[0];
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2;
            if (!picked)
            {
                for (int k = 0; k < Main.npc.Length; k++)
                {
                    if(Vector2.Distance(Main.npc[k].Center, projectile.Center) < Vector2.Distance(target.Center, projectile.Center) && Main.npc[k].active && !Main.npc[k].friendly) 
                    {
                        target = Main.npc[k];
                    }
                }
                picked = true;
                projectile.velocity *= 2f;
            }

            if (Main.rand.Next(2) == 0)
            {
                Dust.NewDust(projectile.position, 1, 1, mod.DustType<Dusts.Gold>(), 0, 0, 0, default, 0.4f);
            }
            if (Vector2.Distance(target.Center, projectile.Center) <= 800)
            {
                projectile.velocity += Vector2.Normalize(target.Center - projectile.Center) * 0.14f;
            }
        }
    }
}

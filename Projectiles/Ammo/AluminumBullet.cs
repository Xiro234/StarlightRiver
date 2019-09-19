using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.Ammo
{
    class AluminumBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.damage = 8;
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 270;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.extraUpdates = 3;
            aiType = ProjectileID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Tracer");
        }

        bool picked = false;
        NPC target = Main.npc[0];
        public override void AI()
        {                     
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
                projectile.velocity *= 0.5f;
            }

            Dust.NewDust(projectile.position, 1, 1, mod.DustType<Dusts.Starlight>(),0,0,0,default,0.4f);
            if (Vector2.Distance(target.Center, projectile.Center) <= 800)
                {
                projectile.velocity += Vector2.Normalize(target.Center - projectile.Center) * 0.05f;
            }
        }
    }
}

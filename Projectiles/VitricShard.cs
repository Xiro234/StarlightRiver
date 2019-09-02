using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Projectiles
{
    public class VitricShard : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.penetrate = 2;
            projectile.timeLeft = 180;
            projectile.melee = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitric Shard");
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.timeLeft <= 10 || projectile.timeLeft >= 160)
            {
                return false;
            }
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 1;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.rotation += 0.5f;
            if (projectile.timeLeft < 10 || projectile.penetrate < 2)
            {
                if (projectile.Hitbox.Intersects(player.Hitbox))
                {
                    projectile.Kill();
                }
                projectile.alpha = 255;
                Dust.NewDustPerfect(projectile.Center, mod.DustType("Air"), new Vector2(Main.rand.Next(-5, 5) * 0.1f, Main.rand.Next(-5, 5) * 0.1f));
                projectile.velocity = Vector2.Normalize(player.Center - projectile.Center) * (10f);
                projectile.timeLeft = 10;
                return;
            }
            if (projectile.timeLeft <= 160)
            {
                projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
                NPC p = Main.npc[(int)projectile.ai[1]];
                if (p != null)
                {
                    projectile.velocity = Vector2.Normalize(p.Center - projectile.Center) * (18f);
                }
                else
                {
                    for (int k = 0; k <= 200; k++)
                    {
                        if (Vector2.Distance(Main.npc[k].Center, projectile.Center) <= 200)
                        {
                            projectile.velocity = Vector2.Normalize(Main.npc[k].Center - projectile.Center);
                        }
                    }
                }
            }
            projectile.velocity *= 0.99f;
        }
    }
}
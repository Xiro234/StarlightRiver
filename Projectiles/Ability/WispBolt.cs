using System;
using StarlightRiver.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.Ability
{
    public class WispBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.width = 1;
            projectile.height = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fae Bolt");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        }
        bool picked = false;
        float anglediff;
        NPC target = Main.npc[0];
        public override void AI()
        {
            if (!picked)
            {
                for (int k = 0; k < Main.npc.Length; k++)
                {
                    if (Vector2.Distance(Main.npc[k].Center, projectile.Center) < Vector2.Distance(target.Center, projectile.Center) && Main.npc[k].active && !Main.npc[k].friendly)
                    {
                        target = Main.npc[k];
                    }
                }
                picked = true;
                anglediff = (projectile.velocity.ToRotation() - (target.Center - projectile.Center).ToRotation() + 9.42f) % 6.28f - 3.14f;
            }
            Dust.NewDust(projectile.position, 1, 1, ModContent.DustType<Purify>(), 0, 0, 0,Color.MediumOrchid, 1f);
            if (Vector2.Distance(target.Center, projectile.Center) <= 800 && anglediff <= 0.55f && anglediff >= -0.55f)
            {
                projectile.velocity += Vector2.Normalize(target.Center - projectile.Center) * 0.50f;
            }
            projectile.velocity = Vector2.Normalize(projectile.velocity) * 5;
        }
    }
}
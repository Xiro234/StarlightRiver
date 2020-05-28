using Microsoft.Xna.Framework;
using StarlightRiver.Items.Misc;
using System;
using Terraria;
using Terraria.ModLoader;
namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    public class ThrowawayJokeProjectile : ModProjectile
    {
        private bool crit = false;
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.timeLeft = 120;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Throwaway Joke");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.velocity.X = -projectile.velocity.X;
            projectile.velocity.Y = -projectile.velocity.Y;
            this.crit = crit;
        }
        public override void AI()
        {
            projectile.rotation += (float)Math.PI / 10f;
            if (projectile.penetrate == 1)
            {
                projectile.tileCollide = false;
                projectile.velocity += Vector2.Normalize(Main.player[projectile.owner].Center - projectile.Center) * 6f;
                projectile.velocity = Vector2.Normalize(projectile.velocity) * 20f;
                Player player = Main.player[projectile.owner];
                projectile.timeLeft = 2;
                if (projectile.Hitbox.Intersects(player.Hitbox))
                {
                    projectile.Kill();
                    if (player.HeldItem.type.Equals(ModContent.ItemType<ThrowawayJoke>()))
                    {
                        ThrowawayJoke joke = (ThrowawayJoke)player.HeldItem.modItem;
                        joke.Reload(crit);
                    }
                }
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.penetrate == 2)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
    }
}
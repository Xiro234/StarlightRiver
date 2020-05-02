using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    class CursedRondureProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.width = 46;
            projectile.height = 46;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.melee = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BalloChain2");
        }
        public override void AI()
        {
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];
            for (float k = 0; k <= 1; k += 1 / (Vector2.Distance(player.Center, projectile.Center) / 16))
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Projectiles/WeaponProjectiles/CursedRondureChain"), Vector2.Lerp(projectile.Center, player.Center, k) - Main.screenPosition,
                    new Rectangle(0, 0, 8, 16), lightColor, (projectile.Center - player.Center).ToRotation() + 1.58f, new Vector2(4, 8), 1, 0, 0);
            }
            return true;
        }
    }
}
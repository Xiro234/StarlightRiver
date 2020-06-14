using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using StarlightRiver.Core;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    class StarwoodSlingshotProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shooting Star");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        //These stats get scaled when empowered
        private int ScaleMult = 1;
        private Color glowColor = new Color(255, 220, 200, 150);
        private Vector3 lightColor = new Vector3(0.2f, 0.1f, 0.05f);
        private int dustType = ModContent.DustType<Dusts.Stamina>();


        public override void SetDefaults()
        {
            projectile.timeLeft = 600;

            projectile.width = 22;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.aiStyle = -1;
            projectile.rotation = Main.rand.NextFloat(4f);
        }


        public override void AI()
        {
            Player projOwner = Main.player[projectile.owner];
            StarlightPlayer mp = Main.player[projectile.owner].GetModPlayer<StarlightPlayer>();

            projectile.rotation += 0.2f;

            if (projectile.timeLeft == 600 && mp.Empowered)
            {
                projectile.frame = 1;
                glowColor = new Color(220, 200, 255, 150);
                lightColor = new Vector3(0.05f, 0.1f, 0.2f);
                ScaleMult = 2;
                dustType = ModContent.DustType<Dusts.BlueStamina>();
                projectile.velocity *= 1.2f;
            }
            Lighting.AddLight(projectile.Center, lightColor);
            if(projectile.velocity.Y < 50)
            {
                projectile.velocity.Y += 0.25f;
            }
            projectile.velocity.X *= 0.995f;
        }

        public override void ModifyHitNPC(NPC target,ref int damage,ref float knockback,ref bool crit,ref int hitDirection)
        {
            damage *= ScaleMult;
        }

        public override void Kill(int timeLeft)
        {
            DustHelper.DrawStar(projectile.Center, dustType, pointAmount: 5, mainSize: 1f * ScaleMult, dustDensity: 0.5f, pointDepthMult: 0.3f);
            Main.PlaySound(SoundID.Item4, projectile.Center);
            for (int k = 0; k < 35 ; k++)
            {
                Dust.NewDustPerfect(projectile.Center, dustType, Vector2.One.RotatedByRandom(6.28f) * (Main.rand.NextFloat(0.25f, 1.2f) * ScaleMult), 0, default, 1.5f);
            }

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, Main.projectileTexture[projectile.type].Height * 0.25f);

            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Color color = new Color(255, 255, 255, 64) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                float scale = projectile.scale * (float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length;

                spriteBatch.Draw(Main.projectileTexture[projectile.type],
                projectile.oldPos[k] + drawOrigin - Main.screenPosition,
                new Rectangle(0, (Main.projectileTexture[projectile.type].Height / 2) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 2),
                color,
                projectile.oldRot[k],
                drawOrigin,
                scale, default, default);
            }

            spriteBatch.Draw(Main.projectileTexture[projectile.type], 
            projectile.Center - Main.screenPosition, 
            new Rectangle(0, (Main.projectileTexture[projectile.type].Height / 2) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 2),
            new Color(255, 255, 255, 32), 
            projectile.rotation, 
            new Vector2(Main.projectileTexture[projectile.type].Width / 2, Main.projectileTexture[projectile.type].Height / 4), 
            1f, default, default);

            return false;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using StarlightRiver.Core;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    class StarwoodSlingshotProjectile : ModProjectile, IDrawAdditive
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shooting Star");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;   
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }

        //These stats get scaled when empowered
        private float ScaleMult = 1;
        private Color glowColor = new Color(255, 220, 200, 150);
        private Vector3 lightColor = new Vector3(0.2f, 0.1f, 0.05f);
        private int dustType = ModContent.DustType<Dusts.Stamina>();
        private bool empowered;


        public override void SetDefaults()
        {
            projectile.timeLeft = 600;

            projectile.width = 22;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.aiStyle = -1;
            projectile.rotation = Main.rand.NextFloat(4f);
        }


        public override void AI()
        {
            projectile.rotation += 0.2f;

            if (projectile.timeLeft == 600)
            {
                StarlightPlayer mp = Main.player[projectile.owner].GetModPlayer<StarlightPlayer>();
                if (mp.Empowered)
                {
                    projectile.frame = 1;
                    glowColor = new Color(220, 200, 255, 150);
                    lightColor = new Vector3(0.05f, 0.1f, 0.2f);
                    ScaleMult = 1.5f;
                    dustType = ModContent.DustType<Dusts.BlueStamina>();
                    projectile.velocity *= 1.35f;
                    empowered = true;
                }
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
            if (empowered)
            {
                damage += 5;
                if (projectile.penetrate <= 1)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        Main.NewText(k);
                        Projectile.NewProjectile(projectile.position, projectile.velocity.RotatedBy(Main.rand.NextFloat(-0.25f, 0.25f)) * Main.rand.NextFloat(0.5f, 0.8f), ModContent.ProjectileType<WeaponProjectiles.StarWShard>(), damage / 2, knockback, projectile.owner, Main.rand.Next(2));

                    }
                }
            }
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
            Texture2D tex = ModContent.GetTexture(Texture);
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, empowered ? 24 : 0, 22, 24), Color.White, projectile.rotation, new Vector2(11, 12), projectile.scale, default, default);
            return false;
        }

        public void DrawAdditive(SpriteBatch spriteBatch)
        {
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Color color = (empowered ? new Color(200, 220, 255) * 0.35f : new Color(255, 255, 200) * 0.3f) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                if (k <= 4) color *= 1.2f;
                float scale = projectile.scale * (float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length * 0.8f;
                Texture2D tex = ModContent.GetTexture("StarlightRiver/Keys/Glow");

                spriteBatch.Draw(tex, projectile.oldPos[k] + projectile.Size / 2 - Main.screenPosition, null, color, 0, tex.Size() / 2, scale, default, default);
            }
        }
    }

    class StarWShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Fragment");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            projectile.timeLeft = 9;
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.aiStyle = -1;
            projectile.rotation = Main.rand.NextFloat(4f);
        }
        public override void AI()
        {
            projectile.rotation += 0.3f;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDustPerfect(projectile.position, ModContent.DustType<Dusts.StarFragment>(), projectile.velocity.RotatedBy(Main.rand.NextFloat(-0.2f, 0.2f)) * Main.rand.NextFloat(0.3f, 0.5f), 0, Color.White, 1.5f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = ModContent.GetTexture(Texture);
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, projectile.ai[0] > 0 ? 10 : 0, 12, 10), Color.White, projectile.rotation, new Vector2(6, 5), projectile.scale, default, default);
            //Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            //for (int k = 0; k < projectile.oldPos.Length; k++)
            //{
            //    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
            //    Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
            //    spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            //}
            return false;
        }

        //public void DrawAdditive(SpriteBatch spriteBatch)
        //{
        //    for (int k = 0; k < projectile.oldPos.Length; k++)
        //    {
        //        Color color = (new Color(200, 220, 255, 220) * 0.35f * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length));
        //        if (k <= 4) color *= 1.2f;
        //        float scale = projectile.scale * (float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length * 0.4f;
        //        Texture2D tex = ModContent.GetTexture("StarlightRiver/Keys/Glow");

        //        spriteBatch.Draw(tex, projectile.oldPos[k] + projectile.Size / 2 - Main.screenPosition, null, color, 0, tex.Size() / 2, scale, default, default);
        //    }
        //}
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(160, 160, 160, 100);
        }
    }
}

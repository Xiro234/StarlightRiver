using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    class StarwoodBoomerangProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starwood Boomerang");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }

        private const int maxChargeTime = 50;//how long it takes to charge up
        private int maxDistTime;//only set on spawn, used to simplify things, all uses could be replaced with: projectile.timeLeft - 30

        private float chargeMult;//a multiplier used during charge up, used both in ai and for drawing (goes from 0 to 1)

        //These stats get scaled when empowered
        private int ScaleMult = 2;
        private Vector3 lightColor = new Vector3(0.4f, 0.2f, 0.1f);
        private int dustType = ModContent.DustType<Dusts.Stamina>();


        public override void SetDefaults()
        {
            projectile.timeLeft = 1200;
            maxDistTime = projectile.timeLeft - 30;//the only time this is set

            projectile.width = 18;
            projectile.height = 18;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.aiStyle = -1;
        }


        public override void AI()
        {
            Player projOwner = Main.player[projectile.owner];
            StarlightPlayer mp = Main.player[projectile.owner].GetModPlayer<StarlightPlayer>();

            projectile.rotation += 0.3f;

            if (projectile.timeLeft == 1200 && mp.Empowered)
            {
                projectile.frame = 1;
                lightColor = new Vector3(0.1f, 0.2f, 0.4f);
                ScaleMult = 3;
                dustType = ModContent.DustType<Dusts.BlueStamina>();
            }

            Lighting.AddLight(projectile.Center, lightColor * 0.5f);

            switch (projectile.ai[0])
            {
                case 0://flying outward
                    if (mp.Empowered)
                    {
                        projectile.velocity += Vector2.Normalize(Main.MouseWorld - projectile.Center);
                        if (projectile.velocity.Length() > 10)//swap this for shootspeed or something
                        { //if more than max speed
                            projectile.velocity = Vector2.Normalize(projectile.velocity) * 10;//cap to max speed
                        }
                    }

                    if (projectile.timeLeft < maxDistTime) {//if it doesn't collide, start it over time
                        NextPhase(0);
                    }
                    break;
                case 1://has hit something
                    if (projOwner.controlUseItem || projectile.ai[1] >= maxChargeTime - 5) {
                        if(projectile.ai[1] == 0)
                        {
                            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/ImpactHeal"), projectile.Center);
                        }

                        chargeMult = projectile.ai[1] / (maxChargeTime + 3);
                        projectile.ai[1]++;
                        projectile.velocity *= 0.75f;
                        Lighting.AddLight(projectile.Center, lightColor * chargeMult);

                        if (projectile.ai[1] >= maxChargeTime + 3)//reset stats and start return phase
                        {
                            projectile.position = projectile.Center;
                            projectile.width = 18;
                            projectile.height = 18;
                            projectile.Center = projectile.position;
                            NextPhase(1);//ai[]s reset here
                        }
                        else if (projectile.ai[1] == maxChargeTime)//change hitbox size, stays for 3 frames
                        {
                            projectile.position = projectile.Center;
                            projectile.width = 67 * ScaleMult;
                            projectile.height = 67 * ScaleMult;
                            projectile.Center = projectile.position;
                        }
                        else if(projectile.ai[1] == maxChargeTime - 5)//sfx
                        {
                            DustHelper.DrawStar(projectile.Center, dustType, pointAmount: 5, mainSize: 2.25f * ScaleMult, dustDensity: 2, pointDepthMult: 0.3f);
                            Lighting.AddLight(projectile.Center, lightColor * 2);
                            //Main.PlaySound(SoundID.Item74, projectile.Center);
                            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/MagicAttack"), projectile.Center);
                            for (int k = 0; k < 50; k++)
                            {
                                Dust.NewDustPerfect(projectile.Center, dustType, Vector2.One.RotatedByRandom(6.28f) * (Main.rand.NextFloat(0.25f, 1.5f) * ScaleMult), 0, default, 1.5f);
                            }
                        }
                    }
                    else { //if mouse isnt held or let go, start next phase
                        NextPhase(1); // ai[]s and damage reset here
                    }
                    break;
                case 2://heading back
                    if(Vector2.Distance(projOwner.Center, projectile.Center) < 24){ //if close enough delete projectile
                        projectile.Kill();
                    }
                    else if(Vector2.Distance(projOwner.Center, projectile.Center) < 200){ //faster turning if close enough
                        projectile.velocity += Vector2.Normalize(projOwner.Center - projectile.Center) * 4;
                    }
                    else{ //turning
                        projectile.velocity += Vector2.Normalize(projOwner.Center - projectile.Center);
                    }

                    if (projectile.velocity.Length() > 10)//swap this for shootspeed or something
                    { //if more than max speed
                        projectile.velocity = Vector2.Normalize(projectile.velocity) * 10;//cap to max speed
                    }
                    break;
            }

            if (projectile.ai[0] != 1)
            {
                if (projectile.timeLeft % 8 == 0)
                {
                    Main.PlaySound(SoundID.Item7, projectile.Center);
                    Dust.NewDustPerfect(projectile.Center, dustType, (projectile.velocity * 0.5f).RotatedByRandom(0.5f), Scale: Main.rand.NextFloat(0.8f, 1.5f));
                }
            }
        }

        public override void ModifyHitNPC(NPC target,ref int damage,ref float knockback,ref bool crit,ref int hitDirection)
        {
            StarlightPlayer mp = Main.player[projectile.owner].GetModPlayer<StarlightPlayer>();
            if (projectile.ai[0] == 1)
            {
                if(projectile.ai[1] >= maxChargeTime - 3 && projectile.ai[1] <= maxChargeTime + 3)
                {
                    if (mp.Empowered)
                    {
                        damage *= ScaleMult;
                        knockback *= ScaleMult;
                    }
                    else
                    {
                        damage *= ScaleMult;
                        knockback *= ScaleMult;
                    }
                }
                else
                {
                    damage = ScaleMult;
                    knockback *= 0.1f;
                }
            }
            else if (mp.Empowered)
            {
                damage += 3;
            }
        }


        #region on any collide start next phase
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            NextPhase(0, true);
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            NextPhase(0, true);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            NextPhase(0, true);
        }
        #endregion

        //private Texture2D LightTrailTexture => ModContent.GetTexture("StarlightRiver/Projectiles/WeaponProjectiles/glow");
        private Texture2D GlowingTrail => ModContent.GetTexture("StarlightRiver/Projectiles/WeaponProjectiles/StarwoodBoomerangGlowTrail");

        //private static Texture2D MainTexture => ModContent.GetTexture("StarlightRiver/Items/StarwoodBoomerang");
        private Texture2D GlowingTexture => ModContent.GetTexture("StarlightRiver/Projectiles/WeaponProjectiles/StarwoodBoomerangGlow");
        private Texture2D AuraTexture => ModContent.GetTexture("StarlightRiver/Tiles/Interactive/WispSwitchGlow2");

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);

            if (projectile.ai[0] != 1)
            {
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Color color = projectile.GetAlpha(Color.White) * (((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length) * 0.5f);
                    float scale = projectile.scale * (float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length;

                    spriteBatch.Draw(GlowingTrail,
                    projectile.oldPos[k] + drawOrigin - Main.screenPosition,
                    new Rectangle(0, (Main.projectileTexture[projectile.type].Height / 2) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 2),
                    color,
                    projectile.rotation,
                    new Vector2(Main.projectileTexture[projectile.type].Width / 2, Main.projectileTexture[projectile.type].Height / 4),
                    scale, default, default);

                    //spriteBatch.Draw(LightTrailTexture,
                    //projectile.oldPos[k] + drawOrigin - Main.screenPosition,
                    //LightTrailTexture.Frame(),
                    //Color.White * 0.3f,
                    //0f,
                    //new Vector2(LightTrailTexture.Width / 2, LightTrailTexture.Height / 2),
                    //scale, default, default);
                }
            }

            spriteBatch.Draw(Main.projectileTexture[projectile.type], 
                projectile.Center - Main.screenPosition, 
                new Rectangle(0, (Main.projectileTexture[projectile.type].Height / 2) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 2), 
                lightColor, 
                projectile.rotation, 
                new Vector2(Main.projectileTexture[projectile.type].Width / 2, Main.projectileTexture[projectile.type].Height / 4), 
                1f, default, default);

            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Color color = Color.White * chargeMult;

            spriteBatch.Draw(GlowingTexture, 
                projectile.Center - Main.screenPosition, 
                new Rectangle(0, (GlowingTexture.Height / 2) * projectile.frame, GlowingTexture.Width, GlowingTexture.Height / 2), 
                color, 
                projectile.rotation,
                new Vector2(GlowingTexture.Width / 2, GlowingTexture.Height / 4),
                1f, default, default);
            
            spriteBatch.Draw(AuraTexture, projectile.Center - Main.screenPosition, AuraTexture.Frame(), (Color.White * (projectile.ai[1] / maxChargeTime)), 0, AuraTexture.Size() / 2, (-chargeMult + 1) / 1.2f, 0, 0);
        }

        #region phase change void
        private void NextPhase(int phase, bool bounce = false)
        {
            if (phase == 0 && projectile.ai[0] == phase)
            {
                if (bounce)
                {
                    projectile.velocity = -projectile.velocity;
                }

                projectile.tileCollide = false;
                projectile.ignoreWater = true;
                projectile.ai[0] = 1;
            }
            else if (phase == 1 && projectile.ai[0] == phase)
            {
                //projectile.damage = oldDamage / 2;//half damage on the way back
                projectile.velocity.Y += 1f;
                projectile.ai[0] = 2;
                projectile.ai[1] = 0;
            }
        }
        #endregion
    }
}

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
            Main.projFrames[projectile.type] = 2;
        }

        //todo, explosion sfx, trail, more

        private int maxDistTime;//only set on spawn, used to simplify things, all uses could be replaced with: projectile.timeLeft - 30

        private float chargeMult;
        private int ScaleMult = 2;
        private Color glowColor = new Color(255, 220, 180, 255);
        int dustType = ModContent.DustType<Dusts.Stamina>();

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

        private readonly int maxcharge = 50;

        public override void AI()
        {
            Player projOwner = Main.player[projectile.owner];
            Vector3 lightColor = new Vector3(0.4f, 0.2f, 0.1f);

            chargeMult = projectile.ai[1] / (maxcharge + 3);
            projectile.rotation += 0.3f;

            StarlightPlayer mp = Main.player[projectile.owner].GetModPlayer<StarlightPlayer>();
            switch (mp.Empowered)
            {
                case true: //if empowered
                    projectile.frame = 1;
                    glowColor = new Color(200, 180, 255, 255);
                    lightColor = new Vector3(0.1f, 0.2f, 0.4f);//these are only set when empowered since they get set every time AI() is called
                    ScaleMult = 3;
                    dustType = ModContent.DustType<Dusts.BlueStamina>();
                    break;

                case false: //if not empowered (this can be removed if it does not convert back)
                    projectile.frame = 0;
                    glowColor = new Color(255, 220, 180, 255);
                    ScaleMult = 2;
                    dustType = ModContent.DustType<Dusts.Stamina>();
                    break;
            }

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
                    if (projOwner.controlUseItem || projectile.ai[1] >= maxcharge - 5) {
                        projectile.ai[1]++;
                        projectile.velocity *= 0.75f;
                        Lighting.AddLight(projectile.Center, lightColor * chargeMult);
                        if (projectile.timeLeft % 8 == 0)
                        {
                            Main.PlaySound(SoundID.Item24, projectile.Center);
                        }

                        if (projectile.ai[1] >= maxcharge + 3)//reset stats and start return phase
                        {
                            projectile.position = projectile.Center;
                            projectile.width = 18;
                            projectile.height = 18;
                            projectile.Center = projectile.position;
                            NextPhase(1);//ai[]s reset here
                        }
                        else if (projectile.ai[1] == maxcharge)//change hitbox size, stays for 3 frames
                        {
                            projectile.position = projectile.Center;
                            projectile.width = 67 * ScaleMult;
                            projectile.height = 67 * ScaleMult;
                            projectile.Center = projectile.position;
                        }
                        else if(projectile.ai[1] == maxcharge - 5)//sfx
                        {
                            DustHelper.DrawStar(projectile.Center, dustType, pointAmount: 5, mainSize: 2.25f * ScaleMult, dustDensity: 2, pointDepthMult: 0.3f);
                            Lighting.AddLight(projectile.Center, lightColor * 2);
                            Main.PlaySound(SoundID.Item74, projectile.Center);
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
                }

                if (Main.rand.Next(7) <= ScaleMult)
                {
                    Dust.NewDustPerfect(projectile.Center, dustType, (projectile.velocity * 0.5f).RotatedByRandom(0.5f), Scale: Main.rand.NextFloat(0.8f, 1.5f));
                }
            }
        }

        public override void ModifyHitNPC(NPC target,ref int damage,ref float knockback,ref bool crit,ref int hitDirection)
        {
            StarlightPlayer mp = Main.player[projectile.owner].GetModPlayer<StarlightPlayer>();
            if (projectile.ai[0] == 1)
            {
                if(projectile.ai[1] >= maxcharge - 3 && projectile.ai[1] <= maxcharge + 3)
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

        //private static Texture2D MainTexture => ModContent.GetTexture("StarlightRiver/Items/StarwoodBoomerang");
        private Texture2D GlowingTexture => ModContent.GetTexture("StarlightRiver/Projectiles/WeaponProjectiles/StarwoodBoomerangGlow");
        private Texture2D AuraTexture => ModContent.GetTexture("StarlightRiver/Tiles/Interactive/WispSwitchGlow2");

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
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
            Color color = glowColor * chargeMult;

            spriteBatch.Draw(GlowingTexture, 
                projectile.Center - Main.screenPosition, 
                new Rectangle(0, (GlowingTexture.Height / 2) * projectile.frame, GlowingTexture.Width, GlowingTexture.Height / 2), 
                color, 
                projectile.rotation,
                new Vector2(GlowingTexture.Width / 2, GlowingTexture.Height / 4),
                1f, default, default);
            
            spriteBatch.Draw(AuraTexture, projectile.Center - Main.screenPosition, AuraTexture.Frame(), glowColor * (projectile.ai[1] / maxcharge), 0, AuraTexture.Size() / 2, (-chargeMult + 1) / 1.2f, 0, 0);
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

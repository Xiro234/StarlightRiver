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
        }

        //todo, explosion sfx, trail, more

        private int maxDistTime;
        private int oldDamage;
        private float chargeMult;

        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.friendly = true;

            projectile.timeLeft = 1200;
            maxDistTime = projectile.timeLeft - 30;

            projectile.damage = 20;
            oldDamage = projectile.damage;

            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.aiStyle = -1;
            //projectile.knockBack = 3f; no work?
        }

        private int maxcharge = 50; //make const

        public override void AI()
        {

            projectile.rotation += 0.3f;

            Player projOwner = Main.player[projectile.owner];
            chargeMult = projectile.ai[1] / (maxcharge + 3);

            switch (projectile.ai[0])
            {
                case 0://flying outward
                    if (projectile.timeLeft < maxDistTime) {//if it doesn't collide, start it over time
                        NextPhase(0);
                    }
                    break;
                case 1://has hit something
                    if (projOwner.controlUseItem) {
                        projectile.ai[1]++;
                        projectile.velocity *= 0.75f;
                        projectile.damage = 3;
                        Lighting.AddLight(projectile.Center, new Vector3(0.4f, 0.2f, 0.1f) * chargeMult);
                        if (projectile.ai[1] >= maxcharge + 3)//reset stats and start return phase
                        {
                            projectile.position = projectile.Center;
                            projectile.width = 18;
                            projectile.height = 18;
                            projectile.Center = projectile.position;
                            NextPhase(1);// ai[]s and damage reset here
                        }
                        else if (projectile.ai[1] == maxcharge)//change hitbox size, stays for 3 frames
                        {
                            projectile.position = projectile.Center;
                            projectile.width = 150;
                            projectile.height = 150;
                            projectile.Center = projectile.position;
                            projectile.damage = oldDamage * 2;
                        }
                        else if(projectile.ai[1] == maxcharge - 5)
                        {
                            DustHelper.DrawStar(projectile.Center, ModContent.DustType<Dusts.Stamina>(), pointAmount: 5, mainSize: 4.5f, dustDensity: 2, pointDepthMult: 0.3f);
                            Lighting.AddLight(projectile.Center, new Vector3(0.8f, 0.4f, 0.2f));
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

                    if (projectile.velocity.Length() > 10)
                    { //if more than max speed
                        projectile.velocity = Vector2.Normalize(projectile.velocity) * 10;//cap to max speed
                    }
                    break;
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

        private Texture2D MainTexture => ModContent.GetTexture("StarlightRiver/Items/StarwoodBoomerang");
        private Texture2D GlowingTexture => ModContent.GetTexture("StarlightRiver/Items/StarwoodBoomerangGlow");
        private Texture2D AuraTexture => ModContent.GetTexture("StarlightRiver/Tiles/Interactive/WispSwitchGlow2");

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Color color = new Color(255, 180, 220, 255) * chargeMult;
            spriteBatch.Draw(MainTexture, projectile.Center - Main.screenPosition, MainTexture.Frame(), lightColor, projectile.rotation, MainTexture.Size() / 2, 1f, default, default);

            spriteBatch.Draw(GlowingTexture, projectile.Center - Main.screenPosition, GlowingTexture.Frame(), color, projectile.rotation, GlowingTexture.Size() / 2, 1f, default, default);

            spriteBatch.Draw(AuraTexture, projectile.Center - Main.screenPosition, AuraTexture.Frame(), Color.LightYellow * (projectile.ai[1] / maxcharge), 0, AuraTexture.Size() / 2, (-chargeMult + 1) / 1.2f, 0, 0);
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
                projectile.damage = oldDamage / 2;
                projectile.velocity.Y += 1f;
                projectile.ai[0] = 2;
                projectile.ai[1] = 0;
            }
        }
        #endregion
    }
}

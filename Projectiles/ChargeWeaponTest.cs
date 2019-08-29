using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Projectiles
{
    public class ChargeWeaponTest : ModProjectile
    {
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        //charging and staff related
        bool projectileFired = false; //this is so that the projectile is fired only once, do not change

        public virtual bool shouldUpdateRotation() //should the staff update its rotation after fireing
        {
            return true;
        }
        public virtual float maxCharge() //the max amount of ai[1]
        {
            return 8f;
        }
        public virtual float chargeSpeed() //how fast ai[1] increases
        {
            return 0.2f;
        }
        public virtual float unchargeSpeed() //how fast ai[1] decreases after releasing the trigger
        {
            return 1f;
        }
        public virtual float baseDamage() //raw damage
        {
            return 0.25f;
        }
        public virtual void SpawnDustCharging(float num1, float num2)
        {

        }

        public virtual void SpawnDustCharged(float num1)
        {

        }
        public virtual void Shoot()
        {

        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 playerPosition = player.RotatedRelativePoint(player.Center, true);
            float rotationValue = 1.57079632679f;

            projectile.position = player.RotatedRelativePoint(player.Center, true) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + rotationValue;
            projectile.spriteDirection = projectile.direction;
            bool Channel = player.channel && !player.noItems && !player.CCed;
            if (projectile.ai[1] > 0 && !Channel) //dust which appears right before fireing projectile
            {
                for (int a = 0; a <= 20; a++)
                {
                    SpawnDustCharged(a);
                }
            }
            if (Main.myPlayer == projectile.owner)
            {
                if (Channel) //charging
                {
                    for (float b = 0; b <= 1; b += 0.25f) //foreach loop meant to be used for showing how charged the weapon is, does stuff for 0.25, 0.5, 0.75, 1.0
                    {
                        if (projectile.ai[1] >= maxCharge() * b)
                        {
                            for (int c = 0; c <= 11; c++)
                            {
                                SpawnDustCharging(b, c);
                            }
                        }
                    }
                    if (projectile.ai[1] <= maxCharge())
                    {
                        projectile.ai[1] += chargeSpeed();
                        projectile.ai[0] = projectile.ai[1];
                    }
                }
                if (!Channel) //release trigger
                { 
                    projectile.ai[1] -= unchargeSpeed();
                    if (projectile.ai[1] <= -maxCharge())
                    {
                        projectile.Kill();
                    }
                    float scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    Vector2 value2 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - playerPosition;
                    float damage = baseDamage() * projectile.ai[0];

                    if (projectile.ai[1] <= 0f)
                    {
                        if (!projectileFired)
                        {
                            projectileFired = true;
                            Shoot();
                        }
                    }
                }
            }
            if (shouldUpdateRotation()) //updates the rotation of the projectile
            {
                float velocity = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                Vector2 mousePosition = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - playerPosition;
                if (player.gravDir == -1f)
                {
                    mousePosition.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - playerPosition.Y;
                }
                Vector2 normalizedMousePosition = Vector2.Normalize(mousePosition);
                if (float.IsNaN(normalizedMousePosition.X) || float.IsNaN(normalizedMousePosition.Y))
                {
                    normalizedMousePosition = -Vector2.UnitY;
                }
                normalizedMousePosition *= velocity;
                if (normalizedMousePosition.X != projectile.velocity.X || normalizedMousePosition.Y != projectile.velocity.Y)
                {
                    projectile.netUpdate = true;
                }
                projectile.velocity = normalizedMousePosition;
            }
            if (!projectileFired)
            {
                projectile.timeLeft = 16;
            }
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
        }
    }
}
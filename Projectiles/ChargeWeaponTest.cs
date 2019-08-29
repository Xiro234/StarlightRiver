using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Projectiles
{
    public class ChargeWeaponTest : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ranged = true;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ChargeWeaponTest moment");
        }
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
        bool shouldUpdateRotation = true; //should the projectile rotate as you move your mouse
        float maxCharge = 8f; //the max amount of ai[1]
        float chargeSpeed = 0.2f; //how fast ai[1] increases
        //dust related
        int dustType = 4;
        float dustScale = 1.1f;
        //Note for future use
        //p.ai[0] is used to keep a track of the maximum charge ever reached
        //p.ai[1] is used to keep a track of charge
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 playerPosition = player.RotatedRelativePoint(player.MountedCenter, true);
            float rotationValue = 1.57079637f;
            bool Channel = player.channel && !player.noItems && !player.CCed;
            Vector2 offset = Vector2.UnitX * projectile.height;
            offset = offset.RotatedBy((double)(projectile.rotation - 1.57079637f), default(Vector2));

            Vector2 position = projectile.Center + offset;
            if (projectile.ai[1] > 0 && !Channel)
            {
                for (int k = 0; k <= 10; k++)
                {
                    Vector2 dustPos = position + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((11f * projectile.ai[1]) - (float)(k * 2));
                    int dust = Dust.NewDust(dustPos - Vector2.One * 8f, 16, 16, dustType, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 0, default(Color), 0.6f);
                    Main.dust[dust].velocity = Vector2.Normalize(position - dustPos) * 1.5f * (10f - (float)k * 2f) / 10f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = dustScale;
                    Main.dust[dust].customData = player;
                }
            }
            if (Main.myPlayer == projectile.owner)
            {
                if (Channel) //charging
                {
                    if (shouldUpdateRotation) //updates the rotation of the projectile
                    {
                        float scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                        Vector2 value2 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - playerPosition;
                        if (player.gravDir == -1f)
                        {
                            value2.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - playerPosition.Y;
                        }
                        Vector2 vector4 = Vector2.Normalize(value2);
                        if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
                        {
                            vector4 = -Vector2.UnitY;
                        }
                        vector4 *= scaleFactor;
                        if (vector4.X != projectile.velocity.X || vector4.Y != projectile.velocity.Y)
                        {
                            projectile.netUpdate = true;
                        }
                        projectile.velocity = vector4;
                    }
                    if (projectile.ai[1] <= maxCharge)
                    {
                        projectile.ai[1] += chargeSpeed;
                        projectile.ai[0] = projectile.ai[1];
                    }
                }
                if (!Channel) //release trigger
                { 
                    projectile.ai[1] -= chargeSpeed * 2;
                    if (projectile.ai[1] <= -10f)
                    {
                        projectile.Kill();
                    }
                    float scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    Vector2 value2 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - playerPosition;
                    float spread = 15f;
                    int projectileType = 251;
                    int positionOffset = 6;
                    float damage = 0.25f * projectile.ai[0];

                    if (projectile.ai[1] <= 0f)
                    {
                        if (!projectileFired)
                        {
                            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 92); //charged zap sound
                            projectileFired = true;
                            for (int j = 0; j < 1; j++)
                            {
                                playerPosition = projectile.Center + new Vector2((float)Main.rand.Next(-positionOffset, positionOffset + 1), (float)Main.rand.Next(-positionOffset, positionOffset + 1));
                                Vector2 vector5 = Vector2.Normalize(projectile.velocity) * spread;
                                vector5 = vector5.RotatedBy(Main.rand.NextDouble() * 0.1f - 0.1f / 2, default(Vector2));
                                if (float.IsNaN(vector5.X) || float.IsNaN(vector5.Y))
                                {
                                    vector5 = -Vector2.UnitY;
                                }
                                Projectile.NewProjectile(position.X, position.Y, vector5.X, vector5.Y, projectileType, projectile.damage * (int)damage, projectile.knockBack, projectile.owner, 0f, damage);
                                Main.PlaySound(SoundID.Item11, projectile.position);
                            }
                        }
                    }
                }
            }
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + rotationValue;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
        }
    }
}
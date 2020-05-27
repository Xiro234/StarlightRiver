using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Dusts;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.WeaponProjectiles.Summons
{
    public class EbonyPrismSummon : ModProjectile
    {
        public static int prismsPerSlot = 2;

        #region tml hooks
        public override string Texture => "StarlightRiver/Invisible";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebony Prism");
        }
        public override bool OnTileCollide(Vector2 oldVelocity) => false;
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.extraUpdates = 10;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.netImportant = true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D glowmask = mod.GetTexture("Projectiles/WeaponProjectiles/Summons/EbonyPrism");
            int frameHeight = glowmask.Height;
            for (int i = 0; i < (projectile.minionSlots * prismsPerSlot); i++)
            {
                Main.spriteBatch.Draw(glowmask, getPrismPosition(i + 1) - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, glowmask.Width, frameHeight)), projectile.GetAlpha(Color.White), projectile.rotation, new Vector2(glowmask.Width / 2f, frameHeight / 2f), projectile.scale, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            base.PostDraw(spriteBatch, lightColor);
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        #endregion

        #region helper methods
        public Vector2 getPrismPosition(int currentPrism)
        {
            float speed = 80;
<<<<<<< HEAD
            float dist = 60;
            float rot = currentPrism / (projectile.minionSlots * prismsPerSlot) * 6.28f + (float)Main.time % speed / speed * 6.28f;

            float posX = projectile.Center.X + (float)(Math.Cos(rot) * dist) * 0.25f;
            float posY = projectile.Center.Y + (float)(Math.Sin(rot) * dist) * 0.6f;
=======
            float dist = 50;
            if (projectile.minionSlots > 2)
            {
                dist += (projectile.minionSlots - 2) * 18;
            }
            float rot = currentPrism / (projectile.minionSlots * prismsPerSlot) * 6.28f + (float)Main.time % speed / speed * 6.28f;

            float posX = projectile.Center.X + (float)(Math.Cos(rot) * dist) * 0.25f;
            float posY = projectile.Center.Y - 10 + (float)(Math.Sin(rot) * dist) * 0.6f;
>>>>>>> 17bf96c09ddc66bfdb2c62d069e49e37f7514e9b
            return new Vector2(posX, posY);
        }
        public void ChangeState(AIStates state)
        {
            projectile.ai[1] = 0;
            projectile.ai[0] = (int)state;
        }
        #endregion

        #region AI

        public enum AIStates
        {
            Charging = 0,
            Shooting = 1
        }
        NPC target = Main.npc[0];
        int lastShot;
        public override void AI()
        {
<<<<<<< HEAD

=======
            //to-do 
            //fix this stupid ass projectile ass looking ass randomly getting NAN position, caching pos and then setting p. pos to it if it has NANs in it donest seem to work
>>>>>>> 17bf96c09ddc66bfdb2c62d069e49e37f7514e9b
            projectile.timeLeft = 100;
            /*
             * AI slots:
             * 0: State
             * 1: Timer
             */
<<<<<<< HEAD
=======
            #region targetting
>>>>>>> 17bf96c09ddc66bfdb2c62d069e49e37f7514e9b
            if (projectile.OwnerMinionAttackTargetNPC == null || !projectile.OwnerMinionAttackTargetNPC.active)//automatically choose target
            {
                for (int k = 0; k < Main.npc.Length; k++)
                {
<<<<<<< HEAD
                    if (Main.npc[k].active && !Main.npc[k].friendly)
                    {
                        if (Vector2.Distance(Main.npc[k].Center, projectile.Center) < Vector2.Distance(target.Center, projectile.Center))
                        {
                            target = Main.npc[k];
=======
                    if (Helper.IsTargetValid(Main.npc[k]))
                    {
                        if (Vector2.Distance(Main.npc[k].Center, projectile.Center) < Vector2.Distance(target.Center, projectile.Center))
                        {
                            if (Collision.CanHitLine(projectile.Center, 2, 2, Main.npc[k].Center, 2, 2))
                            {
                                target = Main.npc[k];
                            }
>>>>>>> 17bf96c09ddc66bfdb2c62d069e49e37f7514e9b
                        }
                    }
                }
            }
<<<<<<< HEAD
            else
            {
                target = projectile.OwnerMinionAttackTargetNPC; //set target to selected npc
            }
            if (target.active)
=======
            else if (Collision.CanHitLine(projectile.Center, 2, 2, projectile.OwnerMinionAttackTargetNPC.Center, 2, 2))
            {
                target = projectile.OwnerMinionAttackTargetNPC; //set target to selected npc
            }
            if (!Helper.IsTargetValid(target))
            {
                target = Main.npc[0];
            }
            #endregion

            #region attacking
            if (Helper.IsTargetValid(target))
>>>>>>> 17bf96c09ddc66bfdb2c62d069e49e37f7514e9b
            {
                if (projectile.ai[0] == (int)AIStates.Charging)
                {
                    projectile.ai[1]++;
                    if (projectile.ai[1] >= 800)
                    {
                        ChangeState(AIStates.Shooting);
                        lastShot = 1;
                    }
                }
                if (projectile.ai[0] == (int)AIStates.Shooting)
                {
                    projectile.ai[1]++;
                    if (projectile.ai[1] >= (400 / (projectile.minionSlots * prismsPerSlot)))
                    {
                        if (Collision.CanHitLine(projectile.Center, 2, 2, target.Center, 2, 2))
                        {
<<<<<<< HEAD
                            Projectile.NewProjectile(getPrismPosition(lastShot), Vector2.Normalize(target.Center - getPrismPosition(lastShot)) * 20f, ModContent.ProjectileType<EbonyPrismProjectile>(), projectile.damage, projectile.knockBack, projectile.owner);
=======
                            Projectile.NewProjectile(getPrismPosition(lastShot), Vector2.Normalize(target.Center - getPrismPosition(lastShot)) * 20f, ModContent.ProjectileType<EbonyPrismProjectile>(), projectile.damage, projectile.knockBack, projectile.owner, target.whoAmI);
>>>>>>> 17bf96c09ddc66bfdb2c62d069e49e37f7514e9b
                        }
                        lastShot++;
                        projectile.ai[1] = 0;
                    }
                    if (lastShot > (projectile.minionSlots * prismsPerSlot))
                    {
                        ChangeState(AIStates.Charging);
                    }
                }
            }
<<<<<<< HEAD
            else
            {
                target = Main.npc[0];
            }

            Player player = Main.player[projectile.owner];
            Vector2 targetPos = player.Center - new Vector2(player.direction * 20, 0);
            projectile.velocity = Vector2.Normalize(targetPos - projectile.Center) * 0.5f;
            if (target.active)
            {
                projectile.rotation = Vector2.Normalize(target.Center - projectile.Center).ToRotation();
=======
            #endregion

            #region movement and rotation
            Player player = Main.player[projectile.owner];
            Vector2 targetPos = player.Center - new Vector2(player.direction * 20, 0);
            projectile.velocity = Vector2.Zero;
            if (targetPos != projectile.position)
            {
                projectile.velocity = (targetPos - projectile.Center).SafeNormalize(Vector2.UnitX) * 0.5f;
            }
            if (Helper.IsTargetValid(target))
            {
                projectile.rotation = Vector2.Normalize(target.Center - projectile.Center).ToRotation() + 1.57f;
>>>>>>> 17bf96c09ddc66bfdb2c62d069e49e37f7514e9b
            }
            else
            {
                projectile.rotation = 0;
            }
<<<<<<< HEAD
=======
            #endregion
            Main.NewText(projectile.position);
>>>>>>> 17bf96c09ddc66bfdb2c62d069e49e37f7514e9b
        }
        #endregion
    }
    public class EbonyPrismProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebony Prism");
<<<<<<< HEAD
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 80;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
=======
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 40;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
>>>>>>> 17bf96c09ddc66bfdb2c62d069e49e37f7514e9b
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.minion = true;
<<<<<<< HEAD
            projectile.timeLeft = 80;
            projectile.extraUpdates = 4;
            projectile.penetrate = 2;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.penetrate == 1)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.timeLeft <= 40)
            {
                projectile.velocity *= 0.97f;
                projectile.penetrate = 1;
            }
            projectile.velocity = oldVelocity;
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 1;
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void AI()
        {
            if (Main.rand.Next(4) == 0)
=======
            projectile.timeLeft = 180;
            projectile.extraUpdates = 1;
            projectile.penetrate = 1;
        }

        public override void AI()
        {
            if (Main.rand.Next(2) == 0)
>>>>>>> 17bf96c09ddc66bfdb2c62d069e49e37f7514e9b
            {
                Dust dust = Dust.NewDustPerfect(projectile.Center + Vector2.Normalize(projectile.velocity) * 4, 240, Vector2.Zero, 125);
                dust.noGravity = true;
            }
<<<<<<< HEAD
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
            projectile.velocity *= 0.97f;
            if (projectile.timeLeft <= 40 || projectile.penetrate == 1)
            {
                projectile.extraUpdates = 0;
                projectile.alpha += 5;
                projectile.velocity *= 0.95f;
            }
=======
            projectile.ai[1] += 0.2f;
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
            NPC target = Main.npc[(int)projectile.ai[0]];
            projectile.velocity += Vector2.Normalize(target.Center - projectile.Center) * 0.7f;
            projectile.velocity = Vector2.Normalize(projectile.velocity) * (5 + projectile.ai[1]);
>>>>>>> 17bf96c09ddc66bfdb2c62d069e49e37f7514e9b
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + projectile.Size / 2 + new Vector2(0f, projectile.gfxOffY);
<<<<<<< HEAD
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k * 3) / projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, projectile.Size / 2, projectile.scale - (k * 0.01f), SpriteEffects.None, 0f);
            }
            return true;
=======
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k * 6) / projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.oldRot[k], projectile.Size / 2, projectile.scale - (k * 0.02f), SpriteEffects.None, 0f);
            }
            return false;
>>>>>>> 17bf96c09ddc66bfdb2c62d069e49e37f7514e9b
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 12; k++)
            {
                int dust = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 240, 0f, 0f, 0, default, 1f);
                Main.dust[dust].velocity = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3));
                Main.dust[dust].fadeIn *= 3f;
                Main.dust[dust].noGravity = true;
            }
            base.Kill(timeLeft);
        }
    }
}
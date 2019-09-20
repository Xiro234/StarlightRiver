using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.NPCs;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    public class VitricIcicleProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.extraUpdates = 1;
            projectile.timeLeft = 800;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VortexPenisStaff moment 2");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2;
            int dustType = mod.DustType("Air");
            if (Main.rand.Next(3) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 160, default(Color), 1f)];
                dust.noGravity = true;
                dust.scale = 0.8f;
                dust.fadeIn = 0.4f;
                dust.noLight = true;
            }
            if (projectile.ai[1] != 0)
            {
                int maxCrystals = 6;
                Point[] array2 = new Point[maxCrystals];
                projectile.Center = Main.npc[(int)projectile.ai[1]].Center - projectile.velocity * 1.4f;
            }
            Point[] array = new Point[99];
            int stacksofDebuff = 1;

            for (int npcCounter = 0; npcCounter < 200; npcCounter++)
            {
                for (int projectileCounter = 0; projectileCounter < 1000; projectileCounter++)
                {
                    if (projectileCounter != projectile.whoAmI && Main.projectile[projectileCounter].active && Main.projectile[projectileCounter].owner == Main.myPlayer && Main.projectile[projectileCounter].type == projectile.type && Main.projectile[projectileCounter].ai[1] == npcCounter)
                    {
                        array[stacksofDebuff++] = new Point(projectileCounter, Main.projectile[projectileCounter].timeLeft);
                    }
                }
            }
            projectile.damage = stacksofDebuff * 2;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[1] != 0)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[1] = target.whoAmI;
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            for (int counter = 0; counter <= 14; counter++)
            {
                int dustType = mod.DustType("Air");
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, projectile.velocity.X, projectile.velocity.Y, 160, default(Color), 1f)];
                dust.velocity += new Vector2(Main.rand.NextFloat(-1.6f, 1.6f), Main.rand.NextFloat(-1.6f, 1.6f));
                dust.velocity = dust.velocity / 4f;
                dust.noGravity = true;
                dust.fadeIn = 1.1f;

                dust.noLight = true;
            }
        }
    }
}
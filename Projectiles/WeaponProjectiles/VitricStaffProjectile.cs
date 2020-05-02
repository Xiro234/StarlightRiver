using Microsoft.Xna.Framework;
using StarlightRiver.Dusts;
using StarlightRiver.Items.Vitric;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.ModBrowser;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    class VitricStaffProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 600;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Glass");
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item27);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            for (int k = 0; k < Main.projectile.Length; k++)
            {
                if (Main.projectile[k].active)
                {
                    if (Main.projectile[k].type == projectile.type)
                    {
                        if (Main.projectile[k].ai[0] == 1 && Main.projectile[k].ai[1] == target.whoAmI)
                        {
                            damage++;
                        }
                    }
                }
            }
            projectile.timeLeft = 180;
            projectile.ai[0] = 1;
            projectile.ai[1] = target.whoAmI;
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[0] == 1)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            if (projectile.ai[0] == 1)
            {
                projectile.ignoreWater = true;
                projectile.tileCollide = false;
                NPC stuckTarget = Main.npc[(int)projectile.ai[1]];
                if (stuckTarget.active && !stuckTarget.dontTakeDamage)
                {
                    projectile.Center = stuckTarget.Center - projectile.velocity * 1.7f;
                    projectile.gfxOffY = stuckTarget.gfxOffY;
                }
            }
            Dust.NewDust(projectile.Center, 20, 20, ModContent.DustType<Air>(), 0,0);
        }
    }
}
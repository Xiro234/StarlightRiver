using Microsoft.Xna.Framework;
using StarlightRiver.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    public class PHFrost : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 180;
            projectile.magic = true;
            projectile.ignoreWater = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VortexPenisStaff moment 2");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            for (float counter = -1.2f; counter <= 1.2f; counter += 2.4f)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, -projectile.velocity.X, -projectile.velocity.Y, 255, default(Color), 1f)];
                dust.velocity = dust.velocity.RotatedBy(counter);
                dust.scale = 1.4f;
                dust.position = projectile.Center - dust.velocity;
                dust.noGravity = true;
                dust.noLight = true;
            }
            Dust dust2 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, -projectile.velocity.X, -projectile.velocity.Y, 255, default(Color), 1f)];
            dust2.velocity += new Vector2(Main.rand.NextFloat(-1.6f, 1.6f), Main.rand.NextFloat(-1.6f, 1.6f));
            dust2.scale = 0.9f;
            dust2.position = projectile.Center;
            dust2.noGravity = true;
            dust2.noLight = true;

            float anglediff = (projectile.velocity.ToRotation() - (Main.MouseWorld - projectile.Center).ToRotation() + 9.42f) % 6.28f - 3.14f;
            if (Vector2.Distance(Main.MouseWorld, projectile.Center) <= 800 && anglediff <= 2f && anglediff >= -2f)
            {
                projectile.velocity += Vector2.Normalize(Main.MouseWorld - projectile.Center) * 0.2f;
            }
            projectile.velocity = Vector2.Normalize(projectile.velocity) * 5;


            Vector2 vectorToCursor = projectile.Center - player.Center;
            bool projDirection = projectile.Center.X < player.Center.X;
            if (projectile.Center.X < player.Center.X)
            {
                vectorToCursor = -vectorToCursor;
            }
            player.direction = ((projDirection) ? -1 : 1);
            player.itemRotation = vectorToCursor.ToRotation();
            player.itemTime = 20;
            player.itemAnimation = 20;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 120, false);
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 14); //boom
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 74); //fork boom
            int explosion = Projectile.NewProjectile(projectile.Center, new Vector2(0f, 0f), mod.ProjectileType("AOEExplosion"), projectile.damage, projectile.knockBack, player.whoAmI);
            Main.projectile[explosion].ai[0] = 80;
            for (int counter = 0; counter <= 18; counter++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, 0f, 0f, 255, default(Color), 1f)];
                dust.velocity += new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f));
                dust.scale = 2f;
                dust.position = projectile.Center;
                dust.noGravity = true;
                dust.noLight = true;
            }
        }
    }
    public class HMFrost : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 180;
            projectile.magic = true;
            projectile.ignoreWater = false;
            projectile.extraUpdates = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VortexPenisStaff moment 2");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.ai[1] += 0.1f;
            if (projectile.ai[1] >= 1.4f)
            {
                projectile.ai[1] = -1.4f;
            }
            for (float counter = -1.2f; counter <= 1.2f; counter += 2.4f)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, -projectile.velocity.X, -projectile.velocity.Y, 255, default(Color), 1f)];
                dust.velocity = dust.velocity.RotatedBy(counter);
                dust.velocity *= projectile.ai[1];
                dust.fadeIn = 1.5f;
                dust.scale = 1.2f;
                dust.position = projectile.Center - dust.velocity;
                dust.noGravity = true;
                dust.noLight = true;
            }
            Dust dust2 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, 0f, 0f, 255, default(Color), 1f)];
            dust2.scale = 0.9f;
            dust2.fadeIn = 1.7f;
            dust2.position = projectile.Center;
            dust2.noGravity = true;
            dust2.noLight = true;

            float anglediff = (projectile.velocity.ToRotation() - (Main.MouseWorld - projectile.Center).ToRotation() + 9.42f) % 6.28f - 3.14f;
            if (Vector2.Distance(Main.MouseWorld, projectile.Center) <= 800 && anglediff <= 2f && anglediff >= -2f)
            {
                projectile.velocity += Vector2.Normalize(Main.MouseWorld - projectile.Center) * 0.2f;
            }
            projectile.velocity = Vector2.Normalize(projectile.velocity) * 5;


            Vector2 vectorToCursor = projectile.Center - player.Center;
            bool projDirection = projectile.Center.X < player.Center.X;
            if (projectile.Center.X < player.Center.X)
            {
                vectorToCursor = -vectorToCursor;
            }
            player.direction = ((projDirection) ? -1 : 1);
            player.itemRotation = vectorToCursor.ToRotation();
            player.itemTime = 20;
            player.itemAnimation = 20;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.GetGlobalNPC<DebuffHandler>().frozenTime = 60;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 14); //boom
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 74); //fork boom
            int explosion = Projectile.NewProjectile(projectile.Center, new Vector2(0f, 0f), mod.ProjectileType("AOEExplosion"), projectile.damage, projectile.knockBack, player.whoAmI);
            Main.projectile[explosion].ai[0] = 120;
            for (int counter = 0; counter <= 24; counter++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, 0f, 0f, 255, default(Color), 1f)];
                dust.velocity += new Vector2(Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f));
                dust.scale = 2.4f;
                dust.position = projectile.Center;
                dust.noGravity = true;
                dust.noLight = true;
            }
        }
    }
}
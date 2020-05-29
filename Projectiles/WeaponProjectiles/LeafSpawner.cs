using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.WeaponProjectiles
{
    internal class LeafSpawner : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.damage = 0;
            projectile.aiStyle = -1;
            projectile.ignoreWater = true;
            projectile.friendly = true;
        }
        public int proj { get; set; }
        public override void AI()
        {
            projectile.ai[0]++;
            if (!Main.projectile[proj].active) projectile.Kill();
            projectile.position = Main.projectile[proj].position;
            if (projectile.ai[0] % 10 == 0) Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<Leaf>(), projectile.damage, projectile.knockBack, projectile.owner);
        }
    }
}

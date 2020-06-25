using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Items.Aluminum
{
    class StarSniper : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 140;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useAmmo = AmmoID.FallenStar;
            item.ranged = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.crit = 20;
            item.shoot = ProjectileType<StarSniperBolt>();
            item.shootSpeed = 5;
            item.UseSound = SoundID.Item40;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-10, 0);
    }

    class StarSniperBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.width = 8;
            projectile.height = 8;
            projectile.extraUpdates = 20;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
            Dust.NewDustPerfect(projectile.Center, DustType<Dusts.Stamina>(), projectile.velocity * -Main.rand.NextFloat(), 0, default, 2);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, ProjectileType<StarSniperAura>(), 20, 0, projectile.owner);
        }
    }

    class StarSniperAura : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.width = 64;
            projectile.height = 64;
            projectile.timeLeft = 60;
            projectile.penetrate = -1;
        }

        public override void AI() => projectile.rotation += 0.1f;

        public override Color? GetAlpha(Color lightColor) => Color.White * (projectile.timeLeft / 60f);
    }
}

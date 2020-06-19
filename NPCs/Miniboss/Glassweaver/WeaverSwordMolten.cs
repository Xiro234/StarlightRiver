using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace StarlightRiver.NPCs.Miniboss.Glassweaver
{
    class WeaverSwordMolten : ModProjectile
    {
        Player target;

        Vector2 moveTarget;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molten Glass Sword");
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.hostile = true;
            projectile.timeLeft = 600;
        }

        public override void AI()
        {
            int lifeTime = 600 - projectile.timeLeft;

            if (lifeTime == 0) target = Helper.FindNearestPlayer(projectile.Center);

            if (lifeTime <= 75)
            {
                projectile.scale = lifeTime / 60f;
                projectile.rotation = 1.57f * 0.5f + (target.Center - projectile.Center).ToRotation() + lifeTime / 25f * 6.28f;
            }

            if (lifeTime == 76)
            {
                moveTarget = target.Center;
                projectile.velocity = Vector2.Normalize(projectile.Center - moveTarget) * -6;
                Main.PlaySound(SoundID.DD2_WitherBeastAuraPulse, projectile.Center);
            }

            if (lifeTime > 76) Dust.NewDustPerfect(projectile.Center, DustType<Dusts.Air>(), Vector2.Zero, 0, default, 0.5f);
        }

        public override void Kill(int timeLeft)
        {
            for (int x = -2; x < 2; x++)
                for (int y = -2; y < 2; y++)
                    Projectile.NewProjectile(((projectile.Center / 16).ToPoint16().ToVector2() + new Vector2(x + 0.5f, y + 0.5f)) * 16, Vector2.Zero, ProjectileType<MoltenBurn>(), 5, 0);

            Main.PlaySound(SoundID.Drown, projectile.Center);
        }
    }
}

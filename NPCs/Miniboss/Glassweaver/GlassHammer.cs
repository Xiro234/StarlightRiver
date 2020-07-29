using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.NPCs.Miniboss.Glassweaver
{
    class GlassHammer : ModProjectile
    {
        Vector2 origin;

        public override string Texture => "StarlightRiver/Invisible";

        public override void SetStaticDefaults() => DisplayName.SetDefault("Woven Hammer");

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.hostile = true;
            projectile.timeLeft = 120;
            projectile.tileCollide = false;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
        }

        public override void AI()
        {
            if (projectile.timeLeft == 120) origin = projectile.Center; //sets origin when spawned

            if (projectile.timeLeft >= 60)
            {
                float radius = (120 - projectile.timeLeft) * 2;
                float rotation = -(120 - projectile.timeLeft) / 60f * 0.8f; //ai 0 is direction

                projectile.Center = origin - Vector2.UnitY.RotatedBy(rotation * projectile.ai[0]) * radius;
            }
            else if (projectile.timeLeft >= 40)
            {
                float rotation = -0.8f + (120 - projectile.timeLeft - 60) / 20f * ((float)Math.PI / 2 + 1.2f);

                projectile.Center = origin - Vector2.UnitY.RotatedBy(rotation * projectile.ai[0]) * 120;

                if (projectile.timeLeft == 40)
                {
                    Main.PlaySound(SoundID.Shatter, projectile.Center);
                    Main.LocalPlayer.GetModPlayer<Core.StarlightPlayer>().Shake += 15;

                    for (int k = 0; k < 30; k++)
                    {
                        Vector2 vector = Vector2.UnitY.RotatedByRandom((float)Math.PI / 2);
                        Dust.NewDustPerfect(projectile.Center + vector * Main.rand.NextFloat(25), DustType<Dusts.Sand>(), vector * Main.rand.NextFloat(3, 5), 150, Color.White, 0.5f);
                    }
                }
            }
            else if (projectile.timeLeft % 2 == 0)
            {
                Main.NewText(projectile.ai[0]);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if(projectile.timeLeft <= 60 && projectile.timeLeft >= 40)
            {
                target.AddBuff(BuffType<Buffs.Squash>(), 180);
            }
        }
    }
}

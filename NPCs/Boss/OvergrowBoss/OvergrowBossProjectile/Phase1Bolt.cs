﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.OvergrowBoss.OvergrowBossProjectile
{
    internal class Phase1Bolt : ModProjectile
    {
        public override string Texture => "StarlightRiver/Invisible";
        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 16;
            projectile.height = 16;
            projectile.timeLeft = 900;
            projectile.extraUpdates = 3;
            projectile.hostile = true;
        }

        public override void AI()
        {
            Dust.NewDustPerfect(projectile.Center + Vector2.One.RotatedByRandom(6.28f), ModContent.DustType<Dusts.Gold2>(),
                Vector2.Normalize(projectile.velocity.RotatedBy(1.58f)) * (float)Math.Sin(LegendWorld.rottime * 16) * 0.6f, 0, default, 0.8f);
        }
        public override void Kill(int timeLeft)
        {
            //Main.PlaySound(ModLoader.GetMod("StarlightRiver").GetLegacySoundSlot(SoundType.Custom, "Sounds/ProjectileImpact1").WithVolume(0.5f), projectile.Center);
            for (int k = 0; k < 20; k++)
            {
                Dust.NewDustPerfect(projectile.Center, ModContent.DustType<Dusts.Gold2>(), Vector2.One.RotatedByRandom(6.28f));
            }
        }
    }
}

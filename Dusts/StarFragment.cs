﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Dusts
{
    public class StarFragment : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += 0.10f;
            dust.velocity *= 0.93f;
            dust.color *= 0.96f;

            dust.scale *= 0.96f;
            if (dust.scale < 0.1f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}

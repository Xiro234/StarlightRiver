﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Dusts
{
    internal class BioLumen : ModDust
    {
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }
        public override bool Update(Dust dust)
        {
            dust.position.Y += (float)Math.Sin(LegendWorld.rottime + dust.fadeIn) * 0.3f;
            dust.position += dust.velocity;
            dust.scale *= 0.994f;
            //Lighting.AddLight(dust.position, dust.color.ToVector3() * dust.scale);
            if (dust.scale <= 0.2f) dust.active = false;
            return false;
        }
    }
}

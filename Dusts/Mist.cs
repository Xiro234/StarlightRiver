
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Dusts
{
    public class Mist : ModDust
    {
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }
        public override void OnSpawn(Dust dust)
        {
            dust.scale *= Main.rand.NextFloat(2, 5);
            dust.fadeIn = 0;
            dust.noLight = false;
            dust.frame = new Rectangle(0, 0, 40, 10);
        }
        public override bool Update(Dust dust)
        {
            dust.position.X += dust.velocity.X + Main.LocalPlayer.velocity.X * -0.4f * (dust.scale * 0.01f);
            dust.position.Y += Main.LocalPlayer.velocity.Y * -0.1f * (dust.scale * 0.01f);
            dust.velocity.X = 1.1f;
            dust.position.Y += (float)Math.Sin(LegendWorld.rottime + dust.fadeIn / 30f) * 0.4f;
            dust.scale *= 0.999f;

            dust.fadeIn++;
            float alpha = (dust.fadeIn / 30f) - ((float)Math.Pow(dust.fadeIn, 2) / 3600f);
            dust.color = Color.White * 0.4f * alpha;

            if (dust.fadeIn > 120)
            {
                dust.active = false;
            }
            return false;
        }
    }

}

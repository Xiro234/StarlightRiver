using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace spritersguildwip.Dusts
{
    public class Fire : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.3f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 1.4f;
            dust.color.R = 255;
            dust.color.G = 255;
            dust.color.B = 80;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }
        public override bool Update(Dust dust)
        {
            Player player = Main.LocalPlayer;
            dust.position += player.velocity;
            dust.position.Y -= 1;
            dust.position.X += dust.velocity.X * 0.25f;
            dust.color.G -= 4;

            dust.scale *= 0.97f;


            if (dust.scale < 0.4f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}

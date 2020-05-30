﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Dusts
{
    public class Gas : ModDust
    {
        //private readonly float mult;

        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }

        public override bool Update(Dust dust)
        {
            //Lighting.AddLight((int)dust.position.X / 16, (int)dust.position.Y / 16, 0.2f, 0.19f, 0.0f);
            dust.color = Lighting.GetColor((int)(dust.position.X / 16), (int)(dust.position.Y / 16)).MultiplyRGB(Color.White) * 0.07f;
            dust.position += dust.velocity * 0.1f;
            dust.scale *= 0.992f;
            dust.velocity *= 0.97f;
            dust.rotation += 0.1f;

            if (dust.scale <= 0.2f)
            {
                dust.active = false;
            }
            return false;
        }
    }

    public class GasGreen : Gas { }
}
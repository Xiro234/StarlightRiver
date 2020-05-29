using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Dusts
{
    public class Shadowflame : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
            dust.color = new Color(196 + Main.rand.Next(20), 136+Main.rand.Next(20), 251);
        }
        public override bool Update(Dust dust)
        {
            if (dust.alpha > 255 || dust.scale <= 0)
            {
                dust.active = false;
                return false;
            }
            if (dust.color.R >= 182 && Main.rand.NextBool())
            {
                dust.color.R -= 1;
            }
            if (dust.color.G >= 27)
            {
                dust.color.G -= 6;
            }
            Lighting.AddLight(dust.position, dust.color.ToVector3() * dust.scale * 0.75f);

            dust.velocity *= 0.9f;
            dust.position += dust.velocity;
            dust.rotation += 0.05f;
            dust.scale -= 0.04f;
            return false;
        }
    }
}

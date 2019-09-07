
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace spritersguildwip.Dusts
    {
    public class Purify : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
            dust.color = Color.White;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }
        public override bool Update(Dust dust)
        {
            dust.color *= 0.94f;
            dust.velocity *= 0.94f;
            dust.scale *= 0.94f;
            dust.rotation += dust.velocity.Length() * 0.1f;
            if (dust.scale <= 0.3)
            {
                dust.active = false;
            }

            float light = 0.2f * dust.scale;
            Lighting.AddLight(dust.position, new Vector3(1,1,1) * light);
            
            return false;
        }
   
    }

}

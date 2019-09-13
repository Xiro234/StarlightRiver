
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Dusts
    {
    public class Darkness : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
        }
        public override bool Update(Dust dust)
        {
            dust.position.Y -= 0.6f;
            dust.velocity *= 0.94f;
            dust.scale *= 0.94f;
            if (dust.scale <= 0.2)
            {
                dust.active = false;
            }

            float light = 0.4f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, new Vector3(1.1f,1.12f,1) * light);
            }
            return false;
        }
   
    }

}

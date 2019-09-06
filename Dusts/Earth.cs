using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace spritersguildwip.Dusts
{
    public class Stone : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = false;
            dust.noLight = false;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.velocity.Y += 0.2f;
            if(Main.tile[(int)dust.position.X / 16, (int)dust.position.Y / 16].active() && Main.tile[(int)dust.position.X / 16, (int)dust.position.Y / 16].collisionType == 1)
            {
                dust.velocity *= -0.5f;
            }

            dust.rotation = dust.velocity.ToRotation();
            dust.scale *= 0.99f;
            if (dust.scale < 0.2f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}

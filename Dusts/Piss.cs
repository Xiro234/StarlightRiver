using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Dusts
{
    public class Piss : ModDust
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Dusts/Air";
            return true;
        }
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = false;
            dust.noLight = false;
            dust.alpha = 180;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.velocity.Y += 0.1f;
            if (Main.tile[(int)dust.position.X / 16, (int)dust.position.Y / 16].active() && Main.tile[(int)dust.position.X / 16, (int)dust.position.Y / 16].collisionType == 1)
            {
                Dust.NewDustPerfect(dust.position, ModContent.DustType<Gas>(), Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(8f), 0, default, 5);
                dust.active = false;
            }

            dust.rotation += 0.1f;
            dust.scale *= 0.99f;
            return false;
        }
    }
}
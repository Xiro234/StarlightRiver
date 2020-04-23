using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Dusts
{
    public class JungleEnergy : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
            dust.color.R = 180;
            dust.color.G = 255;
            dust.color.B = 80;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += 0.05f;
            dust.color.R--;
            dust.scale *= 0.992f;
            if (dust.scale < 0.3f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}

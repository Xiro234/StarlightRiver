using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Dusts
{
    public class Electric : ModDust
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Dusts/Stamina";
            return base.Autoload(ref name, ref texture);
        }

        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
            dust.color.R = 100;
            dust.color.G = 200;
            dust.color.B = 255;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }

        public override bool Update(Dust dust)
        {
            Lighting.AddLight(dust.position, new Vector3(0.1f, 0.35f, 0.5f) * 1.5f * dust.scale);
            dust.rotation += Main.rand.NextFloat(2f);
            dust.color *= 0.92f;
            if (dust.color.G > 80) dust.color.G -= 4;

            dust.scale *= 0.92f;
            if (dust.scale < 0.2f)
            {
                dust.active = false;
            }
            return false;
        }
    }

    public class Electric2 : Electric
    {
        public override bool Update(Dust dust)
        {
            Lighting.AddLight(dust.position, new Vector3(0.1f, 0.35f, 0.5f) * 1.5f * dust.scale);
            dust.rotation += Main.rand.NextFloat(2f);
            dust.color *= 0.92f;
            if (dust.color.G > 80) dust.color.G -= 4;

            dust.scale *= 0.98f;
            if (dust.scale < 0.1f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}
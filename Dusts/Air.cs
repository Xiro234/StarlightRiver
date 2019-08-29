using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace spritersguildwip.Dusts
{
	public class Air : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.velocity *= 0.3f;
			dust.noGravity = true;
			dust.noLight = false;
			dust.scale *= 3f;
        }
        public override bool Update(Dust dust)
		{
            dust.position.Y += dust.velocity.Y * 2;
            dust.velocity.Y += 0.1f;
			dust.position.X += dust.velocity.X * 2;
            dust.rotation += 0.05f;

                dust.scale *= 0.97f;

                                     
			if (dust.scale < 0.4f)
			{
				dust.active = false;
			}
			return false;
		}
    }

    public class Gold : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.3f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 3f;
            dust.color.R = 255;
            dust.color.G = 220;
            dust.color.B = 100;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }
        public override bool Update(Dust dust)
        {
            dust.rotation += 0.05f;

            dust.scale *= 0.97f;
            if (dust.scale < 0.2f)
            {
                dust.active = false;
            }
            return false;
        }
    }

    public class Gold2 : Gold
    {
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += 0.05f;

            dust.scale *= 0.92f;
            if (dust.scale < 0.3f)
            {
                dust.active = false;
            }
            return false;
        }
    }

    public class Void : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.1f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 3f;
            dust.color.R = 130;
            dust.color.G = 20;
            dust.color.B = 235;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += 0.05f;

            dust.scale *= 0.94f;


            if (dust.scale < 0.2f)
            {
                dust.active = false;
            }
            return false;
        }
    }

    public class Void2 : Void
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.1f;
            dust.velocity += new Vector2(Main.rand.Next(-100, 100) * 0.1f, Main.rand.Next(-100, 100) * 0.1f);
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 3f;
            dust.color.R = 130;
            dust.color.G = 20;
            dust.color.B = 235;
        }
        public override bool Update(Dust dust)
        {
            Player player = Main.LocalPlayer;
            dust.velocity = (player.Center - dust.position ) / 30;
            dust.position += dust.velocity;
            dust.rotation += 0.05f;

            dust.scale *= 0.98f;


            if (dust.scale < 0.2f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}


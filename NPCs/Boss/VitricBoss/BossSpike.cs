using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    internal class BossSpike : ModProjectile
    {
        public override string Texture => "StarlightRiver/Invisible";

        public override void SetDefaults()
        {
            projectile.hostile = false;
            projectile.width = 220;
            projectile.height = 128;
            projectile.timeLeft = 180;
        }

        public override bool CanHitPlayer(Player target)
        {
            if (projectile.ai[0] > 70 && Abilities.AbilityHelper.CheckDash(target, projectile.Hitbox))
            {
                for (int k = 0; k < 50; k++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, DustType<Dusts.Glass2>());
                }
                Main.PlaySound(Terraria.ID.SoundID.Shatter, target.Center);
                projectile.Kill();
                return false;
            }
            return true;
        }

        public override void AI()
        {
            projectile.ai[0]++; //ticks up the timer

            if (projectile.ai[0] < 90 && projectile.ai[0] > 10)
            {
                Dust.NewDust(projectile.position + new Vector2(0, projectile.height), projectile.width, 1, DustType<Dusts.Glass2>());
                int i = Dust.NewDust(projectile.position + new Vector2(0, projectile.height), projectile.width, 1, DustType<Dusts.AirDash>(), 0, -5);
                Main.dust[i].fadeIn = 30;
            }
            if (projectile.ai[0] == 90) //when this projectile goes off
            {
                projectile.hostile = true;
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.ai[0] > 90)
            {
                Texture2D tex = GetTexture("StarlightRiver/NPCs/Boss/VitricBoss/BossSpike");
                int off = projectile.ai[0] < 100 ? (int)((projectile.ai[0] - 90) / 10f * projectile.height) : projectile.height - (int)((projectile.ai[0] - 100) / 80f * projectile.height);
                Rectangle targetRect = new Rectangle((int)(projectile.position.X - Main.screenPosition.X), (int)(projectile.position.Y - off - Main.screenPosition.Y + projectile.height), projectile.width, off);
                Rectangle sourceRect = new Rectangle(0, 0, projectile.width, off);
                spriteBatch.Draw(tex, targetRect, sourceRect, lightColor, 0, Vector2.Zero, 0, 0);
            }
        }
    }
}
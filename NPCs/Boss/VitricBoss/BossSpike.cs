using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    class BossSpike : ModProjectile
    {
        public override string Texture => "StarlightRiver/Invisible";
        public override void SetDefaults()
        {
            projectile.hostile = false;
            projectile.width = 48;
            projectile.height = 128;
            projectile.timeLeft = 2;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.ai[0] > 70 && Abilities.AbilityHelper.CheckDash(target, projectile.Hitbox))
            {
                for(int k = 0; k < 50; k++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.Glass2>());
                }
                Main.PlaySound(Terraria.ID.SoundID.Shatter, target.Center);
                projectile.Kill();
                return false;
            }
            return true;
        }
        public override void AI()
        {
            projectile.timeLeft = 2;
            projectile.ai[0]++; //ticks up the timer

            if(projectile.ai[0] < 60 && projectile.ai[0] > 10)
            {
                Dust.NewDust(projectile.position + new Vector2(0, projectile.height), projectile.width, 1, ModContent.DustType<Dusts.Glass2>());
                int i = Dust.NewDust(projectile.position + new Vector2(0, projectile.height), projectile.width, 1, ModContent.DustType<Dusts.AirDash>(), 0, -5);
                Main.dust[i].fadeIn = 30;
            }
            if (projectile.ai[0] == 60) //when this projectile goes off
            {
                projectile.hostile = true;
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.ai[0] > 60)
            {
                Texture2D tex = ModContent.GetTexture("StarlightRiver/NPCs/Boss/VitricBoss/BossSpike");
                int off = projectile.ai[0] < 70 ? (int)((projectile.ai[0] - 60) / 10f * projectile.height) : projectile.height;
                Rectangle targetRect = new Rectangle((int)(projectile.position.X - Main.screenPosition.X), (int)(projectile.position.Y - off - Main.screenPosition.Y + projectile.height), projectile.width, off);
                Rectangle sourceRect = new Rectangle(0, 0, projectile.width, off);
                spriteBatch.Draw(tex, targetRect, sourceRect, lightColor, 0, Vector2.Zero, 0, 0);
            }

            if (projectile.ai[0] > 70)
            { 
                Texture2D tex2 = ModContent.GetTexture("StarlightRiver/NPCs/Boss/VitricBoss/BossSpikeGlow");
                spriteBatch.Draw(tex2, projectile.position - Vector2.One - Main.screenPosition, tex2.Frame(), Color.White * (float)Math.Sin(LegendWorld.rottime));
            }
        }
    }
}

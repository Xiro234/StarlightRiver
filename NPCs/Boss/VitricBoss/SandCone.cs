using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    class SandCone : ModProjectile
    {
        public override string Texture => "StarlightRiver/Invisible";
        public override void SetDefaults()
        {
            projectile.hostile = false;
            projectile.width = 1;
            projectile.height = 1;
            projectile.timeLeft = 2;
        }
        public override void AI()
        {
            projectile.timeLeft = 2;
            projectile.ai[0]++; //ticks up the timer

            if (projectile.ai[0] >= 70) //when this projectile goes off
            {
                for(int k = 0; k < 100; k++) Dust.NewDustPerfect(projectile.Center, ModContent.DustType<Dusts.Sand>(), new Vector2(Main.rand.NextFloat(-20f, 0), 0).RotatedBy(projectile.rotation + Main.rand.NextFloat(-0.2f, 0.2f)), Main.rand.Next(50, 150));
                foreach (Player player in Main.player.Where(n => Helper.CheckConicalCollision(projectile.Center, 700, projectile.rotation, 0.2f, n.Hitbox)))
                {
                    player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " bit the dust..."), Main.expertMode ? 50 : 35, 0); //hurt em
                    if (Main.rand.Next(Main.expertMode ? 1 : 2) == 0) player.AddBuff(BuffID.Obstructed, 180); //blind em
                }
                Main.PlaySound(SoundID.DD2_BookStaffCast); //sound
                projectile.Kill(); //self-destruct
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(default, BlendState.Additive);

            if(projectile.ai[0] <= 66) //draws the proejctile's tell ~1 second before it goes off
            {
                Texture2D tex = ModContent.GetTexture("StarlightRiver/NPCs/Boss/VitricBoss/ConeTell");
                float alpha = ((projectile.ai[0] * 2 / 33) - ((float)Math.Pow(projectile.ai[0], 2) / 1086)) * 0.5f;
                spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, tex.Frame(), Color.White * alpha, projectile.rotation - 1.57f, new Vector2(tex.Width / 2, tex.Height), 1, 0, 0);
            }

            spriteBatch.End();
            spriteBatch.Begin();
        }
    }
}

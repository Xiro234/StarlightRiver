using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace StarlightRiver.NPCs.Boss.OvergrowBoss
{
    public partial class OvergrowBoss : ModNPC
    {
        private void Phase1Spin(float size)
        {
            if(npc.ai[3] <= 60)
                flail.npc.Center = Vector2.Lerp(flail.npc.Center, npc.Center, npc.ai[3] / 20);

            if (npc.ai[3] > 60 && npc.ai[3] <= 80)
                flail.npc.velocity.Y += size;
            if (npc.ai[3] > 80 && npc.ai[3] <= 100)
                flail.npc.velocity.Y -= size;

            if (npc.ai[3] > 100 && npc.ai[3] <= 220)
            {
                int x = (int)npc.ai[3] - 100;
                float rot = 0.314f * -0.042f * x + 0.003f * (float)Math.Pow(x, 2) - 0.00002f * (float)Math.Pow(x, 3);
                //function to model the desired rotation, thanks wolfram alpha :3
                flail.npc.Center = npc.Center + new Vector2(0, 1).RotatedBy(rot) * 39 * size * 20 * 0.5f;

                if (npc.ai[3] > 105 && npc.ai[3] < 190)
                {
                    for (int k = 0; k < 3; k++)
                        Dust.NewDust(flail.npc.position, flail.npc.width, flail.npc.height, ModContent.DustType<Dusts.Gold2>());
                    for (int k = 0; k < 8; k++)
                        Dust.NewDustPerfect(Vector2.Lerp(flail.npc.Center, flail.npc.oldPosition + flail.npc.Size / 2, k / 8f), ModContent.DustType<Dusts.Gold2>(), Vector2.One.RotatedByRandom(6.28f) * 0.5f);
                }
            }
            if (npc.ai[3] == 220)
            {
                flail.npc.velocity = flail.npc.position - flail.npc.oldPosition;
                flail.npc.velocity.X *= 0.2f;
            }
            if(npc.ai[3] > 220 && npc.ai[3] <= 390)
            {
                if (Vector2.Distance(flail.npc.Center, npc.Center) < 39 * size * 20 * 0.5f) flail.npc.velocity.Y += 0.8f;
                else
                {
                    float cos = (float)Math.Cos((npc.Center - flail.npc.Center).ToRotation());
                    flail.npc.velocity.X += flail.npc.velocity.Y * cos;
                    flail.npc.velocity.Y *= -0.1f;
                }

                flail.npc.velocity.X += (npc.Center.X - flail.npc.Center.X) * 0.01f;
                flail.npc.velocity *= 0.96f;
            }
            if (npc.ai[3] == 391) ResetAttack();
        }
        private void Phase1Pendulum()
        {

        }
        private void Phase1Bolts()
        {

        }
        private void Phase1Toss()
        {

        }
        private void Phase1Trap()
        {

        }

        private void ResetAttack()
        {
            flail.npc.velocity *= 0;
            npc.ai[3] = 0;
            npc.ai[2] = 0;
        }

    }
}

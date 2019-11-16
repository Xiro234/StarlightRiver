using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using StarlightRiver.Projectiles;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver.NPCs.Hostile
{
    class OvergrowRockThrower : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[PH] VortexPenisEnemy");
        }
        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 48;
            npc.damage = 10;
            npc.defense = 5;
            npc.lifeMax = 155;
            npc.value = 10f;
            npc.knockBackResist = 0.2f;
            npc.aiStyle = -1;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            npc.ai[3] += 0.05f;
            if (npc.ai[3] > 6.28f) npc.ai[3] = 0;
            npc.ai[2] = 3;
            switch (npc.ai[0])
            {
                case 0: //Spawn
                    {
                        npc.TargetClosest();
                        npc.ai[0] = 1;
                    }
                    break;
            }
        }

        private Vector2[] drawpoints = new Vector2[3];

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int k = 0; k < 3; k++)
            {
                float rot = npc.ai[3] + (k + 1) / 3f * 6.28f;
                if (rot % 6.28f > 3.14f && npc.ai[2] >= k + 1)
                {
                    drawpoints[k] = npc.Center + new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot) / 2) * 35 - Main.screenPosition;
                    spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Projectiles/ZapperGlow1"), drawpoints[k], new Rectangle(0,0,16,16), Color.White, 0, Vector2.One * 8, 1, 0, 0);
                }
            }
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int k = 0; k < 3; k++)
            {
                float rot = npc.ai[3] + (k + 1) / 3f * 6.28f;
                if (rot % 6.28f < 3.14f && npc.ai[2] >= k + 1)
                {
                    drawpoints[k] = npc.Center + new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot) / 2) * 35 - Main.screenPosition;
                    spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Projectiles/ZapperGlow1"), drawpoints[k], new Rectangle(0, 0, 16, 16), Color.White, 0, Vector2.One * 8, 1, 0, 0);
                }
            }
        }
    }
}

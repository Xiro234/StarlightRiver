using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    class VitricBossEye
    {
        Vector2 Position;
        VitricBoss Parent;
        public VitricBossEye(Vector2 pos, VitricBoss parent)
        {
            Position = pos;
            Parent = parent;
        }

        public void Draw(SpriteBatch sb)
        {
            Texture2D tex = ModContent.GetTexture("StarlightRiver/NPCs/Boss/VitricBoss/VitricBossEye");
            float rot = (Parent.npc.position + Position - Main.player[Parent.npc.target].Center).ToRotation();
            Color color = new Color(160, 220, 250);

            if (Parent.npc.ai[2] == 1)
            {
                rot = LegendWorld.rottime * 2;
                color = new Color(255, 120, 120);
            }

            sb.Draw(tex, Parent.npc.position + Position + Vector2.One.RotatedBy(rot) * 10 - Main.screenPosition, color);
        }
    }
}

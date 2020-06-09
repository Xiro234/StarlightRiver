using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace StarlightRiver.Buffs
{
    class Illuminant : ModBuff
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return base.Autoload(ref name, ref texture);
        }
        public override void SetDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (Main.rand.Next(4) == 0)
            {
                int i = Dust.NewDust(npc.position, npc.width, npc.height, 264, 0, 0, 0, new Color(255, 255, 200));
                Main.dust[i].noGravity = true;
            }
        }
    }
}

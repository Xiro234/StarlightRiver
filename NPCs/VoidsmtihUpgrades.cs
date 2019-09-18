using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs
{
    class VoidsmtihUpgrades : GlobalNPC
    {
        

        public override void BuffTownNPC(ref float damageMult, ref int defense)
        {
            defense += LegendWorld.NPCUpgrades[1] * 10;
        }

        public override void PostAI(NPC npc)
        {
            if (npc.townNPC)
            {
                npc.lifeMax = 250 + LegendWorld.NPCUpgrades[0] * 50;
            }
        }
    }
}

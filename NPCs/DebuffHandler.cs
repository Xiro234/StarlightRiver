using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs
{
    public class DebuffHandler : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool snared = false;
        public bool ivy = false;
        public override void ResetEffects(NPC npc)
        {
            snared = false;
            ivy = false;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            
            if (snared && npc.aiStyle != 6)
            {
                npc.position = npc.oldPosition;
            }
            if (ivy)
            {
                int lifeRegen = npc.boss ? 8 : 4;
                npc.lifeRegen -= lifeRegen;
            }   
        }
    }
}
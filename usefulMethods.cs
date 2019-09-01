
using Terraria;

namespace spritersguildwip
{
    public class usefulMethods
    {
        public static void EndMePls(NPC npc)
        {
            npc.active = false;
            npc.life = 0;
            npc.checkDead();
            npc.HitEffect();
        }
    }
}

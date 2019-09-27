using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver
{
    public static class Helper
    {
        /// <summary>
        /// Kills the NPC.
        /// </summary>
        /// <param name="npc"></param>
        public static void Kill(this NPC npc)
        {
            bool modNPCDontDie = npc.modNPC != null && !npc.modNPC.CheckDead();
            if (modNPCDontDie)
                return;

            npc.life = 0;
            npc.checkDead();
            npc.HitEffect();
            npc.active = false;
        }

        public static void PlaceMultitile(int width, int height, int xPos, int yPos, int type)
        {
            for (int multiN = 0; multiN < width; multiN++)
            {
                for (int multiM = 0; multiM < height; multiM++)
                {
                    Tile tileAt = Main.tile[multiN + xPos, multiM + yPos];
                    //WorldGen.PlaceTile(multiN + xPos, multiM + yPos, type, false, true);
                    tileAt.type = (ushort)type;
                    tileAt.frameX = (short)(multiN * 18);
                    tileAt.frameY = (short)(multiM * 18);
                }
            }
        }
    }
}


using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    sealed partial class VitricBoss : ModNPC
    {
        private void ResetAttack()
        {
            npc.ai[3] = 0;
            npc.ai[2] = 0;
        }
        private void NukePlatforms()
        {
            if (npc.ai[3] == 1)
            {
                List<Vector2> possibleLocations = new List<Vector2>(CrystalLocations);
                for (int k = 0; k < Crystals.Count; k++)
                {
                    NPC npc = Crystals[k];
                    VitricBossCrystal crystal = npc.modNPC as VitricBossCrystal;

                    crystal.StartPos = npc.Center;
                    Vector2 target = possibleLocations.OrderBy(n => Vector2.Distance(npc.Center, n)).ToList()[0]; //sort the remaining possible unique platforms by distance, this ensures crystals go to theirn earest unique platform
                    crystal.TargetPos = target;
                    possibleLocations.Remove(target);
                    npc.ai[1] = 0; //reset the crystal's timers
                    npc.ai[2] = 1; //set them into this attack's mode
                }
            }
            if (npc.ai[3] == 180) Crystals.FirstOrDefault(n => n.ai[0] == 2).ai[0] = 0;
            if(npc.ai[3] >= 720)
            {
                ResetAttack();
            }
        }
        private void Angry()
        {

        }
    }
}

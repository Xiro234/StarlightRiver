using Terraria.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using System.Linq;
using StarlightRiver.Ability;

namespace StarlightRiver.Items.Debug
{
    class DebugPlayer : ModPlayer
    {
        public override void UpdateLifeRegen()
        {
            for (int k = 0; k <= 200; k++)
            {
                if (Main.npc[k].type == mod.NPCType("TestEnemy") && Main.npc[k].active)
                {
                    break;
                }
                if (k == 200)
                {
                    NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, mod.NPCType("TestEnemy"), 0, 0, 0, 0, 0);
                }
            }
        }
    }
}


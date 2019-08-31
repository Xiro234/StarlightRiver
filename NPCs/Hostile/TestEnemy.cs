using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spritersguildwip.Ability;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.NPCs.Hostile
{
    class TestEnemy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Test Enemy");
        }
        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 48;
            npc.lifeMax = 69420;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.aiStyle = -1;
        }
    }
}

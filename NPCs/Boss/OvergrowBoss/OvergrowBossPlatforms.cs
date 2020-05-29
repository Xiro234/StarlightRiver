﻿using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace StarlightRiver.NPCs.Boss.OvergrowBoss
{
    class OvergrowBossVerticalPlatform : MovingPlatform
    {
        public override string Texture => "StarlightRiver/NPCs/Boss/OvergrowBoss/OvergrowBossPlatform";
        public override void SafeSetDefaults()
        {
            npc.width = 100;
            npc.height = 16;
        }
        public override void SafeAI()
        {
            npc.ai[0] += 0.04f;
            if (npc.ai[0] > 6.28f) npc.ai[0] = 0;
            npc.velocity.Y = (float)Math.Sin(npc.ai[0]) * 3;
        }
    }
    class OvergrowBossCircularPlatform : MovingPlatform
    {
        public override string Texture => "StarlightRiver/NPCs/Boss/OvergrowBoss/OvergrowBossPlatform";
        public override void SafeSetDefaults()
        {
            npc.width = 100;
            npc.height = 16;
        }
        public override void SafeAI()
        {
            npc.ai[0] += 0.04f;
            if (npc.ai[0] > 6.28f) npc.ai[0] = 0;
            npc.velocity += new Vector2(1, 0).RotatedBy(npc.ai[0]) * 0.1f;
        }
    }
}

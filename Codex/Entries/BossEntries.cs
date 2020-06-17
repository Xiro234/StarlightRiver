﻿using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Codex.Entries
{
    internal class CeirosEntry : CodexEntry
    {
        public CeirosEntry()
        {
            Category = Categories.Bosses;
            Title = "Ceiros";
            Body = "Ceiros is a boss. Ceiros is a cool boss. This is a codex entry for the boss named Ceiros. Cerios' title is `shattered sentinel`. Sometimes it's glass tax returns instead.";
            Hint = "Guards an ancient temple below the desert...";
            Image = GetTexture("StarlightRiver/NPCs/Boss/VitricBoss/VitricBoss2");
            Icon = GetTexture("StarlightRiver/Codex/Entries/BossIconCeiros");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Codex.Entries
{
    class CeirosEntry : CodexEntry
    {
        public CeirosEntry()
        {
            Category = Categories.Bosses;
            Title = "Ceiros";
            Body = "Ceiros is a boss. Ceiros is a cool boss. This is a codex entry for the boss named Ceiros. Cerios' title is `shattered sentinel`. Sometimes it's glass tax returns instead.";
            Image = ModContent.GetTexture("StarlightRiver/NPCs/Boss/VitricBoss/VitricBoss2");
            Icon = ModContent.GetTexture("StarlightRiver/Codex/Entries/BossIconCeiros");
        }
    }
}

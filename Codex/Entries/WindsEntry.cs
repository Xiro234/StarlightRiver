using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace StarlightRiver.Codex.Entries
{
    class WindsEntry : CodexEntry
    {
        public WindsEntry()
        {
            Category = (int)Categories.Abilities;
            Title = "Forbidden Winds";
            Body = "Wow this is such an amzing test entry\nYes dont you thing wow wow\nhahaha\nDont you just love multiline strings?";
            Image = ModContent.GetTexture("StarlightRiver/Achievements/WindsAchievementOn");
            Icon = ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wind1");
        }
    }
    class FaeEntry : CodexEntry
    {
        public FaeEntry()
        {
            Category = (int)Categories.Abilities;
            Title = "Faeflame";
            Body = "Shoop de poop loob";
            Image = ModContent.GetTexture("StarlightRiver/Achievements/WispAchievementOn");
            Icon = ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wisp1");
        }
    }
    class PureEntry : CodexEntry
    {
        public PureEntry()
        {
            Category = (int)Categories.Abilities;
            Title = "Corona of Purity";
            Body = "T\nT\nT\nT\nT\nT";
            Image = ModContent.GetTexture("StarlightRiver/Achievements/WindsAchievementOff");
            Icon = ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Purity1");
        }
    }
}

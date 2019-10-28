using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Achievements;

namespace StarlightRiver.Achievements
{
    class Achievements
    {
        public static void CallAchievements(Mod mod)
        {
            Mod achLib = ModLoader.GetMod("AchievementLib");
            if (achLib != null)
            {
                achLib.Call("AddAchievement", mod, "Stormcaller", "Unlock the forbidden winds ability, hidden deep beneath the desert sands.",
                ModContent.GetTexture("StarlightRiver/Achievements/WindsAchievementOff"), ModContent.GetTexture("StarlightRiver/Achievements/WindsAchievementOn"), AchievementCategory.Explorer);

                achLib.Call("AddAchievement", mod, "Faerie Blaze", "Unlock the faeflame ability, locked away in the surreal halls of the overgrowth.",
                ModContent.GetTexture("StarlightRiver/Achievements/WispAchievementOff"), ModContent.GetTexture("StarlightRiver/Achievements/WispAchievementOn"), AchievementCategory.Explorer);
            }
        }

        public static void QuickGive(String name, Player player)
        {
            Mod achLib = ModLoader.GetMod("AchievementLib");
            if (achLib != null)
            {
                achLib.Call("UnlockLocal", "StarlightRiver", name, player);
            }
        }
    }
}

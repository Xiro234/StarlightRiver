using System;
using Terraria;
using Terraria.Achievements;
using Terraria.ModLoader;

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

                achLib.Call("AddAchievement", mod, "Otherworldly Coronation", "Unlock the cornoa of purity ability, sealed away deep within the depths of the world...",
                ModContent.GetTexture("StarlightRiver/Achievements/WispAchievementOff"), ModContent.GetTexture("StarlightRiver/Achievements/WispAchievementOn"), AchievementCategory.Explorer);

                achLib.Call("AddAchievement", mod, "Shatterer", "Unlock the gaia's fist ability, sealed away and forgotten deep in the jungle.",
                ModContent.GetTexture("StarlightRiver/Achievements/WispAchievementOff"), ModContent.GetTexture("StarlightRiver/Achievements/WispAchievementOn"), AchievementCategory.Explorer);

                achLib.Call("AddAchievement", mod, "Living Shadow", "Unlock the ??? ability, long forgotten by any living soul.",
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

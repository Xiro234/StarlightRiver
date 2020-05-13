using Terraria.ModLoader;

namespace StarlightRiver.Codex.Entries
{
    class LoreEntry : CodexEntry
    {
        public LoreEntry()
        {
            Category = Categories.Abilities;
            Title = "Starlight Codex";
            Body = "A mysterious compendium containing lost knowledge, it seems to write itself as you travel. Click the codex icon in your inventory to view the codex.";
            Image = ModContent.GetTexture("StarlightRiver/Codex/Entries/AbilityImageLore");
            Icon = ModContent.GetTexture("StarlightRiver/GUI/Book1Closed");
        }
    }
    class WindsEntry : CodexEntry
    {
        public WindsEntry()
        {
            Category = Categories.Abilities;
            Title = "Forbidden Winds";
            Body = "A collection of strange energies found deeep within a tomb buried in the vitric desert. These 'winds' hold the power to shatter certain objects on touch and propel you forward at great speeds.";
            Image = ModContent.GetTexture("StarlightRiver/Achievements/WindsAchievementOn");
            Icon = ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wind1");
        }
    }
    class FaeEntry : CodexEntry
    {
        public FaeEntry()
        {
            Category = Categories.Abilities;
            Title = "Faeflame";
            Body = "NO TEXT";
            Image = ModContent.GetTexture("StarlightRiver/Codex/Entries/AbilityImageWisp");
            Icon = ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wisp1");
        }
    }
    class PureEntry : CodexEntry
    {
        public PureEntry()
        {
            Category = Categories.Abilities;
            Title = "Corona of Purity";
            Body = "NO TEXT";
            Image = ModContent.GetTexture("StarlightRiver/Achievements/WindsAchievementOff");
            Icon = ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Purity1");
        }
    }
    class SmashEntry : CodexEntry
    {
        public SmashEntry()
        {
            Category = Categories.Abilities;
            Title = "Gaia's Fist";
            Body = "NO TEXT";
            Image = ModContent.GetTexture("StarlightRiver/Achievements/WindsAchievementOff");
            Icon = ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Smash1");
        }
    }
    class LoreEntry2 : CodexEntry
    {
        public LoreEntry2()
        {
            Category = Categories.Abilities;
            Title = "Rift Codex";
            Body = "NO TEXT";
            Image = ModContent.GetTexture("StarlightRiver/GUI/Book2Closed");
            Icon = ModContent.GetTexture("StarlightRiver/GUI/Book2Closed");
            RequiresUpgradedBook = true;
        }
    }
    class CloakEntry : CodexEntry
    {
        public CloakEntry()
        {
            Category = Categories.Abilities;
            Title = "Zzelera's Cloak";
            Body = "NO TEXT";
            Image = ModContent.GetTexture("StarlightRiver/Achievements/WindsAchievementOff");
            Icon = ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Cloak1");
            RequiresUpgradedBook = true;
        }
    }
}

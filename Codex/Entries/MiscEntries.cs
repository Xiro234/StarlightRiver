using Terraria.ModLoader;

namespace StarlightRiver.Codex.Entries
{
    internal class StaminaEntry : CodexEntry
    {
        public StaminaEntry()
        {
            Category = Categories.Misc;
            Title = "Stamina";
            Body =
                "All of your abilities utilize stamina, represtented\n" +
                "by the orange crystals to the left of your mana bar.\n" +
                "Stamina is consumed when an ability is used, and\n" +
                "can be passively regenerated over time.";

            Image = ModContent.GetTexture("StarlightRiver/GUI/Stamina");
            Icon = ModContent.GetTexture("StarlightRiver/GUI/Stamina");
        }
    }

    internal class InfusionEntry : CodexEntry
    {
        public InfusionEntry()
        {
            Category = Categories.Misc;
            Title = "Infusions";
            Body =
                "Infusions are special upgrades which, when slotted\n" +
                "into their appropriate slots, will upgrade your\n" +
                "abilities. Each ability has 2 major infusions, which\n" +
                "grant special abilities to them.";

            Image = ModContent.GetTexture("StarlightRiver/Items/Infusions/DashAstralItem");
            Icon = ModContent.GetTexture("StarlightRiver/Items/Infusions/DashAstralItem");
        }
    }
}

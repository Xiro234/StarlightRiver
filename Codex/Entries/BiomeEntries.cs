using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Codex.Entries
{
    class VitricEntry : CodexEntry
    {
        public VitricEntry()
        {
            Category = (int)Categories.Biomes;
            Title = "Vitric Desert";
            Body = Helper.WrapString("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum", 
                500, Main.fontDeathText, 0.8f);

            Image = ModContent.GetTexture("StarlightRiver/Codex/Entries/BiomeImageVitric");
            Icon = ModContent.GetTexture("StarlightRiver/Codex/Entries/BiomeIconVitric");
        }
    }
}

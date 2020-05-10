using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Codex.Entries
{
    class VitricEntry : CodexEntry
    {
        public VitricEntry()
        {
            Category = Categories.Biomes;
            Title = "Vitric Desert";
            Body = Helper.WrapString("NO TEXT", 
                500, Main.fontDeathText, 0.8f);

            Image = ModContent.GetTexture("StarlightRiver/Codex/Entries/BiomeImageVitric");
            Icon = ModContent.GetTexture("StarlightRiver/Codex/Entries/BiomeIconVitric");
        }
    }
    class OvergrowEntry : CodexEntry
    {
        public OvergrowEntry()
        {
            Category = Categories.Biomes;
            Title = "The Overgrowth";
            Body = Helper.WrapString("NO TEXT",
                500, Main.fontDeathText, 0.8f);

            Image = ModContent.GetTexture("StarlightRiver/Codex/Entries/BiomeImageOvergrow");
            Icon = ModContent.GetTexture("StarlightRiver/Codex/Entries/BiomeIconOvergrow");
        }
    }
}

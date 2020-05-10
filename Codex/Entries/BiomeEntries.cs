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
            Body = Helper.WrapString("NO TEXT", 
                500, Main.fontDeathText, 0.8f);

            Image = ModContent.GetTexture("StarlightRiver/Codex/Entries/BiomeImageVitric");
            Icon = ModContent.GetTexture("StarlightRiver/Codex/Entries/BiomeIconVitric");
        }
    }
}

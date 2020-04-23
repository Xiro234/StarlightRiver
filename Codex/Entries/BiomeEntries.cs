using Terraria.ModLoader;

namespace StarlightRiver.Codex.Entries
{
    class VitricEntry : CodexEntry
    {
        public VitricEntry()
        {
            Category = (int)Categories.Biomes;
            Title = "Vitric Desert";
            Body = "Dual make a fucking sunlight IL\nHaha\nfor real tho please";
            Image = ModContent.GetTexture("StarlightRiver/Codex/Entries/BiomeImageVitric");
            Icon = ModContent.GetTexture("StarlightRiver/Codex/Entries/BiomeIconVitric");
        }
    }
}

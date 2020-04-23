using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Waters
{
    public class WaterJungleCorrupt : ModWaterStyle
    {
        public override bool ChooseWaterStyle()
        {
            return Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleCorrupt;
        }

        public override int ChooseWaterfallStyle()
        {
            return mod.GetWaterfallStyleSlot("WaterfallJungleCorrupt");
        }

        public override int GetSplashDust()
        {
            return mod.DustType("Corrupt");
        }

        public override int GetDropletGore()
        {
            return mod.GetGoreSlot("Gores/Ward0");
        }

        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = 0.8f;
            g = 0.65f;
            b = 1f;
        }

        public override Color BiomeHairColor()
        {
            return Color.Purple;
        }
    }

    public class WaterfallJungleCorrupt : ModWaterfallStyle { }
}
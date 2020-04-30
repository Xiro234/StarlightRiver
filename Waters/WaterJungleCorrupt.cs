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
            return mod.GetWaterfallStyleSlot<WaterfallJungleCorrupt>();
        }

        public override int GetSplashDust()
        {
            return mod.DustType("Corrupt");
        }

        public override int GetDropletGore()
        {
            return mod.GetGoreSlot("Gores/DropJungleCorrupt");
        }

        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = 0.95f;
            g = 0.75f;
            b = 0.95f;
        }

        public override Color BiomeHairColor()
        {
            return Color.DarkViolet;
        }
    }

    public class WaterfallJungleCorrupt : ModWaterfallStyle { }
}
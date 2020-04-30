using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Waters
{
    public class WaterJungleBloody : ModWaterStyle
    {
        public override bool ChooseWaterStyle()
        {
            return Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleBloody;
        }

        public override int ChooseWaterfallStyle()
        {
            return mod.GetWaterfallStyleSlot<WaterfallJungleBloody>();
        }

        public override int GetSplashDust()
        {
            return mod.DustType("Corrupt");
        }

        public override int GetDropletGore()
        {
            return mod.GetGoreSlot("Gores/DropJungleBloody");
        }

        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = 0.95f;
            g = 0.95f;
            b = 0.75f;
        }

        public override Color BiomeHairColor()
        {
            return Color.DarkRed;
        }
    }

    public class WaterfallJungleBloody : ModWaterfallStyle { }
}
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace StarlightRiver.Backgrounds
{
    public class BlankBG : ModUgBgStyle
    {
        public override bool ChooseBgStyle() => StarlightWorld.VitricBiome.Intersects(new Rectangle((int)Main.screenPosition.X / 16, (int)Main.screenPosition.Y / 16, Main.screenWidth / 16, Main.screenHeight / 16));

        public override void FillTextureArray(int[] textureSlots)
        {
            for (int k = 0; k <= 5; k++) textureSlots[k] = mod.GetBackgroundSlot("Backgrounds/Blank");
        }
    }

    public class PermafrostBG : ModUgBgStyle
    {
        public override bool ChooseBgStyle() => Main.LocalPlayer.GetModPlayer<BiomeHandler>().zonePermafrost;

        public override void FillTextureArray(int[] textureSlots)
        {
            for (int k = 0; k <= 5; k++) textureSlots[k] = mod.GetBackgroundSlot("Backgrounds/PermafrostBack");
        }
    }

    public class JungleCorruptBG : ModUgBgStyle
    {
        public override bool ChooseBgStyle() => Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleCorrupt;

        public override void FillTextureArray(int[] textureSlots)
        {
            for (int k = 0; k <= 5; k++) textureSlots[k] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
        }
    }

    public class JungleBloodyBG : ModUgBgStyle
    {
        public override bool ChooseBgStyle() => Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleBloody;

        public override void FillTextureArray(int[] textureSlots)
        {
            for (int k = 0; k <= 5; k++) textureSlots[k] = mod.GetBackgroundSlot("Backgrounds/bloodyjunglebackground");
        }
    }

    public class JungleHolyBG : ModUgBgStyle
    {
        public override bool ChooseBgStyle() => Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleHoly;

        public override void FillTextureArray(int[] textureSlots)
        {
            for (int k = 0; k <= 5; k++) textureSlots[k] = mod.GetBackgroundSlot("Backgrounds/bloodyjunglebackground");
        }
    }
}

using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Backgrounds
{
    public class BlankBG : ModUgBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneGlass;
        }
        public override void FillTextureArray(int[] textureSlots)
        {
            for (int k = 0; k <= 5; k++)
            {
                textureSlots[k] = mod.GetBackgroundSlot("Backgrounds/Blank");
            }
        }
    }

    public class JungleCorruptBG : ModUgBgStyle
	{
		public override bool ChooseBgStyle()
		{
			return Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleCorrupt;
		}

		public override void FillTextureArray(int[] textureSlots)
		{
            for (int k = 0; k <= 5; k++)
            {
                textureSlots[k] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
            }
        }
	}
    public class JungleBloodyBG : ModUgBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleBloody;
        }

        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
            textureSlots[1] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
            textureSlots[2] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
            textureSlots[3] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
            textureSlots[4] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
            textureSlots[5] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
        }
    }
    public class JungleHolyBG : ModUgBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleHoly;
        }

        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
            textureSlots[1] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
            textureSlots[2] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
            textureSlots[3] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
            textureSlots[4] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
            textureSlots[5] = mod.GetBackgroundSlot("Backgrounds/corruptjunglebackground");
        }
    }
}
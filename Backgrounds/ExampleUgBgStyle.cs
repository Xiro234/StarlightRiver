
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Backgrounds
{
	public class JungleCorruptBG : ModUgBgStyle
	{
		public override bool ChooseBgStyle()
		{
			return Main.LocalPlayer.GetModPlayer<BiomeHandler>(mod).ZoneJungleCorrupt;
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
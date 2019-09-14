
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Backgrounds
{
	public class ExampleUgBgStyle : ModUgBgStyle
	{
		public override bool ChooseBgStyle()
		{
			return Main.LocalPlayer.GetModPlayer<BiomeHandler>(mod).ZoneGlass;
		}

		public override void FillTextureArray(int[] textureSlots)
		{
			textureSlots[0] = mod.GetBackgroundSlot("Backgrounds/Glass0");
			textureSlots[1] = mod.GetBackgroundSlot("Backgrounds/Glass1");
			textureSlots[2] = mod.GetBackgroundSlot("Backgrounds/Glass2");
			textureSlots[3] = mod.GetBackgroundSlot("Backgrounds/Glass3");
            textureSlots[4] = mod.GetBackgroundSlot("Backgrounds/Glass4");
        }
	}
}
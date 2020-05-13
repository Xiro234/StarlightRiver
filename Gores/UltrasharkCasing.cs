using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Gores
{
	public class UltrasharkCasing : ModGore
	{
		public override void OnSpawn(Gore gore) {
			gore.timeLeft = 40;
		}
		public override bool Update(Gore gore)
		{
			if (gore.timeLeft <= 20)
			{
				gore.alpha += 10;
			}
			return base.Update(gore);
		}
	}
}
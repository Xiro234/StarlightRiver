using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Gores
{
    class ChainGore : ModGore
    {
        public override void OnSpawn(Gore gore)
        {
            gore.timeLeft = 180;
        }
    }
}

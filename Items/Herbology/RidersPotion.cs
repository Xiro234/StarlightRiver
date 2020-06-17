using static Terraria.ModLoader.ModContent;
using StarlightRiver.Buffs;

namespace StarlightRiver.Items.Herbology
{
    internal class RidersPotion : QuickPotion
    {
        public RidersPotion() : base("Riders Potion", "Increases crit chance while on a mount", 36000, BuffType<RidersPotionBuff>(), 2)
        {
        }
    }
}
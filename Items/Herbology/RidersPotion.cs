using StarlightRiver.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{
    class RidersPotion : QuickPotion
    {
        public RidersPotion() : base("Riders Potion", "Increases crit chance while on a mount", 36000, ModContent.BuffType<RidersPotionBuff>(), 2) { }
    }
}

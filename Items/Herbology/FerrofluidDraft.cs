using StarlightRiver.Buffs;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{
    internal class FerrofluidDraft : QuickPotion
    {
        public FerrofluidDraft() : base("Ferrofluid Draft", "Turns you into a magnet for items", 36000, ModContent.BuffType<FerrofluidDraftBuff>(), 3)
        {
        }
    }
}
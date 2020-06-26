using static Terraria.ModLoader.ModContent;
using StarlightRiver.Buffs;

namespace StarlightRiver.Items.Herbology
{
    internal class FerrofluidDraft : QuickPotion
    {
        public FerrofluidDraft() : base("Ferrofluid Draft", "Turns you into a magnet for items", 36000, BuffType<FerrofluidDraftBuff>(), 3)
        {
        }
    }
}
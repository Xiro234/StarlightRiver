using StarlightRiver.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{
    class FerrofluidDraft : QuickPotion
    {
        public FerrofluidDraft() : base("Ferrofluid Draft", "Turns you into a magnet for items", 36000, ModContent.BuffType<FerrofluidDraftBuff>(), 3) { }
    }
}
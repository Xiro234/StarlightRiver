using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles
{
    internal class OreEbony : QuickBlock
    {public OreEbony() : base(0, DustID.Stone, SoundID.Tink, new Color(80, 80, 90), ModContent.ItemType<Items.EbonyIvory.OreEbonyItem>(), true, true, "Ebony Ore"){ }}

    internal class OreIvory : QuickBlock
    {public OreIvory() : base(100, DustID.Stone, SoundID.Tink, new Color(255, 255, 220), ModContent.ItemType<Items.EbonyIvory.OreIvoryItem>(), true, true, "Ivory Ore") { }}
}
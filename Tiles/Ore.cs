using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles
{
    internal class OreEbony : ModTile

    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 0, DustID.Stone, SoundID.Tink, new Color(80, 80, 90), ItemType<Items.EbonyIvory.OreEbonyItem>(), true, true, "Ebony Ore");
        }
    }

    internal class OreIvory : ModTile

    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 100, DustID.Stone, SoundID.Tink, new Color(255, 255, 220), ItemType<Items.EbonyIvory.OreIvoryItem>(), true, true, "Ivory Ore");
        }
    }
}
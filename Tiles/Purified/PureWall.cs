using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Purified
{
    internal class WallStonePure : ModWall
    {
        public override void SetDefaults()
        {
            dustType = DustType<Dusts.Purify>();
        }
    }

    internal class WallGrassPure : ModWall
    {
        public override void SetDefaults()
        {
            dustType = DustType<Dusts.Purify>();
        }
    }
}
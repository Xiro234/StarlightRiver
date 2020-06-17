using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Purified
{
    class WallStonePure : ModWall
    {
        public override void SetDefaults()
        {
            dustType = ModContent.DustType<Dusts.Purify>();
        }
    }
    class WallGrassPure : ModWall
    {
        public override void SetDefaults()
        {
            dustType = ModContent.DustType<Dusts.Purify>();
        }
    }
}

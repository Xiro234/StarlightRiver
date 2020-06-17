using Microsoft.Xna.Framework;
using StarlightRiver.Items;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Temple
{
    internal class TempleWall : ModWall
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSetWall(this, DustID.Stone, SoundID.Dig, ItemType<TempleWallItem>(), true, new Color(20, 20, 20));
        }
    }

    internal class TempleWallItem : QuickWallItem
    {
        public TempleWallItem() : base("Ancient Temple Brick Wall", "", WallType<TempleWall>(), ItemRarityID.White)
        {
        }
    }

    internal class TempleWallBig : ModWall
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSetWall(this, DustID.Stone, SoundID.Dig, ItemType<TempleWallBigItem>(), true, new Color(20, 20, 20));
        }
    }

    internal class TempleWallBigItem : QuickWallItem
    {
        public TempleWallBigItem() : base("Large Ancient Temple Brick Wall", "", WallType<TempleWallBig>(), ItemRarityID.White)
        {
        }
    }
}
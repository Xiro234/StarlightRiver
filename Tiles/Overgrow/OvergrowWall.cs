using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Overgrow
{
    public class WallOvergrowGrass : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            dustType = mod.DustType("Gold2");
            AddMapEntry(new Color(114, 65, 37));
        }
    }
    public class WallOvergrowBrick : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            dustType = ModContent.DustType<Dusts.Stone>();
            AddMapEntry(new Color(62, 68, 55));
        }
    }
}
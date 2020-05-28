using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Vitric
{
    internal class AncientSandstone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            minPick = 1;
            AddMapEntry(new Color(150, 105, 65));

            Main.tileMerge[Type][ModContent.TileType<VitricSand>()] = true;
        }
    }

    internal class AncientSandstonePlatform : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileBlockLight[Type] = false;
            minPick = 1;
            AddMapEntry(new Color(150, 105, 65));
        }
    }

    internal class AncientSandstoneWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = false;
            dustType = ModContent.DustType<Dusts.Stone>();
            AddMapEntry(new Color(62, 68, 55));
        }
    }
}

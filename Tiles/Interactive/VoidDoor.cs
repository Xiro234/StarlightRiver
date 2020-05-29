using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Interactive
{
    internal class VoidDoorOn : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            drop = ModContent.ItemType<Items.VoidDoorItem>();
            dustType = ModContent.DustType<Dusts.Void>();
            Main.tileMerge[Type][ModContent.TileType<VoidDoorOff>()] = true;
            AddMapEntry(new Color(0, 0, 0));

            animationFrameHeight = 88;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 5)
            {
                frameCounter = 0;
                if (++frame >= 3)
                {
                    frame = 0;
                }
            }
        }
    }

    internal class VoidDoorOff : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = false;
            drop = ModContent.ItemType<Items.VoidDoorItem>();
            dustType = ModContent.DustType<Dusts.Void>();
            Main.tileMerge[Type][ModContent.TileType<VoidDoorOn>()] = true;
        }
    }
}

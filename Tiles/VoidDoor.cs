using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Ability;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles
{
    class VoidDoorOn : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            drop = mod.ItemType("VoidDoor");
            dustType = mod.DustType("Darkness");
            Main.tileMerge[Type][mod.GetTile("VoidDoorOff").Type] = true;
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
    class VoidDoorOff : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = false;
            drop = mod.ItemType("VoidDoor");
            dustType = mod.DustType("Darkness");
            Main.tileMerge[Type][mod.GetTile("VoidDoorOn").Type] = true;
        }
    }
}

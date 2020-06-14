using Microsoft.Xna.Framework;
using StarlightRiver.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Overgrow.Blocks
{
    internal class GlowBrickOvergrow : QuickBlock
    {
        public GlowBrickOvergrow() : base(210, DustID.Stone, SoundID.Tink, new Color(79, 76, 71), ModContent.ItemType<BrickOvergrowItem>(), true, true) { }
        public override void SafeSetDefaults()
        {
            Main.tileMerge[Type][mod.GetTile("GrassOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("BrickOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("LeafOvergrow").Type] = true;

            animationFrameHeight = 270;
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 5)
            {
                frameCounter = 0;
                if (++frame >= 5) frame = 0;
            }
        }
        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            frameYOffset = 270 * ((j + Main.tileFrame[type]) % 6);
        }
    }
    internal class GlowBrickOvergrowItem : QuickTileItem { public GlowBrickOvergrowItem() : base("Awoken Runic Bricks", "They have a pulse...", ModContent.TileType<GlowBrickOvergrow>(), 1) { } }
}

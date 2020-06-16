using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Items;
using StarlightRiver.Items.Vitric;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Vitric.Blocks
{
    internal class VitricBrick : ModTile
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 0, ModContent.DustType<Dusts.Air>(), SoundID.Shatter, new Color(190, 255, 245), ModContent.ItemType<VitricBrickItem>());
            TileID.Sets.DrawsWalls[Type] = true;
        }
    }

    internal class VitricBrickItem : QuickTileItem { public VitricBrickItem() : base("Vitric Brick", "", TileType<VitricBrick>(), 0) { } }
}

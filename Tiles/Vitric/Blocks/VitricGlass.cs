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
    internal class VitricGlass : ModTile
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 0, ModContent.DustType<Dusts.Air>(), SoundID.Shatter, new Color(190, 255, 245), ModContent.ItemType<VitricGlassItem>());
            TileID.Sets.DrawsWalls[Type] = true;
        }
    }

    public class VitricGlassItem : QuickTileItem { public VitricGlassItem() : base("Fuseglass", "", TileType<VitricGlass>(), 0) { } }
}
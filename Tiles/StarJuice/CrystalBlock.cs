using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.StarJuice
{
    internal class CrystalBlock : QuickBlock
    {
        public CrystalBlock() : base(0, ModContent.DustType<Dusts.Starlight>(), SoundID.Tink, new Color(150, 180, 190), ModContent.ItemType<CrystalBlockItem>(), false, false, "Star Crystal") { }
        public override void SafeSetDefaults()
        {
            TileID.Sets.DrawsWalls[Type] = true;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Dust.NewDust(new Vector2(i, j) * 16, 16, 16, ModContent.DustType<Dusts.Starlight>(), 0, 4, 0, default, 0.5f);
        }
    }
    internal class CrystalBlockItem : QuickTileItem { public CrystalBlockItem() : base("Star Crytsal", "Entrancing Crystalized Starlight...", ModContent.TileType<CrystalBlock>(), 1){} }
}
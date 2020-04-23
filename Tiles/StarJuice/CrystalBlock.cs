using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.StarJuice
{
    class CrystalBlock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            TileID.Sets.DrawsWalls[Type] = true;
            drop = mod.ItemType("OreEbonyItem");

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Star Crystal");
            AddMapEntry(new Color(150, 180, 190), name);
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Dust.NewDust(new Vector2(i, j) * 16, 16, 16, ModContent.DustType<Dusts.Starlight>(), 0, 4, 0, default, 0.5f);
        }
    }
}
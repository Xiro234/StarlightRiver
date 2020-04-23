using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Overgrow
{
    class MarkerGem : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = true;

            dustType = ModContent.DustType<Dusts.Gold>();
            AddMapEntry(new Color(255, 255, 80));
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Tile tile = Framing.GetTileSafely(i, j);

            if (Main.player.Any(n => Vector2.Distance(n.Center, new Vector2(i, j) * 16) <= 100) && tile.frameX == 0)
            {
                tile.frameX += 20;
            }
            if (tile.frameX == 20)
            {
                Lighting.AddLight(new Vector2(i, j) * 16, new Vector3(1, 1, 0.5f));
            }
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Tile tile = Framing.GetTileSafely(i, j);

            if (tile.frameX == 20)
            {
                Dust.NewDust(new Vector2(i, j) * 16, 16, 16, ModContent.DustType<Dusts.Gold2>(), 0, -1);
                Texture2D tex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/MarkerGem");
                spriteBatch.Draw(tex, (new Vector2(i, j) + Helper.TileAdj) * 16, new Rectangle(20, 0, 16, 16), Color.White);
            }
        }
    }
}

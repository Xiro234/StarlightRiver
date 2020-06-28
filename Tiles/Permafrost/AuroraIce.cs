using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using StarlightRiver.Items;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver.Tiles.Permafrost
{
    class AuroraIce : ModTile
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 0, DustID.Ice, SoundID.Tink, new Color(100, 255, 255), ItemType<AuroraIceItem>());
            Main.tileBlockLight[Type] = false;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) => false;

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            int off = i + j;

            float sin2 = (float)Math.Sin(StarlightWorld.rottime + off * 0.2f * 0.2f);
            float cos = (float)Math.Cos(StarlightWorld.rottime + off * 0.2f);
            Color color = new Color(100 * (1 + sin2) / 255f, 140 * (1 + cos) / 255f, 180 / 255f);
            float mult = 0.2f - Lighting.Brightness(i, j) * 0.2f;

            spriteBatch.Draw(Main.tileTexture[tile.type], (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition, new Rectangle(tile.frameX, tile.frameY, 16, 16), color * mult);
            Lighting.AddLight(new Vector2(i, j) * 16, color.ToVector3() * 0.1f);
        }
    }

    class AuroraIceItem : QuickTileItem
    {
        public AuroraIceItem() : base("Aurora Ice", "Oooh... Preeetttyyy", TileType<AuroraIce>(), ItemRarityID.White) { }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using StarlightRiver.Items;

namespace StarlightRiver.Tiles.Temple
{
    class JarTall : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Stamina Jar");//Map name
            AddMapEntry(new Color(204, 91, 50), name);
            dustType = ModContent.DustType<Dusts.Stamina>();
            disableSmartCursor = true;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Lighting.AddLight(new Vector2(i, j) * 16, new Vector3(1, 0.5f, 0.2f) * 0.3f);
            if (Main.rand.Next(4) == 0) Dust.NewDustPerfect(new Vector2(i + Main.rand.NextFloat(), j + Main.rand.NextFloat()) * 16, ModContent.DustType<Dusts.Stamina>(), new Vector2(0, -Main.rand.NextFloat()));
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                Texture2D tex = ModContent.GetTexture("StarlightRiver/Tiles/Temple/JarTallGlow");
                Texture2D tex2 = ModContent.GetTexture("StarlightRiver/Tiles/Temple/JarTallGlow2");
                Texture2D tex3 = ModContent.GetTexture("StarlightRiver/Keys/Glow");

                spriteBatch.End();
                spriteBatch.Begin(default, BlendState.Additive);
                spriteBatch.Draw(tex3, (Helper.TileAdj + new Vector2(i, j)) * 16 + new Vector2(16, 32) - Main.screenPosition, tex3.Frame(), Color.OrangeRed * 0.7f, 0, tex3.Size() / 2, 0.8f, 0, 0);
                spriteBatch.End();
                spriteBatch.Begin();

                spriteBatch.Draw(tex, (Helper.TileAdj + new Vector2(i, j)) * 16 - Main.screenPosition, Color.White);
                spriteBatch.Draw(tex2, (Helper.TileAdj + new Vector2(i, j)) * 16 + new Vector2(-2, 0) - Main.screenPosition, Color.White * (float)Math.Sin(LegendWorld.rottime));

            }
        }
    }
    public class JarTallItem : QuickTileItem
    {
        public JarTallItem() : base("Stamina Jar Placer (Tall)", "Places a stamina jar, for debugging.", ModContent.TileType<JarTall>(), -12) { }
        public override string Texture => "StarlightRiver/MarioCumming";
    }
}

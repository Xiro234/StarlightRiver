using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Vitric
{
    internal class SandstoneDoor : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return base.Autoload(ref name, ref texture);
        }
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.Width = 8;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0);

            TileObjectData.addTile(Type);

            minPick = 1;
            AddMapEntry(new Color(130, 85, 45));
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (LegendWorld.DesertOpen && !Main.npc.Any(n => n.type == ModContent.NPCType<NPCs.Boss.VitricBoss.VitricBoss>() && n.active))
            {
                Main.tileSolid[Type] = false;
            }
            else
            {
                Main.tileSolid[Type] = true;
            }
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                Texture2D tex = ModContent.GetTexture("StarlightRiver/Tiles/Vitric/SandstoneDoor");
                Vector2 basepos = (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition;
                int off = LegendWorld.DesertOpen ? 46 : 0;
                spriteBatch.Draw(tex, basepos + new Vector2(-off, 0), tex.Frame(), drawColor, 0, Vector2.Zero, 1, 0, 0);
                spriteBatch.Draw(tex, basepos + new Vector2(tex.Width + off, 0), tex.Frame(), drawColor, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
            }
        }
    }
}

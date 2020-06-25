using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver.Tiles.Overgrow
{
    internal class BossWindow : DummyTile
    {
        public override int DummyType => ProjectileType<BossWindowDummy>();

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Vector2 dpos = (Helper.TileAdj + new Vector2(i + 0.5f, j + 0.5f)) * 16 + Vector2.UnitY * 16 - Main.screenPosition;

            Texture2D frametex = GetTexture("StarlightRiver/Tiles/Overgrow/WindowFrame");
            Texture2D glasstex = GetTexture("StarlightRiver/Tiles/Overgrow/WindowGlass");

            spriteBatch.Draw(glasstex, dpos, glasstex.Frame(), Color.White * 0.2f, 0, glasstex.Frame().Size() / 2, 1, 0, 0); //glass
            spriteBatch.Draw(frametex, dpos, frametex.Frame(), new Color(255, 255, 200), 0, frametex.Frame().Size() / 2, 1, 0, 0); //frame
        }
    }
}
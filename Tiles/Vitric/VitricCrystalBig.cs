using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Vitric
{
    internal class VitricCrystalBig : ModWall
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }

        public override void KillWall(int i, int j, ref bool fail) => fail = true;

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.GetTexture("StarlightRiver/Tiles/Vitric/CrystalOver1");

            spriteBatch.Draw(tex, ((new Vector2(i, j) + Helper.TileAdj) * 16) - Main.screenPosition, tex.Frame(), Color.White, 0, new Vector2(80, 176), 1, 0, 0);
        }
    }

    internal class VitricCrystalCollision : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, int.MaxValue, ModContent.DustType<Dusts.Air>(), SoundID.CoinPickup, new Color(115, 182, 158), -1);
            Main.tileBlockLight[Type] = false;
        }
    }
}
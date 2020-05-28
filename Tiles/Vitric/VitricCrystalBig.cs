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
        public override void KillWall(int i, int j, ref bool fail)
        {
            fail = true;
        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            dustType = ModContent.DustType<Dusts.Air>();
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.GetTexture("StarlightRiver/Tiles/Vitric/CrystalOver1");
            spriteBatch.Draw(tex, (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition, tex.Frame(), Color.White * (0.7f + (float)Math.Sin(LegendWorld.rottime + (i * 0.06f)) * 0.2f), 0, Vector2.Zero, 1, 0, 0);
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
            Main.tileSolid[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            minPick = int.MaxValue;
            soundStyle = SoundID.CoinPickup;
            dustType = ModContent.DustType<Dusts.Air>();
            AddMapEntry(new Color(115, 182, 158));
        }
    }
}

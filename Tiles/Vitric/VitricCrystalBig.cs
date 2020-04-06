using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace StarlightRiver.Tiles.Vitric
{
    class VitricCrystalBig : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            minPick = int.MaxValue;
            dustType = ModContent.DustType<Dusts.Air>();
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Texture2D tex = ModContent.GetTexture("StarlightRiver/Tiles/Vitric/CrystalOver1");
            spriteBatch.Draw(tex, (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition, tex.Frame(), Color.White * 0.95f, 0, Vector2.Zero, 1, 0, 0);
        }
    }
    class VitricCrystalCollision : ModTile
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
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Vitric
{
    internal class DenialAura : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Vector2 center = new Vector2(i, j - 2) * 16 + Vector2.One * 8;

            for (int k = 0; k < 2; k++)
                //Dust.NewDustPerfect(center + Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(120), ModContent.DustType<Dusts.Mist>());

                if (Main.rand.Next(10) == 0)
                    Dust.NewDustPerfect(center + Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(120), ModContent.DustType<Dusts.Air>());
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Rectangle box = new Rectangle(i * 16 - 72, j * 16 - 72, 144, 144);
        }
    }
}
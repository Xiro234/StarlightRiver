using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Ability;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles
{
    class SandGlass : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;         
            drop = mod.ItemType("Sandglass");
            AddMapEntry(new Color(172, 131, 105));

            animationFrameHeight = 88;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            if (Main.rand.Next(200) == 0)
            {
                Dust.NewDustPerfect(new Vector2(i * 16, j * 16), mod.DustType("Air"), Vector2.Zero);
            }
        }
    }
}

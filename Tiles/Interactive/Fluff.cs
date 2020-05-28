using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Interactive
{
    internal class Fluff : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileSolid[Type] = false;
            Main.tileSolidTop[Type] = true;
            Main.tileBlockLight[Type] = false;
            TileID.Sets.DrawsWalls[Type] = true;

            drop = mod.ItemType("Fluff");
            dustType = mod.DustType("Air");
            AddMapEntry(new Color(115, 182, 158));
            animationFrameHeight = 88;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            foreach (Player player in Main.player)
            {
                if (player.Hitbox.Intersects(new Rectangle(i * 16, j * 16, 16, 16)))
                {
                    if (player.velocity.Y > -player.maxFallSpeed)
                    {
                        player.velocity.Y -= 0.7f;
                    }
                    else
                    {
                        player.velocity.Y = -player.maxFallSpeed;
                    }
                }
            }
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 5)
            {
                frameCounter = 0;
                if (++frame >= 3)
                {
                    frame = 0;
                }
            }
        }
        /*public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            var deathShader = GameShaders.Misc["StarlightRiver:Pass1"];
            deathShader.Apply(null);
            return true;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }*/
    }
}

using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Vitric.Blocks
{
    internal class VitricSand : ModTile
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 0, DustType<Dusts.Air>(), SoundID.Dig, new Color(172, 131, 105), ItemType<VitricSandItem>());
            Main.tileMerge[Type][TileType<VitricSpike>()] = true;
            Main.tileMerge[Type][TileType<AncientSandstone>()] = true;
            Main.tileMerge[Type][TileType<VitricSoftSand>()] = true;
            Main.tileMerge[Type][TileType<VitricMoss>()] = true;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color light = Lighting.GetColor(i, j);
            Color airColor = new Color(190, 255, 245);
            if (Main.rand.Next(200) == 0 && light.R >= 10 && light.B >= 10 && light.G >= 10)
            {
                Dust.NewDustPerfect(new Vector2(i * 16, j * 16), DustType<Dusts.Air3>(), Vector2.Zero, 0, airColor * ((light.R + light.G + light.B) / 765f));
            }
        }
    }
    internal class VitricSandItem : QuickTileItem { public VitricSandItem() : base("Glassy Sand", "", TileType<VitricSand>(), 0) { } }
}

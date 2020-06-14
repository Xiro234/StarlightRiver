using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Items.Vitric;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Vitric.Blocks
{
    internal class VitricSand : ModTile
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 0, ModContent.DustType<Dusts.Air>(), SoundID.Dig, new Color(172, 131, 105), ModContent.ItemType<VitricSandItem>());
            Main.tileMerge[Type][ModContent.TileType<VitricSpike>()] = true;
            Main.tileMerge[Type][ModContent.TileType<AncientSandstone>()] = true;
            Main.tileMerge[Type][ModContent.TileType<VitricSoftSand>()] = true;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color light = Lighting.GetColor(i, j);
            Color airColor = new Color(190, 255, 245);
            if (Main.rand.Next(200) == 0 && light.R >= 10 && light.B >= 10 && light.G >= 10)
            {
                Dust.NewDustPerfect(new Vector2(i * 16, j * 16), mod.DustType("Air3"), Vector2.Zero, 0, airColor * ((light.R + light.G + light.B) / 765f));
            }
        }
    }

    internal abstract class VitricTile : ModTile
    {
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color light = Lighting.GetColor(i, j);
            Color airColor = new Color(190, 255, 245);
            if (Main.rand.Next(40) == 0 && light.R >= 10 && light.B >= 10 && light.G >= 10)
            {
                Dust.NewDustPerfect(new Vector2(i * 16, j * 16), mod.DustType("Air3"), Vector2.Zero, 0, airColor * ((light.R + light.G + light.B) / 765f));
            }
        }
    }

    internal class VitricGlass : VitricTile
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 0, ModContent.DustType<Dusts.Air>(), SoundID.Shatter, new Color(190, 255, 245), ModContent.ItemType<VitricGlassItem>());
            TileID.Sets.DrawsWalls[Type] = true;
        }
    }

    internal class VitricGlassCrystal : VitricTile
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 0, ModContent.DustType<Dusts.Air>(), SoundID.Shatter, new Color(180, 235, 225), ModContent.ItemType<VitricGlassCrystalItem>());
            TileID.Sets.DrawsWalls[Type] = true;
        }
    }

    internal class VitricBrick : VitricTile
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 0, ModContent.DustType<Dusts.Air>(), SoundID.Shatter, new Color(190, 255, 245), ModContent.ItemType<VitricBrickItem>());
            TileID.Sets.DrawsWalls[Type] = true;
        }
    }

    internal class VitricSoftSand : ModTile
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 0, ModContent.DustType<Dusts.Air>(), SoundID.Dig, new Color(172, 131, 105), ModContent.ItemType<VitricSandItem>());
            Main.tileMerge[Type][ModContent.TileType<VitricSpike>()] = true;
            Main.tileMerge[Type][ModContent.TileType<AncientSandstone>()] = true;
            Main.tileMerge[Type][ModContent.TileType<VitricSand>()] = true;
        }
    }
}
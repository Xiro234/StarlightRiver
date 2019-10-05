using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ModLoader;
using StarlightRiver.Items.Vitric;
using StarlightRiver.Items.Debug;
using System;

namespace StarlightRiver.Tiles
{
    class VitricSand : ModTile
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
            Color light = Lighting.GetColor(i, j);
            Color airColor = new Color(190, 255, 245);
            if (Main.rand.Next(200) == 0 && light.R >= 10 && light.B >= 10 && light.G >= 10)
            {
                Dust.NewDustPerfect(new Vector2(i * 16, j * 16), mod.DustType("Air3"), Vector2.Zero, 0, airColor * ((light.R + light.G + light.B) / 765f));
            }
        }
    }

    class VitricTile : ModTile
    {
        new int Drop = 0;
        int Chance = 0;
        int Pick = 0;
        public VitricTile(int drop, int chance, int pick)
        {
            Drop = drop;
            Chance = chance;
            Pick = pick;
        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            drop = Drop;
            minPick = Pick;
            AddMapEntry(new Color(172, 131, 105));
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color light = Lighting.GetColor(i, j);
            Color airColor = new Color(190, 255, 245);
            if (Main.rand.Next(Chance) == 0 && light.R >= 10 && light.B >= 10 && light.G >= 10)
            {
                Dust.NewDustPerfect(new Vector2(i * 16, j * 16), mod.DustType("Air3"), Vector2.Zero, 0, airColor * ((light.R + light.G + light.B) / 765f));
            }
        }
    }

    internal class VitricGlass : VitricTile
    {
        public VitricGlass() : base(StarlightRiver.Instance.ItemType<Items.Vitric.Sandglass2>(), 30, 50)
        {

        }
    }
    internal class VitricGlassCrystal : VitricTile
    {
        public VitricGlassCrystal() : base(StarlightRiver.Instance.ItemType<Items.Vitric.GlassCrystalItem>(), 30, 50)
        {

        }
    }
    internal class VitricBrick : VitricTile
    {
        public VitricBrick() : base(StarlightRiver.Instance.ItemType<Items.Vitric.VitricBrickItem>(), 60, 65)
        {

        }
    }

}

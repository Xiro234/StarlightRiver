﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.StarJuice
{
    internal class CrystalBlock : ModTile
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 0, DustType<Dusts.Starlight>(), SoundID.Tink, new Color(150, 180, 190), ItemType<CrystalBlockItem>(), false, false, "Star Crystal");
            TileID.Sets.DrawsWalls[Type] = true;
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Dust.NewDust(new Vector2(i, j) * 16, 16, 16, DustType<Dusts.Starlight>(), 0, 4, 0, default, 0.5f);
        }
    }

    internal class CrystalBlockItem : QuickTileItem {
        public CrystalBlockItem() : base("Star Crytsal", "Entrancing Crystalized Starlight...", TileType<CrystalBlock>(), 1)
        {
        }
    }
}
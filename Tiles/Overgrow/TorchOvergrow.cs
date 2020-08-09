﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Overgrow
{
    internal class TorchOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileID.Sets.FramesOnKillWall[Type] = true;

            drop = mod.ItemType("TorchOvergrowItem");
            dustType = mod.DustType("Gold2");
            AddMapEntry(new Color(115, 182, 158));
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Dust.NewDustPerfect(new Vector2(4 + i * 16, 2 + j * 16), DustType<Dusts.Gold2>(), Vector2.UnitX * (float)Math.Sin(StarlightWorld.rottime));
            Lighting.AddLight(new Vector2(i * 16, j * 16), new Vector3(255, 200, 110) * 0.004f);
        }
    }
}
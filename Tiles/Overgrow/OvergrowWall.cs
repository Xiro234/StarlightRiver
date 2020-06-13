﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Overgrow
{
    public class WallOvergrowGrass : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            dustType = mod.DustType("Gold2");
            AddMapEntry(new Color(114, 65, 37));
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (i > Main.screenPosition.X / 16 && i < Main.screenPosition.X / 16 + Main.screenWidth / 16 && j > Main.screenPosition.Y / 16 && j < Main.screenPosition.Y / 16 + Main.screenHeight / 16)
            {
                Texture2D tex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/WallOvergrowGrassFlow");
                float offset = i * j % 6.28f;
                float sin = (float)Math.Sin(StarlightWorld.rottime + offset);
                spriteBatch.Draw(tex, (new Vector2(i + 0.5f, j + 0.5f) + Helper.TileAdj) * 16 + new Vector2(1, 0.5f) * sin * 1.2f - Main.screenPosition,
                    new Rectangle(i % 4 * 26, 0, 24, 24), Lighting.GetColor(i, j), offset + sin * 0.06f, new Vector2(12, 12), 1 + sin / 14f, 0, 0);
            }
        }
    }

    public class WallOvergrowBrick : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            dustType = ModContent.DustType<Dusts.Stone>();
            AddMapEntry(new Color(62, 68, 55));
        }
    }

    public class WallOvergrowInvisible : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = false;
            WallID.Sets.Transparent[Type] = true;
            dustType = ModContent.DustType<Dusts.Stone>();
            AddMapEntry(new Color(31, 34, 27));
        }
    }
}
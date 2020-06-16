﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Overgrow.Blocks
{
    internal class TallgrassOvergrow : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }
        public override void SetDefaults()
        {
            Main.tileCut[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                ModContent.TileType<GrassOvergrow>(),
                ModContent.TileType<VineOvergrow>()
            };
            TileObjectData.addTile(Type);
            soundType = SoundID.Grass;
            dustType = ModContent.DustType<Dusts.Leaf>();
            AddMapEntry(new Color(202, 157, 49));
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/TallgrassOvergrowFlow"), new Rectangle((i + (int)Helper.TileAdj.X) * 16 - (int)Main.screenPosition.X + 8,
                (j + (int)Helper.TileAdj.Y + 1) * 16 - (int)Main.screenPosition.Y + 2, 16, 16), new Rectangle(i % 2 * 16, 0, 16, 16), drawColor, (float)Math.Sin(StarlightWorld.rottime + i % 6.28f) * 0.25f,
                new Vector2(8, 16), 0, 0);
        }
    }
}

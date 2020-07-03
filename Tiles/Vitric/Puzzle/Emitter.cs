﻿using Microsoft.Xna.Framework;
using StarlightRiver.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Vitric.Puzzle
{
    class Emitter : ModTile
    {
        public override void SetDefaults() => QuickBlock.QuickSetFurniture(this, 1, 1, DustType<Dusts.Air>(), SoundID.Tink, false, new Color(0, 255, 255), false, true, "Light Emitter");

        public override bool NewRightClick(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            Vector2 velocity;

            switch (tile.frameX / 16)
            {
                case 0: velocity = new Vector2(1, 0); break;
                case 1: velocity = new Vector2(0, -1); break;
                case 2: velocity = new Vector2(-1, 0); break;
                case 3: velocity = new Vector2(0, 1); break;
                default: velocity = new Vector2(0, 0); break;
            }

            Projectile.NewProjectile(new Vector2(i + 0.5f, j + 0.5f) * 16 + velocity * 16, velocity, ProjectileType<LightBeam>(), 0, 0);
            return true;
        }

        public override bool Slope(int i, int j)
        {
            Tile tile = Main.tile[i, j];

            tile.frameX += 16;
            if (tile.frameX > 16 * 3) tile.frameX = 0;
            return false;
        }
    }

    class EmitterItem : QuickTileItem
    {
        public EmitterItem() : base("Light Emitter", "", TileType<Emitter>(), 1) { }
    }
}

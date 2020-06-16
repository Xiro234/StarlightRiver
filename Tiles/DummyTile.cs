﻿using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace StarlightRiver.Tiles
{
    internal abstract class DummyTile : ModTile
    {
        public virtual int DummyType { get; }

        public virtual bool SpawnConditions(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            return tile.frameX == 0 && tile.frameY == 0;
        }
        public virtual void SafeNearbyEffects(int i, int j, bool closer) { }
        public sealed override void NearbyEffects(int i, int j, bool closer)
        {
            if (!Main.tileFrameImportant[Type] || SpawnConditions(i, j))
            {
                if (!Main.projectile.Any(n => n.active && n.type == DummyType && n.position == new Vector2(i, j) * 16))
                {
                    Projectile p = new Projectile();
                    p.SetDefaults(DummyType);

                    Projectile.NewProjectile(new Vector2(i, j) * 16 + p.Size / 2, Vector2.Zero, DummyType, 1, 0);
                    p = null;
                }
            }
            SafeNearbyEffects(i, j, closer);
        }
    }
}

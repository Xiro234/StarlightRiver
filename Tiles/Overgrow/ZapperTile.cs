﻿using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Overgrow
{
    internal class ZapperTile : ModTile
    {
        public override void SetDefaults() { QuickBlock.QuickSetFurniture(this, 5, 2, DustID.Stone, SoundID.Tink, false, -1, new Color(100, 100, 80)); }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                if (!(Main.projectile.Any(proj => proj.modProjectile is Projectiles.Zapper && (proj.modProjectile as Projectiles.Zapper).parent == Main.tile[i, j] && proj.active)))
                {
                    int proj = Projectile.NewProjectile(new Vector2(i + 2, j + 2) * 16, Vector2.Zero, ModContent.ProjectileType<Projectiles.Zapper>(), 1, 1);
                    (Main.projectile[proj].modProjectile as Projectiles.Zapper).parent = Main.tile[i, j];
                }
            }
        }
    }
}
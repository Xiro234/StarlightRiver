using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Overgrow
{
    class HatchOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);

            dustType = ModContent.DustType<Dusts.Gold2>();
            disableSmartCursor = true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if(!Main.projectile.Any(proj => proj.active && proj.type == ModContent.ProjectileType<Projectiles.Dummies.HatchDummy>() &&
            proj.Hitbox.Intersects(new Rectangle(i * 16, j * 16, 16, 16))) && Main.tile[i, j].frameX == 18)
            {
                Projectile.NewProjectile(new Vector2(i, j) * 16, Vector2.Zero, ModContent.ProjectileType<Projectiles.Dummies.HatchDummy>(), 0, 0);
            }

            Lighting.AddLight(new Vector2(i, j + 2) * 16, new Vector3(0.6f, 0.6f, 0.5f));
        }
    }
}
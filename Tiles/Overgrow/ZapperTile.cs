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
    class ZapperTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("");//Map name
            AddMapEntry(new Color(0, 0, 0), name);
            dustType = ModContent.DustType<Dusts.Gold2>();
            disableSmartCursor = true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if(Main.tile[i,j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                if(!(Main.projectile.Any(proj => proj.modProjectile is Projectiles.Zapper && (proj.modProjectile as Projectiles.Zapper).parent == Main.tile[i,j] && proj.active)))
                {
                    int proj = Projectile.NewProjectile(new Vector2(i + 2,j + 2) * 16, Vector2.Zero, ModContent.ProjectileType<Projectiles.Zapper>(), 1, 1);
                    (Main.projectile[proj].modProjectile as Projectiles.Zapper).parent = Main.tile[i, j];
                }
            }
        }
    }
}
﻿using StarlightRiver.Tiles.Decoration;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace StarlightRiver
{
    public partial class LegendWorld : ModWorld
    {
        private void VineGen(GenerationProgress progress)
        {
            progress.Message = "Hanging vines...";
            for (int x = 30; x < Main.maxTilesX - 30; x++)
            {
                int y = AnyTree(x);
                if (y != 0)
                {
                    Point16 target = ScanTrees(x, y);
                    if (y != 0 && target != new Point16(0, 0))
                    {
                        WorldGen.PlaceTile(x, y - 1, ModContent.TileType<VineBanner>(), true, true);
                        TileEntity.PlaceEntityNet(x, y - 1, ModContent.TileEntityType<VineBannerEntity>());

                        if (TileEntity.ByPosition.ContainsKey(new Point16(x, y - 1)))
                        {
                            ((VineBannerEntity)TileEntity.ByPosition[new Point16(x, y - 1)]).Endpoint = target - new Point16(x, y - 1);
                            ((VineBannerEntity)TileEntity.ByPosition[new Point16(x, y - 1)]).Set = true;
                        }
                    }
                }
            }
        }

        private int AnyTree(int x)
        {
            int tree = 0;
            bool grass = false;
            for (int y = 0; y < Main.worldSurface; y++)
            {
                if (tree == 0 && Main.tile[x, y].type == TileID.Trees && !Main.tile[x, y - 1].active() && Main.tile[x, y + 1].active()
                    && Main.tile[x + 1, y].type != TileID.Trees && Main.tile[x - 1, y].type != TileID.Trees) tree = y;

                if (Main.tile[x, y].type == TileID.JungleGrass) grass = true;
            }
            return grass ? tree : 0;
        }

        private Point16 ScanTrees(int i, int j)
        {
            for (int x = i + 6; x < i + 20; x++)
            {
                for (int y = j - 10; y < j + 10; y++)
                {
                    if (Main.tile[x, y].type == TileID.Trees && !Main.tile[x, y - 1].active() && Main.tile[x, y + 1].active() &&
                        Main.tile[x + 1, y].type != TileID.Trees && Main.tile[x - 1, y].type != TileID.Trees) return new Point16(x, y);
                }
            }
            return new Point16(0, 0);
        }
    }
}
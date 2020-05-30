﻿using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace StarlightRiver
{
    public partial class LegendWorld : ModWorld
    {
        private void ForestHerbGen(GenerationProgress progress)
        {
            progress.Message = "Planting ivy...";
            for (int k = 60; k < Main.maxTilesX - 60; k++)
            {
                if (k > Main.maxTilesX / 3 && k < Main.maxTilesX / 3 * 2) //inner part of the world
                {
                    if (WorldGen.genRand.Next(8) == 0) //Berry Bushes
                    {
                        for (int y = 10; y < Main.worldSurface; y++)
                        {
                            if (Main.tile[k, y].type == TileID.Grass && Helper.CheckAirRectangle(new Point16(k, y - 2), new Point16(2, 2)))
                            {
                                Helper.PlaceMultitile(new Point16(k, y - 2), ModContent.TileType<Tiles.Herbology.ForestBerryBush>());
                            }
                        }
                    }
                    else if (WorldGen.genRand.Next(30) == 0)
                    {
                        for (int y = 10; y < Main.worldSurface; y++)
                        {
                            if (Main.tile[k, y].type == TileID.Grass)
                            {
                                int width = WorldGen.genRand.Next(4, 8);
                                for (int x = k; x < k + width; x++)
                                {
                                    int xRel = x - k;
                                    int xSqr = (-1 * xRel * xRel) / 8 + xRel + 1;
                                    for (int y2 = y - xSqr; y2 < y + xSqr; y2++)
                                    {
                                        WorldGen.PlaceTile(x, y2, ModContent.TileType<Tiles.Forest.Palestone>(), true, true);
                                        WorldGen.SlopeTile(x, y2);
                                        if (y2 == y - xSqr && xRel < width / 2 && WorldGen.genRand.Next(2) == 0) WorldGen.SlopeTile(x, y2, 2);
                                        if (y2 == y - xSqr && xRel > width / 2 && WorldGen.genRand.Next(2) == 0) WorldGen.SlopeTile(x, y2, 1);
                                    }
                                }
                                break;
                            }
                        }
                        k += 15;
                    }
                }

                for (int j = 15; j < Main.worldSurface; j++) //ivy
                {
                    if (WorldGen.genRand.Next(500) == 0) //a check for grass could also be here which would speed up this step
                    {
                        int size = WorldGen.genRand.Next(6, 15);

                        for (int x = k - size / 2; x < k + size / 2; x++)
                        {
                            for (int y = j - size / 2; y < j + size / 2; y++)
                            {
                                if (Main.tile[x, y].active() && Main.tile[x, y].type == TileID.Grass && Main.tile[x, y - 1].collisionType != 1 && Main.tile[x, y].slope() == 0) //!Main.tileSolid[Main.tile[x, y - 1].type] may be redundant
                                {
                                    WorldGen.PlaceTile(x, y - 1, ModContent.TileType<Tiles.Herbology.ForestIvyWild>());
                                    break;
                                }
                            }
                        }
                    }
                }

                if (WorldGen.genRand.Next(30) == 0 && AnyGrass(k))
                {
                    int size = WorldGen.genRand.Next(10, 15);
                    for (int x = 0; x < size; x++)
                    {
                        int surface = 0;
                        for (int j = 50; j < Main.worldSurface; j++) //Wall Bushes
                        {
                            if (Main.tile[k + x, j].wall != 0 && Main.tile[k, j].wall != ModContent.WallType<Tiles.Forest.LeafWall>()) { surface = j; break; }
                        }
                        int xOff = x > size / 2 ? size - x : x;
                        for (int y = surface - (xOff / 2 + WorldGen.genRand.Next(2)) - 3; true; y++)
                        {
                            WorldGen.PlaceWall(k + x, y, ModContent.WallType<Tiles.Forest.LeafWall>());
                            if (y - surface > 20 || !WorldGen.InWorld(k + x, y + 1) || Main.tile[k + x, y + 1].wall != 0) break;
                        }
                    }
                }
            }
        }

        private static bool AnyGrass(int x)
        {
            for (int y = 10; y < Main.maxTilesY; y++)
            {
                if (Main.tile[x, y].type == TileID.Grass) return true;
            }
            return false;
        }
    }
}
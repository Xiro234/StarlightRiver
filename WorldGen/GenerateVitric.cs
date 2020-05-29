using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace StarlightRiver
{
    public partial class LegendWorld : ModWorld
    {
        public static void VitricGen(GenerationProgress progress)
        {
            int vitricHeight = 140;
            Rectangle biomeTarget = new Rectangle(WorldGen.UndergroundDesertLocation.X - 80, WorldGen.UndergroundDesertLocation.Y + WorldGen.UndergroundDesertLocation.Height, WorldGen.UndergroundDesertLocation.Width + 160, vitricHeight);
            LegendWorld.VitricBiome = biomeTarget;

            for (int x = biomeTarget.X; x < biomeTarget.X + biomeTarget.Width; x++)
            {
                for (int y = biomeTarget.Y; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    tile.ClearEverything();
                }
            }

            #region Main shape
            int row = WorldGen.genRand.Next(512);
            for (int x = biomeTarget.X; x < biomeTarget.X + biomeTarget.Width; x++) //base sand + spikes
            {
                int xRel = x - (biomeTarget.X);
                int off = Helper.SamplePerlin2D(xRel, row, 10, 55);
                for (int y = biomeTarget.Y + biomeTarget.Height - off; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    int yRel = y - (biomeTarget.Y + biomeTarget.Height - off);
                    WorldGen.PlaceTile(x, y, yRel <= WorldGen.genRand.Next(1, 4) ? ModContent.TileType<Tiles.Vitric.VitricSpike>() : ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 40; x < biomeTarget.X + biomeTarget.Width / 2 + 40; x++) //flat part of center dune
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 - 40);
                for (int y = biomeTarget.Y + biomeTarget.Height - 76; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }

                if (xRel == 38)
                {
                    Helper.PlaceMultitile(new Point16(x, biomeTarget.Y + 57), ModContent.TileType<Tiles.Vitric.VitricBossAltar>());
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 70; x <= biomeTarget.X + biomeTarget.Width / 2 - 40; x++) //left side of center dune
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 - 70);
                int off = (int)(xRel * 2 - xRel * xRel / 30f);
                for (int y = biomeTarget.Y + biomeTarget.Height - off; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 + 40; x <= biomeTarget.X + biomeTarget.Width / 2 + 70; x++) //right side of center dune
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 + 40);
                int off = (int)(30 - xRel * xRel / 30f);
                for (int y = biomeTarget.Y + biomeTarget.Height - off; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
            }

            row = WorldGen.genRand.Next(512); //re-randomize the seed for perlin sampling
            for (int y = biomeTarget.Y; y < biomeTarget.Y + biomeTarget.Height; y++) //left end
            {
                int yRel = y - biomeTarget.Y;
                int off = (5 * yRel) / 6 - (5 * yRel * yRel) / 576;
                off += Helper.SamplePerlin2D(y, row, 0, 5);
                for (int x = biomeTarget.X - off / 2; x <= biomeTarget.X - off + 28; x++)
                {
                    int xRel = x - (biomeTarget.X - off + 20);
                    WorldGen.PlaceTile(x, y, xRel >= WorldGen.genRand.Next(4, 7) ? ModContent.TileType<Tiles.Vitric.VitricSpike>() : ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
            }

            row = WorldGen.genRand.Next(512); //re-randomize the seed for perlin sampling
            for (int y = biomeTarget.Y; y < biomeTarget.Y + biomeTarget.Height; y++) //right end
            {
                int yRel = y - biomeTarget.Y;
                int off = (5 * yRel) / 6 - (5 * yRel * yRel) / 576;
                off += Helper.SamplePerlin2D(y, row, 0, 5);
                for (int x = biomeTarget.X + biomeTarget.Width + off - 28; x <= biomeTarget.X + biomeTarget.Width + off / 2; x++)
                {
                    int xRel = x - (biomeTarget.X + biomeTarget.Width + off - 28);
                    WorldGen.PlaceTile(x, y, xRel <= WorldGen.genRand.Next(1, 4) ? ModContent.TileType<Tiles.Vitric.VitricSpike>() : ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
            }

            for (int x = biomeTarget.X; x < biomeTarget.X + biomeTarget.Width; x++) //ceiling
            {
                int xRel = x - biomeTarget.X;
                int amp = (int)(Math.Abs(xRel - biomeTarget.Width / 2) / (float)(biomeTarget.Width / 2) * 8);
                int off = Helper.SamplePerlin2D(xRel, row, amp, 8);
                for (int y = biomeTarget.Y; y < biomeTarget.Y + off + 4; y++)
                {
                    WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 35; x <= biomeTarget.X + biomeTarget.Width / 2 + 36; x++) //entrance hole 
            {
                for (int y = biomeTarget.Y; y < biomeTarget.Y + 20; y++)
                {
                    WorldGen.KillTile(x, y);
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 51; x <= biomeTarget.X + biomeTarget.Width / 2 + 52; x++) //sandstone cubes
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 - 51);
                if (xRel < 16 || xRel > 87)
                {
                    for (int y = biomeTarget.Y + biomeTarget.Height - 77; y < biomeTarget.Y + biomeTarget.Height - 67; y++) //bottom
                    {
                        WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.AncientSandstone>(), false, true);
                    }
                    for (int y = biomeTarget.Y - 1; y < biomeTarget.Y + 9; y++) // top
                    {
                        WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.AncientSandstone>(), false, true);
                    }
                }
            }

            for (int y = biomeTarget.Y + 9; y < biomeTarget.Y + biomeTarget.Height - 77; y++) //collision for pillars
            {
                WorldGen.PlaceTile(biomeTarget.X + biomeTarget.Width / 2 - 40, y, ModContent.TileType<Tiles.Vitric.VitricBossBarrier>(), false, false);
                WorldGen.PlaceTile(biomeTarget.X + biomeTarget.Width / 2 + 41, y, ModContent.TileType<Tiles.Vitric.VitricBossBarrier>(), false, false);
            }
            #endregion
            #region Floating islands

            WormFromIsland(VitricBiome.TopLeft().ToPoint16(), 60);

            void WormFromIsland(Point16 start, int startRad)
            {
                int width = WorldGen.genRand.Next(10, 55);
                int height = width / 3;
                float rot = WorldGen.genRand.NextFloat(6.28f);
                int rad = 0;
                int tries = 0;

                while (true)
                {
                    Point16 end = (start.ToVector2() + Vector2.One.RotatedBy(rot) * (startRad + rad)).ToPoint16();
                    if (CheckIsland(end, width, height * 2))
                    {
                        GenerateIsland(end, width, height);
                        WormFromIsland(start + new Point16(width / 2, 0), (int)(width * (1 + WorldGen.genRand.NextFloat())));
                        return;
                    }
                    else
                    {
                        rad += WorldGen.genRand.Next(10);
                        if (rad >= 50)
                        {
                            rot++;
                            rad = 0;
                            tries++;
                        }
                    }
                    if (tries >= 6)
                    {
                        width -= WorldGen.genRand.Next(5);
                        tries = 0;
                    }
                    if (width < 10)
                    {
                        return;
                    }
                }
            }

            void GenerateIsland(Point16 topLeft, int width, int height)
            {
                row = WorldGen.genRand.Next(512);
                for (int x = topLeft.X; x < topLeft.X + width; x++)
                {
                    WorldGen.PlaceTile(x, topLeft.Y, ModContent.TileType<Tiles.Vitric.VitricSand>());

                    int xRel = x - topLeft.X;
                    int off = xRel < width / 2 ? (int)(xRel / (float)(width) * height) : (int)((width - xRel) / (float)(width) * height);
                    off += Helper.SamplePerlin2D(xRel, row, 2, 4);

                    for (int y = topLeft.Y; y < topLeft.Y + off; y++)
                    {
                        WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.VitricSand>());
                    }
                }

            }

            bool CheckIsland(Point16 topLeft, int width, int height)
            {
                if (topLeft.Y < VitricBiome.TopLeft().Y + 30 || topLeft.Y > VitricBiome.TopLeft().Y + VitricBiome.Height - 60)
                {
                    return false; //padding at the top and bottom
                }

                if (topLeft.X > VitricBiome.TopLeft().X + VitricBiome.Width / 2 - 40 && topLeft.X < VitricBiome.TopLeft().X + VitricBiome.Width / 2 + 40)
                {
                    return false; //dont spawn in the boss arena
                }

                Rectangle rect = new Rectangle(topLeft.X - 5, topLeft.Y - 5, width + 10, height + 10);
                for (int x = rect.X; x < rect.X + rect.Width; x++)
                {
                    for (int y = rect.Y; y < rect.Y + rect.Height; y++)
                    {
                        if (Framing.GetTileSafely(x, y).active())
                        {
                            return false;
                        }

                        if (!VitricBiome.Contains(new Point(x, y)))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            #endregion
        }
    }
}

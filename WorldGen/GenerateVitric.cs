using Microsoft.Xna.Framework;
using StarlightRiver.Tiles.Vitric;
using StarlightRiver.Tiles.Vitric.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;
using static Terraria.WorldGen;

namespace StarlightRiver
{
    public partial class StarlightWorld : ModWorld
    {
        /*public static void VitricGen_Old(GenerationProgress progress)
        {
            int vitricHeight = 140;
            Rectangle biomeTarget = new Rectangle(UndergroundDesertLocation.X - 80, UndergroundDesertLocation.Y + UndergroundDesertLocation.Height, UndergroundDesertLocation.Width + 160, vitricHeight);

            VitricBiome = biomeTarget;

            StarlightWorld.VitricBiome = biomeTarget;

            for (int x = biomeTarget.X; x < biomeTarget.X + biomeTarget.Width; x++)
            {
                for (int y = biomeTarget.Y; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    tile.ClearEverything();
                }
            }

            #region Main shape

            int row = genRand.Next(512);
            for (int x = biomeTarget.X; x < biomeTarget.X + biomeTarget.Width; x++) //base sand + spikes
            {
                int xRel = x - (biomeTarget.X);
                int off = Helper.SamplePerlin2D(xRel, row, 10, 55);
                for (int y = biomeTarget.Y + biomeTarget.Height - off; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    int yRel = y - (biomeTarget.Y + biomeTarget.Height - off);
                    PlaceTile(x, y, yRel <= genRand.Next(1, 4) ? TileType<VitricSpike>() : TileType<VitricSand>(), false, true);
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 40; x < biomeTarget.X + biomeTarget.Width / 2 + 40; x++) //flat part of center dune
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 - 40);
                for (int y = biomeTarget.Y + biomeTarget.Height - 76; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    PlaceTile(x, y, TileType<VitricSand>(), false, true);
                }

                if (xRel == 38)
                {
                    Helper.PlaceMultitile(new Point16(x, biomeTarget.Y + 57), TileType<VitricBossAltar>());
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 70; x <= biomeTarget.X + biomeTarget.Width / 2 - 40; x++) //left side of center dune
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 - 70);
                int off = (int)(xRel * 2 - xRel * xRel / 30f);
                for (int y = biomeTarget.Y + biomeTarget.Height - off; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    PlaceTile(x, y, TileType<VitricSand>(), false, true);
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 + 40; x <= biomeTarget.X + biomeTarget.Width / 2 + 70; x++) //right side of center dune
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 + 40);
                int off = (int)(30 - xRel * xRel / 30f);
                for (int y = biomeTarget.Y + biomeTarget.Height - off; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    PlaceTile(x, y, TileType<VitricSand>(), false, true);
                }
            }

            row = genRand.Next(512); //re-randomize the seed for perlin sampling
            for (int y = biomeTarget.Y; y < biomeTarget.Y + biomeTarget.Height; y++) //left end
            {
                int yRel = y - biomeTarget.Y;
                int off = (5 * yRel) / 6 - (5 * yRel * yRel) / 576;
                off += Helper.SamplePerlin2D(y, row, 0, 5);
                for (int x = biomeTarget.X - off / 2; x <= biomeTarget.X - off + 28; x++)
                {
                    int xRel = x - (biomeTarget.X - off + 20);
                    PlaceTile(x, y, xRel >= genRand.Next(4, 7) ? TileType<VitricSpike>() : TileType<VitricSand>(), false, true);
                }
            }

            row = genRand.Next(512); //re-randomize the seed for perlin sampling
            for (int y = biomeTarget.Y; y < biomeTarget.Y + biomeTarget.Height; y++) //right end
            {
                int yRel = y - biomeTarget.Y;
                int off = (5 * yRel) / 6 - (5 * yRel * yRel) / 576;
                off += Helper.SamplePerlin2D(y, row, 0, 5);
                for (int x = biomeTarget.X + biomeTarget.Width + off - 28; x <= biomeTarget.X + biomeTarget.Width + off / 2; x++)
                {
                    int xRel = x - (biomeTarget.X + biomeTarget.Width + off - 28);
                    PlaceTile(x, y, xRel <= genRand.Next(1, 4) ? TileType<VitricSpike>() : TileType<VitricSand>(), false, true);
                }
            }

            for (int x = biomeTarget.X; x < biomeTarget.X + biomeTarget.Width; x++) //ceiling
            {
                int xRel = x - biomeTarget.X;
                int amp = (int)(Math.Abs(xRel - biomeTarget.Width / 2) / (float)(biomeTarget.Width / 2) * 8);
                int off = Helper.SamplePerlin2D(xRel, row, amp, 8);
                for (int y = biomeTarget.Y; y < biomeTarget.Y + off + 4; y++)
                {
                    PlaceTile(x, y, TileType<VitricSand>(), false, true);
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 35; x <= biomeTarget.X + biomeTarget.Width / 2 + 36; x++) //entrance hole
                for (int y = biomeTarget.Y; y < biomeTarget.Y + 20; y++)
                    KillTile(x, y);

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 51; x <= biomeTarget.X + biomeTarget.Width / 2 + 52; x++) //sandstone cubes
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 - 51);
                if (xRel < 16 || xRel > 87)
                {
                    for (int y = biomeTarget.Y + biomeTarget.Height - 77; y < biomeTarget.Y + biomeTarget.Height - 67; y++) //bottom
                    {
                        PlaceTile(x, y, TileType<AncientSandstone>(), false, true);
                    }
                    for (int y = biomeTarget.Y - 1; y < biomeTarget.Y + 9; y++) // top
                    {
                        PlaceTile(x, y, TileType<AncientSandstone>(), false, true);
                    }
                }
            }

            for (int y = biomeTarget.Y + 9; y < biomeTarget.Y + biomeTarget.Height - 77; y++) //collision for pillars
            {
                PlaceTile(biomeTarget.X + biomeTarget.Width / 2 - 40, y, TileType<VitricBossBarrier>(), false, false);
                PlaceTile(biomeTarget.X + biomeTarget.Width / 2 + 41, y, TileType<VitricBossBarrier>(), false, false);
            }

            #endregion Main shape

            #region Floating islands

            WormFromIsland(VitricBiome.TopLeft().ToPoint16(), 60);

            void WormFromIsland(Point16 start, int startRad)
            {
                int width = genRand.Next(10, 55);
                int height = width / 3;
                float rot = genRand.NextFloat(6.28f);
                int rad = 0;
                int tries = 0;

                while (true)
                {
                    Point16 end = (start.ToVector2() + Vector2.One.RotatedBy(rot) * (startRad + rad)).ToPoint16();
                    if (CheckIsland(end, width, height * 2))
                    {
                        GenerateIsland(end, width, height);
                        WormFromIsland(start + new Point16(width / 2, 0), (int)(width * (1 + genRand.NextFloat())));
                        return;
                    }
                    else
                    {
                        rad += genRand.Next(10);
                        if (rad >= 50)
                        {
                            rot++;
                            rad = 0;
                            tries++;
                        }
                    }
                    if (tries >= 6)
                    {
                        width -= genRand.Next(5);
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
                row = genRand.Next(512);
                for (int x = topLeft.X; x < topLeft.X + width; x++)
                {
                    PlaceTile(x, topLeft.Y, TileType<VitricSand>());

                    int xRel = x - topLeft.X;
                    int off = xRel < width / 2 ? (int)(xRel / (float)(width) * height) : (int)((width - xRel) / (float)(width) * height);
                    off += Helper.SamplePerlin2D(xRel, row, 2, 4);

                    for (int y = topLeft.Y; y < topLeft.Y + off; y++)
                    {
                        PlaceTile(x, y, TileType<VitricSand>());
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

            #endregion Floating islands
        }*/

        private const int VitricSlopeOffset = 48;

        public static void VitricGen(GenerationProgress progress)
        {
            int[] validGround = new int[] { TileType<VitricSand>(), TileType<VitricSoftSand>() };
            int vitricHeight = 140;
            Rectangle biomeTarget = new Rectangle(UndergroundDesertLocation.X - 80, UndergroundDesertLocation.Y + UndergroundDesertLocation.Height, UndergroundDesertLocation.Width + 160, vitricHeight);
            VitricBiome = biomeTarget; //Sets up biome information - adjusted from prior code

            for (int x = biomeTarget.X; x < biomeTarget.X + biomeTarget.Width; x++)
            {
                for (int y = biomeTarget.Y; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    tile.ClearEverything();
                }
            } //Clears whole biome

            int minCeilingDepth = (int)((biomeTarget.Y + (biomeTarget.Height / 2)) - (17f * Math.Log(VitricSlopeOffset - 8))); //Various informational variables - not to be changed
            int maxCeilingDepth = minCeilingDepth + 7;
            int minFloorDepth = (int)(biomeTarget.Y + (13f * Math.Log(VitricSlopeOffset - 8))) + (biomeTarget.Height / 2);
            int maxFloorDepth = (int)(biomeTarget.Y + (13f * Math.Log(VitricSlopeOffset - 30))) + (biomeTarget.Height / 2);

            //0 is the top of the biome, 1 is the bottom of the top layer of sand, 2 is the top of the bottom layer of sand, and 3 is the bottom of the bottom layer of sand
            int[] layers = new int[4] { biomeTarget.Y, biomeTarget.Y + biomeTarget.Height / 2, biomeTarget.Y + biomeTarget.Height / 2, biomeTarget.Y + biomeTarget.Height };
            for (int x = biomeTarget.X; x < biomeTarget.X + biomeTarget.Width; x++)
            {
                int xDif = x - biomeTarget.X;

                if (xDif < VitricSlopeOffset) //Start curve
                {
                    layers[1] = (int)((biomeTarget.Y + (biomeTarget.Height / 2)) - (17f * Math.Log(-8 + xDif)));

                    if (xDif < 10) layers[1] = biomeTarget.Y + biomeTarget.Height / 2;
                    else if (xDif < 17) layers[1] += genRand.Next(-1, 2);

                    layers[2] = (int)(biomeTarget.Y + (13f * Math.Log(-8 + xDif))) + (biomeTarget.Height / 2);

                    if (xDif < 10) layers[2] = biomeTarget.Y + biomeTarget.Height / 2;
                    else if (xDif < 17) layers[2] += genRand.Next(-1, 2);
                }
                else if (xDif == VitricSlopeOffset) //Begin flatway
                {
                    layers[1] = minCeilingDepth;
                    layers[2] = minFloorDepth;
                }
                else if (xDif > VitricSlopeOffset && xDif < biomeTarget.Width - VitricSlopeOffset) //Flatway
                {
                    if (genRand.Next(3) == 0 && x % 2 == 1)
                    {
                        if (layers[1] >= minCeilingDepth && layers[1] <= maxCeilingDepth) layers[1] += genRand.Next(-1, 2);
                        else if (layers[1] < minCeilingDepth) layers[1] += genRand.Next(2);
                        else if (layers[1] > maxCeilingDepth || biomeTarget.Width - VitricSlopeOffset - 30 < xDif) layers[1] += genRand.Next(-1, 1);
                    }

                    if (genRand.Next(3) == 0 && x % 2 == 1)
                    {
                        //IMPLEMENT FASTNOISE
                    }
                }
                else //End curve
                {
                    int adjXDif = (biomeTarget.Width - xDif);
                    layers[1] = (int)((biomeTarget.Y + (biomeTarget.Height / 2)) - (17f * Math.Log(-8 + adjXDif)));

                    if (xDif > biomeTarget.Width - 10) layers[1] = biomeTarget.Y + biomeTarget.Height / 2;
                    else if (xDif > biomeTarget.Width - 17) layers[1] += genRand.Next(-1, 2);

                    layers[2] = (int)(biomeTarget.Y + (13f * Math.Log(-8 + adjXDif))) + (biomeTarget.Height / 2);

                    if (xDif < 10) layers[2] = biomeTarget.Y + biomeTarget.Height / 2;
                    else if (xDif < 17) layers[2] += genRand.Next(-1, 2);
                }

                if (layers[1] > biomeTarget.Y + biomeTarget.Height / 2) layers[1] = biomeTarget.Y + biomeTarget.Height / 2;
                if (layers[2] < biomeTarget.Y + biomeTarget.Height / 2) layers[2] = biomeTarget.Y + biomeTarget.Height / 2;

                for (int y = layers[0]; y < layers[1]; ++y)
                    PlaceTile(x, y, TileType<VitricSand>(), false, true);

                int spikeOff = genRand.Next(2);
                for (int y = layers[2] - (9 - spikeOff); y < layers[3]; ++y)
                {
                    bool validSpike = y < layers[2] && y >= (biomeTarget.Y + (biomeTarget.Height / 2f));
                    PlaceTile(x, y, validSpike ? TileType<VitricSpike>() : TileType<VitricSand>(), false, true);
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 40; x < biomeTarget.X + biomeTarget.Width / 2 + 40; x++) //Flat part of the centre - Ceiros's Arena
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 - 40);
                for (int y = biomeTarget.Y + biomeTarget.Height - 76; y < biomeTarget.Y + biomeTarget.Height; y++) PlaceTile(x, y, TileType<VitricSand>(), false, true);

                if (xRel == 38) Helper.PlaceMultitile(new Point16(x, biomeTarget.Y + 57), TileType<VitricBossAltar>());
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 35; x <= biomeTarget.X + biomeTarget.Width / 2 + 36; x++) //Entrance from Desert
                for (int y = biomeTarget.Y; y < biomeTarget.Y + 20; y++) KillTile(x, y);

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 51; x <= biomeTarget.X + biomeTarget.Width / 2 + 52; x++) //Sandstone Cubes (Pillar Ground)
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 - 51);
                if (xRel < 16 || xRel > 87)
                {
                    for (int y = biomeTarget.Y + biomeTarget.Height - 77; y < biomeTarget.Y + biomeTarget.Height - 67; y++) PlaceTile(x, y, TileType<AncientSandstone>(), false, true);
                    for (int y = biomeTarget.Y - 1; y < biomeTarget.Y + 9; y++) PlaceTile(x, y, TileType<AncientSandstone>(), false, true);
                }
            } //Adjusted from prior code

            List<Point> islands = new List<Point>(); //List for island positions
            for (int i = 0; i < 12; ++i)
            {
                int x;
                int y;
                bool repeat = false;

                do
                {
                    x = biomeTarget.X + (int)(VitricSlopeOffset * 0.8f) + genRand.Next((int)(biomeTarget.Width / 2.7f));
                    if (genRand.Next(2) == 0) x += (int)(biomeTarget.Width / 2f);

                    y = (maxCeilingDepth + 18) + (genRand.Next((int)(biomeTarget.Height / 3f)));

                    if (islands.Any(v => Vector2.Distance(new Vector2(x, y), v.ToVector2()) < 32) || (x > biomeTarget.X + biomeTarget.Width / 2 - 71 && x < biomeTarget.X + biomeTarget.Width / 2 + 70))
                        repeat = true;
                    else repeat = false;
                }
                while (repeat); //Gets a valid island position

                islands.Add(new Point(x, y));
                CreateIsland(x, y); //Adds island pos to list and places island
            }

            for (int i = biomeTarget.X + VitricSlopeOffset; i < biomeTarget.X + (biomeTarget.Width - VitricSlopeOffset); ++i) //Add large crystals
            {
                for (int j = biomeTarget.Y + 2; j < biomeTarget.Y + biomeTarget.Height - 2; ++j)
                {
                    if (i < biomeTarget.X + biomeTarget.Width / 2 - 71 || i > biomeTarget.X + biomeTarget.Width / 2 + 70)
                    {
                        if (validGround.Any(x => x == Main.tile[i, j + 1].type) && validGround.Any(x => x == Main.tile[i + 9, j + 1].type) && Helper.CheckAirRectangle(new Point16(i, j - 19), new Point16(10, 19)))
                        {
                            StructureHelper.StructureHelper.GenerateStructure("Structures/LargeVitricCrystal", new Point16(i + 5, (j + genRand.Next(2)) - 17), StarlightRiver.Instance);
                            i += 10;
                        }
                    }
                }
            }

            for (int i = biomeTarget.X + 5; i < biomeTarget.X + (biomeTarget.Width - 5); ++i) //Add vines & decor
            {
                for (int j = biomeTarget.Y; j < biomeTarget.Y + biomeTarget.Height - 10; ++j)
                {
                    if (genRand.Next(8) == 0 && validGround.Any(x => x == Main.tile[i, j].type) && !Main.tile[i, j + 1].active())
                    {
                        int targSize = genRand.Next(4, 23);
                        for (int k = 1; k < targSize; ++k)
                        {
                            if (Main.tile[i, j + k].active()) break;

                            PlaceTile(i, j + k, TileType<VitricVine>());
                        }
                    }
                    else
                    {
                        int type = genRand.Next(7); //0 = 1x1 tile, 1 = 2x2 tile, 2 = 2x3 tile, 3+ is empty
                        if (type == 0)
                        {
                            if (validGround.Any(x => x == Main.tile[i, j].type) && Helper.CheckAirRectangle(new Point16(i, j - 1), new Point16(1, 1)))
                                PlaceTile(i, j - 1, genRand.Next(2) == 0 ? TileType<VitricSmallCactus>() : TileType<VitricRock>());
                        }
                        else if (type == 1)
                        {
                            if (validGround.Any(x => x == Main.tile[i, j].type) && Helper.CheckAirRectangle(new Point16(i, j - 2), new Point16(2, 2)) && validGround.Any(x => x == Main.tile[i + 1, j].type))
                                PlaceTile(i, j - 2, genRand.Next(2) == 0 ? TileType<VitricRoundCactus>() : TileType<VitricDecor>());
                        }
                        else if (type == 2)
                        {
                            bool vGround = true;
                            for (int k = 0; k < 3; ++k)
                                if (!Main.tile[i + k, j].active() || !validGround.Any(x => x == Main.tile[i + k, j].type)) vGround = false;

                            if (vGround && Helper.CheckAirRectangle(new Point16(i, j - 2), new Point16(3, 2))) PlaceTile(i, j - 2, TileType<VitricDecorLarge>());
                        }
                    }
                }
            }

            for (int y = biomeTarget.Y + 9; y < biomeTarget.Y + biomeTarget.Height - 77; y++) //collision for pillars
            {
                PlaceTile(biomeTarget.X + biomeTarget.Width / 2 - 40, y, TileType<VitricBossBarrier>(), false, false);
                PlaceTile(biomeTarget.X + biomeTarget.Width / 2 + 41, y, TileType<VitricBossBarrier>(), false, false);
            }
        }

        private static void CreateIsland(int rX, int rY)
        {
            int wid = genRand.Next(26, 36);
            int top = 5;
            int depth = 2;

            bool peak = false;
            int peakEnd = 0;
            int peakStart = 0;
            int offset = 0;

            for (int i = rX - (int)(wid / 2f); i < rX + (wid / 2f); ++i)
            {
                if (i == rX - (int)(wid / 2f) + 1) top++;
                else if (i == (rX + (int)(wid / 2f)) - 1) top--;

                if (!peak)
                {
                    if (depth <= 2) depth += genRand.Next(2);
                    else depth += genRand.Next(-1, 2);

                    if (genRand.Next(3) == 0)
                    {
                        peak = true;
                        peakStart = i;
                        peakEnd = i + genRand.Next(3, 8);
                        if (peakEnd > (rX + (wid / 2f)) - 1) peakEnd = (int)(rX + (wid / 2f)) - 1;
                    }
                }
                else
                {
                    int dist = peakEnd - i;
                    int dif = peakEnd - peakStart;
                    int deep = (7 - dif);

                    if (dist > (int)(dif / 2f)) depth += genRand.Next(deep, deep + 2);
                    else depth -= genRand.Next(deep, deep + 2);

                    if (rX >= peakEnd) peak = false;
                }

                if (i % 4 == 0)
                {
                    if (i < rX) top += genRand.Next(2);
                    else top -= genRand.Next(2);
                }

                if (i % 8 == 2) offset += genRand.Next(-1, 2);

                if (top < 3) top = 3;

                for (int j = rY - top + offset; j < rY + depth + offset; j++)
                {
                    PlaceTile(i, j, j > (rY + depth + offset) - 4 ? TileType<VitricSand>() : TileType<VitricSoftSand>(), false, true);
                }
            }
        }
    }
}
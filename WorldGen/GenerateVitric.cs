using static Terraria.ModLoader.ModContent;
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
using static Terraria.WorldGen;
using StarlightRiver.Noise;

namespace StarlightRiver
{
    public partial class StarlightWorld : ModWorld
    {
        /*public static void VitricGen_Old(GenerationProgress progress)
        {
            int vitricHeight = 140;
            Rectangle VitricBiome = new Rectangle(UndergroundDesertLocation.X - 80, UndergroundDesertLocation.Y + UndergroundDesertLocation.Height, UndergroundDesertLocation.Width + 160, vitricHeight);
            VitricBiome = VitricBiome;
            StarlightWorld.VitricBiome = VitricBiome;

            for (int x = VitricBiome.X; x < VitricBiome.X + VitricBiome.Width; x++)
            {
                for (int y = VitricBiome.Y; y < VitricBiome.Y + VitricBiome.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    tile.ClearEverything();
                }
            }

            #region Main shape
            int row = genRand.Next(512);
            for (int x = VitricBiome.X; x < VitricBiome.X + VitricBiome.Width; x++) //base sand + spikes
            {
                int xRel = x - (VitricBiome.X);
                int off = Helper.SamplePerlin2D(xRel, row, 10, 55);
                for (int y = VitricBiome.Y + VitricBiome.Height - off; y < VitricBiome.Y + VitricBiome.Height; y++)
                {
                    int yRel = y - (VitricBiome.Y + VitricBiome.Height - off);
                    PlaceTile(x, y, yRel <= genRand.Next(1, 4) ? TileType<VitricSpike>() : TileType<VitricSand>(), false, true);
                }
            }

            for (int x = VitricBiome.X + VitricBiome.Width / 2 - 40; x < VitricBiome.X + VitricBiome.Width / 2 + 40; x++) //flat part of center dune
            {
                int xRel = x - (VitricBiome.X + VitricBiome.Width / 2 - 40);
                for (int y = VitricBiome.Y + VitricBiome.Height - 76; y < VitricBiome.Y + VitricBiome.Height; y++)
                {
                    PlaceTile(x, y, TileType<VitricSand>(), false, true);
                }

                if (xRel == 38)
                {
                    Helper.PlaceMultitile(new Point16(x, VitricBiome.Y + 57), TileType<VitricBossAltar>());
                }
            }

            for (int x = VitricBiome.X + VitricBiome.Width / 2 - 70; x <= VitricBiome.X + VitricBiome.Width / 2 - 40; x++) //left side of center dune
            {
                int xRel = x - (VitricBiome.X + VitricBiome.Width / 2 - 70);
                int off = (int)(xRel * 2 - xRel * xRel / 30f);
                for (int y = VitricBiome.Y + VitricBiome.Height - off; y < VitricBiome.Y + VitricBiome.Height; y++)
                {
                    PlaceTile(x, y, TileType<VitricSand>(), false, true);
                }
            }

            for (int x = VitricBiome.X + VitricBiome.Width / 2 + 40; x <= VitricBiome.X + VitricBiome.Width / 2 + 70; x++) //right side of center dune
            {
                int xRel = x - (VitricBiome.X + VitricBiome.Width / 2 + 40);
                int off = (int)(30 - xRel * xRel / 30f);
                for (int y = VitricBiome.Y + VitricBiome.Height - off; y < VitricBiome.Y + VitricBiome.Height; y++)
                {
                    PlaceTile(x, y, TileType<VitricSand>(), false, true);
                }
            }

            row = genRand.Next(512); //re-randomize the seed for perlin sampling
            for (int y = VitricBiome.Y; y < VitricBiome.Y + VitricBiome.Height; y++) //left end
            {
                int yRel = y - VitricBiome.Y;
                int off = (5 * yRel) / 6 - (5 * yRel * yRel) / 576;
                off += Helper.SamplePerlin2D(y, row, 0, 5);
                for (int x = VitricBiome.X - off / 2; x <= VitricBiome.X - off + 28; x++)
                {
                    int xRel = x - (VitricBiome.X - off + 20);
                    PlaceTile(x, y, xRel >= genRand.Next(4, 7) ? TileType<VitricSpike>() : TileType<VitricSand>(), false, true);
                }
            }

            row = genRand.Next(512); //re-randomize the seed for perlin sampling
            for (int y = VitricBiome.Y; y < VitricBiome.Y + VitricBiome.Height; y++) //right end
            {
                int yRel = y - VitricBiome.Y;
                int off = (5 * yRel) / 6 - (5 * yRel * yRel) / 576;
                off += Helper.SamplePerlin2D(y, row, 0, 5);
                for (int x = VitricBiome.X + VitricBiome.Width + off - 28; x <= VitricBiome.X + VitricBiome.Width + off / 2; x++)
                {
                    int xRel = x - (VitricBiome.X + VitricBiome.Width + off - 28);
                    PlaceTile(x, y, xRel <= genRand.Next(1, 4) ? TileType<VitricSpike>() : TileType<VitricSand>(), false, true);
                }
            }

            for (int x = VitricBiome.X; x < VitricBiome.X + VitricBiome.Width; x++) //ceiling
            {
                int xRel = x - VitricBiome.X;
                int amp = (int)(Math.Abs(xRel - VitricBiome.Width / 2) / (float)(VitricBiome.Width / 2) * 8);
                int off = Helper.SamplePerlin2D(xRel, row, amp, 8);
                for (int y = VitricBiome.Y; y < VitricBiome.Y + off + 4; y++)
                {
                    PlaceTile(x, y, TileType<VitricSand>(), false, true);
                }
            }

            for (int x = VitricBiome.X + VitricBiome.Width / 2 - 35; x <= VitricBiome.X + VitricBiome.Width / 2 + 36; x++) //entrance hole 
                for (int y = VitricBiome.Y; y < VitricBiome.Y + 20; y++)
                    KillTile(x, y);

            for (int x = VitricBiome.X + VitricBiome.Width / 2 - 51; x <= VitricBiome.X + VitricBiome.Width / 2 + 52; x++) //sandstone cubes
            {
                int xRel = x - (VitricBiome.X + VitricBiome.Width / 2 - 51);
                if (xRel < 16 || xRel > 87)
                {
                    for (int y = VitricBiome.Y + VitricBiome.Height - 77; y < VitricBiome.Y + VitricBiome.Height - 67; y++) //bottom
                    {
                        PlaceTile(x, y, TileType<AncientSandstone>(), false, true);
                    }
                    for (int y = VitricBiome.Y - 1; y < VitricBiome.Y + 9; y++) // top
                    {
                        PlaceTile(x, y, TileType<AncientSandstone>(), false, true);
                    }
                }
            }

            for (int y = VitricBiome.Y + 9; y < VitricBiome.Y + VitricBiome.Height - 77; y++) //collision for pillars
            {
                PlaceTile(VitricBiome.X + VitricBiome.Width / 2 - 40, y, TileType<VitricBossBarrier>(), false, false);
                PlaceTile(VitricBiome.X + VitricBiome.Width / 2 + 41, y, TileType<VitricBossBarrier>(), false, false);
            }
            #endregion
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
            #endregion
        }*/

        public static FastNoise genNoise = new FastNoise(_genRandSeed);
        private const int VitricSlopeOffset = 48;
        private const float VitricNoiseHeight = 10f;

        public static List<Point> VitricIslandLocations { get; private set; }

        /// <summary>Generates the Vitric Desert under the Underground Desert.</summary>
        /// <param name="progress"></param>
        public static void VitricGen(GenerationProgress progress)
        {
            int[] validGround = new int[] { TileType<VitricSand>(), TileType<VitricSoftSand>() };
            int vitricHeight = 140;
            //Basic biome information
            VitricBiome = new Rectangle(UndergroundDesertLocation.X - 80, UndergroundDesertLocation.Y + UndergroundDesertLocation.Height, UndergroundDesertLocation.Width + 150, vitricHeight);

            int minCeilingDepth = (int)((VitricBiome.Y + (VitricBiome.Height / 2)) - (17f * Math.Log(VitricSlopeOffset - 8))); //Various informational variables - not to be changed
            int maxCeilingDepth = minCeilingDepth + 7;
            int minFloorDepth = (int)(VitricBiome.Y + (13f * Math.Log(VitricSlopeOffset - 8))) + (VitricBiome.Height / 2);

            GenerateBase(minCeilingDepth, maxCeilingDepth, minFloorDepth);

            for (int x = VitricBiome.X + VitricBiome.Width / 2 - 40; x < VitricBiome.X + VitricBiome.Width / 2 + 40; x++) //Flat part of the centre - Ceiros's Arena
            {
                int xRel = x - (VitricBiome.X + VitricBiome.Width / 2 - 40);
                for (int y = VitricBiome.Y + VitricBiome.Height - 76; y < VitricBiome.Y + VitricBiome.Height; y++) PlaceTile(x, y, TileType<VitricSand>(), false, true);

                if (xRel == 38) Helper.PlaceMultitile(new Point16(x, VitricBiome.Y + 57), TileType<VitricBossAltar>());
            }

            for (int x = VitricBiome.X + VitricBiome.Width / 2 - 35; x <= VitricBiome.X + VitricBiome.Width / 2 + 36; x++) //Entrance from Desert 
                for (int y = VitricBiome.Y; y < VitricBiome.Y + 20; y++) KillTile(x, y);

            for (int x = VitricBiome.X + VitricBiome.Width / 2 - 51; x <= VitricBiome.X + VitricBiome.Width / 2 + 52; x++) //Sandstone Cubes (Pillar Ground)
            {
                int xRel = x - (VitricBiome.X + VitricBiome.Width / 2 - 51);
                if (xRel < 16 || xRel > 87)
                {
                    for (int y = VitricBiome.Y + VitricBiome.Height - 77; y < VitricBiome.Y + VitricBiome.Height - 67; y++) PlaceTile(x, y, TileType<AncientSandstone>(), false, true);
                    for (int y = VitricBiome.Y - 1; y < VitricBiome.Y + 9; y++) PlaceTile(x, y, TileType<AncientSandstone>(), false, true);
                }
            } //Adjusted from prior code

            List<Point> islands = new List<Point>(); //List for island positions
            for (int i = 0; i < 11; ++i)
            {
                int x;
                int y;
                bool repeat = false;

                do
                {
                    x = VitricBiome.X + (int)(VitricSlopeOffset * 0.8f) + genRand.Next((int)(VitricBiome.Width / 2.7f));
                    if (genRand.Next(2) == 0) x += (int)(VitricBiome.Width / 2f);

                    y = (maxCeilingDepth + 18) + (genRand.Next((int)(VitricBiome.Height / 3.2f)));

                    if (islands.Any(v => Vector2.Distance(new Vector2(x, y), v.ToVector2()) < 36) || (x > VitricBiome.X + VitricBiome.Width / 2 - 71 && x < VitricBiome.X + VitricBiome.Width / 2 + 70))
                        repeat = true;
                    else repeat = false;
                }
                while (repeat); //Gets a valid island position

                islands.Add(new Point(x, y));
                CreateIsland(x, y); //Adds island pos to list and places island
            }
            VitricIslandLocations = islands;

            for (int i = 0; i < 6; ++i) //Mini islands
            {
                int x = VitricBiome.X + (int)(VitricSlopeOffset * 0.8f) + genRand.Next((int)(VitricBiome.Width / 2.7f));
                if (genRand.Next(2) == 0) x += (int)(VitricBiome.Width / 2f);
                int y = (maxCeilingDepth + 20) + (genRand.Next((int)(VitricBiome.Height / 3.2f)));
                if (Helper.ScanForTypeDown(x, y, TileType<VitricSpike>(), 120))
                {
                    for (int k = 0; k < 120; k++)
                    {
                        if (Main.tile[x, y + k].active() && Main.tile[x, y + k].type == TileType<VitricSpike>())
                        {
                            y += k;
                            break;
                        }
                    }
                }
                else
                {
                    i--;
                    continue;
                }

                MiniIsland(x, y + 6);
            }

            for (int i = VitricBiome.X + VitricSlopeOffset; i < VitricBiome.X + (VitricBiome.Width - VitricSlopeOffset); ++i) //Add large crystals
            {
                for (int j = VitricBiome.Y + 2; j < VitricBiome.Y + VitricBiome.Height - 2; ++j)
                {
                    if (i < VitricBiome.X + VitricBiome.Width / 2 - 71 || i > VitricBiome.X + VitricBiome.Width / 2 + 70)
                    {
                        if (validGround.Any(x => x == Main.tile[i - 1, j + 1].type) && validGround.Any(x => x == Main.tile[i + 10, j + 1].type) && Helper.CheckAirRectangle(new Point16(i, j - 19), new Point16(10, 19)))
                        {
                            StructureHelper.StructureHelper.GenerateStructure("Structures/LargeVitricCrystal", new Point16(i + 5, (j + genRand.Next(2)) - 17), StarlightRiver.Instance);
                            i += 10;
                        }
                    }
                }
            }

            GenConsistentMiniIslands();
            GenDecoration(validGround);

            for (int y = VitricBiome.Y + 9; y < VitricBiome.Y + VitricBiome.Height - 77; y++) //collision for pillars
            {
                PlaceTile(VitricBiome.X + VitricBiome.Width / 2 - 40, y, TileType<VitricBossBarrier>(), false, false);
                PlaceTile(VitricBiome.X + VitricBiome.Width / 2 + 41, y, TileType<VitricBossBarrier>(), false, false);
            }
            GenMoss();
        }

        /// <summary>Generates basic biome shape, such as curved walls, noise on floor and ceiling, and spikes on the bottom.</summary>
        /// <seealso cref="https://github.com/Auburns/FastNoise_CSharp"/>
        private static void GenerateBase(int minCeilingDepth, int maxCeilingDepth, int minFloorDepth)
        {
            genNoise.Seed = _genRandSeed;
            genNoise.NoiseType = FastNoise.NoiseTypes.SimplexFractal; //Sets noise to proper type
            genNoise.FractalGain = 0.01f;
            genNoise.Frequency = 0.005f;
            float[] heights = new float[VitricBiome.Width]; //2D heightmap to create terrain

            float leftCurveConst = 13f - ((0.3f + heights[0]) * VitricNoiseHeight); //For curving down into the noise properly, left side
            float rightCurveConst = 13f - ((0.3f + heights[heights.Length - 1]) * VitricNoiseHeight); //Right side

            for (int x = 0; x < heights.Length; x++)
                heights[x] = genNoise.GetNoise(x, 0);

            //Controls Y location of the top, ceiling, floor and bottom of the biome
            Dictionary<string, int> layers = new Dictionary<string, int> { { "TOP", 0 }, { "CEILING", 0 }, { "FLOOR", 0 }, { "BOTTOM", 0 } };

            for (int x = VitricBiome.X; x < VitricBiome.X + VitricBiome.Width; x++) //Basic biome shape
            {
                int xDif = x - VitricBiome.X;

                if (xDif < VitricSlopeOffset) //Start curve
                {
                    layers["CEILING"] = (int)((VitricBiome.Y + (VitricBiome.Height / 2)) - (17f * Math.Log(-8 + xDif))); //17f is the constant that goes to the roof
                    layers["TOP"] = (int)((VitricBiome.Y + (VitricBiome.Height / 2)) - (17f * Math.Log(-8 + (xDif + 12)))) - 8;

                    if (xDif < 10) layers["CEILING"] = VitricBiome.Y + VitricBiome.Height / 2;
                    else if (xDif < 17) layers["CEILING"] += genRand.Next(-1, 2);

                    if (layers["TOP"] < VitricBiome.Y) layers["TOP"] = VitricBiome.Y; //Caps off top

                    layers["FLOOR"] = (int)(VitricBiome.Y + (leftCurveConst * Math.Log(-8 + xDif))) + (VitricBiome.Height / 2); //Curves down towards floor
                    layers["BOTTOM"] = (int)(VitricBiome.Y + (leftCurveConst * Math.Log(-8 + (xDif + 12)))) + (VitricBiome.Height / 2) + 23;

                    if (xDif < 10) layers["FLOOR"] = VitricBiome.Y + VitricBiome.Height / 2;
                    else if (xDif < 17) layers["FLOOR"] += genRand.Next(-1, 2);

                    if (layers["BOTTOM"] > VitricBiome.Y + VitricBiome.Height) layers["BOTTOM"] = VitricBiome.Y + VitricBiome.Height; //Caps off bottom
                }
                else if (xDif == VitricSlopeOffset) //Begin flatway
                {
                    layers["CEILING"] = minCeilingDepth;
                    //layers["FLOOR"] = minFloorDepth;
                }
                else if (xDif > VitricSlopeOffset && xDif < VitricBiome.Width - VitricSlopeOffset) //Flatway
                {
                    if (genRand.Next(3) == 0 && x % 2 == 1)
                    {
                        if (layers["CEILING"] >= minCeilingDepth && layers["CEILING"] <= maxCeilingDepth) layers["CEILING"] += genRand.Next(-1, 2);
                        else if (layers["CEILING"] < minCeilingDepth) layers["CEILING"] += genRand.Next(2);
                        else if (layers["CEILING"] > maxCeilingDepth || VitricBiome.Width - VitricSlopeOffset - 30 < xDif) layers["CEILING"] += genRand.Next(-1, 1);
                    }

                    if (genRand.Next(3) == 0 && x % 2 == 1)
                    {
                        layers["FLOOR"] = (int)Math.Floor(minFloorDepth - ((0.3f + heights[x - VitricBiome.X]) * VitricNoiseHeight));
                    }
                }
                else //End curve
                {
                    int adjXDif = (VitricBiome.Width - xDif);
                    layers["CEILING"] = (int)((VitricBiome.Y + (VitricBiome.Height / 2)) - (17f * Math.Log(-8 + adjXDif)));
                    layers["TOP"] = (int)((VitricBiome.Y + (VitricBiome.Height / 2)) - (17f * Math.Log(-8 + (adjXDif + 12)))) - 8;

                    if (layers["TOP"] < VitricBiome.Y) layers["TOP"] = VitricBiome.Y; //Caps off top

                    if (xDif > VitricBiome.Width - 10) layers["CEILING"] = VitricBiome.Y + VitricBiome.Height / 2;
                    else if (xDif > VitricBiome.Width - 17) layers["CEILING"] += genRand.Next(-1, 2);

                    layers["FLOOR"] = (int)(VitricBiome.Y + (rightCurveConst * Math.Log(-8 + adjXDif))) + (VitricBiome.Height / 2);
                    layers["BOTTOM"] = (int)(VitricBiome.Y + (rightCurveConst * Math.Log(-8 + (adjXDif + 12)))) + (VitricBiome.Height / 2) + 23;

                    if (xDif < 10) layers["FLOOR"] = VitricBiome.Y + VitricBiome.Height / 2;
                    else if (xDif < 17) layers["FLOOR"] += genRand.Next(-1, 2);

                    if (layers["BOTTOM"] > VitricBiome.Y + VitricBiome.Height) layers["BOTTOM"] = VitricBiome.Y + VitricBiome.Height; //Caps off bottom
                }

                if (layers["CEILING"] > VitricBiome.Y + VitricBiome.Height / 2) layers["CEILING"] = VitricBiome.Y + VitricBiome.Height / 2;
                if (layers["FLOOR"] < VitricBiome.Y + VitricBiome.Height / 2) layers["FLOOR"] = VitricBiome.Y + VitricBiome.Height / 2;

                for (int y = layers["CEILING"]; y < layers["FLOOR"]; ++y) //Dig out cave
                    Framing.GetTileSafely(x, y).ClearEverything();

                for (int y = layers["TOP"]; y < layers["CEILING"]; ++y)
                {
                    PlaceTile(x, y, TileType<VitricSand>(), false, true);
                    Main.tile[x, y].slope(0);
                    KillWall(x, y, false);
                }

                int spikeOff = genRand.Next(2);
                for (int y = layers["FLOOR"] - (9 - spikeOff); y < layers["BOTTOM"]; ++y)
                {
                    bool validSpike = y < layers["FLOOR"] && y >= (VitricBiome.Y + (VitricBiome.Height / 2f));
                    PlaceTile(x, y, validSpike ? TileType<VitricSpike>() : TileType<VitricSand>(), false, true);
                    Main.tile[x, y].slope(0);
                    KillWall(x, y, false);
                }
            }
        }

        /// <summary>Gens decor of every type throughout the biome</summary>
        /// <param name="validGround">Tiles that can be placed on validly</param>
        private static void GenDecoration(int[] validGround)
        {
            for (int i = VitricBiome.X + 5; i < VitricBiome.X + (VitricBiome.Width - 5); ++i) //Add vines & decor
            {
                for (int j = VitricBiome.Y; j < VitricBiome.Y + VitricBiome.Height - 10; ++j)
                {
                    if (genRand.Next(8) == 0 && validGround.Any(x => x == Main.tile[i, j].type) && !Main.tile[i, j + 1].active()) //Generates vines, random size between 4-23
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
                        int type = genRand.Next(7); //Generates multitile decoration randomly
                        if (type == 0)
                        {
                            if (validGround.Any(x => x == Main.tile[i, j].type) && Helper.CheckAirRectangle(new Point16(i, j - 1), new Point16(1, 1)))
                                PlaceTile(i, j - 1, genRand.Next(2) == 0 ? TileType<VitricSmallCactus>() : TileType<VitricRock>(), false, false, -1, genRand.Next(4));
                        }
                        else if (type == 1)
                        {
                            if (validGround.Any(x => x == Main.tile[i, j].type) && Helper.CheckAirRectangle(new Point16(i, j - 2), new Point16(2, 2)) && validGround.Any(x => x == Main.tile[i + 1, j].type))
                                PlaceTile(i, j - 2, genRand.Next(2) == 0 ? TileType<VitricRoundCactus>() : TileType<VitricDecor>(), false, false, -1, genRand.Next(4));
                        }
                        else if (type == 2)
                        {
                            bool vGround = true;
                            for (int k = 0; k < 3; ++k)

                                if (!Main.tile[i + k, j].active() || !validGround.Any(x => x == Main.tile[i + k, j].type)) vGround = false;
                            if (vGround && Helper.CheckAirRectangle(new Point16(i, j - 2), new Point16(3, 2))) PlaceTile(i, j - 2, TileType<VitricDecorLarge>(), true, false, -1, genRand.Next(6));
                        }
                    }
                }
            }
        }

        /// <summary>Generates the 2 consistent mini islands on the sides of the arena.</summary>
        private static void GenConsistentMiniIslands()
        {
            int miniIslandX = VitricBiome.X + VitricBiome.Width / 2 - 81;
            int yVal = FindType(miniIslandX, VitricBiome.Y + (VitricBiome.Height / 2), VitricBiome.Y + VitricBiome.Height, TileType<VitricSpike>());
            if (yVal == -1) yVal = Main.maxTilesY - 200;
            MiniIsland(miniIslandX, yVal); //Add 2 constant small islands in order to ease access to the Temple
            miniIslandX = VitricBiome.X + VitricBiome.Width / 2 + 80;
            yVal = FindType(miniIslandX, VitricBiome.Y + (VitricBiome.Height / 2), VitricBiome.Y + VitricBiome.Height, TileType<VitricSpike>());
            if (yVal == -1) yVal = Main.maxTilesY - 200;
            MiniIsland(miniIslandX, yVal); //without allowing crystals to potentially block or obstruct the entrance
        }

        /// <summary>Generates Vitric Moss at 7-9 random positions throughout the biome.</summary>
        private static void GenMoss()
        {
            int reps = genRand.Next(7, 10);
            for (int i = 0; i < 8; ++i) //Moss. This is ugly and I'm sorry.
            {
                Point pos = VitricIslandLocations[genRand.Next(VitricIslandLocations.Count)]; //Random island position
                pos.X += genRand.Next(-10, 11);
                int bot = (pos.Y + genRand.Next(12, 24)); //Depth scan
                int siz = genRand.Next(2, 5); //Width/2
                for (int j = -siz + pos.X; j < pos.X + siz; ++j)
                {
                    for (int k = pos.Y; k < bot; ++k)
                    {
                        bool endCheck = false;
                        for (int x = -1; x < 1; ++x)
                        {
                            for (int y = -1; y < 1; ++y)
                            {
                                if (Main.tile[j, k].type == TileType<VitricSand>() && !Main.tile[j - x, k - y].active())
                                {
                                    Main.tile[j, k].type = (ushort)TileType<VitricMoss>();
                                    endCheck = true;
                                    break;
                                }
                            }
                            if (endCheck)
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>Generates a large island at X/Y.</summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        private static void CreateIsland(int x, int y)
        {
            int wid = genRand.Next(32, 42);
            int top = 5;
            int depth = 2;

            bool peak = false;
            int peakEnd = 0;
            int peakStart = 0;
            int offset = 0;

            for (int i = x - (int)(wid / 2f); i < x + (wid / 2f); ++i)
            {
                if (i == x - (int)(wid / 2f) + 1) top++;
                else if (i == (x + (int)(wid / 2f)) - 1) top--;

                if (!peak)
                {
                    if (depth <= 2) depth += genRand.Next(2);
                    else depth += genRand.Next(-1, 2);

                    if (genRand.Next(3) == 0)
                    {
                        peak = true;
                        peakStart = i;
                        peakEnd = i + genRand.Next(3, 8);
                        if (peakEnd > (x + (wid / 2f)) - 1) peakEnd = (int)(x + (wid / 2f)) - 1;
                    }
                }
                else
                {
                    int dist = peakEnd - i;
                    int dif = peakEnd - peakStart;
                    int deep = (7 - dif);

                    if (dist > (int)(dif / 2f)) depth += genRand.Next(deep, deep + 2);
                    else depth -= genRand.Next(deep, deep + 2);

                    if (x >= peakEnd) peak = false;
                }

                if (i % 4 == 0)
                {
                    if (i < x) top += genRand.Next(2);
                    else top -= genRand.Next(2);
                }

                if (i % 8 == 2) offset += genRand.Next(-1, 2);

                if (top < 3) top = 3;

                if (i > x + (wid / 2f) - 4 && depth > 4)
                    depth--;
                if (i > x + (wid / 2f) - 4 && depth > 8)
                    depth--;

                for (int j = y - top + offset; j < y + depth + offset; j++)
                {
                    int t = j > (y + depth + offset) - 4 ? TileType<VitricSand>() : TileType<VitricSoftSand>();
                    PlaceTile(i, j, t, false, true);
                }
            }
        }

        /// <summary>Generates a small island at X/Y. Cannot go past the bottom of the Vitric Desert.</summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static void MiniIsland(int x, int y)
        {
            int width = genRand.Next(12, 18);
            for (int i = -width / 2; i < width / 2; ++i)
            {
                int pY = y;
                while (Main.tile[x, pY].active())
                    pY--;
                int depth = (int)Math.Pow((width / 2) - Math.Abs(i), 2);
                for (int j = 0; j < depth; ++j)
                {
                    if (pY + j > VitricBiome.Y + VitricBiome.Height) break;
                    KillTile(x + i, pY + j, false, false, true);
                    KillTile(x + i, pY - (int)Math.Sqrt(j), false, false, true);
                    PlaceTile(x + i, pY + j, TileType<VitricSand>(), true, false, -1, 0);
                }
            }
        }

        /// <summary>Goes down until it hits a tile of any type in types; or until maxDepth is reached or somehow exceeded</summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Initial Y position.</param>
        /// <param name="maxDepth">Max Y position in tile position before the loop fails gracefully.</param>
        /// <param name="types">Dictates which tile types are valid to stop on</param>
        /// <returns>Resulting y position, if a tile is found, or -1 if not.</returns>
        private static int FindType(int x, int y, int maxDepth = -1, params int[] types)
        {
            if (maxDepth == -1) maxDepth = Main.maxTilesY - 20; //Set default
            while (true)
            {
                if (y >= maxDepth)
                    break;
                if (Main.tile[x, y].active() && types.Any(i => i == Main.tile[x, y].type))
                    return y; //Returns first valid tile under intitial Y pos, -1 if max depth is reached
                y++;
            }
            return -1; //fallout case
        }
    }
}
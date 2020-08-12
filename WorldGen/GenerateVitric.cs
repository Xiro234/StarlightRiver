﻿using Microsoft.Xna.Framework;
using StarlightRiver.Noise;
using StarlightRiver.Tiles.Vitric;
using StarlightRiver.Tiles.Vitric.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using static Terraria.WorldGen;

namespace StarlightRiver
{
    public partial class StarlightWorld : ModWorld
    {
        public static FastNoise genNoise = new FastNoise(_genRandSeed);
        private const int VitricSlopeOffset = 48;
        private const float VitricNoiseHeight = 9f;

        private static int crystalsPlaced = 0;

        private static int[] ValidGround = new int[] { TileType<VitricSand>(), TileType<VitricSoftSand>() };

        public static List<Point> VitricIslandLocations { get; private set; }
        public static List<Point> RuinedPillarPositions { get; private set; }

        /// <summary>Generates the Vitric Desert under the Underground Desert.</summary>
        /// <param name="progress"></param>
        public static void VitricGen(GenerationProgress progress)
        {
            progress.Message = "Digging Vitric Desert";

            int vitricHeight = 140;
            ValidGround = new int[] { TileType<VitricSand>(), TileType<VitricSoftSand>() };
            //Basic biome information
            VitricBiome = new Rectangle(UndergroundDesertLocation.X - 80, UndergroundDesertLocation.Y + UndergroundDesertLocation.Height, UndergroundDesertLocation.Width + 150, vitricHeight);

            int minCeilingDepth = (int)((VitricBiome.Y + (VitricBiome.Height / 2)) - (17f * Math.Log(VitricSlopeOffset - 8))); //Various informational variables - not to be changed
            int maxCeilingDepth = minCeilingDepth + 7;
            int minFloorDepth = (int)(VitricBiome.Y + (13f * Math.Log(VitricSlopeOffset - 8))) + (VitricBiome.Height / 2);

            GenerateBase(minCeilingDepth, maxCeilingDepth, minFloorDepth);

            for (int x = VitricBiome.Center.X - 40; x < VitricBiome.Center.X + 40; x++) //Flat part of the centre - Ceiros's Arena
            {
                int xRel = x - (VitricBiome.Center.X - 40);
                for (int y = VitricBiome.Y + VitricBiome.Height - 76; y < VitricBiome.Y + VitricBiome.Height; y++) PlaceTile(x, y, TileType<VitricSand>(), false, true);

                if (xRel == 38) Helper.PlaceMultitile(new Point16(x, VitricBiome.Y + 57), TileType<VitricBossAltar>());
            }

            for (int x = VitricBiome.Center.X - 35; x <= VitricBiome.Center.X + 36; x++) //Entrance from Desert 
            {
                for (int y = VitricBiome.Y - 6; y < VitricBiome.Y + 20; y++)
                {
                    KillTile(x, y);
                    if (y > VitricBiome.Y + 5 && y < VitricBiome.Y + 9) PlaceTile(x, y, TileType<VitricBossBarrier>(), true, true);
                }
            }

            for (int y = VitricBiome.Y + 9; y < VitricBiome.Y + VitricBiome.Height - 77; y++) //collision for pillars
            {
                PlaceTile(VitricBiome.X + VitricBiome.Width / 2 - 40, y, TileType<VitricBossBarrier>(), false, false);
                PlaceTile(VitricBiome.X + VitricBiome.Width / 2 + 41, y, TileType<VitricBossBarrier>(), false, false);
            }

            for (int x = VitricBiome.Center.X - 51; x <= VitricBiome.Center.X + 52; x++) //Sandstone Cubes (Pillar Ground)
            {
                int xRel = x - (VitricBiome.Center.X - 51);
                if (xRel < 16 || xRel > 87)
                {
                    for (int y = VitricBiome.Y + VitricBiome.Height - 77; y < VitricBiome.Y + VitricBiome.Height - 67; y++) PlaceTile(x, y, TileType<AncientSandstone>(), false, true);
                    for (int y = VitricBiome.Y - 1; y < VitricBiome.Y + 9; y++) PlaceTile(x, y, TileType<AncientSandstone>(), false, true);
                }
            } //Adjusted from prior code

            VitricIslandLocations = new List<Point>(); //List for island positions
            int fail = 0;
            for (int i = 0; i < (VitricBiome.Width / 40) - 1; ++i)
            {
                int x;
                int y;
                bool repeat = false;

                do
                {
                    x = genRand.Next(2) == 0 ? genRand.Next(VitricBiome.X + VitricSlopeOffset + 20, VitricBiome.Center.X - 61) : genRand.Next(VitricBiome.Center.X + 62, VitricBiome.Right - VitricSlopeOffset - 20);
                    y = (maxCeilingDepth + 18) + (genRand.Next((int)(VitricBiome.Height / 2.8f)));

                    if (VitricIslandLocations.Any(v => Vector2.Distance(new Vector2(x, y), v.ToVector2()) < 32) || (x > VitricBiome.X + VitricBiome.Width / 2 - 71 && x < VitricBiome.X + VitricBiome.Width / 2 + 70))
                    {
                        repeat = true;
                        if (fail++ >= 50) break;
                    }
                    else
                        repeat = false;
                }
                while (repeat); //Gets a valid island position

                if (fail >= 50) break; //Could not get a valid position, stop trying

                VitricIslandLocations.Add(new Point(x, y));
                CreateIsland(x, y); //Adds island pos to list and places island
            }

            for (int i = 0; i < 6; ++i) //Mini islands v2, outer only
            {
                int x = i <= 2 ? VitricBiome.X + 6 + genRand.Next((int)(VitricSlopeOffset * 1.3f)) : VitricBiome.Right - 6 - genRand.Next((int)(VitricSlopeOffset * 1.3f));
                int y = genRand.Next(VitricBiome.Y + 22, VitricBiome.Bottom - 44);
                CreateIsland(x, y, true);
            }

            for (int i = 0; i < VitricBiome.Width / 160; ++i)
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

                if (!FloorCrystal(x, y + 6))
                {
                    i--;
                    continue;
                }
            } //Mini islands

            progress.Message = "Populating the Vitric";

            GenConsistentMiniIslands();
            GenSandstonePillars();
            RuinedPillarPositions = new List<Point>();
            GenRuins();
            GenForge();
            GenDecoration();
            GenMoss();
            GenTemple();
        }

        /// <summary>Generates basic biome shape, such as curved walls, noise on floor and ceiling, and spikes on the bottom.</summary>
        /// <seealso cref="https://github.com/Auburns/FastNoise_CSharp"/>
        private static void GenerateBase(int minCeilingDepth, int maxCeilingDepth, int minFloorDepth)
        {
            genNoise.Seed = _genRandSeed;
            genNoise.FractalGain = 0.04f;
            genNoise.Frequency = 0.004f;
            genNoise.FractalLacunarity = 3.0f;
            genNoise.FractalOctaves = 2;
            genNoise.NoiseType = FastNoise.NoiseTypes.SimplexFractal; //Sets noise to proper type
            float[] heights = new float[VitricBiome.Width]; //2D heightmap to create terrain

            for (int x = 0; x < heights.Length; x++)
                heights[x] = genNoise.GetNoise(x, 0);

            float leftCurveConst = 13f;// 15f - ((0.3f + heights[0]) * VitricNoiseHeight); //For curving down into the noise properly, left side
            float rightCurveConst = 13f;// 15f - ((0.3f + heights[heights.Length - 1]) * VitricNoiseHeight); //Right side

            //Controls Y location of the top, ceiling, floor and bottom of the biome
            Dictionary<string, int> layers = new Dictionary<string, int> { { "TOP", 0 }, { "CEILING", 0 }, { "FLOOR", 0 }, { "BOTTOM", 0 } };

            for (int x = VitricBiome.X; x < VitricBiome.X + VitricBiome.Width; x++) //Basic biome shape
            {
                int xDif = x - VitricBiome.X;
                int adjXDif = (VitricBiome.Width - xDif);

                if (xDif < VitricSlopeOffset) //Start curve
                {
                    layers["CEILING"] = (int)((VitricBiome.Center.Y) - (17f * Math.Log(-8 + xDif))); //17f is the constant that goes to the roof
                    layers["TOP"] = (int)((VitricBiome.Center.Y) - (17f * Math.Log(-8 + (xDif + 12)))) - 8;

                    if (xDif < 10) layers["CEILING"] = VitricBiome.Y + VitricBiome.Height / 2;
                    else if (xDif < 17) layers["CEILING"] += genRand.Next(-1, 2);

                    if (layers["TOP"] < VitricBiome.Y) layers["TOP"] = VitricBiome.Y; //Caps off top

                    layers["FLOOR"] = (int)(VitricBiome.Y + (leftCurveConst * Math.Log(-8 + xDif))) + (VitricBiome.Height / 2); //Curves down towards floor
                    layers["BOTTOM"] = (int)(VitricBiome.Y + (leftCurveConst * Math.Log(-8 + (xDif + 12)))) + (VitricBiome.Height / 2) + 28;

                    if (xDif < 10) layers["FLOOR"] = VitricBiome.Y + VitricBiome.Height / 2;
                    else if (xDif < 17) layers["FLOOR"] += genRand.Next(-1, 2);

                    if (layers["BOTTOM"] > VitricBiome.Y + VitricBiome.Height) layers["BOTTOM"] = VitricBiome.Y + VitricBiome.Height; //Caps off bottom
                }
                else if (xDif == VitricSlopeOffset) layers["CEILING"] = minCeilingDepth; //Flatway start
                else if (xDif > VitricSlopeOffset && xDif < VitricBiome.Width - VitricSlopeOffset) //Flatway
                {
                    if (genRand.Next(2) == 0 && x % 2 == 1)
                        if (layers["CEILING"] >= minCeilingDepth && layers["CEILING"] <= maxCeilingDepth) layers["CEILING"] += genRand.Next(-1, 2);

                    if (layers["TOP"] >= layers["CEILING"]) layers["TOP"] = layers["CEILING"];
                    if (layers["CEILING"] < minCeilingDepth) layers["CEILING"] += genRand.Next(2);
                    if (layers["CEILING"] > maxCeilingDepth || (layers["CEILING"] > VitricBiome.Y + 12 && x > VitricBiome.Center.X - 30 && x < VitricBiome.Center.X + 30))
                        layers["CEILING"] -= genRand.Next(2);

                    if (xDif < (VitricBiome.Width / 2) - 81 && xDif > (VitricBiome.Width / 2) + 84 && layers["CEILING"] > VitricBiome.Y + 7) //Adjust for boss pillars
                        layers["CEILING"]--;
                    if (x < VitricBiome.Y - VitricSlopeOffset - 16 && layers["CEILING"] > VitricBiome.Y + 4)
                        layers["CEILING"]--;

                    layers["FLOOR"] = (int)Math.Floor(minFloorDepth - ((0.3f + heights[x - VitricBiome.X]) * VitricNoiseHeight));

                    if (x < VitricBiome.Center.X - 35 && genRand.Next(4) == 0)
                        layers["TOP"] -= genRand.Next(2);
                    if (x > VitricBiome.Center.X + 35 && genRand.Next(4) == 0)
                        layers["TOP"] += genRand.Next(2);
                    if (layers["TOP"] > VitricBiome.Y) layers["TOP"] = VitricBiome.Y;
                }
                else //End curve
                {
                    layers["CEILING"] = (int)(VitricBiome.Center.Y - (17f * Math.Log(-8 + adjXDif)));
                    layers["TOP"] = (int)(VitricBiome.Center.Y - (17f * Math.Log(-8 + (adjXDif + 12)))) - 8;

                    if (layers["TOP"] < VitricBiome.Y) layers["TOP"] = VitricBiome.Y; //Caps off top

                    if (xDif > VitricBiome.Width - 10) layers["CEILING"] = VitricBiome.Center.Y;
                    else if (xDif > VitricBiome.Width - 17) layers["CEILING"] += genRand.Next(-1, 2);

                    layers["FLOOR"] = (int)(VitricBiome.Y + (rightCurveConst * Math.Log(-8 + adjXDif))) + (VitricBiome.Height / 2);
                    layers["BOTTOM"] = (int)(VitricBiome.Y + (rightCurveConst * Math.Log(-8 + (adjXDif + 12)))) + (VitricBiome.Height / 2) + 23;

                    if (xDif < 10) layers["FLOOR"] = VitricBiome.Center.Y;
                    else if (xDif < 17) layers["FLOOR"] += genRand.Next(-1, 2);

                    if (layers["BOTTOM"] > VitricBiome.Bottom) layers["BOTTOM"] = VitricBiome.Bottom; //Caps off bottom
                }

                if (layers["CEILING"] > VitricBiome.Y + VitricBiome.Height / 2) layers["CEILING"] = VitricBiome.Y + VitricBiome.Height / 2;
                if (layers["FLOOR"] < VitricBiome.Y + VitricBiome.Height / 2) layers["FLOOR"] = VitricBiome.Y + VitricBiome.Height / 2;

                for (int y = layers["CEILING"]; y < layers["FLOOR"]; ++y) //Dig out cave
                    Framing.GetTileSafely(x, y).ClearEverything();

                for (int y = layers["TOP"] - 8; y < layers["CEILING"]; ++y)
                {
                    if (x > VitricBiome.Center.X - 35 && x <= VitricBiome.Center.X + 36)
                        break;
                    int xRand = xDif < 20 ? xDif : VitricBiome.Width - xDif;
                    Tile t = Main.tile[x, y];
                    if ((y < layers["TOP"] && genRand.Next(layers["TOP"] - y) == 0 && t.active() && Main.tileSolid[t.type]) || ((xDif < 8 || xDif > VitricBiome.Width - 8) && genRand.Next(xRand) == 0) || y >= layers["TOP"])
                    {
                        PlaceTile(x, y, TileType<VitricSand>(), false, true);
                        t.slope(0);
                        KillWall(x, y, false);
                    }
                }
                
                for (int y = layers["FLOOR"] - (9 - genRand.Next(2)); y < layers["BOTTOM"] + 8; ++y)
                {
                    bool validSpike = y < layers["FLOOR"] && y >= (VitricBiome.Y + (VitricBiome.Height / 2f));
                    int xRand = xDif < 20 ? xDif : VitricBiome.Width - xDif;
                    Tile t = Main.tile[x, y];
                    if ((y > layers["BOTTOM"] && genRand.Next(y - layers["BOTTOM"]) == 0 && t.active() && Main.tileSolid[t.type]) || ((xDif < 8 || xDif > VitricBiome.Width - 8) && genRand.Next(xRand) == 0) || y <= layers["BOTTOM"])
                    {
                        PlaceTile(x, y, validSpike ? TileType<VitricSpike>() : TileType<VitricSand>(), false, true);
                        t.slope(0);
                        KillWall(x, y, false);
                    }
                }
            }
        }

        /// <summary>Generates sandstone pillars (as walls) between some floating islands</summary>
        private static void GenSandstonePillars()
        {
            for (int i = 0; i < VitricIslandLocations.Count - 1; i++)
            {
                Point p = VitricIslandLocations[i];
                int dir = p.Y < VitricBiome.Center.Y ? -1 : 1;
                int offsetX = 0;
                int width = 5;
                int hitCount = 10;

                bool hasLeftIsland = false;
                while (true)
                {
                    if (p.Y < VitricBiome.Y - 10 || p.Y > VitricBiome.Bottom + 10) break; //Fallout case
                    Tile t = Main.tile[p.X + offsetX, p.Y];
                    if (!hasLeftIsland)
                    {
                        if (!t.active() || !ValidGround.Any(v => v == t.type))
                            hasLeftIsland = true;
                    }
                    else
                    {
                        if (t.active() && ValidGround.Any(v => v == t.type) && --hitCount == 0)
                            break;
                    }
                    p.Y += 1 * dir;
                    for (int j = -width; j < width; ++j)
                        PlaceWall(p.X + j + offsetX, p.Y, WallType<VitricSandWall>(), true); //Type TBR

                    if (p.Y % 2 == 0) offsetX += genRand.Next(-1, 2);
                    if (p.Y % 2 == 0) width += genRand.Next(-1, 2);
                    if (width <= 5) width = 5;
                }
            }
        }

        /// <summary>Generates ruins using premade structures, or using the GenPillar method.</summary>
        /// <param name="validGround"></param>
        private static void GenRuins()
        {
            Point16[] ruinedHouseSizes = new Point16[6] { new Point16(8, 7), new Point16(14, 7), new Point16(12, 7), new Point16(10, 6), new Point16(12, 5), new Point16(14, 7) };
            int failCount = 0;
            for (int i = 0; i < 6; ++i)
            {
                if (failCount > 120) break; //To many fails, stop
                int x = VitricBiome.X + VitricSlopeOffset + genRand.Next(VitricBiome.Width - (VitricSlopeOffset * 2));
                while (x > VitricBiome.X + VitricBiome.Width / 2 - 71 && x < VitricBiome.X + VitricBiome.Width / 2 + 70)
                    x = VitricBiome.X + genRand.Next(VitricBiome.Width);
                int ty = genRand.Next(ruinedHouseSizes.Length);
                Point16 size = ruinedHouseSizes[ty];
                int y = FindType(x, (VitricBiome.Y + 38) + (genRand.Next((int)(VitricBiome.Height / 3.2f))), -1, ValidGround) + genRand.Next(2);
                if ((x < VitricBiome.X + VitricBiome.Width / 2 - 71 || x > VitricBiome.X + VitricBiome.Width / 2 + 70) && Helper.CheckAirRectangle(new Point16(x, y - size.Y), new Point16(size.X, size.Y - 3)) && //ScanRectangle(x, y, size.X, size.Y) < 10
                    ValidGround.Any(v => v == Main.tile[x + 1, y].type) && ValidGround.Any(v => v == Main.tile[x + size.X - 1, y].type))
                    StructureHelper.StructureHelper.GenerateStructure("Structures/Vitric/VitricTempleRuins_" + ty, new Point16(x, y - size.Y), StarlightRiver.Instance);
                else { i--; failCount++; continue; }
            }

            failCount = 0;
            for (int i = 0; i < 4; ++i)
            {
                if (failCount > 60) break; //Too many fails, stop
                int x = VitricBiome.X + VitricSlopeOffset + genRand.Next(VitricBiome.Width - (VitricSlopeOffset * 2));
                while (x > VitricBiome.Center.X - 71 && x < VitricBiome.Center.X + 70)
                    x = VitricBiome.X + VitricSlopeOffset + genRand.Next(VitricBiome.Width - (VitricSlopeOffset * 2));
                int y = VitricBiome.Y + genRand.Next(VitricBiome.Height);
                while (Main.tile[x, y].active())
                    y = VitricBiome.Y + genRand.Next(VitricBiome.Height);
                if (RuinedPillarPositions.Any(v => Vector2.Distance(v.ToVector2(), new Vector2(x, y)) < 40) || !GenPillar(x, y))
                {
                    i--;
                    failCount++;
                }
            }
        }

        private static void GenForge()
        {
            int side = 0;
            int x = side == 0 ? VitricBiome.X - 40 : VitricBiome.Right - 40;
            StructureHelper.StructureHelper.GenerateStructure("Structures/VitricForge", new Point16(x, VitricBiome.Center.Y - 10), StarlightRiver.Instance);
        }

        private static void GenTemple()
        {
            StructureHelper.StructureHelper.GenerateStructure("Structures/VitricTemple", new Point16(VitricBiome.Center.X - 43, VitricBiome.Center.Y - 6), StarlightRiver.Instance);
            DashSP = new Vector2(VitricBiome.Center.X * 16, VitricBiome.Center.Y * 16 + 200);
        }

        /// <summary>Generates decor of every type throughout the biome</summary>
        private static void GenDecoration()
        {
            for (int i = VitricBiome.X + 5; i < VitricBiome.X + (VitricBiome.Width - 5); ++i) //Add vines & decor
            {
                for (int j = VitricBiome.Y; j < VitricBiome.Y + VitricBiome.Height - 10; ++j)
                {
                    if (i >= VitricBiome.Center.X - 52 && i <= VitricBiome.Center.X - 51) continue;

                    if (Main.tile[i, j].active() && !Main.tile[i, j + 1].active() && genRand.Next(9) == 0 && ValidGround.Any(x => x == Main.tile[i, j].type)) //Generates vines, random size between 4-23
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
                            if (ValidGround.Any(x => x == Main.tile[i, j].type) && Helper.CheckAirRectangle(new Point16(i, j - 1), new Point16(1, 1)))
                                PlaceTile(i, j - 1, genRand.Next(2) == 0 ? TileType<VitricSmallCactus>() : TileType<VitricRock>(), false, false, -1, genRand.Next(4));
                        }
                        else if (type == 1)
                        {
                            if (ValidGround.Any(x => x == Main.tile[i, j].type) && Helper.CheckAirRectangle(new Point16(i, j - 2), new Point16(2, 2)) && ValidGround.Any(x => x == Main.tile[i + 1, j].type))
                                PlaceTile(i, j - 2, genRand.Next(2) == 0 ? TileType<VitricRoundCactus>() : TileType<VitricDecor>(), false, false, -1, genRand.Next(4));
                        }
                        else if (type == 2)
                        {
                            bool vGround = true;
                            for (int k = 0; k < 3; ++k)

                                if (!Main.tile[i + k, j].active() || !ValidGround.Any(x => x == Main.tile[i + k, j].type)) vGround = false;
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
            FloorCrystal(miniIslandX, yVal);
            miniIslandX = VitricBiome.X + VitricBiome.Width / 2 + 80;
            yVal = FindType(miniIslandX, VitricBiome.Y + (VitricBiome.Height / 2), VitricBiome.Y + VitricBiome.Height, TileType<VitricSpike>());
            if (yVal == -1) yVal = Main.maxTilesY - 200;
            FloorCrystal(miniIslandX, yVal);
        }

        /// <summary>Generates Vitric Moss at 7-11 random positions throughout the biome.</summary>
        private static void GenMoss()
        {
            int reps = genRand.Next(7, 12);
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
                        if (Main.tile[j, k].type != TileType<VitricSand>())
                            continue; //Skip not-sand tiles
                        bool endCheck = false;
                        for (int x = -1; x < 1; ++x)
                        {
                            for (int y = -1; y < 1; ++y)
                            {
                                if (!Main.tile[j - x, k - y].active())
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
        private static void CreateIsland(int x, int y, bool small = false)
        {
            int wid = genRand.Next(32, 42);
            if (small) wid = genRand.Next(9, 14);
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

                if (i > x + (wid / 2f) - 4 && depth > 4) depth--;
                if (i > x + (wid / 2f) - 4 && depth > 8) depth--;

                for (int j = y - top + offset; j < y + depth + offset; j++)
                {
                    int t = j > (y + depth + offset) - 4 ? TileType<VitricSand>() : TileType<VitricSoftSand>();
                    PlaceTile(i, j, t, false, true);
                }
            }

            //Place crystal if needed
            if (crystalsPlaced <= (VitricBiome.Width / 240) + 1 && !small) //2 on a small world
            {
                int tries = 0;
                while (true)
                {
                    int cX = x + genRand.Next((int)(wid * -0.60f), (int)(wid * 0.60f)) - 3;
                    int cY = y - 5;
                    while (Main.tile[cX, cY].active())
                        cY--;
                    cY = FindType(cX, cY, -1, ValidGround);
                    if (ValidGround.Any(v => v == Main.tile[cX + 1, cY].type) && ValidGround.Any(v => v == Main.tile[cX + 2, cY].type) && ScanRectangle(cX, cY - 6, 4, 6) < 3)
                    {
                        StructureHelper.StructureHelper.GenerateStructure("Structures/Vitric/VitricSmallCrystal_" + genRand.Next(2), new Point16(cX, cY - 6), StarlightRiver.Instance);
                        crystalsPlaced++;
                        break;
                    }
                    else
                        if (tries++ >= 20) break; 
                }
            }
            else if (small)
            {
                if (genRand.Next(2) == 0)
                {
                    int cX = x + genRand.Next((int)(wid * -0.60f), (int)(wid * 0.60f)) - 3;
                    int cY = y - 5;
                    while (Main.tile[cX, cY].active())
                        cY--;
                    cY = FindType(cX, cY, -1, ValidGround);
                    PlaceTile(cX, cY - 4, TileID.Meteorite, true);// PlaceObject(cX, cY, TileType<VitricOre>(), true, 0, 0);
                }
            }
        }

        /// <summary>Generates a large crystal at X/Y.</summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static bool FloorCrystal(int x, int y)
        {
            if (crystalsPlaced <= (VitricBiome.Width / 120) + 1)
            {
                int cY = y - 16;
                while (Main.tile[x, cY].active()) cY--;
                while (!Main.tile[x, cY + 1].active()) cY++;
                if (ScanRectangle(x, y - 17, 10, 17) < 3)
                    StructureHelper.StructureHelper.GenerateStructure("Structures/Vitric/VitricLargeCrystal_" + genRand.Next(2), new Point16(x, cY - 17), StarlightRiver.Instance);
                else
                    return false;
                crystalsPlaced++;
            }
            return true;
        }

        /// <summary>
        /// Generates a broken temple pillar. Only places between vitric sand. Automatically scans for crystals, and returns false if a crystal is in the way.
        /// </summary>
        /// <param name="x">Center X position.</param>
        /// <param name="y">Y position. Can be anywhere between a ceiling and floor; will generate appropriately.</param>
        /// <returns>True if a pillar was successfully placed within an area</returns>
        public static bool GenPillar(int x, int y)
        {
            int ceil = FindTypeUp(x, y, VitricBiome.Y - 20, TileType<VitricSand>(), TileType<VitricSoftSand>());
            int floor = FindType(x, y, VitricBiome.Bottom + 20, TileType<VitricSand>(), TileType<VitricSoftSand>());
            if (ceil == -1 || floor == -1 || ceil >= floor) return false; //If there's an invalid ceiling or floor, or if the floor is above or on the ceiling, kill
            int height = floor - ceil; //Height of pillar
            if (height < 7 || height > 50) return false; //If it's too short or too tall
            int wid = genRand.Next(3, 6); //Width of pillar

            int GetHeight(int xPos) { return Math.Abs(ceil - FindTypeUp(xPos, y, VitricBiome.Y - 20, TileType<VitricSand>(), TileType<VitricSoftSand>())); }
            int GetDepth(int xPos) { return Math.Abs(floor - FindType(xPos, y, VitricBiome.Y - 20, TileType<VitricSand>(), TileType<VitricSoftSand>())); }

            for (int i = -wid; i < wid + 1; ++i) //Checks for crystals. If there's a crystal, kill this pillar before it gens
            {
                if (Helper.ScanForTypeDown(x + i, y, TileType<VitricLargeCrystal>(), 100)) return false; //Crystal found, can't place here
                if (Helper.ScanForTypeDown(x + i, y, TileType<VitricSmallCrystal>(), 100)) return false; //Crystal found, can't place here
                if (GetHeight(x + i) - 30 > GetHeight(x - wid) || GetHeight(x + i) - 30 > GetHeight(x + wid)) return false; //Large height differencial found, can't place
                if (GetDepth(x + i) + 30 < GetDepth(x - wid) || GetDepth(x + i) + 30 < GetDepth(x + wid)) return false; //Large height differencial found, can't place
            }

            for (int i = -wid; i < wid + 1; ++i)
            {
                //Tile placement
                int depth = genRand.Next(2) + 1;
                if (Math.Abs(i) == wid || Math.Abs(i) == wid - 2) depth = (int)Math.Ceiling(height / 4f) + genRand.Next((int)Math.Ceiling(-height / 6f), (int)Math.Ceiling(height / 6f));
                if (Math.Abs(i) == wid - 1) depth = (int)Math.Ceiling(height / 3f) + genRand.Next((int)Math.Ceiling(-height / 6f), (int)Math.Ceiling(height / 6f));
                int ceilingY = FindTypeUp(x + i, y, VitricBiome.Y - 20, TileType<VitricSand>(), TileType<VitricSoftSand>());
                int floorY = FindType(x + i, y, VitricBiome.Bottom + 20, TileType<VitricSand>(), TileType<VitricSoftSand>());

                for (int j = 0; j < depth; ++j)
                {
                    KillTile(x + i, ceilingY + j, false, false, true);
                    PlaceTile(x + i, ceilingY + j, TileType<AncientSandstone>(), true, false);
                    KillTile(x + i, floorY - j, false, false, true);
                    PlaceTile(x + i, floorY - j, TileType<AncientSandstone>(), true, false);
                }

                //Wall placement
                depth = (int)Math.Ceiling(height / 2f) + 2;
                if (Math.Abs(i) >= wid) depth = genRand.Next(2) + 1;
                if (Math.Abs(i) == wid - 2) depth = (int)Math.Ceiling(height / 3f) + genRand.Next((int)Math.Ceiling(-height / 6f), (int)Math.Ceiling(height / 4f));
                if (Math.Abs(i) == wid - 1) depth = (int)Math.Ceiling(height / 4f) + genRand.Next((int)Math.Ceiling(-height / 6f), (int)Math.Ceiling(height / 6f));

                for (int j = 0; j < depth; ++j)
                {
                    KillWall(x + i, ceilingY + j, false);
                    PlaceWall(x + i, ceilingY + j, WallType<AncientSandstoneWall>(), true);
                    KillWall(x + i, floorY - j, false);
                    PlaceWall(x + i, floorY - j, WallType<AncientSandstoneWall>(), true);
                }
            }
            RuinedPillarPositions.Add(new Point(x, y));
            return true;
        }

        /// <summary>Goes down until it hits a tile of any type in types; or until maxDepth is reached or somehow exceeded.</summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Initial Y position.</param>
        /// <param name="maxDepth">Max Y position in tile position before the loop fails gracefully.</param>
        /// <param name="types">Dictates which tile types are valid to stop on. Should have at least 1 element.</param>
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

        /// <summary>Goes up until it hits a tile of any type in types; or until minDepth is reached or somehow exceeded.</summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Initial Y position.</param>
        /// <param name="maxDepth">Minimum Y position in tile position before the loop cuts itself off.</param>
        /// <param name="types">Dictates which tile types are valid to stop on. Should have at least 1 element.</param>
        /// <returns>Resulting y position, if a tile is found, or -1 if not.</returns>
        private static int FindTypeUp(int x, int y, int minDepth = -1, params int[] types)
        {
            if (minDepth == -1) minDepth = 20; //Set default
            while (true)
            {
                if (y <= minDepth)
                    break;
                if (Main.tile[x, y].active() && types.Any(i => i == Main.tile[x, y].type))
                    return y; //Returns first valid tile under intitial Y pos, -1 if max depth is reached
                y--;
            }
            return -1; //fallout case
        }

        private static int ScanRectangle(int x, int y, int wid, int hei)
        {
            int count = 0;
            for (int i = x; i < x + wid; ++i)
                for (int j = y; j < y + hei; ++j)
                    if (Main.tile[i, j].active()) count++;
            return count;
        }
    }
}
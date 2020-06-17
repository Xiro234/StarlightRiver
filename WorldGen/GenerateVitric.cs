using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace StarlightRiver
{
    public partial class StarlightWorld : ModWorld
    {
        public static void VitricGen_Old(GenerationProgress progress)
        {
            int vitricHeight = 140;
            Rectangle biomeTarget = new Rectangle(WorldGen.UndergroundDesertLocation.X - 80, WorldGen.UndergroundDesertLocation.Y + WorldGen.UndergroundDesertLocation.Height, WorldGen.UndergroundDesertLocation.Width + 160, vitricHeight);
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
                for (int y = biomeTarget.Y; y < biomeTarget.Y + 20; y++)
                    WorldGen.KillTile(x, y);

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

        private const int VitricSlopeOffset = 48;

        public static void VitricGen(GenerationProgress progress)
        {
            int[] validGround = new int[] { ModContent.TileType<Tiles.Vitric.VitricSand>(), ModContent.TileType<Tiles.Vitric.VitricSoftSand>() };
            int vitricHeight = 140;
            Rectangle biomeTarget = new Rectangle(WorldGen.UndergroundDesertLocation.X - 80, WorldGen.UndergroundDesertLocation.Y + WorldGen.UndergroundDesertLocation.Height, WorldGen.UndergroundDesertLocation.Width + 160, vitricHeight);
            VitricBiome = biomeTarget; //Sets up biome information - adjusted from prior code

            for (int x = biomeTarget.X; x < biomeTarget.X + biomeTarget.Width; x++)
            {
                for (int y = biomeTarget.Y; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    tile.ClearEverything();
                }
            } //Clears whole biome

            int minCeilDepth = (int)((biomeTarget.Y + (biomeTarget.Height / 2)) - (17f * Math.Log(VitricSlopeOffset - 8))); //Various informational variables - not to be changed
            int maxCeilDepth = minCeilDepth + 7;
            int minFloorDepth = (int)(biomeTarget.Y + (13f * Math.Log(VitricSlopeOffset - 8))) + (biomeTarget.Height / 2);
            int maxFloorDepth = (int)(biomeTarget.Y + (13f * Math.Log(VitricSlopeOffset - 30))) + (biomeTarget.Height / 2);

            //(VitricLayer.Top - 0) Top of the biome, (VitricLayer.TopLow - 1) bottom of the top layer of sand, top of the bottom layer of sand (VitricLayer.BottomHigh), and 3 is the bottom of the bottom layer of sand (VitricLayer.Bottom)
            int[] layers = new int[4] { biomeTarget.Y, biomeTarget.Y + biomeTarget.Height / 2, biomeTarget.Y + biomeTarget.Height / 2, biomeTarget.Y + biomeTarget.Height };
            for (int x = biomeTarget.X; x < biomeTarget.X + biomeTarget.Width; x++)
            {
                int xDif = x - biomeTarget.X;

                if (xDif < VitricSlopeOffset) //Start curve
                {
                    layers[1] = (int)((biomeTarget.Y + (biomeTarget.Height / 2)) - (17f * Math.Log(-8 + xDif)));
                    if (xDif < 10)
                        layers[1] = biomeTarget.Y + biomeTarget.Height / 2;
                    else if (xDif < 17)
                        layers[1] += WorldGen.genRand.Next(-1, 2);

                    layers[2] = (int)(biomeTarget.Y + (13f * Math.Log(-8 + xDif))) + (biomeTarget.Height / 2);
                    if (xDif < 10)
                        layers[2] = biomeTarget.Y + biomeTarget.Height / 2;
                    else if (xDif < 17)
                        layers[2] += WorldGen.genRand.Next(-1, 2);
                }
                else if (xDif == VitricSlopeOffset) //Begin flatway
                {
                    layers[1] = minCeilDepth;
                    layers[2] = minFloorDepth;
                }
                else if (xDif > VitricSlopeOffset && xDif < biomeTarget.Width - VitricSlopeOffset) //Flatway
                {
                    if (WorldGen.genRand.Next(3) == 0 && x % 2 == 1)
                    {
                        if (layers[1] >= minCeilDepth && layers[1] <= maxCeilDepth)
                            layers[1] += WorldGen.genRand.Next(-1, 2);
                        else if (layers[1] < minCeilDepth)
                            layers[1] += WorldGen.genRand.Next(2);
                        else if (layers[1] > maxCeilDepth || biomeTarget.Width - VitricSlopeOffset - 30 < xDif)
                            layers[1] += WorldGen.genRand.Next(-1, 1);
                    }

                    if (WorldGen.genRand.Next(3) == 0 && x % 2 == 1)
                    {
                        //IMPLEMENT FASTNOISE
                    }
                }
                else //End curve
                {
                    int adjXDif = (biomeTarget.Width - xDif);
                    layers[1] = (int)((biomeTarget.Y + (biomeTarget.Height / 2)) - (17f * Math.Log(-8 + adjXDif)));
                    if (xDif > biomeTarget.Width - 10)
                        layers[1] = biomeTarget.Y + biomeTarget.Height / 2;
                    else if (xDif > biomeTarget.Width - 17)
                        layers[1] += WorldGen.genRand.Next(-1, 2);

                    layers[2] = (int)(biomeTarget.Y + (13f * Math.Log(-8 + adjXDif))) + (biomeTarget.Height / 2);
                    if (xDif < 10)
                        layers[2] = biomeTarget.Y + biomeTarget.Height / 2;
                    else if (xDif < 17)
                        layers[2] += WorldGen.genRand.Next(-1, 2);
                }

                if (layers[1] > biomeTarget.Y + biomeTarget.Height / 2) layers[1] = biomeTarget.Y + biomeTarget.Height / 2;
                if (layers[2] < biomeTarget.Y + biomeTarget.Height / 2) layers[2] = biomeTarget.Y + biomeTarget.Height / 2;

                for (int y = layers[0]; y < layers[1]; ++y)
                    WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);

                int spikeOff = WorldGen.genRand.Next(2);
                for (int y = layers[2] - (9 - spikeOff); y < layers[3]; ++y)
                {
                    bool validSpike = y < layers[2] && y >= (biomeTarget.Y + (biomeTarget.Height / 2f));
                    WorldGen.PlaceTile(x, y, validSpike ? ModContent.TileType<Tiles.Vitric.VitricSpike>() : ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 40; x < biomeTarget.X + biomeTarget.Width / 2 + 40; x++) //Flat part of the centre - Ceiros's Arena
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 - 40);
                for (int y = biomeTarget.Y + biomeTarget.Height - 76; y < biomeTarget.Y + biomeTarget.Height; y++)
                    WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);

                if (xRel == 38)
                    Helper.PlaceMultitile(new Point16(x, biomeTarget.Y + 57), ModContent.TileType<Tiles.Vitric.VitricBossAltar>());
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 35; x <= biomeTarget.X + biomeTarget.Width / 2 + 36; x++) //Entrance from Desert 
                for (int y = biomeTarget.Y; y < biomeTarget.Y + 20; y++)
                    WorldGen.KillTile(x, y);

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 51; x <= biomeTarget.X + biomeTarget.Width / 2 + 52; x++) //Sandstone Cubes (Pillar Ground)
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 - 51);
                if (xRel < 16 || xRel > 87)
                {
                    for (int y = biomeTarget.Y + biomeTarget.Height - 77; y < biomeTarget.Y + biomeTarget.Height - 67; y++) //Bottom
                    {
                        WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.AncientSandstone>(), false, true);
                    }
                    for (int y = biomeTarget.Y - 1; y < biomeTarget.Y + 9; y++) //Top
                    {
                        WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.AncientSandstone>(), false, true);
                    }
                }
            } //Adjusted from prior code

            List<Point> islands = new List<Point>(); //List for island positions
            for (int i = 0; i < 12; ++i)
            {
                int rX;
                int rY;

                bool rep = false;
                do
                {
                    rX = biomeTarget.X + (int)(VitricSlopeOffset * 0.8f) + WorldGen.genRand.Next((int)(biomeTarget.Width / 2.7f));
                    if (WorldGen.genRand.Next(2) == 0) rX += (int)(biomeTarget.Width / 2f);
                    rY = (maxCeilDepth + 18) + (WorldGen.genRand.Next((int)(biomeTarget.Height / 3f)));
                    if (islands.Any(v => Vector2.Distance(new Vector2(rX, rY), v.ToVector2()) < 32) || (rX > biomeTarget.X + biomeTarget.Width / 2 - 71 && rX < biomeTarget.X + biomeTarget.Width / 2 + 70))
                        rep = true;
                    else
                        rep = false;
                } while (rep); //Gets a valid island position

                islands.Add(new Point(rX, rY));
                CreateIsland(rX, rY); //Adds island pos to list and places island
            }

            for (int i = biomeTarget.X + VitricSlopeOffset; i < biomeTarget.X + (biomeTarget.Width - VitricSlopeOffset); ++i) //Add large crystals
            {
                for (int j = biomeTarget.Y + 2; j < biomeTarget.Y + biomeTarget.Height - 2; ++j)
                {
                    if (i < biomeTarget.X + biomeTarget.Width / 2 - 71 || i > biomeTarget.X + biomeTarget.Width / 2 + 70)
                    {
                        if (validGround.Any(x => x == Main.tile[i, j + 1].type) && validGround.Any(x => x == Main.tile[i + 9, j + 1].type) && Helper.CheckAirRectangle(new Point16(i, j - 19), new Point16(10, 19)))
                        {
                            StructureHelper.StructureHelper.GenerateStructure("Structures/LargeVitricCrystal", new Point16(i + 5, (j + WorldGen.genRand.Next(2)) - 17), StarlightRiver.Instance);
                            i += 10;
                        }
                    }
                }
            }

            for (int i = biomeTarget.X + 5; i < biomeTarget.X + (biomeTarget.Width - 5); ++i) //Add vines & decor
            {
                for (int j = biomeTarget.Y; j < biomeTarget.Y + biomeTarget.Height - 10; ++j)
                {
                    if (WorldGen.genRand.Next(8) == 0 && validGround.Any(x => x == Main.tile[i, j].type) && !Main.tile[i, j + 1].active())
                    {
                        int targSize = WorldGen.genRand.Next(4, 23);
                        for (int k = 1; k < targSize; ++k)
                        {
                            if (Main.tile[i, j + k].active())
                                break;
                            WorldGen.PlaceTile(i, j + k, ModContent.TileType<Tiles.Vitric.VitricVine>());
                        }
                    }
                    else
                    {
                        int type = WorldGen.genRand.Next(7); //0 = 1x1 tile, 1 = 2x2 tile, 2 = 2x3 tile, 3+ is empty
                        if (type == 0)
                        {
                            if (validGround.Any(x => x == Main.tile[i, j].type) && Helper.CheckAirRectangle(new Point16(i, j - 1), new Point16(1, 1)))
                            {
                                int id = WorldGen.genRand.Next(2) == 0 ? ModContent.TileType<Tiles.Vitric.VitricSmolCactus>() : ModContent.TileType<Tiles.Vitric.VitricRock>();
                                WorldGen.PlaceTile(i, j - 1, id, true, false, -1, WorldGen.genRand.Next(4));
                            }
                        }
                        else if (type == 1)
                        {
                            if (validGround.Any(x => x == Main.tile[i, j].type) && Helper.CheckAirRectangle(new Point16(i, j - 2), new Point16(2, 2)) && validGround.Any(x => x == Main.tile[i + 1, j].type))
                            {
                                int id = WorldGen.genRand.Next(2) == 0 ? ModContent.TileType<Tiles.Vitric.VitricRoundCactus>() : ModContent.TileType<Tiles.Vitric.VitricDecor>();
                                WorldGen.PlaceTile(i, j - 2, id, true, false, -1, WorldGen.genRand.Next(4));
                            }
                        }
                        else if (type == 2)
                        {
                            bool vGround = true;
                            for (int k = 0; k < 3; ++k)
                                if (!Main.tile[i + k, j].active() || !validGround.Any(x => x == Main.tile[i + k, j].type))
                                    vGround = false;
                            if (vGround && Helper.CheckAirRectangle(new Point16(i, j - 2), new Point16(3, 2)))
                                WorldGen.PlaceTile(i, j - 2, ModContent.TileType<Tiles.Vitric.VitricDecorLarge>(), true, false, -1, WorldGen.genRand.Next(6));
                        }
                    }
                }
            }

            for (int y = biomeTarget.Y + 9; y < biomeTarget.Y + biomeTarget.Height - 77; y++) //collision for pillars
            {
                WorldGen.PlaceTile(biomeTarget.X + biomeTarget.Width / 2 - 40, y, ModContent.TileType<Tiles.Vitric.VitricBossBarrier>(), false, false);
                WorldGen.PlaceTile(biomeTarget.X + biomeTarget.Width / 2 + 41, y, ModContent.TileType<Tiles.Vitric.VitricBossBarrier>(), false, false);
            }
        }

        private enum VitricLayer : int
        {
            Top = 0,
            TopLow,
            BottomHigh,
            Bottom
        }

        private static void CreateIsland(int rX, int rY)
        {
            int wid = WorldGen.genRand.Next(26, 36);
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
                    if (depth <= 2)
                        depth += WorldGen.genRand.Next(2);
                    else
                        depth += WorldGen.genRand.Next(-1, 2);
                    if (WorldGen.genRand.Next(3) == 0)
                    {
                        peak = true;
                        peakStart = i;
                        peakEnd = i + WorldGen.genRand.Next(3, 8);
                        if (peakEnd > (rX + (wid / 2f)) - 1)
                            peakEnd = (int)(rX + (wid / 2f)) - 1;
                    }
                }
                else
                {
                    int dist = peakEnd - i;
                    int dif = peakEnd - peakStart;
                    int deep = (7 - dif);

                    if (dist > (int)(dif / 2f))
                        depth += WorldGen.genRand.Next(deep, deep + 2);
                    else
                        depth -= WorldGen.genRand.Next(deep, deep + 2);

                    if (rX >= peakEnd)
                        peak = false;
                }

                if (i % 4 == 0)
                {
                    if (i < rX)
                        top += WorldGen.genRand.Next(2);
                    else
                        top -= WorldGen.genRand.Next(2);
                }

                if (i % 8 == 2)
                    offset += WorldGen.genRand.Next(-1, 2);

                if (top < 3) top = 3;
                for (int j = rY - top + offset; j < rY + depth + offset; j++)
                {
                    WorldGen.PlaceTile(i, j, j > (rY + depth + offset) - 4 ? ModContent.TileType<Tiles.Vitric.VitricSand>() : ModContent.TileType<Tiles.Vitric.VitricSoftSand>(), false, true);
                }
            }
        }
    }
}
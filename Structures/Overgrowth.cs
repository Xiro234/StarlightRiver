using StarlightRiver.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using StarlightRiver.Tiles.Overgrow;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver.Structures
{
    public partial class GenHelper //Overgrowth Generation
    {
        private const int EntranceDistanceX = 180;
        private static Point EntranceOpening = new Point(0, 0);

        private static int[] dungeonWalls = new int[] { WallID.BlueDungeonSlabUnsafe, WallID.BlueDungeonTileUnsafe, WallID.BlueDungeonUnsafe, WallID.GreenDungeonSlabUnsafe, WallID.GreenDungeonTileUnsafe, WallID.PinkDungeonSlabUnsafe, WallID.PinkDungeonUnsafe, WallID.GreenDungeonUnsafe, WallID.PinkDungeonTileUnsafe };
        private static int[] dungeonTiles = new int[] { TileID.BlueDungeonBrick, TileID.GreenDungeonBrick, TileID.PinkDungeonBrick };

        public static void OvergrowthGen(GenerationProgress progress)
        {
            progress.Message = "Expanding the overgrowth";

            int x = WorldGen.dungeonX;
            int y = WorldGen.dungeonY + 36;
            int dungeonDir = x < Main.maxTilesX / 2 ? -1 : 1;

            DigEntrance(x, y, -dungeonDir);
        }

        private static void DigEntrance(int x, int y, int dir)
        {
            for (int i = 0; i < EntranceDistanceX; ++i)
            {
                for (int j = 0; j < 14; ++j)
                {
                    int X = x + (i * dir);
                    int Y = y + j + (int)(Math.Abs((X - x) * 0.4f));

                    WorldGen.KillTile(X, Y, false, false, true);

                    if ((j <= 2 || j > 10) && !dungeonWalls.Any(k => Main.tile[X, Y].wall == k))
                        WorldGen.PlaceTile(X, Y, ModContent.TileType<BrickOvergrow>());
                    if (!dungeonWalls.Any(k => Main.tile[X, Y].wall == k))
                    {
                        Main.tile[X, Y].wall = (ushort)ModContent.WallType<WallOvergrowGrass>();
                        WorldGen.PlaceWall(X, Y, ModContent.WallType<WallOvergrowGrass>());
                    }
                }
            }

            EntranceOpening = new Point(y + 3, y + (int)(EntranceDistanceX * 0.4f));
            int aX = x + (EntranceDistanceX * dir);

            GenerateOvergrowRoom(ModContent.GetTexture("StarlightRiver/Structures/WispAltar"), new Point16(aX, EntranceOpening.Y), new Rectangle(0, 0, 34, 21));
            LegendWorld.WispSP = new Vector2(aX, EntranceOpening.Y) * 16 + new Vector2(216, 170);

            List<Point> roomCentres = new List<Point>();
            List<RoomType> roomTypes = new List<RoomType>();
            Vector2 roomPos = new Vector2(dir + aX + 16, EntranceOpening.Y + 21);

            int topY = (int)roomPos.Y;
            int gridW = 64;
            int gridH = 32;

            bool[,] grid = new bool[5 * WorldSizeAdj(), 7 * WorldSizeAdj()];
            bool[,] isHall = new bool[5 * WorldSizeAdj(), 7 * WorldSizeAdj()];
            byte[,] hallData = new byte[5 * WorldSizeAdj(), 7 * WorldSizeAdj()]; //0b_0001 = has left, 0b_0010 = has right, 0b_0100 = has top

            //Add rooms
            for (int i = 0; i < 4 * (WorldSizeAdj() + 1) + 1; ++i)
            {
                reset:
                int x2 = WorldGen.genRand.Next(grid.GetLength(0));
                int y2 = WorldGen.genRand.Next(grid.GetLength(1));

                Point pos = (roomPos + new Vector2((x2 * gridW) * dir, y2 * gridH)).ToPoint();

                if (grid[x2, y2])
                    goto reset;

                for (int j = -20; j < 20; ++j)
                {
                    for (int k = -20; k < 20; ++k)
                    {
                        if (Main.tile[pos.X + j, pos.Y + k].active() && dungeonTiles.Any(v => Main.tile[pos.X + j, pos.Y + k].type == v))
                        {
                            goto reset; //I'm sorry
                        }
                    }
                }

                grid[x2, y2] = true;
                isHall[x2, y2] = false;
            }

            //Add large hallways
            for (int i = 0; i < grid.GetLength(0); ++i)
            {
                for (int q = 0; q < grid.GetLength(1); ++q)
                {
                    int x2 = i;
                    int y2 = q;

                    Point pos = (roomPos + new Vector2((x2 * gridW) * dir, y2 * gridH)).ToPoint();

                    for (int j = -20; j < 20; ++j)
                    {
                        for (int k = -20; k < 20; ++k)
                        {
                            if (Main.tile[pos.X + j, pos.Y + k].active() && dungeonTiles.Any(v => Main.tile[pos.X + j, pos.Y + k].type == v))
                            {
                                continue;
                            }
                        }
                    }
                    if (grid[x2, y2])
                        continue;

                    bool willPlace = false;

                    if (i != 0 && i != grid.GetLength(0) - 1) //Place HORIZONTAL
                    {
                        byte canPlace = 0;

                        for (int j = 0; j < grid.GetLength(0); ++j)
                        {
                            if (j != i) {
                                if (grid[j, y2] && j < i && (canPlace & 0b_01) != 0b_01)
                                    canPlace += 0b_01;
                                if (grid[j, y2] && j > i && (canPlace & 0b_10) != 0b_10)
                                    canPlace += 0b_10;
                            }
                        }

                        if ((canPlace & 0b_11) == 0b_11)
                        {
                            willPlace = true;
                            hallData[x2, y2] += 0b_11;
                        }
                    }

                    if (q != 0 && q != grid.GetLength(1) - 1) //Place HORIZONTAL
                    {
                        byte canPlace = 0;

                        for (int j = 0; j < grid.GetLength(1); ++j)
                        {
                            if (j != q)
                            {
                                if (grid[x2, j] && j < q && (canPlace & 0b_0100) != 0b_0100)
                                    canPlace += 0b_0100;
                                if (grid[x2, j] && j > q && (canPlace & 0b_1000) != 0b_1000)
                                    canPlace += 0b_1000;
                            }
                        }

                        if ((canPlace & 0b_1100) == 0b_1100)
                        {
                            willPlace = true;
                            hallData[x2, y2] += 0b_1100;
                        }
                    }

                    if (willPlace)
                    {
                        grid[x2, y2] = true;
                        isHall[x2, y2] = true;
                    }
                }
            }

            for (int i = 0; i < grid.GetLength(0); ++i)
            {
                for (int q = 0; q < grid.GetLength(1); ++q)
                {
                    if (grid[i, q])
                    {
                        Point16 pos = (roomPos + new Vector2((i * gridW) * dir - 20, q * gridH - 10)).ToPoint16();
                        
                        GenerateOvergrowRoom(ModContent.GetTexture("StarlightRiver/Structures/OvergrowRoom1x1"), pos, new Rectangle(0, 0, 40, 20));
                    }
                }
            }
        }

        private static Vector2 GridOffset(int m, params bool[] allows) => GridOffset(m, m, allows);

        private static Vector2 GridOffset(int m, int h, params bool[] allows)
        {
            int r = WorldGen.genRand.Next(4);
            while (!allows[r])
                r = WorldGen.genRand.Next(4);
            switch (r)
            {
                case 0:
                    return new Vector2(m, 0);
                case 1:
                    return new Vector2(-m, 0);
                case 2:
                    return new Vector2(0, h);
            }
            return new Vector2(0, -h);
        }

        private static void GenerateOvergrowRoom(Texture2D blueprint, Point16 start, Rectangle frame)
        {
            for (int y = 0; y < frame.Height; y++) // for every row
            {
                Color[] rawData = new Color[frame.Width]; //array of colors
                Rectangle row = new Rectangle(frame.X, frame.Y + y, frame.Width, 1); //one row of the image
                blueprint.GetData(0, row, rawData, 0, frame.Width); //put the color data from the image into the array

                for (int x = 0; x < frame.Width; x++) //every entry in the row
                {
                    Main.tile[start.X + x, start.Y + y].ClearEverything(); //clear the tile out
                    Main.tile[start.X + x, start.Y + y].liquidType(0); // clear liquids

                    ushort placeType = 0;
                    ushort wallType = 0;
                    switch (rawData[x].R) //select block
                    {
                        case 10: placeType = TileID.Dirt; break;
                        case 20: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.BrickOvergrow>(); break;
                        case 30: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.GrassOvergrow>(); break;
                        case 40: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.GlowBrickOvergrow>(); break;
                        case 50: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.LeafOvergrow>(); break;
                        case 60: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.BigHatchOvergrow>(); break;
                        case 70: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.VineOvergrow>(); break;
                        case 80: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.CrusherTile>(); break;
                        case 100: placeType = (ushort)ModContent.TileType<Tiles.Interactive.StaminaGem>(); break;
                    }
                    switch (rawData[x].B) //select wall
                    {
                        case 10: wallType = (ushort)ModContent.WallType<Tiles.Overgrow.WallOvergrowBrick>(); break;
                        case 20: wallType = (ushort)ModContent.WallType<Tiles.Overgrow.WallOvergrowGrass>(); break;
                    }
                    switch (rawData[x].G)
                    {
                        case 10: Main.tile[start.X + x, start.Y + y].slope(1); break;
                        case 20: Main.tile[start.X + x, start.Y + y].slope(2); break;
                        case 30: Main.tile[start.X + x, start.Y + y].slope(3); break;
                        case 40: Main.tile[start.X + x, start.Y + y].slope(4); break;
                        case 50: Main.tile[start.X + x, start.Y + y].slope(5); break;
                    }

                    if (placeType != 0) { WorldGen.PlaceTile(start.X + x, start.Y + y, placeType, true, true); } //place block
                    if (wallType != 0) { WorldGen.PlaceWall(start.X + x, start.Y + y, wallType, true); } //place wall
                }
            }
        }

        public enum RoomType : int
        {
            ONEBYONE = 0,
            ONEBYTWO,
            TWOBYONE
        }
    }
}

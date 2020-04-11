using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace StarlightRiver
{
    public partial class LegendWorld
    {
        const int RoomWidth = 20;
        const int RoomHeight = 20;
        const int HallWidth = 10;
        const int HallThickness = 2;
        const int HallSize = 30;
        public static void OvergrowGen(GenerationProgress progress)
        {
            progress.Message = "fuck my ass.";
            MakeRoom(new Rectangle(Main.spawnTileX, Main.spawnTileY, RoomWidth, RoomHeight));
            WormFromRoom(new Point16(Main.spawnTileX, Main.spawnTileY));
        }
        private static void WormFromRoom(Point16 parentPos, byte initialDirection = 5)
        {
            byte direction = initialDirection >= 5 ? (byte)Main.rand.Next(4) : initialDirection;
            Rectangle hall;
            Rectangle room;
            byte attempts = 0;
            while (1 == 1)
            {
                switch (direction % 4) //the 4 possible directions that the hallway can generate in, this generates the rectangles for the hallway and room to safety check them.
                {
                    case 0: //up
                        hall = new Rectangle(parentPos.X + RoomWidth / 2, parentPos.Y - HallSize, HallWidth, HallSize);
                        room = new Rectangle(parentPos.X, parentPos.Y - HallSize - RoomHeight, RoomWidth, RoomHeight);
                        break;
                    case 1: //right
                        hall = new Rectangle(parentPos.X + RoomWidth, parentPos.Y + RoomHeight / 2, HallSize, HallWidth);
                        room = new Rectangle(parentPos.X + RoomWidth * 2 + HallSize, parentPos.Y, RoomWidth, RoomHeight);
                        break;
                    case 2: //down
                        hall = new Rectangle(parentPos.X + RoomWidth / 2, parentPos.Y + RoomHeight, HallWidth, HallSize);
                        room = new Rectangle(parentPos.X, parentPos.Y + RoomHeight + HallSize, RoomWidth, RoomHeight);
                        break;
                    case 3: //left
                        hall = new Rectangle(parentPos.X - HallSize, parentPos.Y + RoomHeight / 2, HallSize, HallWidth);
                        room = new Rectangle(parentPos.X - HallSize - RoomWidth, parentPos.Y, RoomWidth, RoomHeight);
                        break;
                    default: //failsafe
                        hall = new Rectangle();
                        room = new Rectangle();
                        attempts = 5;
                        Console.WriteLine("FATAL: someone broke the laws of mathematics. get out of there!");
                        break;
                }
                if (CheckDungeon(hall) && CheckDungeon(room)) //all clear!
                {
                    if(direction % 2 == 0) MakeHallTall(hall); 
                    else MakeHallLong(hall);
                    MakeRoom(room);
                    Console.WriteLine("Successfully wormed");

                    WormFromRoom(room.TopLeft().ToPoint16());                 
                    break;
                }
                else //area is not clear, change direction and try again
                {
                    if(attempts >= 4) //all directions exhausted, cant worm!
                    {
                        Console.WriteLine("WORMING FAILED! no safe place found to worm to in any direction...");
                        break;
                    }
                    direction++;
                    attempts++;
                    Console.WriteLine("Generation attempt failed! Changing direction...");
                }
            }
        }
        private static void MakeHallLong(Rectangle target)
        {
            for (int x = target.X; x <= target.X + target.Width; x++)
            {
                for (int y = target.Y; y <= target.Y + target.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    tile.wall = (ushort)ModContent.WallType<Tiles.Overgrow.WallOvergrowBrick>();
                    if (y - target.Y <= HallThickness || y - target.Y >= HallWidth - HallThickness) tile.type = (ushort)ModContent.TileType<Tiles.Overgrow.BrickOvergrow>();
                }
            }
        }
        private static void MakeHallTall(Rectangle target)
        {
            for (int x = target.X; x <= target.X + target.Width; x++)
            {
                for (int y = target.Y; y <= target.Y + target.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    tile.wall = (ushort)ModContent.WallType<Tiles.Overgrow.WallOvergrowBrick>();
                    if (x - target.X <= HallThickness || x - target.X >= HallWidth - HallThickness) tile.type = (ushort)ModContent.TileType<Tiles.Overgrow.BrickOvergrow>();
                }
            }
        }
        private static void MakeRoom(Rectangle target)
        {
            for (int x = target.X; x <= target.X + target.Width; x++)
            {
                for (int y = target.Y; y <= target.Y + target.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    tile.wall = (ushort)ModContent.WallType<Tiles.Overgrow.WallOvergrowBrick>();
                    tile.type = (ushort)ModContent.TileType<Tiles.Overgrow.BrickOvergrow>();
                }
            }
        }
        private static bool CheckDungeon(Rectangle rect)
        {
            for(int x = rect.X; x <= rect.X + rect.Width; x++)
            {
                for (int y = rect.Y; y <= rect.Y + rect.Height; y++)
                {
                    if(x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
                    {
                        Console.WriteLine("Failed to find a safe place within the rectangle: " + rect + " due to: out of bounds");
                        return false;
                    }
                    Tile tile = Framing.GetTileSafely(x, y);
                    if (tile.type == TileID.BlueDungeonBrick || tile.type == TileID.GreenDungeonBrick || tile.type == TileID.PinkDungeonBrick || tile.type == ModContent.TileType<Tiles.Overgrow.BrickOvergrow>())
                    {
                        Console.WriteLine("Failed to find a safe place within the rectangle: " + rect + 
                            " due to: " + (tile.type == ModContent.TileType<Tiles.Overgrow.BrickOvergrow>() ? "other overgrow tiles" : "vanilla dungeon tiles"));
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

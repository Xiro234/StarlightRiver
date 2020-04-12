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
using System.Diagnostics;

namespace StarlightRiver
{
    public partial class LegendWorld
    {
        const int RoomHeight = 32;
        const int HallWidth = 16;
        const int HallThickness = 2;
        static List<Rectangle> rooms = new List<Rectangle>();

        public static void OvergrowGen(GenerationProgress progress)
        {
            progress.Message = "fuck my ass.";
            Rectangle firstRoom = new Rectangle(Main.dungeonX + 100, (int)Main.worldSurface + 75, 46, RoomHeight);
            MakeRoom(firstRoom);
            WormFromRoom(firstRoom);
            while (rooms.Count <= 10) WormFromRoom(rooms[WorldGen.genRand.Next(rooms.Count)]);

            //TODO: Room self-identifier (where am I connected to?)
            //      Generate that room's insides based on that from file
            //      hallway prefabs
            //      boss room + special rooms, wisp room should be first!

        }
        private static void WormFromRoom(Rectangle parent, byte initialDirection = 5)
        {
            byte direction = initialDirection >= 5 ? (byte)WorldGen.genRand.Next(4) : initialDirection;
            Rectangle hall;
            Rectangle room;
            byte attempts = 0;
            while (1 == 1)
            {
                int RoomWidth = WorldGen.genRand.Next(4) == 0 ? 92 : 46;
                int HallSize = WorldGen.genRand.Next(25, 45);
                switch (direction % 4) //the 4 possible directions that the hallway can generate in, this generates the rectangles for the hallway and room to safety check them.
                {
                    case 0: //up
                        hall = new Rectangle(parent.X + parent.Width / 2 - HallWidth / 2, parent.Y - HallSize - 1, HallWidth, HallSize); //Big brain power required to think back through the math here lol. 
                        room = new Rectangle(parent.X + (parent.Width - RoomWidth) / 2, parent.Y - HallSize - RoomHeight, RoomWidth, RoomHeight);
                        break;
                    case 1: //right
                        hall = new Rectangle(parent.X + parent.Width + 1, parent.Y + RoomHeight / 2 - HallWidth / 2, HallSize, HallWidth);
                        room = new Rectangle(parent.X + parent.Width + HallSize, parent.Y, RoomWidth, RoomHeight);
                        break;
                    case 2: //down
                        hall = new Rectangle(parent.X + parent.Width / 2 - HallWidth / 2, parent.Y + RoomHeight + 1, HallWidth, HallSize);
                        room = new Rectangle(parent.X + (parent.Width - RoomWidth) / 2, parent.Y + RoomHeight + HallSize, RoomWidth, RoomHeight);
                        break;
                    case 3: //left
                        hall = new Rectangle(parent.X - HallSize - 1, parent.Y + RoomHeight / 2 - HallWidth / 2, HallSize, HallWidth);
                        room = new Rectangle(parent.X - HallSize - RoomWidth, parent.Y, RoomWidth, RoomHeight);
                        break;
                    default: //failsafe, this should never happen. If it does, seek shelter immediately, the universe is likely collapsing.
                        hall = new Rectangle();
                        room = new Rectangle();
                        attempts = 5;
                        Debug.WriteLine("FATAL: someone broke the laws of mathematics. get out of there!");
                        break;
                }
                if (CheckDungeon(hall) && CheckDungeon(room)) //all clear!
                {
                    if(direction % 2 == 0) MakeHallTall(hall);  //should we make a sideways or longways hall?
                    else MakeHallLong(hall);
                    MakeRoom(room); //get a room
                    Debug.WriteLine("Successfully wormed");

                    WormFromRoom(room); 
                    if(WorldGen.genRand.Next(3) >= 1) WormFromRoom(room); //chance to worm in an additional direciton

                    break;
                }
                else //area is not clear, change direction and try again
                {
                    if(attempts >= 4) //all directions exhausted, cant worm!
                    {
                        Debug.WriteLine("WORMING FAILED! no safe place found to worm to in any direction...");
                        break;
                    }
                    direction++;
                    attempts++;
                    Debug.WriteLine("Generation attempt failed! Changing direction...");
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
                    tile.ClearEverything();
                    tile.wall = (ushort)ModContent.WallType<Tiles.Overgrow.WallOvergrowBrick>();
                    if (y - target.Y <= HallThickness || y - target.Y >= HallWidth - HallThickness)
                    {
                        tile.type = (ushort)ModContent.TileType<Tiles.Overgrow.BrickOvergrow>();
                        tile.active(true);
                    }
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
                    tile.ClearEverything();
                    tile.wall = (ushort)ModContent.WallType<Tiles.Overgrow.WallOvergrowBrick>();
                    if (x - target.X <= HallThickness || x - target.X >= HallWidth - HallThickness)
                    {
                        tile.type = (ushort)ModContent.TileType<Tiles.Overgrow.BrickOvergrow>();
                        tile.active(true);
                    }
                }
            }
        }
        private static void MakeRoom(Rectangle target)
        {
            rooms.Add(target);
            for (int x = target.X; x <= target.X + target.Width; x++)
            {
                for (int y = target.Y; y <= target.Y + target.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    tile.ClearEverything();
                    tile.wall = (ushort)ModContent.WallType<Tiles.Overgrow.WallOvergrowBrick>();
                    tile.type = (ushort)ModContent.TileType<Tiles.Overgrow.BrickOvergrow>();
                    tile.active(true);
                }
            }
        } 
        private static bool CheckDungeon(Rectangle rect)
        {
            for(int x = rect.X; x <= rect.X + rect.Width; x++)
            {
                for (int y = rect.Y; y <= rect.Y + rect.Height; y++)
                {
                    if(x < 50 || x > Main.maxTilesX - 50 || y < Main.worldSurface || y > Main.maxTilesY - 220) //keeps us out of the ocean, hell, and OOB
                    {
                        Debug.WriteLine("Failed to find a safe place within the rectangle: " + rect + " due to: out of bounds");
                        return false;
                    }
                    if (Math.Abs(x - Main.dungeonX) > Main.maxTilesX / 10) //keeps us close to the dungeon
                    {
                        Debug.WriteLine("Failed to find a safe place within the rectangle: " + rect + " due to: too far from dungeon");
                        return false;
                    }
                    Tile tile = Framing.GetTileSafely(x, y);
                    //keeps us from running into ourselves or the dungeon. Essentially playing snake.
                    if (tile.type == TileID.BlueDungeonBrick || tile.type == TileID.GreenDungeonBrick || tile.type == TileID.PinkDungeonBrick || tile.type == ModContent.TileType<Tiles.Overgrow.BrickOvergrow>())
                    {
                        Debug.WriteLine("Failed to find a safe place within the rectangle: " + rect + 
                            " due to: " + (tile.type == ModContent.TileType<Tiles.Overgrow.BrickOvergrow>() ? "other overgrow tiles" : "vanilla dungeon tiles"));
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

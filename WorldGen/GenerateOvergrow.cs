using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using StarlightRiver.Tiles.Overgrow.Blocks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace StarlightRiver
{
    public partial class StarlightWorld
    {
        private const int RoomHeight = 32;
        private const int HallWidth = 16;
        private const int HallThickness = 2;
        private static List<Rectangle> Rooms = new List<Rectangle>();

        public static void OvergrowGen(GenerationProgress progress)
        {
            Rooms = new List<Rectangle>();

            progress.Message = "Generating The Overgrowth...";
            Rectangle firstRoom = new Rectangle(Main.dungeonX, (int)Main.worldSurface + 50, 38, 23);

            while (!CheckDungeon(firstRoom))
            {
                if (Math.Abs(firstRoom.X - Main.dungeonX) > 100) firstRoom.Y += 5;
                else firstRoom.X += 5 * ((Main.dungeonX > Main.spawnTileX) ? -1 : 1);
            }

            if (ModLoader.GetMod("StructureHelper") != null) StructureHelper.StructureHelper.GenerateStructure("Structures/WispAltar", firstRoom.TopLeft().ToPoint16(), StarlightRiver.Instance);
            WispSP = firstRoom.Center() * 16 + new Vector2(0, 56); //sets faeflame spawnpoint
            WormFromRoom(firstRoom);

            while (Rooms.Count <= 7) WormFromRoom(Rooms[WorldGen.genRand.Next(Rooms.Count)]);

            for(int k = Rooms.Count -1; k >= 0; k--)
            {
                if (WormBossRoom(Rooms[k])) break;
                else if (k == 0) throw new Exception("Boss room could not find a safe place to generate.");
            }

            Rooms.ForEach(PopulateRoom);

            //TODO:
            //      hallway prefabs
            //      special rooms
        }

        private static bool WormBossRoom(Rectangle parent)
        {
            byte direction = (byte)WorldGen.genRand.Next(4);
            Rectangle hall;
            Rectangle room;
            byte attempts = 0;

            while (1 == 1)
            {
                int roomWidth = 105;
                int roomHeight = 84;
                int hallSize = WorldGen.genRand.Next(25, 45);
                switch (direction % 4) //the 4 possible directions that the hallway can generate in, this generates the rectangles for the hallway and room to safety check them.
                {
                    case 0: //up
                        hall = new Rectangle(parent.X + parent.Width / 2 - HallWidth / 2, parent.Y - hallSize + 1, HallWidth, hallSize - 2); 
                        room = new Rectangle(parent.X + (parent.Width - roomWidth) / 2, parent.Y - hallSize - roomHeight, roomWidth, roomHeight);
                        break;

                    case 1: //right
                        hall = new Rectangle(parent.X + parent.Width + 1, parent.Y + parent.Height / 2 - HallWidth / 2, hallSize - 2, HallWidth);
                        room = new Rectangle(parent.X + parent.Width + hallSize, parent.Y, roomWidth, roomHeight);
                        break;

                    case 2: //down
                        hall = new Rectangle(parent.X + parent.Width / 2 - HallWidth / 2, parent.Y + roomHeight + 1, HallWidth, hallSize - 2);
                        room = new Rectangle(parent.X + (parent.Width - roomWidth) / 2, parent.Y + roomHeight + hallSize, roomWidth, roomHeight);
                        break;

                    case 3: //left
                        hall = new Rectangle(parent.X - hallSize + 1, parent.Y + parent.Height / 2 - HallWidth / 2, hallSize - 2, HallWidth);
                        room = new Rectangle(parent.X - hallSize - roomWidth, parent.Y, roomWidth, roomHeight);
                        break;

                    default: //failsafe, this should never happen. If it does, seek shelter immediately, the universe is likely collapsing.
                        hall = new Rectangle();
                        room = new Rectangle();
                        attempts = 5;
                        break;
                }

                if (CheckDungeon(hall, true) && CheckDungeon(room, true)) //check lenient so we can generate farther in the world if needed
                {
                    StructureHelper.StructureHelper.GenerateStructure("Structures/OvergrowBossRoom", room.TopLeft().ToPoint16(), StarlightRiver.Instance);

                    if (direction % 2 == 0) MakeHallTall(hall); 
                    else MakeHallLong(hall);

                    return true;        
                }
                else //retry
                {
                    if (attempts >= 4) break; //break out and return false

                    direction++;
                    attempts++;
                }
            }
            return false;
        }

        private static void WormFromRoom(Rectangle parent, byte initialDirection = 5)
        {
            byte direction = initialDirection >= 5 ? (byte)WorldGen.genRand.Next(4) : initialDirection;
            Rectangle hall;
            Rectangle room;
            byte attempts = 0;
            while (1 == 1)
            {
                int roomWidth = 46;
                int hallSize = WorldGen.genRand.Next(25, 45);
                switch (direction % 4) //the 4 possible directions that the hallway can generate in, this generates the rectangles for the hallway and room to safety check them.
                {
                    case 0: //up
                        hall = new Rectangle(parent.X + parent.Width / 2 - HallWidth / 2, parent.Y - hallSize + 1, HallWidth, hallSize - 2); //Big brain power required to think back through the math here lol.
                        room = new Rectangle(parent.X + (parent.Width - roomWidth) / 2, parent.Y - hallSize - RoomHeight, roomWidth, RoomHeight);
                        break;

                    case 1: //right
                        hall = new Rectangle(parent.X + parent.Width + 1, parent.Y + RoomHeight / 2 - HallWidth / 2, hallSize - 2, HallWidth);
                        room = new Rectangle(parent.X + parent.Width + hallSize, parent.Y, roomWidth, RoomHeight);
                        break;

                    case 2: //down
                        hall = new Rectangle(parent.X + parent.Width / 2 - HallWidth / 2, parent.Y + RoomHeight + 1, HallWidth, hallSize - 2);
                        room = new Rectangle(parent.X + (parent.Width - roomWidth) / 2, parent.Y + RoomHeight + hallSize, roomWidth, RoomHeight);
                        break;

                    case 3: //left
                        hall = new Rectangle(parent.X - hallSize + 1, parent.Y + RoomHeight / 2 - HallWidth / 2, hallSize - 2, HallWidth);
                        room = new Rectangle(parent.X - hallSize - roomWidth, parent.Y, roomWidth, RoomHeight);
                        break;

                    default: //failsafe, this should never happen. If it does, seek shelter immediately, the universe is likely collapsing.
                        hall = new Rectangle();
                        room = new Rectangle();
                        attempts = 5;
                        break;
                }

                if (CheckDungeon(hall) && CheckDungeon(room)) //all clear!
                {
                    MakeRoom(room); //get a room
                    if (direction % 2 == 0) MakeHallTall(hall);  //should we make a sideways or longways hall?
                    else MakeHallLong(hall);

                    WormFromRoom(room);
                    if (WorldGen.genRand.Next(3) >= 1) WormFromRoom(room); //chance to worm in an additional direciton

                    break;
                }
                else //area is not clear, change direction and try again
                {
                    if (attempts >= 4) break;//all directions exhausted, cant worm!

                    direction++;
                    attempts++;
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
                    tile.wall = (ushort)WallType<Tiles.Overgrow.WallOvergrowBrick>();
                    if (y - target.Y <= HallThickness || y - target.Y >= HallWidth - HallThickness)
                    {
                        tile.type = (ushort)TileType<BrickOvergrow>();
                        tile.active(true);
                    }
                    if (y - target.Y == HallWidth / 2 && (x == target.X + 1 || x == target.X + target.Width - 1))
                    {
                        tile.type = (ushort)TileType<Tiles.Overgrow.MarkerGem>();
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
                    tile.wall = (ushort)WallType<Tiles.Overgrow.WallOvergrowBrick>();
                    if (x - target.X <= HallThickness || x - target.X >= HallWidth - HallThickness)
                    {
                        tile.type = (ushort)TileType<BrickOvergrow>();
                        tile.active(true);
                    }
                    if (x - target.X == HallWidth / 2 && (y == target.Y + 1 || y == target.Y + target.Height - 1))
                    {
                        tile.type = (ushort)TileType<Tiles.Overgrow.MarkerGem>();
                        tile.active(true);
                    }
                }
            }
        }

        private static void MakeRoom(Rectangle target)
        {
            Rooms.Add(target);
            for (int x = target.X; x <= target.X + target.Width; x++)
            {
                for (int y = target.Y; y <= target.Y + target.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    tile.ClearEverything();
                    tile.wall = (ushort)WallType<Tiles.Overgrow.WallOvergrowBrick>();
                    tile.type = (ushort)TileType<BrickOvergrow>();
                    tile.active(true);
                }
            }
        }

        private static bool CheckDungeon(Rectangle rect, bool lenient = false)
        {
            if (!lenient && Rooms.Count > 20) return false; //limit to 20 rooms if not lenient

            for (int x = rect.X; x <= rect.X + rect.Width; x++)
            {
                for (int y = rect.Y; y <= rect.Y + rect.Height; y++)
                {
                    //keeps us out of the ocean, hell, and OOB
                    if (x < 50 || x > Main.maxTilesX - 50 || y < Main.worldSurface || y > Main.maxTilesY - 220) return false;

                    //keeps us close to the dungeon, only when not lenient
                    if (!lenient && Math.Abs(x - Main.dungeonX) > Main.maxTilesX / 10) return false;

                    Tile tile = Framing.GetTileSafely(x, y);

                    //keeps us from running into blacklisted tiles.
                    if (tile.type == TileID.BlueDungeonBrick || tile.type == TileID.GreenDungeonBrick || tile.type == TileID.PinkDungeonBrick || tile.type == TileType<BrickOvergrow>() ||
                        tile.type == TileID.LihzahrdBrick || tile.type == TileType<Tiles.Vitric.Blocks.VitricSand>())
                        return false; 
                }
            }
            return true;
        }

        private static void PopulateRoom(Rectangle room)
        {
            //this will determine what kind of room this is based on it's openings.
            bool up = false;
            bool down = false;
            bool left = false;
            bool right = false;
            //bool isLong = room.Width > 20;
            int type = TileType<Tiles.Overgrow.MarkerGem>();

            for (int x = room.X; x <= room.X + room.Width; x++) if (Framing.GetTileSafely(x, room.Y - 2).type == type) up = true;
            for (int x = room.X; x <= room.X + room.Width; x++) if (Framing.GetTileSafely(x, room.Y + room.Height + 2).type == type) down = true;
            for (int y = room.Y; y <= room.Y + room.Height; y++) if (Framing.GetTileSafely(room.X - 2, y).type == type) left = true;
            for (int y = room.Y; y <= room.Y + room.Height; y++) if (Framing.GetTileSafely(room.X + room.Width + 2, y).type == type) right = true;

            for (int x = room.X; x <= room.X + room.Width; x++)
            {
                int xRel = x - room.X;
                for (int y = room.Y; y <= room.Y + room.Height; y++)
                {
                    int yRel = y - room.Y;

                    #region openings
                    int halfWidth = HallWidth / 2 - 2;

                    if (up)
                    {
                        if (xRel > (room.Width / 2) - halfWidth && xRel < (room.Width / 2) + halfWidth && yRel < 3) WorldGen.KillTile(x, y);
                    }
                    if (down)
                    {
                        if (xRel > (room.Width / 2) - halfWidth && xRel < (room.Width / 2) + halfWidth && yRel > room.Height - 3) WorldGen.KillTile(x, y);
                    }
                    if (left)
                    {
                        if (yRel > (room.Height / 2) - halfWidth && yRel < (room.Height / 2) + halfWidth && xRel < 3) WorldGen.KillTile(x, y);
                    }
                    if (right)
                    {
                        if (yRel > (room.Height / 2) - halfWidth && yRel < (room.Height / 2) + halfWidth && xRel > room.Width - 3) WorldGen.KillTile(x, y);
                    }

                    #endregion openings

                    if (xRel > 2 && xRel < room.Width - 2 && yRel > 2 && yRel < room.Height - 2) //clear out
                    {
                        WorldGen.KillTile(x, y);
                    }
                }
            }
            StructureHelper.StructureHelper.GenerateMultistructureRandom("Structures/OvergrowRooms", room.TopLeft().ToPoint16() + new Point16(3, 3), StarlightRiver.Instance);
        }
    }
}
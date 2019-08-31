using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace spritersguildwip
{
    public class LegendWorld : ModWorld
    {
        public static float rottime = 0;
        public override void PreUpdate()
        {
            rottime += (float)Math.PI / 60;
            if(rottime >= Math.PI * 2)
            {
                rottime = 0;
            }
        }
        public override void PostWorldGen()
        {
            // Top-Left Position
            Vector2 startpoint = new Vector2(Main.spawnTileX, Main.spawnTileY - 30);

            // Slopes in order: full=0, BL, BR, TL, TR, half
            // Example: bottom-left thick slope is 001X_XXXX
            const byte a = 32, b = 33, c = 34, d = 35, e = 36;

            byte[][] altar = new byte[][] //Tiles
            {
                new byte[] { 0+a, 000, 001, 000, 0+b },
                new byte[] { 000, 001, 001, 001, 000 },
                new byte[] { 001, 001, 001, 001, 001 },
                new byte[] { 000, 001, 001, 001, 000 },
                new byte[] { 0+c, 000, 001, 000, 0+d }
            };

            ushort[][] altarWalls = new ushort[][] //Walls
            {
                new ushort[] { c, c, c, c, c },
                new ushort[] { c, c, c, c, c },
                new ushort[] { c, c, c, c, c },
                new ushort[] { c, c, c, c, c },
                new ushort[] { c, c, c, c, c }
            };
            

            for (int y = 0; y < altar.Length; y++)
            {
                for (int x = 0; x < altar[0].Length; x++)
                {

                    ushort placeType = TileID.Dirt;

                    switch (altar[y][x] & 0b0001_1111)
                    {
                        //This is your block pallete
                        case 0: placeType = TileID.Dirt; break;
                        case 1: placeType = TileID.Stone; break;
                        case 2: placeType = TileID.Dirt; break;
                        case 3: placeType = TileID.Dirt; break;
                        case 4: placeType = TileID.Dirt; break;
                        case 5: placeType = TileID.Dirt; break;
                        case 6: placeType = TileID.Dirt; break;
                        case 7: placeType = TileID.Dirt; break;
                    }

                    WorldGen.PlaceTile((int)startpoint.X + x, (int)startpoint.Y + y, placeType, false, true);
                    WorldGen.PlaceWall((int)startpoint.X + x, (int)startpoint.Y + y, altarWalls[y][x], false);

                    if (altar[y][x] >> 5 > 0)
                    {
                        if (altar[y][x] >> 5 == 4)
                            Main.tile[(int)startpoint.X + x, (int)startpoint.Y + y].halfBrick(true);
                        else
                            WorldGen.SlopeTile((int)startpoint.X + x, (int)startpoint.Y + y, altar[y][x] >> 5);
                    }
                }
            }           
        }
    }
}

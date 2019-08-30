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

namespace spritersguildwip
{
    public class LegendWorld : ModWorld
    {
        public override void PostWorldGen()
        {
            //Top-Left Position
            Vector2 startpoint = new Vector2(Main.spawnTileX, Main.spawnTileY - 30);

            //Slopes in order: normal, half, 
            const byte a = 33, b = 34, c = 35, d = 36, e = 37, f = 38;
            //Walls

            byte[][] altar = new byte[][] //Tiles
            {
                new byte[] { 0+a, 0, 1, 0, 0+b },
                new byte[] { 0, 1, 1, 1, 0 },
                new byte[] { 1, 1, 1, 1, 1 },
                new byte[] { 0, 1, 1, 1, 0 },
                new byte[] { 0+c, 0, 1, 0, 0+d }
            };

            ushort[][] altarw = new ushort[][] //Walls
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
                    ushort var1 = TileID.Dirt;

                    switch (altar[y][x] >> 3)
                    {
                        //This is your block pallete
                        case 0: var1 = TileID.Dirt; break;
                        case 1: var1 = TileID.Stone; break;
                        case 2: var1 = TileID.Dirt; break;
                        case 3: var1 = TileID.Dirt; break;
                        case 4: var1 = TileID.Dirt; break;
                        case 5: var1 = TileID.Dirt; break;
                        case 6: var1 = TileID.Dirt; break;
                        case 7: var1 = TileID.Dirt; break;
                    }

                    WorldGen.PlaceTile((int)startpoint.X + x, (int)startpoint.Y + y, var1, false, true);
                    WorldGen.SlopeTile((int)startpoint.X + x, (int)startpoint.Y + y, altar[y][x] << 5);
                    WorldGen.PlaceWall((int)startpoint.X + x, (int)startpoint.Y + y, altarw[y][x], false);
                }
            }           
        }
    }
}

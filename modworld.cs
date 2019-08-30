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
            Vector2 startpoint = new Vector2(Main.spawnTileX, Main.spawnTileY - 120);

            //Tiles
            const byte a = 0, b = 1, c = 2, d = 3, e = 4, f = 5, g = 6, h = 7;
            const byte i = 8, j = 9, k = 10, l = 11, m = 12, n = 13;
            //Walls

            byte[][] altar = new byte[][] //Tiles
            {
                new byte[] { a+m, a+m, b+m, a+m, a+m },
                new byte[] { a, b, b, b, a },
                new byte[] { b, b, b, b, b },
                new byte[] { a, b, b, b, a },
                new byte[] { a, a, b, a, a }
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
                    ushort var1;

                    int var3;
                    switch (altar[y][x])
                    {
                        case 0: var1 = TileID.Dirt; break;
                        case 1: var1 = TileID.Stone; break;
                        case 2: var1 = TileID.Dirt; break;
                        case 3: var1 = TileID.Dirt; break;
                        case 4: var1 = TileID.Dirt; break;
                        case 5: var1 = TileID.Dirt; break;
                        case 6: var1 = TileID.Dirt; break;
                        case 7: var1 = TileID.Dirt; break;


                    }


                    WorldGen.PlaceTile((int)startpoint.X + x, (int)startpoint.Y + y, var1, false, true, -1, var3);
                    WorldGen.PlaceWall((int)startpoint.X + x, (int)startpoint.Y + y, altarw[y][x], false);
                }
            }           
        }
    }
}

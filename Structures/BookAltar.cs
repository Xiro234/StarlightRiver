using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using static StarlightRiver.StarlightRiver;

namespace StarlightRiver.Structures
{
    public partial class GenHelper
    {
        public static void BookAltarGen(GenerationProgress progress)
        {
            progress.Message = "Hiding Codex...";

            Texture2D Altar = ModContent.GetTexture("StarlightRiver/Structures/BookAltar");
            Vector2 spawn = FindSand();
            LegendWorld.BookSP = spawn * 16 + new Vector2(54, 174);

            for (int y = 0; y < Altar.Height; y++) // for every row
            {
                Color[] rawData = new Color[Altar.Width]; //array of colors
                Rectangle row = new Rectangle(0, y, Altar.Width, 1); //one row of the image
                Altar.GetData<Color>(0, row, rawData, 0, Altar.Width); //put the color data from the image into the array

                for (int x = 0; x < Altar.Width; x++) //every entry in the row
                {
                    Main.tile[(int)spawn.X + x, (int)spawn.Y + y].ClearEverything(); //clear the tile out
                    Main.tile[(int)spawn.X + x, (int)spawn.Y + y].liquidType(0); // clear liquids

                    ushort placeType = 0;
                    ushort wallType = 0;
                    switch (rawData[x].R) //select block
                    {
                        case 10: placeType = TileID.SandstoneBrick; break;
                        case 20: placeType = TileID.WoodBlock; break;
                        case 30: placeType = TileID.Sand; break;
                        case 40: placeType = TileID.Platforms; break;
                    }
                    switch (rawData[x].B) //select wall
                    {
                        case 10: wallType = WallID.SandstoneBrick; break;
                        case 20: wallType = WallID.Wood; break;
                    }

                    if (placeType != 0) { WorldGen.PlaceTile((int)spawn.X + x, (int)spawn.Y + y, placeType, true, true); } //place block
                    if (wallType != 0) { WorldGen.PlaceWall((int)spawn.X + x, (int)spawn.Y + y, wallType, true); } //place wall
                }
            }
        }

        private static Vector2 FindSand()
        {
            for(int i = WorldGen.UndergroundDesertLocation.X; i < Main.maxTilesX; i++)
            {
                for(int j = 0; j < Main.maxTilesY; j++)
                {
                    if(i > 20 && Main.tile[i,j].type == TileID.Sand && Helper.AirScanUp(new Vector2(i,j), 40))
                    {
                        return new Vector2(i, j);
                    }
                }               
            }
            return new Vector2(WorldGen.UndergroundDesertLocation.X, 200);
        }
    }
}

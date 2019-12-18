using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver.Structures
{
    public partial class GenHelper
    {
        public static void DebugWispGen(GenerationProgress progress)
        {
            progress.Message = "DebugAltar...";

            Texture2D Altar = ModContent.GetTexture("StarlightRiver/Structures/WispAltar");
            Vector2 spawn = new Vector2(Main.spawnTileX, Main.spawnTileY - 100);
            LegendWorld.WispSP = spawn * 16 + new Vector2(216, 170);

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
                        case 10: placeType = TileID.Dirt; break;
                        case 20: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.BrickOvergrow>(); break;
                        case 30: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.GrassOvergrow>(); break;
                        case 40: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.GlowBrickOvergrow>(); break;
                        case 50: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.LeafOvergrow>(); break;
                        case 60: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.BigHatchOvergrow>(); break;
                        case 70: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.VineOvergrow>(); break;
                    }
                    switch (rawData[x].B) //select wall
                    {
                        case 20: wallType = (ushort)ModContent.WallType<Tiles.Overgrow.WallOvergrowGrass>(); break;
                    }
                    switch (rawData[x].G)
                    {
                        case 10: Main.tile[(int)spawn.X + x, (int)spawn.Y + y].slope(1); break;
                        case 20: Main.tile[(int)spawn.X + x, (int)spawn.Y + y].slope(2); break;
                        case 30: Main.tile[(int)spawn.X + x, (int)spawn.Y + y].slope(3); break;
                        case 40: Main.tile[(int)spawn.X + x, (int)spawn.Y + y].slope(4); break;
                        case 50: Main.tile[(int)spawn.X + x, (int)spawn.Y + y].slope(5); break;

                    }

                    if (placeType != 0) { WorldGen.PlaceTile((int)spawn.X + x, (int)spawn.Y + y, placeType, true, true); } //place block
                    if (wallType != 0) { WorldGen.PlaceWall((int)spawn.X + x, (int)spawn.Y + y, wallType, true); } //place wall
                }
            }
        }
    }
}

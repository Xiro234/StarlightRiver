using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace StarlightRiver.Structures
{
    public partial class GenHelper
    {
        public static void DebugWispGen(GenerationProgress progress)
        {
            progress.Message = "DebugAltar...";

            Texture2D Altar = ModContent.GetTexture("StarlightRiver/Structures/OvergrowBossRoom");
            Vector2 spawn = new Vector2(Main.spawnTileX, Main.spawnTileY + 300);

            for (int y = 0; y < Altar.Height; y++) // for every row
            {
                Color[] rawData = new Color[Altar.Width]; //array of colors
                Rectangle row = new Rectangle(0, y, Altar.Width, 1); //one row of the image
                Altar.GetData(0, row, rawData, 0, Altar.Width); //put the color data from the image into the array

                for (int x = 0; x < Altar.Width; x++) //every entry in the row
                {
                    Main.tile[(int)spawn.X + x, (int)spawn.Y + y].ClearEverything(); //clear the tile out
                    Main.tile[(int)spawn.X + x, (int)spawn.Y + y].liquidType(0); // clear liquids

                    ushort placeType = 0;
                    ushort wallType = 0;
                    switch (rawData[x].R) //select block
                    {
                        case 10: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.BrickOvergrow>(); break;
                        case 20: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.BossWindow>(); break;
                        case 30: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.AppearingBrick>(); break;
                        case 40: placeType = (ushort)ModContent.TileType<Tiles.Overgrow.GrassOvergrow>(); break;
                    }
                    switch (rawData[x].B) //select wall
                    {
                        case 10: wallType = (ushort)ModContent.WallType<Tiles.Overgrow.WallOvergrowBrick>(); break;
                        case 20: wallType = (ushort)ModContent.WallType<Tiles.Overgrow.WallOvergrowInvisible>(); break;
                        case 30: wallType = (ushort)ModContent.WallType<Tiles.Overgrow.WallOvergrowGrass>(); break;
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
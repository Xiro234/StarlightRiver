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
        public static void WindsAltarGen(GenerationProgress progress)
        {
            progress.Message = "Hiding Abilities...";

            Texture2D Altar = ModContent.GetTexture("StarlightRiver/Structures/WindsAltar");
            Vector2 spawn = LegendWorld.VitricBiome.TopLeft() + new Vector2(0, 110);
            LegendWorld.DashSP = spawn * 16 + new Vector2(216, 170);

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
                        case 10: placeType = (ushort)ModContent.TileType<Tiles.Vitric.VitricBrick>(); break;
                        case 20: placeType = (ushort)ModContent.TileType<Tiles.Vitric.VitricGlass>(); break;
                    }
                    switch (rawData[x].B) //select wall
                    {
                        case 10: wallType = (ushort)ModContent.WallType<Tiles.Void.VoidWall>(); break;
                    }

                    if (placeType != 0) { WorldGen.PlaceTile((int)spawn.X + x, (int)spawn.Y + y, placeType, true, true); } //place block
                    if (wallType != 0) { WorldGen.PlaceWall((int)spawn.X + x, (int)spawn.Y + y, wallType, true); } //place wall
                }
            }
        }
    }
}

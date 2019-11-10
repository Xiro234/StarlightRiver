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
        public static void BoulderSlope(GenerationProgress progress)
        {
            progress.Message = "Setting ancient traps...";

            Texture2D BoulderSlope = ModContent.GetTexture("StarlightRiver/Structures/BoulderSlope");
            Vector2 spawn = new Vector2(0, 0);

            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * .00015); k++) //too common atm, add another zero to reduce it to (likely) reasonable levels
            {
                spawn.X = WorldGen.genRand.Next(0 + 200, Main.maxTilesX - 200);//keeps it 200 blocks away from edge of world, can be decreased if need be
                spawn.Y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY - 200);

                if (Main.tile[(int)spawn.X, (int)spawn.Y].type == TileID.Stone)//checks origin for stone, may shift this check to the middle of the structure later, or even check a area for X% or more of stone
                {
                    for (int y = 0; y < BoulderSlope.Height; y++) // for every row
                    {
                        Color[] rawData = new Color[BoulderSlope.Width]; //array of colors
                        Rectangle row = new Rectangle(0, y, BoulderSlope.Width, 1); //one row of the image
                        BoulderSlope.GetData<Color>(0, row, rawData, 0, BoulderSlope.Width); //put the color data from the image into the array

                        for (int x = 0; x < BoulderSlope.Width; x++) //every entry in the row
                        {
                            ushort placeType = 0;
                            ushort wallType = 0;
                            byte slopeType = 0;
                            //wireType = 0;

                            switch (rawData[x].R) //select block
                            {
                                case 40: placeType = TileID.Stone; break; //TODO only fill air blocks instead of replacing all with stone
                                case 80: placeType = TileID.ActiveStoneBlock; break;
                                case 120: placeType = TileID.PressurePlates; break; //doesn't work
                                case 160: placeType = TileID.Boulder; break; //These do not work atm
                                case 255: placeType = 255; break;
                            }
                            switch (rawData[x].B) //select wall
                            {
                                case 80: wallType = WallID.Stone; break;
                            }
                            switch (rawData[x].G) //select slope (and wire)
                            {
                                case 40: slopeType = 1; break;
                                case 80: slopeType = 2; break;
                                case 120: slopeType = 6; break; //TODO just do wireType = X instead of using slopeType for 2 things
                            }

                            if (placeType != 0 && placeType != 255) { WorldGen.PlaceTile((int)spawn.X + x, (int)spawn.Y + y, placeType, true, true); } //place block
                            else if (placeType == 255) { Main.tile[(int)spawn.X + x, (int)spawn.Y + y].ClearEverything(); } //clear tiles

                            if (wallType != 0) { WorldGen.PlaceWall((int)spawn.X + x, (int)spawn.Y + y, wallType, true); } //place wall

                            if (slopeType != 0 && slopeType <= 5) { Main.tile[(int)spawn.X + x, (int)spawn.Y + y].slope(slopeType); } //place wall
                            else if (slopeType == 6) { Main.tile[(int)spawn.X + x, (int)spawn.Y + y].wire(true); } //place wall
                        }
                    }
                }
            }
        }
    }
}

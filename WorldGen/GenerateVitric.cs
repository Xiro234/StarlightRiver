using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.World.Generation;
using StarlightRiver.Structures;

namespace StarlightRiver
{
    public partial class LegendWorld : ModWorld
    {
        public static void VitricGen(GenerationProgress progress)
        {
            int vitricHeight = 94;
            Rectangle biomeTarget = new Rectangle(WorldGen.UndergroundDesertLocation.X - 40, WorldGen.UndergroundDesertLocation.Y + WorldGen.UndergroundDesertLocation.Height, WorldGen.UndergroundDesertLocation.Width + 80, vitricHeight);
            LegendWorld.VitricBiome = biomeTarget;

            for (int x = biomeTarget.X; x < biomeTarget.X + biomeTarget.Width; x++)
            {
                for (int y = biomeTarget.Y; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    tile.ClearEverything();
                }
            }

            for (int x = biomeTarget.X; x < biomeTarget.X + biomeTarget.Width; x++) //base sand + spikes
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 - 40);
                int off = (int)((float)Math.Sin(xRel * 0.2f) * 4) + 8;
                for (int y = biomeTarget.Y + biomeTarget.Height - off; y < biomeTarget.Y + biomeTarget.Height + 12; y++)
                {
                    int yRel = y - (biomeTarget.Y + biomeTarget.Height - off);
                    WorldGen.PlaceTile(x, y, yRel <= WorldGen.genRand.Next(1, 4) ? ModContent.TileType<Tiles.Vitric.VitricSpike>() : ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
                if ((x > biomeTarget.X + 25 && x < biomeTarget.X + biomeTarget.Width / 2 - 70) || (x < biomeTarget.X + biomeTarget.Width - 25 && x > biomeTarget.X + biomeTarget.Width / 2 + 70)) //lay out the crystal pits
                {
                    if (xRel % 25 == 0) //the big crystals!
                    {
                        int xShift = WorldGen.genRand.Next(-3, 3);
                        int thisOff = (int)((float)Math.Sin((xRel + xShift) * 0.2f) * 4) + 8;
                        GenHelper.CrystalGen(new Vector2(x + xShift, biomeTarget.Y + biomeTarget.Height + WorldGen.genRand.Next(3, 6) - 19 - thisOff));
                    }
                    if (xRel % 10 == 0) //fog makers
                    {
                        int y = biomeTarget.Y + biomeTarget.Height - off - 1;
                        WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.DenialAura>());
                    }
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 40; x < biomeTarget.X + biomeTarget.Width / 2 + 40; x++) //flat part of center dune
            {
                for (int y = biomeTarget.Y + biomeTarget.Height - 30; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 - 70; x <= biomeTarget.X + biomeTarget.Width / 2 - 40; x++) //left side of center dune
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 - 70);
                int off = (int)(xRel * 2 - xRel * xRel / 30f);
                for (int y = biomeTarget.Y + biomeTarget.Height - off; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
            }

            for (int x = biomeTarget.X + biomeTarget.Width / 2 + 40; x <= biomeTarget.X + biomeTarget.Width / 2 + 70; x++) //right side of center dune
            {
                int xRel = x - (biomeTarget.X + biomeTarget.Width / 2 + 40);
                int off = (int)(30 - xRel * xRel / 30f);
                for (int y = biomeTarget.Y + biomeTarget.Height - off; y < biomeTarget.Y + biomeTarget.Height; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
            }

            for (int y = biomeTarget.Y; y < biomeTarget.Y + biomeTarget.Height; y++) //left end
            {
                int yRel = y - biomeTarget.Y;
                int off = (5 * yRel) / 6 - (5 * yRel * yRel) / 576;
                for (int x = biomeTarget.X; x <= biomeTarget.X - off + 28; x++) 
                {
                    int xRel = x - (biomeTarget.X - off + 20);
                    WorldGen.PlaceTile(x, y, xRel >= WorldGen.genRand.Next(4, 7) ? ModContent.TileType<Tiles.Vitric.VitricSpike>() : ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
            }


            for (int y = biomeTarget.Y; y < biomeTarget.Y + biomeTarget.Height; y++) //right end
            {
                int yRel = y - biomeTarget.Y;
                int off = (5 * yRel) / 6 - (5 * yRel * yRel) / 576;
                for (int x = biomeTarget.X + biomeTarget.Width + off - 28; x <= biomeTarget.X + biomeTarget.Width; x++)
                {
                    int xRel = x - (biomeTarget.X + biomeTarget.Width + off - 28);
                    WorldGen.PlaceTile(x, y, xRel <= WorldGen.genRand.Next(1, 4) ? ModContent.TileType<Tiles.Vitric.VitricSpike>() : ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
            }

            for (int x = biomeTarget.X; x < biomeTarget.X + biomeTarget.Width; x++) //ceiling
            {
                int xRel = x - biomeTarget.X;
                int amp = (int)(Math.Abs(xRel - biomeTarget.Width / 2) / (float)(biomeTarget.Width / 2) * 8);
                int off = (int)((float)Math.Sin(xRel * 0.2f) * amp) + 5;
                for (int y = biomeTarget.Y; y < biomeTarget.Y + off + 4; y++)
                {
                    WorldGen.PlaceTile(x, y, ModContent.TileType<Tiles.Vitric.VitricSand>(), false, true);
                }
            }
            
            for(int x = biomeTarget.X + biomeTarget.Width / 2 - 30; x <= biomeTarget.X + biomeTarget.Width / 2 + 30; x++) //entrance hole 
            {
                for (int y = biomeTarget.Y; y < biomeTarget.Y + 20; y++)
                {
                    WorldGen.KillTile(x, y);
                }
            }
        }
    }
}

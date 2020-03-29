using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace StarlightRiver
{
    public partial class LegendWorld : ModWorld
    {
        private void ForestHerbGen(GenerationProgress progress)
        {
            progress.Message = "Planting ivy...";
            for (int k = 0; k < Main.maxTilesX; k++)
            {
                if(Main.rand.Next(8) == 0 && k > Main.maxTilesX / 3 && k < Main.maxTilesX / 3 * 2) //Berry Bushes
                {
                    for (int y = 0; y < Main.worldSurface; y++)
                    {
                        if (Main.tile[k, y].type == TileID.Grass && Helper.CheckAirRectangle(new Point16(k, y - 2), new Point16(2, 2)))
                        {
                            Helper.PlaceMultitile(new Point16(k, y-2), ModContent.TileType<Tiles.Herbology.ForestBerryBush>());
                        }
                    }
                }

                else if(Main.rand.Next(10) == 0) //Ivy Patches
                {
                    int size = Main.rand.Next(7, 20);

                    for (int x = k; x < k + size; x++)
                    {
                        for (int y = 0; y < Main.worldSurface; y++)
                        {
                            if (Main.tile[x, y].type == TileID.Grass && Main.tile[x, y - 1].collisionType != 1)
                            {
                                WorldGen.PlaceTile(x, y - 1, ModContent.TileType<Tiles.Herbology.ForestIvyWild>());
                            }
                        }
                    }
                    k += size * 2;
                }
            }
        }
    }
}

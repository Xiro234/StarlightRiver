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
            for (int k = 60; k < Main.maxTilesX - 60; k++)
            {
                if (Main.rand.Next(8) == 0 && k > Main.maxTilesX / 3 && k < Main.maxTilesX / 3 * 2) //Berry Bushes
                {
                    for (int y = 10; y < Main.worldSurface; y++)
                    {
                        if (Main.tile[k, y].type == TileID.Grass && Helper.CheckAirRectangle(new Point16(k, y - 2), new Point16(2, 2)))
                        {
                            Helper.PlaceMultitile(new Point16(k, y - 2), ModContent.TileType<Tiles.Herbology.ForestBerryBush>());
                        }
                    }
                }

                /*else if(Main.rand.Next(30) == 0) //Ivy Patches
                {
                    int size = Main.rand.Next(7, 20);

                    for (int x = k - (int)(size / 2); x < k + (int)(size / 2); x++)
                    {
                        for (int y = 10; y < Main.worldSurface; y++)
                        {
                            if (Main.tile[x, y].type == TileID.Grass && Main.tile[x, y - 1].collisionType != 1 && Main.tile[x, y].slope() == 0)
                            {
                                WorldGen.PlaceTile(x, y - 1, ModContent.TileType<Tiles.Herbology.ForestIvyWild>());
                            }
                        }
                    }
                    //k += size * 2;
                }*/

                for (int j = 15; j < Main.worldSurface; j++)
                {
                    if (Main.rand.Next(500) == 0) //a check for grass could also be here which would speed up this step
                    {
                        int size = Main.rand.Next(3, 10);

                        for (int x = k - (int)(size / 2); x < k + (int)(size / 2); x++)
                        {
                            for (int y = j - (int)(size / 2); y < j + (int)(size / 2); y++)
                            {
                                if (Main.tile[x, y].active() && Main.tile[x, y].type == TileID.Grass && Main.tile[x, y - 1].collisionType != 1 && Main.tile[x, y].slope() == 0) //!Main.tileSolid[Main.tile[x, y - 1].type] may be redundant
                                {
                                    WorldGen.PlaceTile(x, y - 1, ModContent.TileType<Tiles.Herbology.ForestIvyWild>());
                                    //WorldGen.PlaceTile((int)x, y - 2, TileID.RubyGemspark, true, true);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.IO;

namespace StarlightRiver.Dimensions
{
    class Underworld : Dimension
    {
        public Underworld(WorldFileData parent) : base(parent, "UnderDark")
        {

        }

        public override void Generate()
        {
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int k = 0; k < Main.maxTilesY; k++)
                {
                    Main.tile[i, k].ClearEverything();
                    Main.tile[i, k].liquid = 0;
                    if (k >= Main.spawnTileY) WorldGen.PlaceTile(i, k, 1, true, true);
                }
            }
        }
    }
}

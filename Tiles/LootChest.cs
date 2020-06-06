using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles
{
    public abstract class LootChest : ModTile
    {
        internal List<int> GoldLootPool { get; set; }
        internal List<int> SmallLootPool { get; set; }
        public override bool NewRightClick(int i, int j)
        {
            WorldGen.KillTile(i, j);
            return true;
        }
    }
}

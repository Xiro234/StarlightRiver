using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items.Pure
{
    public class StonePureItem : StandardTileItem
    {
        public StonePureItem() : base("Purestone", "It shines brilliantly", 16, 16, ModContent.TileType<Tiles.Purified.StonePure2>())
        {
        }
    }
}

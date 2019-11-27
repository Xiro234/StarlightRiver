using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items.Dolomite
{
    public class DolomiteItem : StandardTileItem
    {
        public DolomiteItem() : base("Dolomite", "", 16, 16, ModContent.TileType<Tiles.Dolomite.Dolomite>())
        {
        }
    }
}

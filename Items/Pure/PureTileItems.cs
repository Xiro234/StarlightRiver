using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Pure
{
    public class StonePureItem : QuickTileItem { public StonePureItem() : base("Purestone", "It shines brilliantly", ModContent.TileType<Tiles.StonePure2>(), 0) { } }
}

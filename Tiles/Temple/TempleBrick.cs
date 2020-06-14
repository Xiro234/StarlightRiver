using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using StarlightRiver.Items;
using Microsoft.Xna.Framework;

namespace StarlightRiver.Tiles.Temple
{
    class TempleBrick : ModTile { public override void SetDefaults() { QuickBlock.QuickSet(this, 0, DustID.Stone, SoundID.Tink, new Color(150, 160, 160), ModContent.ItemType<TempleBrickItem>()); } }
    public class TempleBrickItem : QuickTileItem { public TempleBrickItem() : base("Ancient Temple Bricks", "", ModContent.TileType<TempleBrick>(), 0) { } }
}

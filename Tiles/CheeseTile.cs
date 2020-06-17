using Microsoft.Xna.Framework;
using StarlightRiver.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles
{
    internal class CheeseTile : ModTile { public override void SetDefaults() { QuickBlock.QuickSet(this, 0, DustID.AmberBolt, SoundID.Drown, new Color(255, 255, 200), ModContent.ItemType<CheeseTileItem>(), true); } }
    internal class CheeseTileItem : QuickTileItem { public CheeseTileItem() : base("Cheese", "A chunk of the moon", ModContent.TileType<CheeseTile>(), ItemRarityID.Expert) { } }
}
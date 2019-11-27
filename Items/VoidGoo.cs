using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items
{
    public class VoidGooItem : StandardTileItem
    {
        public VoidGooItem() : base("Void Goo", "Can be passed with a shadow dash", 16, 16, ModContent.TileType<Tiles.Interactive.VoidGoo>(), ItemRarityID.Yellow) { }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
    }

    public class VoidDoorItem : StandardTileItem
    {
        public VoidDoorItem() : base("Void Barrier", "Dissappears when Purified", 16, 16, ModContent.TileType<Tiles.Interactive.VoidDoorOn>(), ItemRarityID.Yellow) { }
        
        
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
    }
}

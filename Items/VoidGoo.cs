using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    public class VoidGoo : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Can be passed with a shadow dash");
            DisplayName.SetDefault("Void Goo");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.consumable = true;
            item.createTile = mod.TileType("VoidGoo");
        }
    }
    public class VoidDoor : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Dissappears when Purified");
            DisplayName.SetDefault("Void Barrier");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.consumable = true;
            item.createTile = mod.TileType("VoidDoorOn");
        }
    }
    public class Fluff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Float upwards");
            DisplayName.SetDefault("GraviWater");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.consumable = true;
            item.createTile = mod.TileType("Fluff");
        }
    }
}

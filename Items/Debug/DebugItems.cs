using static Terraria.ModLoader.ModContent;
using StarlightRiver.Tiles.Vitric;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;

namespace StarlightRiver.Items.Debug
{
    public class DebugPlacer1 : QuickTileItem
    {
        public override string Texture => "StarlightRiver/Items/Debug/DebugPotion";
        public DebugPlacer1() : base("Debug Placer 1", "Suck my huge dragon dong", TileType<Tiles.Overgrow.BossPit>(), 0) { }
    }

    public class DebugPlacer2 : QuickTileItem
    {
        public override string Texture => "StarlightRiver/Items/Debug/DebugPotion";
        public DebugPlacer2() : base("Debug Placer 2", "Suck my huge dragon dong", TileType<Tiles.Overgrow.AppearingBrick>(), 0) { }
    }

    public class DebugPlacer3 : QuickTileItem
    {
        public override string Texture => "StarlightRiver/Items/Debug/DebugPotion";
        public DebugPlacer3() : base("Debug Placer 3", "Suck my huge dragon dong", TileType<Tiles.Overgrow.OvergrowDoorLocked>(), 0) { }
    }

    public class DebugPlacer4 : QuickTileItem
    {
        public override string Texture => "StarlightRiver/Items/Debug/DebugPotion";
        public DebugPlacer4() : base("Debug Placer 4", "Suck my huge dragon dong", TileType<Tiles.Overgrow.BossAltar>(), 0) { }
    }

    public class DebugPotion : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = ItemRarityID.Green;
            item.autoReuse = true;
            item.createTile = TileID.ShadowOrbs;
        }

        public override string Texture => "StarlightRiver/MarioCumming";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Potion of Debugging");
            Tooltip.SetDefault("Effects vary");
        }

        public override bool UseItem(Player player)
        {
            StarlightWorld.OvergrowBossOpen = true;
            StarlightWorld.OvergrowBossFree = false;
            StarlightWorld.RiftLocation = player.Center;
            return true;
        }

        public override void HoldItem(Player player)
        {

        }
    }

    public class DebugPotion2 : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 2;
            item.useTime = 2;
            item.rare = ItemRarityID.Green;
            item.noUseGraphic = true;
        }

        public override string Texture => "StarlightRiver/MarioCumming";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Potion of Debugging 2");
            Tooltip.SetDefault("Effects vary");
        }

        public override bool UseItem(Player player)
        {
            for (int x = 0; x < Main.maxTilesX; x++)
                for (int y = 0; y < Main.maxTilesY; y++) if (Main.tile[x, y].type == TileID.IceBlock) Main.tile[x, y].type = (ushort)TileType<Tiles.Permafrost.PermafrostIce>();

                    return true;
        }

        public override void HoldItem(Player player)
        {

        }
    }
}
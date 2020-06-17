using StarlightRiver.Tiles.Vitric;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Items.Debug
{
    public class DebugPlacer1 : QuickTileItem
    {
        public override string Texture => "StarlightRiver/Items/Debug/DebugPotion";

        public DebugPlacer1() : base("Debug Placer 1", "Suck my huge dragon dong", TileType<VitricOre>(), 0)
        {
        }
    }

    public class DebugPlacer2 : QuickTileItem
    {
        public override string Texture => "StarlightRiver/Items/Debug/DebugPotion";

        public DebugPlacer2() : base("Debug Placer 2", "Suck my huge dragon dong", TileType<VitricOreFloat>(), 0)
        {
        }
    }

    public class DebugPlacer3 : QuickTileItem
    {
        public override string Texture => "StarlightRiver/Items/Debug/DebugPotion";

        public DebugPlacer3() : base("Debug Placer 3", "Suck my huge dragon dong", TileType<Tiles.Temple.TempleChestSimple>(), 0)
        {
        }
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
            item.createTile = TileType<Tiles.Misc.SandscriptTile>();
        }

        public override string Texture => "StarlightRiver/MarioCumming";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Potion of Debugging");
            Tooltip.SetDefault("Effects vary");
        }

        public override bool UseItem(Player player)
        {
            player.GetModPlayer<Abilities.AbilityHandler>().StatStaminaMaxPerm = 1;
            foreach (Abilities.Ability ab in player.GetModPlayer<Abilities.AbilityHandler>().Abilities) ab.Locked = true;

            player.GetModPlayer<Codex.CodexHandler>().CodexState = 0;

            return true;
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
            //foreach (NPC wall in Main.npc.Where(n => n.modNPC is NPCs.Boss.VitricBoss.VitricBackdropLeft)) wall.ai[1] = 3; //make the walls scroll
            //foreach (NPC plat in Main.npc.Where(n => n.modNPC is NPCs.Boss.VitricBoss.VitricBossPlatformUp)) plat.ai[0] = 1; //make the platforms scroll
            //Helper.NewItemPerfect(player.Center + new Vector2(10, -30), Vector2.Normalize(player.Center - Main.MouseWorld).RotatedByRandom(0.3f) * -25, Main.rand.Next(ItemID.Count), 1);
            return true;
        }

        public override void HoldItem(Player player)
        {
            //StarlightRiver.Rotation = (player.Center - Main.MouseWorld).ToRotation() - 1.58f;
        }
    }
}
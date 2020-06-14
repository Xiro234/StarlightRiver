using Microsoft.Xna.Framework;
using StarlightRiver.Tiles.Vitric;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Debug
{
    public class Kill : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 512;
            item.height = 512;
            item.damage = 1000;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = ItemRarityID.Blue;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kill");
            Tooltip.SetDefault("Big Damag");
        }

        public override bool CanUseItem(Player player)
        {
            if (player.name == "a")
            {
                player.KillMeForGood();
            }
            if (player.altFunctionUse == 2)
            {
                if (player.controlDown)
                {
                    item.damage -= 100;
                }
                if (player.controlUp)
                {
                    item.damage += 100;
                }
                Main.NewText(item.damage);
                return false;
            }
            return base.CanUseItem(player);
        }
    }

    public class GrassJungleCorrupt : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Transforms Jungles Corrupt");
            DisplayName.SetDefault("Evil");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 14;
            item.maxStack = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 5;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = false;
            item.shoot = mod.ProjectileType("Clentam");
            item.shootSpeed = 8f;
        }
    }

    public class GrassJungleCorrupt2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Transforms Jungles Crimson");
            DisplayName.SetDefault("Blood");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 14;
            item.maxStack = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 5;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = false;
            item.shoot = mod.ProjectileType("Clentam2");
            item.shootSpeed = 8f;
        }
    }

    public class GrassJungleCorrupt3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Transforms Jungle Hallow");
            DisplayName.SetDefault("Fae");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 14;
            item.maxStack = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 5;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = false;
            item.shoot = mod.ProjectileType("Clentam3");
            item.shootSpeed = 8f;
        }
    }

    public class SealPlacer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seal Placer");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 2;
            item.useTime = 1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            //item.consumable = true;
            item.autoReuse = true;
            item.createWall = ModContent.WallType<Tiles.Overgrow.WallOvergrowBrick>();
        }
    }

    public class FleshPlacer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Potion of Debugging 2");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = false;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            //item.createTile = ModContent.TileType<Tiles.Rift.MainRift>();
        }

        public override bool UseItem(Player player)
        {
            int i = 0;
            string types = "Types: ";
            foreach (NPC npc in Main.npc.Where(npc => npc.type != NPCID.None)) { i++; types += npc.type + ", "; }

            int j = 0;
            foreach (NPC npc in Main.npc.Where(npc => npc.active)) { j++; }

            Main.NewText("Extant NPCs: " + i + "/201");
            Main.NewText("Active NPCs: " + j + "/201");
            Main.NewText(types);
            return true;
        }
    }

    public class DebugPlacer1 : QuickTileItem
    {
        public override string Texture => "StarlightRiver/Items/Debug/DebugPotion";

        public DebugPlacer1() : base("Debug Placer 1", "Suck my huge dragon dong", ModContent.TileType<Tiles.Temple.TempleChestSimple>(), 0)
        {
        }
    }

    public class DebugPlacer2 : QuickTileItem
    {
        public override string Texture => "StarlightRiver/Items/Debug/DebugPotion";

        public DebugPlacer2() : base("Debug Placer 2", "Suck my huge dragon dong", ModContent.TileType<AncientSandstone>(), 0)
        {
        }
    }

    public class DebugPlacer3 : QuickTileItem
    {
        public override string Texture => "StarlightRiver/Items/Debug/DebugPotion";

        public DebugPlacer3() : base("Debug Placer 3", "Suck my huge dragon dong", ModContent.TileType<Tiles.Overgrow.Blocks.GrassOvergrow>(), 0)
        {
        }
    }
    public class DebugPotion3 : ModItem
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
            item.createTile = ModContent.TileType<Tiles.Misc.SandscriptTile>();
        }

        public override string Texture => "StarlightRiver/MarioCumming";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Clentaminator");
            Tooltip.SetDefault("Effects vary");
        }

        public override bool UseItem(Player player)
        {
            for (int x = 0; x < Main.maxTilesX; x++)
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    WorldGen.Convert(x, y, 0);
                }
            return true;
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
            item.createTile = ModContent.TileType<Tiles.Misc.SandscriptTile>();
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
            Helper.NewItemPerfect(player.Center + new Vector2(10, -30), Vector2.Normalize(player.Center - Main.MouseWorld).RotatedByRandom(0.3f) * -25, Main.rand.Next(ItemID.Count), 1);
            return true;
        }

        public override void HoldItem(Player player)
        {
            //StarlightRiver.Rotation = (player.Center - Main.MouseWorld).ToRotation() - 1.58f;
        }
    }
}
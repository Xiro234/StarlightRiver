using Microsoft.Xna.Framework;
using System.Linq;
using StarlightRiver.Abilities;
using StarlightRiver.Codex;
using StarlightRiver.Gases;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using Terraria.IO;
using SubworldLibrary;

namespace StarlightRiver.Items.Debug
{
    public class Kill : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 512;
            item.height = 512;
            item.damage = 1000;
            item.useStyle = 1;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = 1;
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
            item.useStyle = 1;
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
            item.useStyle = 1;
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
            item.useStyle = 1;
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
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = ModContent.TileType<Tiles.Void.Seal>();
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
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = ModContent.TileType<Tiles.Rift.MainRift>();
        }

        public override bool UseItem(Player player)
        {
            int i = 0;
            string types = "Types: ";
            foreach (NPC npc in Main.npc.Where(npc => npc.type != 0)) { i++; types += npc.type + ", "; }

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
        public DebugPlacer1() : base("Debug Placer 1", "Suck my huge dragon dong", ModContent.TileType<Tiles.Overgrow.VineOvergrow>(), 0) { }
    }
    public class DebugPlacer2 : QuickTileItem
    {
        public override string Texture => "StarlightRiver/Items/Debug/DebugPotion";
        public DebugPlacer2() : base("Debug Placer 2", "Suck my huge dragon dong", ModContent.TileType<Tiles.Rift.MainRift>(), 0) { }
    }
    public class DebugPlacer3 : QuickTileItem
    {
        public override string Texture => "StarlightRiver/Items/Debug/DebugPotion";
        public DebugPlacer3() : base("Debug Placer 3", "Suck my huge dragon dong", ModContent.TileType<Tiles.StarJuice.CrystalBlock>(), 0) { }
    }

    public class DebugPotion : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;
            item.useStyle = 5;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = 2;
            item.createTile = ModContent.TileType<Tiles.Vitric.VitricBossAltar>();
        }
        public override string Texture => "StarlightRiver/MarioCumming";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Potion of Debugging");
            Tooltip.SetDefault("Effects vary");
        }

        public override bool UseItem(Player player)
        {
            player.position = Vector2.One * -100;
            return true;
        }
    }

    public class DebugPotion2 : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;
            item.useStyle = 5;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = 2;
            item.createWall = ModContent.WallType<Tiles.Overgrow.WallOvergrowInvisible>();
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
            LegendWorld.SealOpen = false;
            return true;
        }
    }
    public class RedKiller : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.damage = 10;
            item.useStyle = 1;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = 7;
            item.GetGlobalItem<ShieldBreakingItem>().RedHurting = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("RedKiller");
            Tooltip.SetDefault("Debugging Item");
        }
    }
}


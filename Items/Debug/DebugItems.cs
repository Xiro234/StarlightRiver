using StarlightRiver.Abilities;
using StarlightRiver.Gases;
using Terraria;
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
                    item.damage -= 10;
                }
                if (player.controlUp)
                {
                    item.damage += 10;
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
            item.createTile = ModContent.TileType<Tiles.Seal>();
        }
    }
    public class FleshPlacer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Viner");
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
            item.createTile = ModContent.TileType<Tiles.VineOvergrow>();
        }
    }

    public class GasMaker : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;
            item.useStyle = 5;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = 1;
            item.createTile = ModContent.TileType<Tiles.GasVent>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fart");
            Tooltip.SetDefault("Haha poopy haha stinky haha funny");
        }
    }
}


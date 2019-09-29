using StarlightRiver.Ability;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Debug
{
    public class Reset : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;
            item.useStyle = 5;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reset Button");
            Tooltip.SetDefault("Resets all movement abilities");
        }

        public override bool UseItem(Player player)
        {
            AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
            for(int k = 0; k < mp.unlock.Length; k++)
            {
                mp.unlock[k] = 0;
            }
            for (int k = 0; k < mp.upgradeUnlock.Length; k++)
            {
                mp.upgradeUnlock[k] = 0;
            }
            for (int k = 0; k < mp.upgrade.Length; k++)
            {
                mp.upgrade[k] = 0;
            }
            mp.permanentstamina = 0;
            mp.HasSecondSlot = false;
            return true;
        }
    }
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

    public class Unlock : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;
            item.useStyle = 5;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unlock");
            Tooltip.SetDefault("Unlockss all movement abilities");
        }

        public override bool UseItem(Player player)
        {
            AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
            for (int k = 0; k < mp.unlock.Length; k++)
            {
                mp.unlock[k] = 1;
            }
            for (int k = 0; k < mp.upgradeUnlock.Length; k++)
            {
                mp.upgradeUnlock[k] = 1;
            }
            mp.permanentstamina = 2;
            mp.HasSecondSlot = true;
            return true;
        }
    }
    public class Plus : Reset
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plus");
            Tooltip.SetDefault("+1 Max Stamina!");
        }
        public override bool UseItem(Player player)
        {
            AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
            if (mp.permanentstamina < 2)
            {
                mp.permanentstamina++;
            }
            return true;
        }
    }
    public class Fill : Reset
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fill");
            Tooltip.SetDefault("Fills up your stamina!");
        }
        public override bool UseItem(Player player)
        {
            AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
            mp.stamina = mp.staminamax;
            return true;
        }
    }
    public class Infinite : Reset
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinite");
            Tooltip.SetDefault("Fills up your stamina all the time!");
        }
        public override bool UseItem(Player player)
        {
            AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
            mp.infiniteStamina = ((mp.infiniteStamina) ? false : true);
            return true;
        }
    }

    public class Wisper : Reset
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wisper");
            Tooltip.SetDefault("Spawns a wisp");
        }
        public override bool UseItem(Player player)
        {
            NPC.NewNPC((int)Main.screenPosition.X + Main.mouseX, (int)Main.screenPosition.Y + Main.mouseY, mod.NPCType("DesertWisp2"));
            return true;
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
            item.useTime = 1;
            item.useStyle = 1;
            item.consumable = false;
            item.shoot = mod.ProjectileType("Clentam");
            item.shootSpeed = 10f;
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
            item.useTime = 1;
            item.useStyle = 1;
            item.consumable = false;
            item.shoot = mod.ProjectileType("Clentam2");
            item.shootSpeed = 10f;
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
            item.useTime = 1;
            item.useStyle = 1;
            item.consumable = false;
            item.shoot = mod.ProjectileType("Clentam3");
            item.shootSpeed = 10f;
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
            item.createTile = mod.TileType<Tiles.Seal>();
        }
    }
    public class FleshPlacer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flesh Placer");
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
            item.createTile = mod.TileType<Tiles.HellGate>();
        }
    }
}


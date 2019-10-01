using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    class StaminaGemItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Use any ability on this to gain stamina!\n5 second cooldown");
            DisplayName.SetDefault("Stamina Gem");
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
            item.createTile = mod.TileType("StaminaGem");
        }
    }
    class StaminaOrbItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Pass through this to gain stamina!\n5 second cooldown");
            DisplayName.SetDefault("Stamina Orb");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(3, 6));
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
            item.createTile = mod.TileType("StaminaOrb");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{
    public class Ivy : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A common, yet versatile herb");
            DisplayName.SetDefault("Forest Ivy");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = 2;
        }
    }
    public class Deathstalk : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grows on Rich Soil");
            DisplayName.SetDefault("Deathstalk");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = 2;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("Deathstalk");
        }
    }
    public class IvySeeds : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Can grow in hanging planters");
            DisplayName.SetDefault("Forest Ivy Seeds");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = 1;
        }
    }
}

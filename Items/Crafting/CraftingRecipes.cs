using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Crafting
{
    public class AstralOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Aluminum Ore");
            Tooltip.SetDefault("It gives off a faint warmth");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 250;
            item.value = 100;
            item.rare = 2;
        }
    }
    public class AstralBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Aluminum Bar");
            Tooltip.SetDefault("It burns brightly in your hand");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 250;
            item.value = 100;
            item.rare = 2;
        }
    }
}
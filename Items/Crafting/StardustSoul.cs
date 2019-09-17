using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Crafting
{
    public class StardustSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skyshard");
            Tooltip.SetDefault("A glowing fragment of the skies");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 9));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 14;
            item.height = 14;
            item.rare = 4;
            item.maxStack = 999;

        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, .0f, .6f, .6f) ;
            item.position.Y += (float)Math.Sin(LegendWorld.rottime) / 3;
        }
    }
}

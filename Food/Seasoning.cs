using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Food
{
    class Seasoning : ModItem
    {
        public string IName;
        public string ITooltip;
        public int Modifier = 0;
        public float StrengthMod = 0;
        public int Fill = 0;
        public Seasoning(string name, int modifier, int strengthmod, int filling)
        {
            Modifier = modifier;
            StrengthMod = strengthmod;
            Fill = filling;
            IName = name;
            ITooltip = "+ " + Modifier / 60 + " Seconds Duration\n" + "+ " + ((StrengthMod -1) * 100) + "% Power";
        }

        public override bool CloneNewInstances => true;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\n\n\n");
        }
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.width = 32;
            item.height = 32;
            item.useStyle = 1;
            item.useTime = 30;
            item.useAnimation = 30;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName") { line.text = IName; }
                if (line.mod == "Terraria" && line.Name == "Tooltip0") { line.text = "Seasoning"; line.overrideColor = new Color(140, 200, 255); }
                if (line.mod == "Terraria" && line.Name == "Tooltip1") { line.text = ITooltip; line.overrideColor = new Color(80, 140, 255); }
            }
        }
    }

    class Salt : Seasoning { public Salt() : base("Salt", 3600, 1, 600) { } }

}

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
    class MainCourse : ModItem
    {
        public string IName;
        public string ITooltip;
        public int Buff = 0;
        public float Strength = 0;
        public int Fill = 0;
        public MainCourse(string name, string tooltip, int buffID, float strength, int filling)
        {
            Buff = buffID;
            Strength = strength;
            Fill = filling;
            IName = name;
            ITooltip = tooltip;
        }

        public override bool CloneNewInstances => true;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\n\n");
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
                if (line.mod == "Terraria" && line.Name == "Tooltip0") { line.text = "Main Course"; line.overrideColor = new Color(255, 220, 140); }
                if (line.mod == "Terraria" && line.Name == "Tooltip1") { line.text = "+ " + Strength + ITooltip; line.overrideColor = new Color(255, 220, 80); }
            }
        }
    }

    class RoastRabbit : MainCourse { public RoastRabbit() : base("Roast Rabbit", "% Damage", 1, 10, 3600) { } }

}

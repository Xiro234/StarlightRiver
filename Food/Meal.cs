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
    class Meal : ModItem
    {
        public MainCourse mains = null;
        public SideCourse side1 = null;
        public SideCourse side2 = null;
        public Seasoning seasoning = null;
        public override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meal");
            Tooltip.SetDefault("Food that shouldn't exist. You probably shouldnt eat this. Please report me to the devs!\n\n\n\n");
        }

        public override void SetDefaults()
        {
            item.consumable = true;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 1;
            item.width = 32;
            item.height = 32;
        }
        public override bool CanUseItem(Player player)
        {
            int totalfill = 0;
            player.AddBuff(mains.Buff, (seasoning != null) ? seasoning.Modifier : 3600); totalfill += mains.Fill;
            if (side1 != null) { player.AddBuff(side1.Buff, (seasoning != null) ? seasoning.Modifier : 3600); totalfill += side1.Fill; }
            if (side2 != null) { player.AddBuff(side2.Buff, (seasoning != null) ? seasoning.Modifier : 3600); totalfill += side2.Fill; }
            if (seasoning != null) { totalfill += seasoning.Fill; }

            player.AddBuff(mod.BuffType("Full"), totalfill);
            return true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string join1 = (side1 != null) ? " With " : "";
            string join2 = (side2 != null) ? " And " : "";
            string prefix = (seasoning != null) ? seasoning.IName + "ed " : "";
            string title1 = (mains != null) ? mains.IName : "";
            string title2 = (side1 != null) ? side1.IName : "";
            string title3 = (side2 != null) ? side2.IName : "";

            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName" && mains != null) { line.text = prefix + title1 + join1 + title2 + join2 + title3; }
                if (line.mod == "Terraria" && line.Name == "Tooltip0" && mains != null) { line.text = mains.ITooltip; line.overrideColor = new Color(255, 220, 140); }
                if (line.mod == "Terraria" && line.Name == "Tooltip1" && side1 != null) { line.text = side1.ITooltip; line.overrideColor = new Color(140, 255, 140); }
                if (line.mod == "Terraria" && line.Name == "Tooltip2" && side2 != null) { line.text = side2.ITooltip; line.overrideColor = new Color(140, 255, 140); }
                else if (line.mod == "Terraria" && line.Name == "Tooltip2" && seasoning != null) { line.text = seasoning.ITooltip; line.overrideColor = new Color(140, 200, 255); }
                if (line.mod == "Terraria" && line.Name == "Tooltip3" && side2 != null && seasoning != null) { line.text = seasoning.ITooltip; line.overrideColor = new Color(140, 200, 255); }

                int totalfill = 0;
                if (mains != null) { totalfill += mains.Fill; }
                if (side1 != null) { totalfill += side1.Fill; }
                if (side2 != null) {  totalfill += side2.Fill; }
                if (seasoning != null) { totalfill += seasoning.Fill; }

                if (line.mod == "Terraria" && line.Name == "Tooltip4") { line.text = "Fullness: " + totalfill / 60; line.overrideColor = new Color(255, 163, 153); }
            }
        }
    }
}

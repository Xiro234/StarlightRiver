using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Dragons
{
    class Egg : SoulboundItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
        }

        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine nameline = tooltips.FirstOrDefault(n => n.Name == "ItemName" && n.mod == "Terraria");
            nameline.text = Main.LocalPlayer.name + "`s Dragon Egg";
            nameline.overrideColor = new Color(255, 120, 0);

            TooltipLine line = new TooltipLine(mod, "n", "Perhaps it would hatch if it had a nest...");
            line.overrideColor = new Color(255, 255, 200);
            tooltips.Add(line);
        }
    }
}

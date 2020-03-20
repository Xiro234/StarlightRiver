using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    class SoulboundItem : ModItem
    {
        public override bool Autoload(ref string name)
        {
            return GetType().IsSubclassOf(typeof(SoulboundItem));
        }

        public virtual void SafeModifyTooltips(List<TooltipLine> tooltips) { }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            SafeModifyTooltips(tooltips);

            TooltipLine line = new TooltipLine(mod, "binding", "Soulbound");
            line.overrideColor = new Color(150, 255, 255);
            tooltips.Add(line);
        }
    }
}

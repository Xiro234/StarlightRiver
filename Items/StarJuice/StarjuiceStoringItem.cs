using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace StarlightRiver.Items.StarJuice
{
    class StarjuiceStoringItem : ModItem
    {
        public int maxCharge = 100;
        public int charge = 0;

        public StarjuiceStoringItem(int maxcap)
        {
            maxCharge = maxcap;
        }

        public void Refuel(Tiles.StarJuice.TankEntity entity)
        {
            int needed = maxCharge - charge;
            int available = entity.charge;

            if (needed > 0)
            {
                if (available >= needed)
                {
                    charge += needed;
                    entity.charge -= needed;
                }
                else
                {
                    charge += entity.charge;
                    entity.charge = 0;
                }
            }
        }

        public override bool CloneNewInstances => true;

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(mod, "Starlight", "Starlight: " + charge + "/" + maxCharge);
            line.overrideColor = new Color(140, 220, 255);
            tooltips.Add(line);
        }
    }
}

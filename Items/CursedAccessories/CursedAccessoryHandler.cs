using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.CursedAccessories
{
    class CursedAccessoryHandler : ModPlayer
    {
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            //updates visuals for cursed accessories
            CursedAccessory.Bootlegdust.ForEach(BootlegDust => BootlegDust.Update());
            CursedAccessory.Bootlegdust.RemoveAll(BootlegDust => BootlegDust.time <= 0);
        }

        public override void PreUpdate()
        {
            for(int k = 3; k <= 8+player.extraAccessorySlots; k++)
            {
                if(player.armor[k].modItem is CursedAccessory)
                {
                    Item item = player.armor[k];
                }
            }

        }
    }
}

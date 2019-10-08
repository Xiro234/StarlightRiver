using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

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
    }
}

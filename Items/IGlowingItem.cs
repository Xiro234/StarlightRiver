using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    interface IGlowingItem
    {
        void DrawGlowmask(PlayerDrawInfo info);
    }
}

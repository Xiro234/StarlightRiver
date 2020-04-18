using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Gores
{
    class ChainGore : ModGore
    {
        public override void OnSpawn(Gore gore)
        {
            gore.timeLeft = 180;
        }
    }
}

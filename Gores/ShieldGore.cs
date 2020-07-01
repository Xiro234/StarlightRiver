﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Gores
{
    class ShieldGore : ModGore
    {
        public override Color? GetAlpha(Gore gore, Color lightColor) => Color.White * (0.2f * (gore.timeLeft / 180f));

        public override void OnSpawn(Gore gore) => gore.timeLeft = 180;
    }
}

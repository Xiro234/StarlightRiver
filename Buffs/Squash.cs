﻿using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace StarlightRiver.Buffs
{
    class Squash : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Pancaked");
            Description.SetDefault("Flat ass lookin' headass");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}

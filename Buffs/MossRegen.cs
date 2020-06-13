﻿using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Buffs
{
    public class MossRegen : SmartBuff
    {
        public MossRegen() : base("Mending Moss", "Regenerating life quickly!", false) { }
        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 10;
        }
    }
}
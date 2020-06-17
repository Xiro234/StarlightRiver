﻿using Microsoft.Xna.Framework;
using Terraria;

namespace StarlightRiver.Buffs
{
    internal class Illuminant : SmartBuff
    {
        public Illuminant() : base("Illuminant", "Glowing brightly!", true)
        {
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (Main.rand.Next(4) == 0)
            {
                int i = Dust.NewDust(npc.position, npc.width, npc.height, 264, 0, 0, 0, new Color(255, 255, 200));
                Main.dust[i].noGravity = true;
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (Main.rand.Next(4) == 0)
            {
                int i = Dust.NewDust(player.position, player.width, player.height, 264, 0, 0, 0, new Color(255, 255, 200));
                Main.dust[i].noGravity = true;
            }

            player.aggro += 999;
        }
    }
}
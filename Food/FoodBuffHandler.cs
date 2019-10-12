using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace StarlightRiver.Food
{
    class FoodBuffHandler : ModPlayer
    {
        public int[] Buffs = new int[] { 0, 0, 0 };
        public int[] Powers = new int[] { 0, 0, 0 };
        public bool Fed = false;
        public bool Full = false;

        public override void PostUpdateBuffs()
        {
            for(int slot = 0; slot <= 2; slot++)
            {
                if(Buffs[slot] != 0 && Fed)
                {
                    switch (Buffs[slot])
                    {
                        case 1: player.allDamage += Powers[slot] / 100f; break; // 1, universal damage
                        case 6: player.statDefense += Powers[slot]; break; // 6, defense damage
                    }
                }
            }
        }

        public override void ResetEffects()
        {
            Fed = false;
            Full = false;
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(Buffs)] = Buffs,
                [nameof(Powers)] = Powers,
            };
        }

        public override void Load(TagCompound tag)
        {
            Buffs = tag.GetIntArray(nameof(Buffs));
            Powers = tag.GetIntArray(nameof(Powers));
        }
    }
}

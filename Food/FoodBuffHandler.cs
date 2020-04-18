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
        public List<Item> Consumed { get; set; } //all of the ingredients in the food the player ate
        public float Multiplier { get; set; } //the multipler that should be applied to those ingredients

        public override void PostUpdateBuffs()
        {
            if (!player.HasBuff(ModContent.BuffType<Buffs.FoodBuff>()) && Consumed.Count > 0) { Consumed.Clear(); Multiplier = 1; } //clears the player's "belly" if they're not under the effects of food anymore, also resets the multiplier just in case.
        }

        public override void ResetEffects()
        {

        }
    }
}

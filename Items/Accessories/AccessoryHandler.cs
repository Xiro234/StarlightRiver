using Terraria.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using System.Linq;
using StarlightRiver.Abilities;

namespace StarlightRiver.Items.Accessories
{
    class AccessoryHandler : ModPlayer
    {
        public int[] accessories = new int[] //used for tracking what accessories are equipped
        {
             0,0,0
        };
        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();

            if (accessories[2] != 0) //ring of stamina
            {             
                mp.StatStaminaRegenMax -= 60;
            }
            if (accessories[1] != 0) //hourglass
            {
                mp.StatStaminaMaxTemp += 1;
            }           
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (accessories[0] != 0) //bloody scarf
            {
                AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
                mp.StatStamina += 1;
            }
            return true;
        }
        public override void ResetEffects()
        {
            for (int k = 0; k < accessories.Length; k++)
            {
                if (accessories[k] != 0)
                {
                    accessories[k] = 0;
                }
            }
        }
    }
}


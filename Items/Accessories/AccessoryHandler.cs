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
using spritersguildwip.Ability;

namespace spritersguildwip.Items.Accessories
{
    class AccessoryHandler : ModPlayer
    {
        public int[] accessories = new int[] //used for tracking what accessories are equipped
        {
             0,0,0
        };
        public override void PostUpdate()
        {
            if (accessories[2] != 0) //ring of stamina
            {
                AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
                mp.staminaTickerMax -= 60;
            }
            if (accessories[1] != 0) //hourglass
            {
                AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
                mp.staminamax += 1;
            }
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (accessories[0] != 0) //bloody scarf
            {
                AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();
                mp.stamina += 1;
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
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


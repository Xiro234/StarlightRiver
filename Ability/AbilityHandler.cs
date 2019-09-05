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

namespace spritersguildwip.Ability
{
    class AbilityHandler : ModPlayer
    {
        public int[] unlock = new int[]
        {
             0,0,0,0
        };
        public Ability ability { get; set; }
        public bool infiniteStamina = false;
        public int staminamax = 3;
        public int permanentstamina = 0;
        public int stamina = 3;
        int staminaticker = 0;
        public int staminaTickerMax = 180;

        //accessory stuff
        public override TagCompound Save()
        {
            return new TagCompound {
                [nameof(unlock)] = unlock,
                [nameof(staminamax)] = staminamax,
                [nameof(permanentstamina)] = permanentstamina
            };
        }

        public override void Load(TagCompound tag)
        {
            unlock = tag.GetIntArray(nameof(unlock));
            staminamax = tag.GetInt(nameof(staminamax));
            permanentstamina = tag.GetInt(nameof(permanentstamina));
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {          
            if (spritersguildwip.Dash.JustPressed)
            {
                ability = new Dash();
            }

            ability?.Player = this;
            ability?.ConsumeStamina();
            ability?.OnCast();
        }
        public override void ResetEffects()
        {

        }
        public override void PreUpdate()
        {
            if (ability != null && ability.Active)
            {
                ability.InUse();
            }           
        }
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {

        }
    }
}

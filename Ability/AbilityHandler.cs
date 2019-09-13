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

namespace StarlightRiver.Ability
{
    class AbilityHandler : ModPlayer
    {
        public int[] unlock = new int[]
        {
             0,0,0,0,0
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
            if (StarlightRiver.Dash.JustPressed && unlock[0] == 1)
            {
                ability = new Dash();
            }
            if (StarlightRiver.Float.JustPressed && unlock[1] == 1)
            {
                ability = new Float();
            }
            if (StarlightRiver.Purify.JustPressed && unlock[2] == 1)
            {
                ability = new Pure();
            }
            if (StarlightRiver.Smash.JustPressed && unlock[3] == 1)
            {
                ability = new Smash();
            }
            if (StarlightRiver.Superdash.JustPressed && unlock[4] == 1)
            {
                ability = new Superdash();
            }

            if (ability == null || ability.Active) { return; }

            if (stamina >= ability.StaminaCost)
            {
                ability.Handler = this;
                ability.ConsumeStamina();
                ability.OnCast();
            }
        }
        public override void ResetEffects()
        {
            staminamax = 3;
        }
        public override void PreUpdate()
        {
            if (ability != null && ability.Active)
            {
                ability.InUse();
            }
            if (ability != null && !ability.Active)
            {
                ability = null;
            }

            if (infiniteStamina)
            {
                stamina = (staminamax + permanentstamina);
                return;
            }
            if (staminaticker++ >= staminaTickerMax && stamina < (staminamax + permanentstamina) && !(ability is Float && ability.Active))
            {
                stamina++;
            }
            if (staminaticker > staminaTickerMax || stamina == (staminamax + permanentstamina))
            {
                staminaticker = 0;
            }
        }
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if((ability is Float || ability is Superdash) && ability.Active)
            {
                foreach(PlayerLayer layer in layers)
                {
                    layer.visible = false;
                }
            }
        }
    }
}

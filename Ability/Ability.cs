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
    class Ability
    {
        public AbilityHandler Handler { get; set; }
        public int StaminaCost { get; set; }
        public bool Active { get; set; }

        public Ability(int staminaCost)
        {
            StaminaCost = staminaCost;
        }

        public void ConsumeStamina()
        {
            {
                Handler.stamina -= StaminaCost;
            }
        }

        public virtual void OnCast()
        {

        }

        public virtual void InUse()
        {

        }

        public virtual void OnExit()
        {

        }
    }
}

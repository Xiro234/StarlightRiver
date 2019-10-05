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
using System.Runtime.Serialization;

namespace StarlightRiver.Abilities
{
    [DataContract]

    [KnownType(typeof(Dash))]
    [KnownType(typeof(DashAstral))]
    [KnownType(typeof(DashFlame))]
    [KnownType(typeof(DashCombo))]

    [KnownType(typeof(Float))]

    [KnownType(typeof(Pure))]

    [KnownType(typeof(Smash))]

    [KnownType(typeof(Superdash))]
    class Ability
    {
        [DataMember] public AbilityHandler Handler { get; set; }
        [DataMember] public int StaminaCost { get; set; }
        [DataMember] public bool Active { get; set; }
        protected Player player => Handler.player;

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

        public virtual void UseEffects()
        {

        }

        public virtual void OnExit()
        {

        }
    }
}

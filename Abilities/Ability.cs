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
using StarlightRiver.Abilities;

namespace StarlightRiver.Abilities
{
    [DataContract]

    [KnownType(typeof(Dash))]
    [KnownType(typeof(DashAstral))]
    [KnownType(typeof(DashFlame))]
    [KnownType(typeof(DashCombo))]

    [KnownType(typeof(Wisp))]

    [KnownType(typeof(Pure))]

    [KnownType(typeof(Smash))]

    [KnownType(typeof(Superdash))]
    public class Ability
    {
        [DataMember] public int StaminaCost { get; set; }
        [DataMember] public bool Active { get; set; }
        public int Timer { get; set; }
        public int Cooldown { get; set; }
        public bool Locked = true;
        public Player player;

        public Ability(int staminaCost, Player Player)
        {
            StaminaCost = staminaCost;
            player = Player;
        }

        public virtual void StartAbility(AbilityHandler handler)
        {
            //if the player: has enough stamina  && unlocked && not on CD     && Has no other abilities active
            if(handler.StatStamina >= StaminaCost && !Locked && Cooldown == 0 && !handler.Abilities.Any(a => a.Active))
            {
                handler.StatStamina -= StaminaCost; //Consume the stamina
                OnCast(); //Do what the ability should do when it starts
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

        public virtual void OffCooldownEffects()
        {

        }

        public virtual void OnExit()
        {

        }
    }
}

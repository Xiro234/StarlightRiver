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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace StarlightRiver.Abilities
{
    class AbilityHandler : ModPlayer
    {
        public int[] unlock = new int[]
        {
             0,0,0,0,0
        };
        public int[] upgradeUnlock = new int[]
        {
             0,0
        };
        public int[] upgrade = new int[]
        {
            0,0
        };
        public Ability ability { get; set; }
        public bool infiniteStamina = false;
        public bool HasSecondSlot = false;
        public int staminamax = 3;
        public int permanentstamina = 0;
        public int stamina = 3;
        int staminaticker = 0;
        public int staminaTickerMax = 180;

        public float store;
        public override void clientClone(ModPlayer clientClone)
        {
            AbilityHandler clone = clientClone as AbilityHandler;
            clone.ability = ability;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {

        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            AbilityHandler clone = clientPlayer as AbilityHandler;
            if (clone.ability != ability && ability != null)
            {
                var packet = mod.GetPacket();
                var ser = new DataContractJsonSerializer(typeof(Ability));
                var ms = new MemoryStream();

                ser.WriteObject(ms, ability);
                byte[] json = ms.ToArray();
                ms.Close();

                packet.Write(json);
                packet.Send();
            }
        }
        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(unlock)] = unlock,
                [nameof(upgradeUnlock)] = upgradeUnlock,
                [nameof(upgrade)] = upgrade,
                [nameof(staminamax)] = staminamax,
                [nameof(permanentstamina)] = permanentstamina,
                [nameof(HasSecondSlot)] = HasSecondSlot
            };
        }

        public override void Load(TagCompound tag)
        {
            unlock = tag.GetIntArray(nameof(unlock));
            upgradeUnlock = tag.GetIntArray(nameof(upgradeUnlock));
            upgrade = tag.GetIntArray(nameof(upgrade));
            staminamax = tag.GetInt(nameof(staminamax));
            permanentstamina = tag.GetInt(nameof(permanentstamina));
            HasSecondSlot = tag.GetBool(nameof(HasSecondSlot));

            stamina = staminamax + permanentstamina;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (ability == null || !ability.Active)
            {
                if (StarlightRiver.Dash.JustPressed && unlock[0] == 1 && (player.controlUp || player.controlDown || player.controlLeft || player.controlRight))
                {
                    ability = new Dash();
                    if (upgrade[0] == 1 || upgrade[1] == 1) { ability = new DashAstral(); }
                    if (upgrade[0] == 2 || upgrade[1] == 2) { ability = new DashFlame(); }
                    if ((upgrade[0] == 2 || upgrade[1] == 2) && (upgrade[0] == 1 || upgrade[1] == 1)) { ability = new DashCombo(); }
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
                    if (player.wingTime > 0)
                    {
                        store = player.wingTime;
                    }
                    player.wingTime = 0;
                }
            }
        }
        public override void ResetEffects()
        {
            staminamax = 3;
            staminaTickerMax = 180;
        }
        public override void PreUpdate()
        {
            if (ability != null && ability.Active)
            {
                ability.InUse();
                ability.UseEffects();
            }
            if (ability != null && !ability.Active)
            {
                ability = null;

                if (store > 0)
                {
                    player.wingTime = store;
                    store = 0;
                }
                else if (store == 0)
                {
                    player.wingTime = 0;
                }           
            }

            if (player.dead && ability != null)
            {               
                ability.Active = false;
                ability = null;
            }

            if (infiniteStamina)
            {
                stamina = (staminamax + permanentstamina);
                return;
            }
            if (staminaticker++ >= staminaTickerMax && stamina < (staminamax + permanentstamina) && !(ability is Float && ability.Active) && staminaTickerMax > 0 )
            {
                stamina++;
            }
            if (staminaticker > staminaTickerMax || stamina == (staminamax + permanentstamina))
            {
                staminaticker = 0;
            }
            if(stamina > (staminamax + permanentstamina))
            {
                stamina = staminamax + permanentstamina;
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
        //netcode
    }
}

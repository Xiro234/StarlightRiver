using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Dragons;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using static StarlightRiver.StarlightRiver;

namespace StarlightRiver.Abilities
{
    public class Ability
    {
        public int StaminaCost;
        public bool Active;
        public int Timer;
        public int Cooldown;
        public bool Locked = true;
        public virtual Texture2D texture { get; }
        public Player player;
        public virtual bool CanUse { get => true; }

        public Ability(int staminaCost, Player Player)
        {
            StaminaCost = staminaCost;
            player = Player;
        }

        public virtual void StartAbility(Player player)
        {
            AbilityHandler handler = player.GetModPlayer<AbilityHandler>();
            DragonHandler dragon = player.GetModPlayer<DragonHandler>();
            //if the player: has enough stamina  && unlocked && not on CD     && Has no other abilities active
            if (CanUse && handler.StatStamina >= StaminaCost && !Locked && Cooldown == 0 && !handler.Abilities.Any(a => a.Active))
            {
                handler.StatStamina -= StaminaCost; //Consume the stamina
                                                    //if (dragon.DragonMounted) OnCastDragon(); //Do what the ability should do when it starts
                                                    /*else*/
                OnCast();
                Active = true; //Ability is activated

                SendPacket();
            }
        }

        public virtual void OnCast() { }
        public virtual void OnCastDragon() { }

        public virtual void InUse() { }
        public virtual void InUseDragon() { }

        public virtual void UseEffects() { }
        public virtual void UseEffectsDragon() { }

        public virtual void OffCooldownEffects() { }

        public virtual void OnExit() { }

        public virtual void SendPacket(int toWho = -1, int fromWho = -1)
        {
            Player player2;
            if (fromWho != -1) player2 = Main.player[fromWho];
            else player2 = player;
            AbilityHandler handler = player2.GetModPlayer<AbilityHandler>();

            ModPacket packet = StarlightRiver.Instance.GetPacket();
            packet.Write((byte)SLRPacketType.ability);
            packet.Write(player2.whoAmI);
            packet.Write(handler.Abilities.IndexOf( handler.Abilities.FirstOrDefault(n => n.GetType() == this.GetType() ) ) );
            packet.Write(Active);
            packet.Write(Timer);
            packet.Send(toWho, fromWho);
        }
    }
}

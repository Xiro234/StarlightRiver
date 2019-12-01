using Microsoft.Xna.Framework;
using StarlightRiver.Projectiles;
using StarlightRiver.Projectiles.Ability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Abilities
{
    [DataContract]
    public class Pure : Ability
    {
        
        public Pure(Player player) : base(4, player)
        {

        }

        public override void StartAbility(AbilityHandler handler)
        {
            //if the player: has enough stamina  && unlocked && not on CD     && Has no other abilities active
            if (handler.StatStamina >= StaminaCost && !Locked && Cooldown == 0 && !handler.Abilities.Any(a => a.Active) && 
                !(Main.projectile.Any(proj => proj.owner == player.whoAmI && proj.active && (proj.type == ModContent.ProjectileType<Purifier>() || proj.type == ModContent.ProjectileType<PurifierReturn>()))))
            {
                handler.StatStamina -= StaminaCost; //Consume the stamina
                OnCast(); //Do what the ability should do when it starts
            }
        }

        public override void OnCast()
        {
            Active = true;
            Main.PlaySound(SoundID.Item37);
            Cooldown = 600;
        }

        public override void InUse()
        {
            Projectile.NewProjectile(player.Center + new Vector2(16, -24), Vector2.Zero, ModContent.ProjectileType<Purifier>(), 0, 0, player.whoAmI);
            LegendWorld.PureTiles.Add((player.Center + new Vector2(16, -24)) / 16);

            Active = false;
            OnExit();

        }

        public override void OnExit()
        {

        }
    }
}

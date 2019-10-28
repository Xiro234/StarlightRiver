using Microsoft.Xna.Framework;
using StarlightRiver.Dusts;
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
    public class WispHoming : Wisp
    {        
        [DataMember] bool exit = false;
        [DataMember] int shootTime = 0;
        public WispHoming(Player player) : base(player)
        {

        }

        public override void InUse()
        {
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

            Timer--;
            shootTime++;
            player.noItems = true;
            player.maxFallSpeed = 999;
            player.gravity = 0;
            player.velocity = Vector2.Normalize(new Vector2
                (
                Main.screenPosition.X + Main.mouseX - player.Hitbox.Center.X,
                Main.screenPosition.Y + Main.mouseY - player.Hitbox.Center.Y
                )) * 5 + new Vector2(0.25f, 0.25f);

            player.Hitbox = new Rectangle((int)player.Hitbox.X - 7 + 7, (int)player.Hitbox.Y + 21 + 7, 14, 14);

            Lighting.AddLight(player.Center, new Vector3(0.15f, 0.15f, 0f));


            if (shootTime % 20 == 0) { Projectile.NewProjectile(player.Center, new Vector2(1, 1).RotatedByRandom(360), ModContent.ProjectileType<WispBolt>(), 1, 1f, player.whoAmI); }
            if (Timer % 60 == 0 && Timer > 0) { mp.StatStamina--; }
            else if (Timer > 0)
            {
                mp.StatStaminaRegen = (int)((1 - (Timer + 60) % 60 / 60f) * mp.StatStaminaRegenMax);
            }
            else { mp.StatStaminaRegen = mp.StatStaminaRegenMax; }

            if (StarlightRiver.Wisp.JustReleased)
            {
                exit = true;
            }

            if (exit || (mp.StatStamina < 1 && mp.StatStaminaRegen == mp.StatStaminaRegenMax))
            {             
                OnExit();
            }
        }
        public override void UseEffects()
        {
            if (Timer > -1)
            {
                for (int k = 0; k <= 2; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(4, 4), 8, 8, ModContent.DustType<FireDust>(), 0f, 0f, 0, Color.OrangeRed, 5f);
                }
            }
            else
            {
                for (int k = 0; k <= 2; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(4, 4), 8, 8, ModContent.DustType<Dusts.Void>());
                }
            }
        }

        public override void OnExit()
        {
            if (TestExit())
            {
                Timer = 0;
                exit = false;
                player.velocity.X = 0;
                player.velocity.Y = 0;

                for (int k = 0; k <= 30; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, ModContent.DustType<Gold2>(), Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 0, default, 1.2f);
                }
                Active = false;
            }
            else if (Timer < 0)
            {
                player.statLife -= 2;
                if(player.statLife <= 0)
                {
                    player.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason(player.name + " couldn't maintain their form"), 0, 0);
                }
                if (Timer % 10 == 0) { Main.PlaySound(SoundID.PlayerHit, player.Center); }
            }
        }
    }
}

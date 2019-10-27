using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using System;
using StarlightRiver.GUI;
using StarlightRiver.Abilities;
using StarlightRiver.Items.CursedAccessories;

namespace StarlightRiver
{
    public class StarlightPlayer : ModPlayer
    {
        public bool DarkSlow = false;

        public bool VitricSet = false;

        public bool AnthemDagger = false;
		
		public int InvertGrav = 0;
		public override void PreUpdateBuffs()
        {

			if (InvertGrav > 0)
			{
                //Main.NewText("Invert: true");
				player.gravControl = true;
				player.gravDir = -1f;
			}
            else
            {
                //Main.NewText("Invert: false");
            }
        }
		
        public override void PreUpdate()
        {
            Stamina.visible = false;
            Infusion.visible = false;
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

            if (mp.Abilities.Any(a => !a.Locked))
            {
                Stamina.visible = true;
            }

            if (Main.playerInventory)
            {
                Collection.visible = true;
                if (mp.Abilities.Any(a => !a.Locked)) { Infusion.visible = true; }
            }
            else
            {
                Collection.visible = false;
                Infusion.visible = false;
            }

            if (DarkSlow)
            {
                player.velocity.X *= 0.8f;
            }
            DarkSlow = false;
        }

		public override void ResetEffects()
        {
            AnthemDagger = false;
		}

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            //Controls the anthem dagger's mana shield
            if (AnthemDagger)
            {
                if (player.statMana > damage)
                {
                    player.statMana -= damage;
                    player.ManaEffect(damage);
                    damage = 0;
                    player.manaRegenDelay = 0;
                    player.statLife += 1;
                    playSound = false;
                    genGore = false;
                    Main.PlaySound(SoundID.MaxMana);
                    
                }
                else if (player.statMana > 0)
                {
                    player.ManaEffect(player.statMana);
                    damage -= player.statMana;
                    player.statMana = 0;
                    player.manaRegenDelay = 0;
                    Main.PlaySound(SoundID.MaxMana);
                }
            }
            return true;
        }

        public override void PostUpdate()
        {
            //Main.NewText(player.velocity);
            if (InvertGrav > 0)
            {
                if (InvertGrav == 1 && player.velocity.Y < 5 && player.velocity.Y > -5)
                {
                    player.velocity.Y = 0;
                }
                    --InvertGrav;
            }
        }
    }
}

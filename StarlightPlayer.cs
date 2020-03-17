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
    public partial class StarlightPlayer : ModPlayer
    {
        public bool JustHit = false;

        public bool DarkSlow = false;

        public bool AnthemDagger = false;

        public int Shake = 0;

        public int ScreenMoveTime = 0;
        public Vector2 ScreenMoveTarget = new Vector2(0, 0);
        private int ScreenMoveTimer = 0;
		
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
                GUI.Codex.Visible = true;
                if (mp.Abilities.Any(a => !a.Locked)) { Infusion.visible = true; }
            }
            else
            {
                Collection.visible = false;
                GUI.Codex.Visible = false;
                GUI.Codex.Open = false;
                GUI.Codex.Crafting = false;
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
            GuardDamage = 1;
            GuardCrit = 0;
            GuardBuff = 1;
            GuardRad = 0;
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

            if (Main.netMode == 1) { LegendWorld.rottime += (float)Math.PI / 60; } 
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            JustHit = true;
        }
        public override void PostUpdateEquips()
        {
            JustHit = false;
        }


        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if(1 == 0)
            {
                switch (Main.rand.Next(4))
                {
                    case 0: damageSource = PlayerDeathReason.ByCustomReason(player.name + " was juiced."); break;
                    case 1: damageSource = PlayerDeathReason.ByCustomReason(player.name + " wanted a closer look at the grass."); break;
                    case 2: damageSource = PlayerDeathReason.ByCustomReason(player.name + " needs some syrup."); break;
                    case 3: damageSource = PlayerDeathReason.ByCustomReason(player.name + " fused with the floor."); break;
                }             
            }
            return true;
        }

        public override void ModifyScreenPosition()
        {
            Main.screenPosition.Y += Main.rand.Next(-Shake, Shake);
            Main.screenPosition.X += Main.rand.Next(-Shake, Shake);
            if (Shake > 0) { Shake--; }

            if(ScreenMoveTime > 0&& ScreenMoveTarget != Vector2.Zero)
            {
                Vector2 off = new Vector2(Main.screenWidth, Main.screenHeight) / -2;
                if (ScreenMoveTimer <= 30) //go out
                {
                    Main.screenPosition = Vector2.SmoothStep(Main.LocalPlayer.Center + off, ScreenMoveTarget + off, ScreenMoveTimer / 30f);
                }
                else if (ScreenMoveTimer >= ScreenMoveTime - 30) //go in
                {
                    Main.screenPosition = Vector2.SmoothStep(ScreenMoveTarget + off, Main.LocalPlayer.Center + off, (ScreenMoveTimer - (ScreenMoveTime - 30)) / 30f);
                }
                else Main.screenPosition = ScreenMoveTarget + off; //stay on target

                if (ScreenMoveTimer == ScreenMoveTime) { ScreenMoveTime = 0; ScreenMoveTimer = 0; ScreenMoveTarget = Vector2.Zero; }
                ScreenMoveTimer++;
            }
        }
    }
}

﻿using Microsoft.Xna.Framework;
using StarlightRiver.Abilities;
using StarlightRiver.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver
{
    public partial class StarlightPlayer : ModPlayer
    {
        public int Timer { get; private set; }

        public bool JustHit = false;
        public int LastHit = 0;

        public bool DarkSlow = false;

        public bool AnthemDagger = false;

        public int Shake = 0;

        public int ScreenMoveTime = 0;
        public Vector2 ScreenMoveTarget = new Vector2(0, 0);
        public Vector2 ScreenMovePan = new Vector2(0, 0);
        private int ScreenMoveTimer = 0;

        public int InvertGrav = 0;
        public int platformTimer = 0;

        public int PickupTimer = 0;
        public int MaxPickupTimer = 0;
        public NPC PickupTarget;

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
            if (PickupTarget != null)
            {
                PickupTimer++;

                player.immune = true;
                player.immuneTime = 5;
                player.immuneNoBlink = true;

                player.Center = PickupTarget.Center;
                if (PickupTimer >= MaxPickupTimer) PickupTarget = null;
            }
            else PickupTimer = 0;

            platformTimer--;

            if (player.whoAmI == Main.myPlayer)
            {
                AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

                Stamina.visible = false;
                Infusion.visible = false;

                if (mp.Abilities.Any(a => !a.Locked))
                {
                    Stamina.visible = true;
                }

                if (Main.playerInventory)
                {
                    if (player.chest == -1 && Main.npcShop == 0) Collection.visible = true;
                    else Collection.visible = false;

                    GUI.Codex.ButtonVisible = true;
                    if (mp.Abilities.Any(a => !a.Locked)) Infusion.visible = true;
                }
                else
                {
                    Collection.visible = false;
                    Collection.ActiveAbility = null;
                    GUI.Codex.ButtonVisible = false;
                    GUI.Codex.Open = false;
                    Infusion.visible = false;
                }
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

        #region ModifyHitByProjectile
        //for on-hit effects that require more specific effects, projectiles
        public delegate void ModifyHitByProjectileDelegate(Projectile proj, ref int damage, ref bool crit);
        public static event ModifyHitByProjectileDelegate ModifyHitByProjectileEvent;
        public void OnModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit) { ModifyHitByProjectileEvent?.Invoke(proj, ref damage, ref crit); }
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit) { OnModifyHitByProjectile(proj, ref damage, ref crit); }

        #endregion
        #region ModifyHitByNPC
        //for on-hit effects that require more specific effects, contact damage
        public delegate void ModifyHitByNPCDelegate(NPC npc, ref int damage, ref bool crit);
        public static event ModifyHitByNPCDelegate ModifyHitByNPCEvent;
        public void OnModifyHitByNPC(NPC npc, ref int damage, ref bool crit) { ModifyHitByNPCEvent?.Invoke(npc, ref damage, ref crit); }
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit) { OnModifyHitByNPC(npc, ref damage, ref crit); }
        #endregion
        #region ModifyHitNPC
        //For stuff like fire gauntlet
        public delegate void ModifyHitNPCDelegate(Item item, NPC target, ref int damage, ref float knockback, ref bool crit);
        public static event ModifyHitNPCDelegate ModifyHitNPCEvent;
        public void OnModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit) { ModifyHitNPCEvent?.Invoke(item, target, ref damage, ref knockback, ref crit); }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit) { OnModifyHitNPC(item, target, ref damage, ref knockback, ref crit); }
        #endregion
        #region PreHurt
        //this is the grossest one. I am sorry, little ones.
        public delegate bool PreHurtDelegate(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource);
        public static event PreHurtDelegate PreHurtEvent;
        public bool OnPreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            return (bool)PreHurtEvent?.Invoke(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            return OnPreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
        #endregion

        /*
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
            }*/


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

            if (Main.netMode == NetmodeID.MultiplayerClient && player == Main.LocalPlayer) { LegendWorld.rottime += (float)Math.PI / 60; }

            Timer++;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            JustHit = true;
            LastHit = Timer;
        }

        public override void PostUpdateEquips()
        {
            JustHit = false;
        }

        public override void ModifyScreenPosition()
        {
            if (ScreenMoveTime > 0 && ScreenMoveTarget != Vector2.Zero)
            {
                Vector2 off = new Vector2(Main.screenWidth, Main.screenHeight) / -2;
                if (ScreenMoveTimer <= 30) //go out
                {
                    Main.screenPosition = Vector2.SmoothStep(Main.LocalPlayer.Center + off, ScreenMoveTarget + off, ScreenMoveTimer / 30f);
                }
                else if (ScreenMoveTimer >= ScreenMoveTime - 30) //go in
                {
                    Main.screenPosition = Vector2.SmoothStep((ScreenMovePan == Vector2.Zero ? ScreenMoveTarget : ScreenMovePan) + off, Main.LocalPlayer.Center + off, (ScreenMoveTimer - (ScreenMoveTime - 30)) / 30f);
                }
                else
                {
                    if (ScreenMovePan == Vector2.Zero) Main.screenPosition = ScreenMoveTarget + off; //stay on target
                    else if (ScreenMoveTimer <= ScreenMoveTime - 150) Main.screenPosition = Vector2.Lerp(ScreenMoveTarget + off, ScreenMovePan + off, ScreenMoveTimer / (float)(ScreenMoveTime - 150));
                    else Main.screenPosition = ScreenMovePan + off;
                }

                if (ScreenMoveTimer == ScreenMoveTime) { ScreenMoveTime = 0; ScreenMoveTimer = 0; ScreenMoveTarget = Vector2.Zero; ScreenMovePan = Vector2.Zero; }
                ScreenMoveTimer++;
            }

            Main.screenPosition.Y += Main.rand.Next(-Shake, Shake);
            Main.screenPosition.X += Main.rand.Next(-Shake, Shake);
            if (Shake > 0) { Shake--; }
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if (player.HeldItem.modItem is Items.Vitric.VitricSword && (player.HeldItem.modItem as Items.Vitric.VitricSword).Broken) PlayerLayer.HeldItem.visible = false;

            Action<PlayerDrawInfo> layerTarget = DrawGlowmasks; //the Action<T> of our layer. This is the delegate which will actually do the drawing of the layer.
            PlayerLayer layer = new PlayerLayer("ExampleSwordLayer", "Sword Glowmask", layerTarget); //Instantiate a new instance of PlayerLayer to insert into the list
            layers.Insert(layers.IndexOf(layers.FirstOrDefault(n => n.Name == "Arms")), layer); //Insert the layer at the appropriate index.

            void DrawGlowmasks(PlayerDrawInfo info)
            {
                if (info.drawPlayer.HeldItem.modItem is Items.IGlowingItem) (info.drawPlayer.HeldItem.modItem as Items.IGlowingItem).DrawGlowmask(info);
            }
        }

        public override void OnEnterWorld(Player player)
        {
            Collection.ShouldReset = true;
        }
    }
}
 
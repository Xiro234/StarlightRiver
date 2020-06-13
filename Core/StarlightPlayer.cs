using Microsoft.Xna.Framework;
using StarlightRiver.Abilities;
using StarlightRiver.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Core
{
    public partial class StarlightPlayer : ModPlayer
    {
        public int Timer { get; private set; }

        public bool JustHit = false;
        public int LastHit = 0;

        public bool DarkSlow = false;

        public int Shake = 0;

        public int ScreenMoveTime = 0;
        public Vector2 ScreenMoveTarget = new Vector2(0, 0);
        public Vector2 ScreenMovePan = new Vector2(0, 0);
        private int ScreenMoveTimer = 0;

        public int platformTimer = 0;

        public int PickupTimer = 0;
        public int MaxPickupTimer = 0;
        public NPC PickupTarget;

        public float GuardDamage;
        public int GuardCrit;
        public float GuardBuff;
        public int GuardRad;

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
        #region ResetEffects
        public delegate void ResetEffectsDelegate(Player player);
        public static event ResetEffectsDelegate ResetEffectsEvent;
        public override void ResetEffects()
        {
            ResetEffectsEvent?.Invoke(player);
            GuardDamage = 1;
            GuardCrit = 0;
            GuardBuff = 1;
            GuardRad = 0;
        }
        #endregion
        #region ModifyHitByProjectile
        //for on-hit effects that require more specific effects, projectiles
        public delegate void ModifyHitByProjectileDelegate(Player player, Projectile proj, ref int damage, ref bool crit);
        public static event ModifyHitByProjectileDelegate ModifyHitByProjectileEvent;
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit) { ModifyHitByProjectileEvent?.Invoke(player, proj, ref damage, ref crit); }

        #endregion
        #region ModifyHitByNPC
        //for on-hit effects that require more specific effects, contact damage
        public delegate void ModifyHitByNPCDelegate(Player player, NPC npc, ref int damage, ref bool crit);
        public static event ModifyHitByNPCDelegate ModifyHitByNPCEvent;
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit) { ModifyHitByNPCEvent?.Invoke(player, npc, ref damage, ref crit); }
        #endregion
        #region ModifyHitNPC
        //For stuff like fire gauntlet
        public delegate void ModifyHitNPCDelegate(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit);
        public static event ModifyHitNPCDelegate ModifyHitNPCEvent;
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        { ModifyHitNPCEvent?.Invoke(player, item, target, ref damage, ref knockback, ref crit); }
        #endregion
        #region PreHurt
        //this is the grossest one. I am sorry, little ones.
        public delegate bool PreHurtDelegate(Player player, bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource);
        public static event PreHurtDelegate PreHurtEvent;
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            return (bool)PreHurtEvent?.Invoke(player, pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
        #endregion

        public override void PostUpdate()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient && player == Main.LocalPlayer) { StarlightWorld.rottime += (float)Math.PI / 60; }
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
 
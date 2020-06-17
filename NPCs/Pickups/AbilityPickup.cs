﻿using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Core;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Pickups
{
    internal abstract class AbilityPickup : ModNPC
    {
        /// <summary>
        /// Indicates if the pickup should be visible in-world. Should be controlled using clientside vars.
        /// </summary>
        private bool Visible => CanPickup(Main.LocalPlayer);

        public sealed override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 32;
            npc.lifeMax = 2;
            npc.damage = 1;
            npc.dontTakeDamage = true;
            npc.dontCountMe = true;
            npc.immortal = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.friendly = false;
        }

        public override bool CheckActive()
        {
            return false;
        }

        public sealed override bool? CanBeHitByItem(Player player, Item item)
        {
            return false;
        }

        public sealed override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return false;
        }

        /// <summary>
        /// The clientside visual dust that this pickup makes when in-world
        /// </summary>
        public virtual void Visuals() { }

        /// <summary>
        /// The clientside visual dust taht this pickup makes when being picked up, relative to a timer.
        /// </summary>
        /// <param name="timer">The progression along the animation</param>
        public virtual void PickupVisuals(int timer) { }

        /// <summary>
        /// What happens to the player internally when they touch the pickup. This is deterministically synced.
        /// </summary>
        /// <param name="player"></param>
        public virtual void PickupEffects(Player player) { }

        /// <summary>
        /// if the player should be able to pick this up or not
        /// </summary>
        public virtual bool CanPickup(Player player) { return false; }

        public virtual Color GlowColor => Color.White;

        public sealed override void AI()
        {
            StarlightPlayer mp = Main.LocalPlayer.GetModPlayer<StarlightPlayer>(); //the local player since ability pickup visuals are clientside
            if (Visible)
            {
                Visuals();

                if (Vector2.Distance(Main.screenPosition + new Vector2(Main.screenWidth / 2, Main.screenHeight / 2), npc.Center) <= Main.screenWidth / 2 + 100) //shader
                {
                    float timer = Math.Abs((float)Math.Sin(StarlightWorld.rottime));
                    Filters.Scene.Activate("ShockwaveFilter", npc.Center).GetShader().UseProgress(Main.screenWidth / (float)Main.screenHeight).UseIntensity(300).UseDirection(new Vector2(0.005f + timer * 0.03f, 1 * 0.004f - timer * 0.004f));
                }

                if (Vector2.Distance(Main.LocalPlayer.Center, npc.Center) < 200f) //music handling
                {
                    for (int k = 0; k < Main.musicFade.Length; k++)
                    {
                        if (k == Main.curMusic)
                        {
                            Main.musicFade[k] = Vector2.Distance(Main.LocalPlayer.Center, npc.Center) / 200f;
                        }
                    }
                }
            }

            if (mp.PickupTarget == npc) //if the player is picking this up, clientside only also
            {
                PickupVisuals(mp.PickupTimer);
            }
        }

        public sealed override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            StarlightPlayer mp = target.GetModPlayer<StarlightPlayer>();
            if (CanPickup(target) && target.Hitbox.Intersects(npc.Hitbox))
            {
                PickupEffects(target);
                mp.PickupTarget = npc;
            }
            return false;
        }

        public sealed override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (Visible)
            {
                Texture2D tex = GetTexture(Texture);
                Vector2 pos = npc.Center - Main.screenPosition + new Vector2(0, (float)Math.Sin(StarlightWorld.rottime) * 5);
                spriteBatch.Draw(tex, pos, tex.Frame(), Color.White, 0, tex.Size() / 2, 1, 0, 0);
            }
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (Visible)
            {
                Texture2D tex = GetTexture("StarlightRiver/RiftCrafting/Glow0");
                Vector2 pos = npc.Center - Main.screenPosition + new Vector2(0, (float)Math.Sin(StarlightWorld.rottime) * 5);

                spriteBatch.End();
                spriteBatch.Begin(default, BlendState.Additive);

                spriteBatch.Draw(tex, pos, tex.Frame(), GlowColor * 0.3f, 0, tex.Size() / 2, 1, 0, 0);
                spriteBatch.Draw(tex, pos, tex.Frame(), GlowColor * 0.5f, 0, tex.Size() / 2, 0.6f, 0, 0);

                spriteBatch.End();
                spriteBatch.Begin();
            }
        }
    }
}
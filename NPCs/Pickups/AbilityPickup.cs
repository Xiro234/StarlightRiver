using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Pickups
{
    internal abstract class AbilityPickup : ModNPC
    {
        /// <summary>
        /// Indicates if the pickup should be visible in-world. Should be controlled using clientside vars.
        /// </summary>
        private bool Visible { get => CanPickup(Main.LocalPlayer); }
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
        public override bool CheckActive() => false;
        public sealed override bool? CanBeHitByItem(Player player, Item item) => false;
        public sealed override bool? CanBeHitByProjectile(Projectile projectile) => false;
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

        public sealed override void AI()
        {
            StarlightPlayer mp = Main.LocalPlayer.GetModPlayer<StarlightPlayer>(); //the local player since ability pickup visuals are clientside
            if (Visible) Visuals();
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
        public sealed override bool PreDraw(SpriteBatch spriteBatch, Color drawColor) => Visible;
    }
}

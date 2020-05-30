using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.OvergrowBoss
{
    public class OvergrowBossFlail : ModNPC
    {
        public OvergrowBoss parent;
        public Player holder;
        public override string Texture => "StarlightRiver/Projectiles/WeaponProjectiles/ShakerBall";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[PH]Boss Shaker");
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 2000;
            npc.width = 64;
            npc.height = 64;
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.knockBackResist = 0;
            npc.damage = 60;
            for (int k = 0; k < npc.buffImmune.Length; k++) { npc.buffImmune[k] = true; }

            npc.ai[3] = 1;
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override void AI()
        {
            /* AI fields:
             * 0: state
             * 1: timer
             * 2: shocked?
             * 3: chain?
             */

            if (parent == null) return; //safety check

            if (parent.npc.ai[0] <= 4) npc.ai[3] = 1;

            npc.ai[1]++; //ticks our timer

            if (npc.life <= 1) Dust.NewDustPerfect(npc.Center, ModContent.DustType<Dusts.Gold2>(), Vector2.One.RotatedBy(LegendWorld.rottime * 4)); //dust when "destroyed"

            if (npc.ai[2] == 1) //if zapped
            {
                parent.npc.ai[1] = 0; //resets the boss' timer constatnly, effectively freezing it
                parent.ResetAttack(); //also reset's their attack just incase

                if (npc.ai[1] % 5 == 0 && npc.ai[1] < 60) Helper.DrawElectricity(npc.Center, parent.npc.Center, ModContent.DustType<Dusts.Gold>(), 0.5f); //draw zap effects

                if (npc.ai[1] == 60) //after 60 seconds disconnect the flail and phase the boss
                {
                    npc.velocity.Y -= 10; //launches it out of the pit
                    npc.ai[3] = 0; //cut the chain
                    parent.npc.ai[0] = 5; //phase the boss
                }

                if (npc.ai[1] == 80) //some things need to be on a delay
                {
                    npc.ai[2] = 0; //no longer zapped!
                    foreach (Projectile proj in Main.projectile.Where(p => p.type == ModContent.ProjectileType<Projectiles.Dummies.OvergrowBossPitDummy>())) proj.ai[1] = 2; //closes the pits
                }
            }

            if (npc.ai[0] == 1) //pick-upable
            {
                npc.friendly = true;
                npc.rotation += npc.velocity.X / 125f;
                if (npc.velocity.Y == 0 && npc.velocity.X > 0.3f) npc.velocity.X -= 0.3f;
                if (npc.velocity.Y == 0 && npc.velocity.X < -0.3f) npc.velocity.X += 0.3f;
                if (Math.Abs(npc.velocity.X) <= 0.3f) npc.velocity.X = 0;
                if (Main.player.Any(p => p.Hitbox.Intersects(npc.Hitbox)) && holder == null && npc.velocity == Vector2.Zero)
                {
                    holder = Main.player.FirstOrDefault(p => p.Hitbox.Intersects(npc.Hitbox)); //the first person to walk over it picks it up
                }
                if (holder != null)
                {
                    npc.position = holder.Center + new Vector2(-32, -100); //they hold it above their head
                    holder.bodyFrame = new Rectangle(0, 56 * 5, 40, 56); //holding animation
                    holder.AddBuff(BuffID.Cursed, 2, true); //they cant use items!
                    holder.rocketTime = 0; holder.wingTime = 0; //they cant rocket/fly!
                    holder.velocity.X *= 0.95f; //they cant move fast!

                    if (holder.controlUseItem) //but they can YEET THIS MOTHERFUCKER
                    {
                        npc.velocity = Vector2.Normalize(holder.Center - Main.MouseWorld) * -10; //in the direction they are aiming
                        holder = null;
                    }
                }

                npc.velocity.Y += 0.2f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[3] != 0)
            {
                for (float k = 0; k <= 1; k += 1 / (Vector2.Distance(npc.Center, parent.npc.Center) / 16))
                {
                    Vector2 pos = Vector2.Lerp(npc.Center, parent.npc.Center, k) - Main.screenPosition;
                    //shake the chain when tossed
                    if ((parent.npc.ai[2] == 3 || parent.npc.ai[0] == 4) && npc.velocity.Length() > 0) pos += Vector2.Normalize(npc.Center - parent.npc.Center).RotatedBy(1.58f) * (float)Math.Sin(LegendWorld.rottime + k * 20) * 10;

                    spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Projectiles/WeaponProjectiles/ShakerChain"), pos,
                        new Rectangle(0, 0, 8, 16), drawColor, (npc.Center - parent.npc.Center).ToRotation() + 1.58f, new Vector2(4, 8), 1, 0, 0);
                }
            }
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[0] == 1 && holder == null && npc.velocity.X == 0)
            {
                spriteBatch.DrawString(Main.fontMouseText, "Pick up!", npc.Center + new Vector2(Main.fontMouseText.MeasureString("Pick up!").X / -2, -50 + (float)Math.Sin(LegendWorld.rottime) * 5) - Main.screenPosition, Color.Yellow * 0.75f);
            }
        }

        public override bool CheckDead()
        {
            npc.dontTakeDamage = true;
            npc.life = 1;
            return false;
        }
    }
}
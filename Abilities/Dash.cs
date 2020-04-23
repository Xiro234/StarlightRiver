using Microsoft.Xna.Framework;
using StarlightRiver.Dusts;
using System;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Abilities
{
    [DataContract]
    public class Dash : Ability
    {
        [DataMember] protected float X = 0;
        [DataMember] protected float Y = 0;

        public Dash(Player player) : base(1, player)
        {

        }
        public override bool CanUse => Main.LocalPlayer.controlLeft || Main.LocalPlayer.controlRight || Main.LocalPlayer.controlUp || Main.LocalPlayer.controlDown || player.GetModPlayer<Dragons.DragonHandler>().DragonMounted;

        public override void OnCast()
        {
            Main.PlaySound(SoundID.Item45);
            Main.PlaySound(SoundID.Item104);

            X = ((player.controlLeft) ? -1 : 0) + ((player.controlRight) ? 1 : 0);
            Y = ((player.controlUp) ? -1 : 0) + ((player.controlDown) ? 1 : 0);
            Timer = 7;
            Cooldown = 90;
        }

        public override void InUse()
        {
            player.maxFallSpeed = 999;
            Timer--;
            if (X != 0 || Y != 0) { player.velocity = Vector2.Normalize(new Vector2(X, Y)) * 28; }

            if (Vector2.Distance(player.position, player.oldPosition) < 5 && Timer < 4)
            {
                Timer = 0;
                player.velocity *= -0.2f;
            }

            if (Timer <= 0)
            {
                Active = false;
                OnExit();
            }
        }
        public override void UseEffects()
        {
            Vector2 prevPos = player.Center + Vector2.Normalize(player.velocity) * 10;
            int direction = Timer % 2 == 0 ? -1 : 1;
            for (int k = 0; k < 60; k++)
            {
                float rot = (0.1f * k) * direction;
                Dust dus = Dust.NewDustPerfect(prevPos + Vector2.Normalize(player.velocity).RotatedBy(rot) * (k / 2) * (0.5f + Timer / 8f), ModContent.DustType<AirDash>());
                dus.fadeIn = k - Timer * 3;
            }
        }
        public override void OnCastDragon()
        {
            if (player.velocity.Y == 0) //on the ground, set to zero so the game knows to do the pounce
            {
                X = player.direction * 2;
                Y = 0;
            }
            else // jumping/in the air, do the barrel roll
            {
                X = Vector2.Normalize(player.Center - Main.MouseWorld).X;
                Y = Vector2.Normalize(player.Center - Main.MouseWorld).Y;
            }
            Timer = 20;
            Cooldown = 90;
        }
        public override void InUseDragon()
        {
            Timer--;
            if (Math.Abs(X) > 1) //the normalized X should never be greater than 1, so this should be a valid check for the pounce
            {
                player.velocity.X = X * 6;
                if (Timer == 19) player.velocity.Y -= 4;
            }
            else //otherwise, barrelroll
            {
                player.velocity = new Vector2(X, Y) * 0.2f * (((10 - Timer) * (10 - Timer)) - 100);
            }
            if (Timer <= 0)
            {
                Active = false;
                OnExit();
            }
        }
        public override void UseEffectsDragon()
        {
            Dust.NewDust(player.position, 50, 50, ModContent.DustType<Air>());
            if (Math.Abs(X) < 1)
            {

                for (int k = 0; k <= 10; k++)
                {
                    float rot = ((Timer - k / 10f) / 10f * 6.28f) + new Vector2(X, Y).ToRotation();
                    Dust.NewDustPerfect(Vector2.Lerp(player.Center, player.Center + player.velocity, k / 10f) + Vector2.One.RotatedBy(rot) * 30, ModContent.DustType<Air>(), Vector2.Zero);
                }
            }
        }

        public override void OffCooldownEffects()
        {
            for (int k = 0; k <= 25; k++)
            {
                Dust.NewDust(player.Center, 1, 1, ModContent.DustType<Air>());
            }
            Main.PlaySound(SoundID.MaxMana);
        }

        public override void OnExit()
        {
            player.velocity.X *= 0.15f;
            player.velocity.Y *= 0.15f;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Dusts;
using System;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Abilities
{
    [DataContract]
    public class Smash : Ability
    {
        public Smash(Player player) : base(2, player) { }
        public override bool CanUse => player.velocity.Y != 0;
        public override Texture2D texture => ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Smash1");

        public override void OnCast()
        {
            Timer = 0;
        }

        public override void InUse()
        {
            Timer++;
            if (Timer > 15)
            {
                player.maxFallSpeed = 999;
                player.velocity.X = 0;
                player.velocity.Y = 35;


                if (Timer % 15 == 0)
                {
                    Main.PlaySound(SoundID.Item66, player.Center);
                }

                if (player.position.Y - player.oldPosition.Y == 0)
                {
                    Active = false;
                    OnExit();
                }
            }
            else
            {
                player.velocity.X = 0;
                player.velocity.Y = Timer * 2 - 15;
            }
        }

        public override void UseEffects()
        {
            if (Timer > 15)
            {
                for (int k = 0; k <= 5; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, -32), player.height, player.height, ModContent.DustType<Stone>(), 0, 0, 0, default, 1.1f);
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, -32), player.height, player.height, DustID.Dirt, Main.rand.Next(-14, 15) * 0.5f, 0, 0, default, 0.8f);
                    if (k % 2 == 0)
                    {
                        Dust.NewDust(player.Center - new Vector2(player.height / 2, -32), player.height, player.height, ModContent.DustType<Grass>(), Main.rand.Next(-9, 10) * 0.5f, 0, 0, default, 1.1f);
                    }
                }
            }
            else
            {
                float rot = Main.rand.NextFloat(6.28f);
                Dust.NewDustPerfect(player.Center + Vector2.One.RotatedBy(rot) * 40, ModContent.DustType<JungleEnergy>(), Vector2.One.RotatedBy(rot) * -2f, 0, default, 0.3f);
            }
        }

        public override void OnExit()
        {
            int power = (Timer > 60) ? 12 : (int)(Timer / 60f * 12);
            for (float k = 0; k <= 6.28; k += 0.1f - (power * 0.005f))
            {
                Dust.NewDust(player.Center, 1, 1, ModContent.DustType<Stone>(), (float)Math.Cos(k) * power, (float)Math.Sin(k) * power, 0, default, 0.5f + power / 7f);
                Dust.NewDust(player.Center - new Vector2(player.height / 2, -32), player.height, player.height, ModContent.DustType<Grass>(), (float)Math.Cos(k) * power * 0.75f, (float)Math.Sin(k) * power * 0.75f, 0, default, 0.5f + power / 7f);
            }

            Main.PlaySound(SoundID.Item70, player.Center);
            Main.PlaySound(SoundID.NPCHit42, player.Center);
            Main.PlaySound(SoundID.Item14, player.Center);

            player.GetModPlayer<StarlightPlayer>().Shake = power;

            player.velocity.X = 0;
            player.velocity.Y = 0;
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Ability
{
    class Dash : Ability
    {
        Mod mod = StarlightRiver.Instance;
        protected int timer = 0;
        protected float X = 0;
        protected float Y = 0;

        public Dash() : base(1)
        {

        }

        public override void OnCast()
        {
            Active = true;
            Main.PlaySound(SoundID.Item45);
            Main.PlaySound(SoundID.Item104);

            X = ((player.controlLeft) ? -1 : 0) + ((player.controlRight) ? 1 : 0);
            Y = ((player.controlUp) ? -1 : 0) + ((player.controlDown) ? 1 : 0);
            timer = 5;
        }

        public override void InUse()
        {
            player.maxFallSpeed = 999;

            timer--;

            if (X != 0 || Y != 0)
            {
                player.velocity = Vector2.Normalize(new Vector2(X, Y)) * 45;
            }

            if(timer <= 0)
            {
                Active = false;
                OnExit();               
            }
        }
        public override void UseEffects()
        {
            for (int k = 0; k <= 10; k++)
            {
                Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Air"));
            }
        }

        public override void OnExit()
        {
            player.velocity.X *= 0.15f;
            player.velocity.Y *= 0.15f;
        }
    }
}

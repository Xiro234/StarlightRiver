using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Ability
{
    class Dash : Ability
    {
        Mod mod = spritersguildwip.Instance;
        int timer = 0;
        public Dash() : base(1)
        {

        }

        public override void OnCast()
        {
            Active = true;
            Main.PlaySound(SoundID.Item37);
            timer = 5;
        }

        public override void InUse()
        {
            Player player = Handler.player;
            player.maxFallSpeed = 999;

            timer--;

            float X = ((player.controlLeft) ? -1 : 0) + ((player.controlRight) ? 1 : 0);
            float Y = ((player.controlUp) ? -1 : 0) + ((player.controlDown) ? 1 : 0);

            if (X != 0 || Y != 0)
            {
                player.velocity = Vector2.Normalize(new Vector2(X, Y)) * 40;

                for (int k = 0; k <= 10; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Air"));
                }
            }

            if(timer <= 0)
            {
                OnExit();
                Active = false;
            }
        }

        public override void OnExit()
        {
            Main.NewText("Debug");
        }
    }
}

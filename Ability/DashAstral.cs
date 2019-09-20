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
    class DashAstral : Dash
    {
        Mod mod = StarlightRiver.Instance;

        public DashAstral() : base()
        {

        }

        public override void OnCast()
        {
            Active = true;
            Main.PlaySound(SoundID.Item45);
            Main.PlaySound(SoundID.Item104);

            X = ((player.controlLeft) ? -1 : 0) + ((player.controlRight) ? 1 : 0);
            Y = ((player.controlUp) ? -1 : 0) + ((player.controlDown) ? 1 : 0);
            timer = 7;
        }
        public override void UseEffects()
        {
            for (int k = 0; k <= 10; k++)
            {
                Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Starlight"), -10 * Vector2.Normalize(player.velocity).X, -10 * Vector2.Normalize(player.velocity).Y,0,default,0.75f);
                Dust.NewDustPerfect(player.Center + Vector2.Normalize(player.velocity) * Main.rand.Next(-100, 0), mod.DustType("Starlight"), Vector2.Normalize(player.velocity).RotatedBy(1) * (Main.rand.Next(-20, -5) + timer * -3),0,default, 1 - timer*0.1f);
                Dust.NewDustPerfect(player.Center + Vector2.Normalize(player.velocity) * Main.rand.Next(-100, 0), mod.DustType("Starlight"), Vector2.Normalize(player.velocity).RotatedBy(-1) * (Main.rand.Next(-20, -5) + timer * -3), 0,default,1 - timer * 0.1f);
            }
        }
    }
}

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
    class Float : Ability
    {
        Mod mod = StarlightRiver.Instance;
        int timer = 0;
        public Float() : base(1)
        {

        }

        public override void OnCast()
        {
            Player player = Handler.player;
            Active = true;
            timer = Handler.stamina * 60;

            for (int k = 0; k <= 50; k++)
            {
                Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Gold2"), Main.rand.Next(-20, 20), Main.rand.Next(-20, 20), 0, default, 1.2f);
            }
            
        }

        public override void InUse()
        {
            Player player = Handler.player;
            timer--;
            player.maxFallSpeed = 999;
            player.gravity = 0;
            player.velocity = Vector2.Normalize(new Vector2
                (
                Main.screenPosition.X + Main.mouseX - player.Hitbox.Center.X,
                Main.screenPosition.Y + Main.mouseY - player.Hitbox.Center.Y
                )) * 5 + new Vector2(0.25f, 0.25f);

            player.Hitbox = new Rectangle((int)player.Hitbox.X - 7 + 7, (int)player.Hitbox.Y + 21 + 7, 14, 14);

            Lighting.AddLight(player.Center, new Vector3(0.15f, 0.15f, 0f));

            for (int k = 0; k <= 2; k++)
            {
                Dust.NewDust(player.Center - new Vector2(4, 4), 8, 8, mod.DustType("Gold"));
            }
        
            if ((timer % 60 == 0 && timer != 0) || timer == 1)
            {
                Handler.stamina--;
            }

            if (StarlightRiver.Float.JustReleased ||  Handler.stamina < 1)
            {
                Active = false;
                OnExit();
            }
        }

        public override void OnExit()
        {
            Player player = Handler.player;
            timer = 0;
            player.velocity.X = 0;
            player.velocity.Y = 0;

            for (int k = 0; k <= 30; k++)
            {
                Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Gold2"), Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 0, default, 1.2f);
            }
        }
    }
}

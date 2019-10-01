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
        public int timer = 0;
        bool exit = false;
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
            player.AddBuff(BuffID.Cursed, 2, true);
            player.maxFallSpeed = 999;
            player.gravity = 0;
            player.velocity = Vector2.Normalize(new Vector2
                (
                Main.screenPosition.X + Main.mouseX - player.Hitbox.Center.X,
                Main.screenPosition.Y + Main.mouseY - player.Hitbox.Center.Y
                )) * 5 + new Vector2(0.25f, 0.25f);

            player.Hitbox = new Rectangle((int)player.Hitbox.X - 7 + 7, (int)player.Hitbox.Y + 21 + 7, 14, 14);

            Lighting.AddLight(player.Center, new Vector3(0.15f, 0.15f, 0f));


        
            if ((timer % 60 == 0 && timer > 0) || timer == 1)
            {
                Handler.stamina--;
            }

            if (StarlightRiver.Float.JustReleased)
            {
                exit = true;
            }

            if (exit || Handler.stamina < 1)
            {             
                OnExit();
            }
        }
        public override void UseEffects()
        {
            if (timer > -1)
            {
                for (int k = 0; k <= 2; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(4, 4), 8, 8, mod.DustType("Gold"));
                }
            }
            else
            {
                for (int k = 0; k <= 2; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(4, 4), 8, 8, mod.DustType("Void"));
                }
            }
        }

        public override void OnExit()
        {
            Player player = Handler.player;
            if (testExit())
            {
                timer = 0;
                exit = false;
                player.velocity.X = 0;
                player.velocity.Y = 0;

                for (int k = 0; k <= 30; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Gold2"), Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 0, default, 1.2f);
                }
                Active = false;
            }
            else if (timer < 0)
            {
                player.statLife -= 2;
                if(player.statLife <= 0)
                {
                    player.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason(player.name + " couldn't maintain their form"), 0, 0);
                }
                if (timer % 10 == 0) { Main.PlaySound(SoundID.PlayerHit, player.Center); }
            }
        }

        public bool testExit()
        {
            int cleartiles = 0;
            for (int x2 = (int)(player.position.X / 16); x2 <= (int)(player.position.X / 16) + 1; x2++)
            {
                for (int y2 = (int)(player.position.Y / 16) - 2; y2 <= (int)(player.position.Y / 16); y2++)
                {
                    if (Main.tile[x2, y2].collisionType == 0) { cleartiles++; }
                }
            }
            if (cleartiles == 6) { return true; }
            else { return false; }            
        }
    }
}

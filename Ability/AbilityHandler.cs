using Terraria.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using System.Linq;

namespace spritersguildwip.Ability
{
    class AbilityHandler : ModPlayer
    {
        public static int[] ability = new int[]
        {
             0,0,0,0
        };

        public static int[] cooldowns = new int[]
        {
             0,0,0,0
        };
        float storedtime;
        float timer = 0;
        Vector2 objective = new Vector2(0,0);
        Vector2 start = new Vector2(0,0);
        bool landed = false;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if(spritersguildwip.Dash.JustPressed && cooldowns[0] == 0) //dash key
            {
                cooldowns[0] = 60;
                Main.PlaySound(SoundID.Item37);

                if (player.wingTime > 0)
                {
                    storedtime = player.wingTime;
                }
                player.wingTime = 0;
            }

            if (spritersguildwip.Superdash.JustPressed && cooldowns[1] == 0) //superdash key
            {
                cooldowns[1] = 300;
                Main.PlaySound(SoundID.Item8);

                for (int k = 0; k <= 10; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(100, 100), 200, 200, mod.DustType("Void2"),0,0,0, default, 1.2f);
                }

                objective = new Vector2
                    (
                    Main.screenPosition.X + Main.mouseX - player.position.X,
                    Main.screenPosition.Y + Main.mouseY - player.position.Y
                    );
                start = player.position;


                if (player.wingTime > 0)
                {
                    storedtime = player.wingTime;
                }
                player.wingTime = 0;
            }

            if (spritersguildwip.Smash.JustPressed && cooldowns[2] == 0) //smash key
            {
                cooldowns[2] = 300;
                Main.PlaySound(SoundID.Item37);

                if (player.wingTime > 0)
                {
                    storedtime = player.wingTime;
                }
                player.wingTime = 0;
            }
        }

        public override void PreUpdate()
        {
            if (cooldowns[0] >= 55) // dash action
            {
                player.maxFallSpeed = 999;
                player.velocity.X = ((player.controlLeft) ? -30 : 0) + ((player.controlRight) ? 30 : 0);
                player.velocity.Y = ((player.controlUp) ? -30 : 0) + ((player.controlDown) ? 30 : 0);

                for (int k = 0; k <= 10; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Air"));
                }
            }

            if(cooldowns[0] == 54)
            {
                player.velocity.X = 0;
                player.velocity.Y = 0;
                player.wingTime = storedtime;
                storedtime = 0;
            }

            //-------------------------------------------------

            if (cooldowns[1] > 1 && !(Vector2.Distance(player.position, start) >= objective.Length() || ((player.position - player.oldPosition).Length() < 14) && cooldowns[1] <= 299)) // superdash action
            {
                player.maxFallSpeed = 999;
                player.velocity = Vector2.Normalize(objective) * 15;
                player.immune = true;

                Main.PlaySound(SoundID.Item24);

                timer += ((float)Math.PI * 2 / 12);
                if (timer >= (float)Math.PI * 2)
                {
                    timer = 0;
                }

                    float rot = Vector2.Normalize(player.velocity).ToRotation();
                    float x = player.Center.X + (float)Math.Sin(rot) * ((float)Math.Sin(timer) * 20);
                    float y = player.Center.Y + (float)Math.Cos(rot) * ((float)Math.Sin(timer) * -20);

                    Dust.NewDustPerfect(new Vector2(x,y), mod.DustType("Void"));

                for (int k = 0; k <= 10; k++)
                {
                    Dust.NewDust(new Vector2(x, y), 10, 10, mod.DustType("Void"), Main.rand.Next(-20, 20), Main.rand.Next(-20, 20), 0, default, 0.5f);
                }

                for (int k = 0; k <= 10; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Void"),Main.rand.Next(-50, 50), Main.rand.Next(-50, 50), 0,default,0.4f);                   
                }
            }

            if (((Vector2.Distance(player.position, start) >= objective.Length() || ((player.position - player.oldPosition).Length() < 14) && cooldowns[1] <= 299) && objective != new Vector2(0,0)))
            {
                player.velocity.X = 0;
                player.velocity.Y = 0;
                player.wingTime = storedtime;
                storedtime = 0;
                player.immune = false;
                objective = new Vector2(0,0);

                for (int k = 0; k <= 100; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Void"), Main.rand.Next(-70, 70), Main.rand.Next(-70, 70), 0, default, 1.2f);
                }

                Main.PlaySound(SoundID.Item38);
            }

            //-----------------------------------------------------------------------

            
            if (cooldowns[2] > 1 && !landed) // smash action
            {
                player.maxFallSpeed = 999;
                player.velocity.X = 0;
                player.velocity.Y = 30;

                for (int k = 0; k <= 10; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Air"));
                }

                if(cooldowns[2] == 2)
                {
                    cooldowns[2] = 3;
                }
            }

            if (player.position.Y - player.oldPosition.Y == 0 && cooldowns[2] > 1 && !landed)
            {
                player.velocity.X = 0;
                player.velocity.Y = 0;
                player.wingTime = storedtime;
                storedtime = 0;
                landed = true;
            }

            if(cooldowns[2] == 0)
            {
                landed = false;
            }

            for (int k = 0; k <= 3; k++) // tick down cooldowns
            {
                if (cooldowns[k] > 0)
                {
                    cooldowns[k]--;
                }
            }
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if (cooldowns[1] > 1 && !(Vector2.Distance(player.position, start) >= objective.Length() || ((player.position - player.oldPosition).Length() < 14) && cooldowns[1] <= 299))
            {
                foreach (PlayerLayer layer in layers)
                {
                    layer.visible = false;
                }
            }
        }
    }
}

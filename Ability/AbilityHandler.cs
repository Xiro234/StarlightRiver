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
        public int[] ability = new int[]
        {
             0,0,0,0
        };

        public int staminamax = 3;
        public int stamina = 3;
        int staminaticker = 0;

        public int dashcd = 0;
        float storedtime;
        float timer = 0;
        public Vector2 objective = new Vector2(0,0);
        public Vector2 start = new Vector2(0,0);
        public int shadowcd = 0;
        public bool smash = false;
        public bool landed = false;
        public bool floating = false;
        public int floattime = 0;

        public override TagCompound Save()
        {
            string staminaSave = stamina.ToString();
            string staminaMaxSave = staminamax.ToString();

            return new TagCompound {
                {"staminaSave", staminaSave},
                {"staminaMaxSave",staminaMaxSave},
            };
        }

        public override void Load(TagCompound tag)
        {
            var staminaSave = tag.GetString("staminaSave");
            var staminaMaxSave = tag.GetString("staminaMaxSave");
            stamina = int.Parse(staminaSave);
            staminamax = int.Parse(staminaMaxSave);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if(spritersguildwip.Dash.JustPressed && dashcd == 0 && stamina >= 1 && ability[0] == 1) //dash key
            {
                stamina -= 1;
                dashcd = 6;
                Main.PlaySound(SoundID.Item37);

                if (player.wingTime > 0)
                {
                    storedtime = player.wingTime;
                }
                player.wingTime = 0;
            }

            if (spritersguildwip.Superdash.JustPressed && stamina >= 3 && ability[1] == 1) //superdash key
            {
                stamina -= 3;
                shadowcd = 4;
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

            if (spritersguildwip.Smash.JustPressed && stamina >= 2) //smash key
            {
                stamina -= 2;
                smash = true;
                Main.PlaySound(SoundID.Item37);

                if (player.wingTime > 0)
                {
                    storedtime = player.wingTime;
                }
                player.wingTime = 0;
            }

            if(spritersguildwip.Float.JustPressed && stamina >= 2 && ability[3] == 1) // float key
            {
                stamina--;
                floating = true;              
                floattime = stamina * 60;
                if (player.wingTime > 0)
                {
                    storedtime = player.wingTime;
                }
                player.wingTime = 0;

                for (int k = 0; k <= 50; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Gold2"), Main.rand.Next(-20, 20), Main.rand.Next(-20, 20), 0, default, 1.2f);
                }
            }
        }

        public override void PreUpdate()
        {
            if (dashcd > 1) // dash action
            {
                player.maxFallSpeed = 999;
                player.velocity.X = ((player.controlLeft) ? -30 : 0) + ((player.controlRight) ? 30 : 0);
                player.velocity.Y = ((player.controlUp) ? -30 : 0) + ((player.controlDown) ? 30 : 0);

                for (int k = 0; k <= 10; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Air"));
                }
            }

            if(dashcd == 1)
            {
                player.velocity.X = 0;
                player.velocity.Y = 0;
                player.wingTime = storedtime;
                storedtime = 0;
            }

            //-------------------------------------------------

            if (shadowcd >= 1 && !(Vector2.Distance(player.position, start) >= objective.Length() || ((player.position - player.oldPosition).Length() < 14) && shadowcd <= 3)) // superdash action
            {
                player.maxFallSpeed = 999;
                player.velocity = Vector2.Normalize(objective) * 15;
                player.immune = true;
                player.immuneTime = 5;

                Main.PlaySound(SoundID.Item24);

                timer += ((float)Math.PI * 2 / 12);
                if (timer >= (float)Math.PI * 2)
                {
                    timer = 0;
                }
                if(shadowcd <= 2)
                {
                    shadowcd = 2;
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

            if (((Vector2.Distance(player.position, start) >= objective.Length() || ((player.position - player.oldPosition).Length() < 14) && shadowcd <= 3) && objective != new Vector2(0,0)))
            {
                player.velocity.X = 0;
                player.velocity.Y = 0;
                player.wingTime = storedtime;
                storedtime = 0;
                player.immune = false;
                objective = new Vector2(0,0);
                shadowcd = 0;

                for (int k = 0; k <= 100; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Void"), Main.rand.Next(-70, 70), Main.rand.Next(-70, 70), 0, default, 1.2f);
                }

                Main.PlaySound(SoundID.Item38);
            }

            //-----------------------------------------------------------------------

            
            if (smash && !landed) // smash action
            {
                player.maxFallSpeed = 999;
                player.velocity.X = 0;
                player.velocity.Y = 30;

                for (int k = 0; k <= 10; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Air"));
                }
            }

            if (player.position.Y - player.oldPosition.Y == 0 && smash && !landed)
            {
                player.velocity.X = 0;
                player.velocity.Y = 0;
                player.wingTime = storedtime;
                storedtime = 0;
                smash = false;
                landed = true;
            }

            if(!smash)
            {
                landed = false;
            }

            //----------------------------------------------------------------------

            if (floating) // float action
            {
                floattime--;
                player.maxFallSpeed = 999;
                player.gravity = 0;
                player.velocity = Vector2.Normalize(new Vector2
                    (
                    Main.screenPosition.X + Main.mouseX - player.position.X,
                    Main.screenPosition.Y + Main.mouseY - player.position.Y
                    )) * 5;
                //player.Hitbox = new Rectangle((int)player.Center.X - 8, (int)player.Center.Y - 8, 16, 16);
                for (int k = 0; k <= 2; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(4,4), 8, 8, mod.DustType("Gold"));
                }
            }
            if((floattime % 60 == 0 && floattime != 0) || floattime == 1)
            {
                stamina--;
            }

            if ((spritersguildwip.Float.JustReleased && ability[3] == 1 && floating) || (floating && stamina < 1))
            {
                floating = false;
                floattime = 0;
                player.velocity.X = 0;
                player.velocity.Y = 0;
                player.wingTime = storedtime;
                storedtime = 0;

                for (int k = 0; k <= 30; k++)
                {
                    Dust.NewDust(player.Center - new Vector2(player.height / 2, player.height / 2), player.height, player.height, mod.DustType("Gold2"), Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 0, default, 1.2f);
                }
            }

            if (dashcd == 1)
            {
                player.velocity.X = 0;
                player.velocity.Y = 0;
                player.wingTime = storedtime;
                storedtime = 0;
            }

            if (dashcd > 0)
            {
                dashcd--;
            }

            if (shadowcd > 0)
            {
                shadowcd--;
            }

            if (staminaticker++ >= 180 && stamina < staminamax && !floating)
            {
                stamina++;
            }
            if(staminaticker > 180 || stamina == staminamax)
            {
                staminaticker = 0;
            }
        }
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if ((shadowcd >= 1 && !(Vector2.Distance(player.position, start) >= objective.Length() || ((player.position - player.oldPosition).Length() < 14) && shadowcd <= 3)) || floating)
            {
                foreach (PlayerLayer layer in layers)
                {
                    layer.visible = false;
                }
            }
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using spritersguildwip.Ability;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.NPCs.Pickups
{
    class Cloak : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[PH] Void Dash Ability");
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 32;
            npc.aiStyle = -1;
            npc.immortal = true;
            npc.lifeMax = 1;
            npc.knockBackResist = 0;
            npc.noGravity = true;
        }

        int animate = 0;
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

            if (npc.Hitbox.Intersects(player.Hitbox) && mp.unlock[4] == 0)
            {
                mp.unlock[4] = 1;
                animate = 300;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Pickups/get"));
            }

            if (animate >= 1)
            {
                player.position = new Vector2(npc.position.X, npc.position.Y - 16);
                if (animate > 100 && animate < 290)
                {
                    float rot = Main.rand.NextFloat(0, (float)Math.PI * 2);
                    Dust.NewDustPerfect(player.Center + new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot)) * -1000, mod.DustType("Void3"), new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot)) * 15, 0, default, 3f);
                }
                if (animate <= 30 && animate % 10 == 0)
                {
                    for(float k = 0; k <= (float)Math.PI * 2; k += (float)Math.PI / 40)
                    {
                        Dust.NewDustPerfect(player.Center, mod.DustType("Void"), new Vector2((float)Math.Cos(k), (float)Math.Sin(k)) * -5, 0, default, 1.5f);
                    }
                }
                if (animate == 1)
                {
                    player.AddBuff(BuffID.Featherfall, 120);
                }

                for (int k = 0; k <= 6000; k++)
                {
                    if (Main.dust[k].type == mod.DustType("Gold3"))
                    {
                        Dust.NewDustPerfect(Main.dust[k].position, mod.DustType("Gold"), null, 0, default, 0.5f);
                    }
                }
            }

            if (animate > 0)
            {
                animate--;
            }

        }

        public static Texture2D wind = ModContent.GetTexture("spritersguildwip/NPCs/Pickups/Wind1");

        float timer = 0;
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();

            timer += (float)(Math.PI * 2) / 120;
            if (timer >= Math.PI * 2)
            {
                timer = 0;
            }

            if (mp.unlock[4] == 0)
            {
                spriteBatch.Draw(wind, npc.position - Main.screenPosition + new Vector2(0, (float)Math.Sin(timer) * 16), Color.White);
                Dust.NewDust(npc.position + new Vector2(0, (float)Math.Sin(timer) * 16), npc.width, npc.height, mod.DustType("Void"));
            }
            if (mp.unlock[4] == 1 && animate == 0)
            {
                spriteBatch.DrawString(Main.fontItemStack, "Q: Void dash", npc.position - Main.screenPosition + new Vector2(-30, -32), Color.White);
            }

        }
    }
}

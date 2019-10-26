using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Pickups
{
    class Wind : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forbidden Winds");
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

        public override bool CheckActive() { return true; }

        int animate = 0;
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

            if (npc.Hitbox.Intersects(player.Hitbox) && mp.dash.Locked)
            {
                mp.dash.Locked = false;
                mp.StatStaminaMaxPerm += 1;
                animate = 300;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Pickups/get"));
            }

            if (animate >= 1)
            {
                player.position = new Vector2(npc.position.X, npc.position.Y - 16);
                player.immune = true;
                player.immuneTime = 5;
                player.immuneNoBlink = true;
                if (animate > 100 && animate < 290)
                {
                    float rot = Main.rand.NextFloat(0, (float)Math.PI * 2);
                    Dust dus = Dust.NewDustPerfect(player.Center, mod.DustType("Air2"), new Vector2((float)Math.Cos(rot) * 5, (float)Math.Sin(rot) * 5));
                    dus.customData = animate - 50;
                }
                if(animate == 1)
                {
                    player.AddBuff(BuffID.Featherfall, 120);
                    Achievements.Achievements.QuickGive("Stormcaller", player);
                }
            }

            if(animate > 0)
            {
                animate--;
            }
            
        }
        float timer = 0;
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            AbilityHandler mp = Main.LocalPlayer.GetModPlayer<AbilityHandler>();

            timer += (float)(Math.PI * 2) / 120;
            if(timer >= Math.PI * 2)
            {
                timer = 0;
            }

            if (mp.dash.Locked)
            {
                Vector2 pos = npc.position - Main.screenPosition - (new Vector2((int)((Math.Cos(timer * 3) + 1) * 4f), (int)((Math.Sin(timer * 3) + 1) * 4f)) / 2) + new Vector2(0, (float)Math.Sin(timer) * 8);

                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wind1"), npc.position + new Vector2(0, (float)Math.Sin(timer) * 8) - Main.screenPosition, Color.White);
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Bubble"), new Rectangle((int)pos.X - 4, (int)pos.Y - 4, 40 + (int)((Math.Cos(timer * 3) +1) * 4f), 40 + (int)((Math.Sin(timer * 3) + 1) * 4f)), Color.White);
                Dust.NewDust(new Vector2(npc.position.X - 4, npc.position.Y - 4), 40, 40, mod.DustType("Air"),0,0,0,default,0.5f);
            }
            if(!mp.dash.Locked && animate == 0)
            {
                spriteBatch.DrawString(Main.fontItemStack, "Left Shift + A/W/S/D: Dash", npc.position - Main.screenPosition + new Vector2(-90, -32), Color.White);
            }
            
        }
    }
}

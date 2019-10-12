using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs
{
    class Stamina : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void SetDefaults()
        {
            npc.width = 22;
            npc.height = 22;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.lifeMax = 1;
            npc.noGravity = true;
            npc.dontTakeDamage = true;
            npc.dontTakeDamageFromHostiles = true;
            npc.dontCountMe = true;
            npc.behindTiles = true;
        }
        public override void AI()
        {            
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

            if (npc.localAI[0] > 0) { npc.localAI[0]--; }
            else if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(npc.position, 16, 16, ModContent.DustType<Dusts.Stamina>());
            }

            if (npc.Hitbox.Intersects(player.Hitbox) && mp.stamina < (mp.staminamax+mp.permanentstamina) && mp.ability != null && mp.ability.Active && npc.localAI[0] == 0)
            {
                mp.stamina++;
                if (mp.ability is Float) { (mp.ability as Float).timer = 60 * mp.stamina - 1; }
                npc.localAI[0] = 300;

                Main.PlaySound(SoundID.Shatter, npc.Center);
                Main.PlaySound(SoundID.Item112, npc.Center);
                for (float k = 0; k <= 6.28; k+= 0.1f)
                {
                    Dust.NewDustPerfect(npc.Center, ModContent.DustType<Dusts.Stamina>(), new Vector2((float)Math.Cos(k), (float)Math.Sin(k)) * (Main.rand.Next(50) * 0.1f),0,default,3f);
                }
            }

            if (Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16].type != mod.TileType("StaminaGem"))
            {
                Helper.Kill(npc);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver
{
    class ShieldHandler : GlobalNPC
    {
        public int MaxShield;
        public int Shield;

        public override bool InstancePerEntity => true;

        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (Shield > 0)
            {
                Shield -= (int)(damage / 10);
                damage *= 0.5f;
                Main.PlaySound(SoundID.NPCHit34, npc.Center);
                if (Shield <= 0) { Main.PlaySound(SoundID.NPCDeath37, npc.Center); }
                for (int k = 0; k <= 10; k++)
                {
                    Dust.NewDustPerfect(npc.Center, ModContent.DustType<Dusts.Starlight>(), Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(15));
                }
                if (Shield <= 0)
                {
                    for (int k = 0; k <= 30; k++)
                    {
                        Dust.NewDustPerfect(npc.Center, ModContent.DustType<Dusts.Starlight>(), Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(25));
                    }
                }
            }
            return true;
        }

        public override void PostAI(NPC npc)
        {
            if (Shield > MaxShield) Shield = MaxShield;
            if (Shield < 0) Shield = 0;
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (Shield > 0 && npc.modNPC != null)
            {
                Vector2 drawpos = npc.Center - Main.screenPosition;
                Texture2D tex = ModContent.GetTexture(npc.modNPC.Texture);
                spriteBatch.Draw(tex, drawpos, new Rectangle(0, 0, npc.width, npc.height), new Color(80, 230, 255) * (0.5f + (float)Math.Sin(LegendWorld.rottime * 2) * 0.2f),
                    0, npc.Size / 2, 1.1f + (float)Math.Sin(LegendWorld.rottime * 4) * 0.05f, 0, 0);
            }
        }

        public override bool? DrawHealthBar(NPC npc, byte hbPosition, ref float scale, ref Vector2 position)
        {
            if (Shield > 0)
            {
                SpriteBatch spriteBatch = Main.spriteBatch;
                Vector2 drawpos = position - Main.screenPosition;
                Rectangle target = new Rectangle((int)drawpos.X - 18, (int)drawpos.Y + 10, (int)(Shield/(float)MaxShield * 36f), 10);
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/ShieldBar1"), target, new Color(60, 210, 255) * Lighting.Brightness((int)npc.position.X / 16, (int)npc.position.Y / 16));
            }
            return true;
        }
    }
}

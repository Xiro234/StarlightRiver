using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using StarlightRiver.Abilities;
using StarlightRiver.Codex;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Pickups
{
    class Lore : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starlight Codex");
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
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            if (npc.Hitbox.Intersects(player.Hitbox) && player.GetModPlayer<CodexHandler>().CodexState == 0)
            {
                player.GetModPlayer<CodexHandler>().CodexState = 1;
                Main.PlaySound(SoundID.NPCDeath7);
                for(float k = 0; k <= 6.28f; k += 0.1f)
                {
                    Dust.NewDustPerfect(npc.Center, ModContent.DustType<Dusts.Stamina>(), Vector2.One.RotatedBy(k) * Main.rand.NextFloat(8), 0, default, 2);
                }
            }           
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D book = ModContent.GetTexture("StarlightRiver/GUI/Book1Closed");
            if (Main.LocalPlayer.GetModPlayer<CodexHandler>().CodexState == 0)
            {
                Lighting.AddLight(npc.Center, new Vector3(1, 0.5f, 0));

                spriteBatch.Draw(book, npc.position - Main.screenPosition + new Vector2(4, (float)Math.Sin(LegendWorld.rottime) * 4), Color.White);
                float rot = Main.rand.NextFloat(6.28f);
                Dust.NewDustPerfect(npc.Center + Vector2.One.RotatedBy(rot) * 20, ModContent.DustType<Dusts.Stamina>(), Vector2.One.RotatedBy(rot) * -1);
            }
        }
    }
}

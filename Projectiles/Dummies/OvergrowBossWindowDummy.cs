using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.Dummies
{
    class OvergrowBossWindowDummy : ModNPC //yeah its actually an NPC fucking fight me
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override string Texture => "StarlightRiver/Invisible";
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.knockBackResist = 0;
            npc.aiStyle = -1;
            npc.lifeMax = 1;
            npc.immortal = true;
            npc.noGravity = true;
            npc.behindTiles = true;
        }

        public override void AI()
        {
            //Dust
            if (npc.ai[0] > 0 && npc.ai[0] < 359) Dust.NewDustPerfect(npc.Center + Vector2.One.RotatedByRandom(6.28f) * 412, ModContent.DustType<Dusts.Stone>());

            if (Main.rand.Next(4) == 0 && npc.ai[0] >= 360)
            {
                float rot = Main.rand.NextFloat(-1.5f, 1.5f);
                Dust.NewDustPerfect(npc.Center + new Vector2(0, 1).RotatedBy(rot) * 500, ModContent.DustType<Dusts.Gold4>(), (new Vector2(0, 1).RotatedBy(rot) + new Vector2(0, 1.6f)) * (0.1f + Math.Abs(rot / 5f)), 0, default, 0.23f + Math.Abs(rot / 5f));
            }

            //Screenshake
            if (npc.ai[0] < 359 && npc.ai[0] > 0) Main.LocalPlayer.GetModPlayer<StarlightPlayer>().Shake += (int)(359 - npc.ai[0]) / 175;


            //Lighting
            for(float k = 0; k <= 6.28f; k+= 0.2f)
            {
                Lighting.AddLight(npc.Center + Vector2.One.RotatedBy(k) * 23 * 16, new Vector3(1, 1, 0.7f) * 0.8f);
            }
            for(int k = 0; k < 6; k++)
            {
                if (npc.ai[0] > 0)
                {
                    float bright = npc.ai[0] / 60f; if (bright > 1) bright = 1;
                    Lighting.AddLight(npc.Center + new Vector2(560 + k * 35, 150 + k * 80), new Vector3(1, 1, 0.7f) * bright);
                    Lighting.AddLight(npc.Center + new Vector2(-560 - k * 35, 150 + k * 80), new Vector3(1, 1, 0.7f) * bright);
                }
                if (npc.ai[0] > 60)
                {
                    float bright = (npc.ai[0] - 60) / 150f; if (bright > 1) bright = 1;
                    Lighting.AddLight(npc.Center + new Vector2(450 + k * 15, 300 + k * 50), new Vector3(1, 1, 0.7f) * bright);
                    Lighting.AddLight(npc.Center + new Vector2(-450 - k * 15, 300 + k * 50), new Vector3(1, 1, 0.7f) * bright);
                }
                if (npc.ai[0] > 210)
                {
                    float bright = (npc.ai[0] - 210) / 70f; if (bright > 1) bright = 1;
                    Lighting.AddLight(npc.Center + new Vector2(250 + k * 5, 350 + k * 40), new Vector3(1, 1, 0.7f) * bright);
                    Lighting.AddLight(npc.Center + new Vector2(-250 - k * 5, 350 + k * 40), new Vector3(1, 1, 0.7f) * bright);
                }
                if (npc.ai[0] > 280)
                {
                    float bright = (npc.ai[0] - 280) / 50f; if (bright > 1) bright = 1;
                    Lighting.AddLight(npc.Center + new Vector2(40, 550 + k * 10), new Vector3(1, 1, 0.7f) * bright);
                    Lighting.AddLight(npc.Center + new Vector2(-40, 550 + k * 10), new Vector3(1, 1, 0.7f) * bright);
                }
            }
            if (Main.player.Any(p => Vector2.Distance(p.Center, npc.Center) < 2000) && !Main.npc.Any(n => n.active && n.type == ModContent.NPCType<NPCs.Boss.OvergrowBoss.OvergrowBoss>()) && !LegendWorld.OvergrowBossFree)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 250, ModContent.NPCType<NPCs.Boss.OvergrowBoss.OvergrowBoss>());

                NPC.NewNPC((int)npc.Center.X - 790, (int)npc.Center.Y + 450, ModContent.NPCType<NPCs.Boss.OvergrowBoss.OvergrowBossAnchor>());
                NPC.NewNPC((int)npc.Center.X + 790, (int)npc.Center.Y + 450, ModContent.NPCType<NPCs.Boss.OvergrowBoss.OvergrowBossAnchor>());
                NPC.NewNPC((int)npc.Center.X - 300, (int)npc.Center.Y + 600, ModContent.NPCType<NPCs.Boss.OvergrowBoss.OvergrowBossAnchor>());
                NPC.NewNPC((int)npc.Center.X + 300, (int)npc.Center.Y + 600, ModContent.NPCType<NPCs.Boss.OvergrowBoss.OvergrowBossAnchor>());
            }

            if (LegendWorld.OvergrowBossOpen && npc.ai[0] <= 360) npc.ai[0]++;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 pos = npc.Center;
            Vector2 dpos = pos - Main.screenPosition;

            Texture2D frametex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/WindowFrame");
            Texture2D glasstex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/WindowGlass");
        
            spriteBatch.Draw(glasstex, dpos, glasstex.Frame(), Color.White * 0.2f, 0, glasstex.Frame().Size() / 2, 1, 0, 0); //glass

            spriteBatch.Draw(frametex, dpos, frametex.Frame(), new Color(255, 255, 200), 0, frametex.Frame().Size() / 2, 1, 0, 0); //frame

            return false;
        }
    }
}

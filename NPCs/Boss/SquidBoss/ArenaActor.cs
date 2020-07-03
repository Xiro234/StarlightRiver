﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Core;
using StarlightRiver.Items.Permafrost;
using StarlightRiver.Tiles.Permafrost;
using Terraria;
using Terraria.Graphics;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.NPCs.Boss.SquidBoss
{
    class ArenaActor : ModNPC
    {
        int whitelistID = WallType<AuroraBrickWall>();
        public float WaterLevel { get => npc.Center.Y + 35 * 16 - npc.ai[0]; }

        public override string Texture => "StarlightRiver/Invisible";

        public override void SetStaticDefaults() => DisplayName.SetDefault("");

        public override bool? CanBeHitByItem(Player player, Item item) => false;

        public override bool? CanBeHitByProjectile(Projectile projectile) => false;

        public override bool CheckActive() => false;

        public override void SetDefaults()
        {
            npc.dontTakeDamage = true;
            npc.dontCountMe = true;
            npc.immortal = true;
            npc.noGravity = true;
            npc.lifeMax = 2;
        }

        public override void AI()
        {
            npc.ai[1] += 0.04f; //used as timers for visuals
            npc.ai[2] += 0.01f;

            if (npc.ai[0] < 150) npc.ai[0] = 150; //water clamping and return logic

            if (!Main.npc.Any(n => n.active && n.modNPC is SquidBoss) && npc.ai[0] > 150) npc.ai[0]--;

            if (npc.ai[1] > 6.28f) npc.ai[1] = 0;

            if (!Main.npc.Any(n => n.active && n.modNPC is IcePlatform)) //spawn platforms if not present
            {
                SpawnPlatform(-640, 200);
                SpawnPlatform(640, 200);

                SpawnPlatform(-400, -70);
                SpawnPlatform(400, -70);

                SpawnPlatform(-150, -260);
                SpawnPlatform(150, -260);

                SpawnPlatform(-240, -150, true);
                SpawnPlatform(240, -150, true);

                SpawnPlatform(-460, 30, true);
                SpawnPlatform(460, 30, true);

                SpawnPlatform(-140, 300, true);
                SpawnPlatform(140, 300, true);

                SpawnPlatform(-340, 240, true);
                SpawnPlatform(340, 240, true);

                NPC.NewNPC((int)(npc.Center.X), (int)(npc.Center.Y - 2000), NPCType<GoldPlatform>());
            }

            Vector2 pos = npc.Center + new Vector2(-800, 35 * 16) + new Vector2(0, -npc.ai[0]);

            //Lighting
            for(int k = 0; k < 45; k++)
            {
                Vector2 target = pos + new Vector2(k / 45f * 3200, 0);

                if (Main.tile[(int)target.X / 16, (int)target.Y / 16].wall == whitelistID)
                {
                    float sin = (float)Math.Sin(npc.ai[1] + k);
                    float sin2 = (float)Math.Sin(npc.ai[2] + k * 0.2f);
                    float cos = (float)Math.Cos(npc.ai[2] + k);
                    Lighting.AddLight(target, new Vector3(10 * (1 + sin2), 14 * (1 + cos), 18) * (0.02f + sin * 0.003f));
                }
            }

            for (int k = 0; k < 10; k++)
            {
                Lighting.AddLight(npc.Center + new Vector2(0, -200 + k * 60), new Vector3(1, 1, 1) * 0.4f);
                Lighting.AddLight(npc.Center + new Vector2(-400, -200 + k * 60), new Vector3(1, 1, 1) * 0.2f);
                Lighting.AddLight(npc.Center + new Vector2(400, -200 + k * 60), new Vector3(1, 1, 1) * 0.2f);
            }

            foreach (Player player in Main.player.Where(n => n.Hitbox.Intersects(new Rectangle((int)pos.X, (int)pos.Y, 100 * 16, (int)npc.ai[0])))) //water collision
            {
                player.wet = true;
                player.AddBuff(BuffType<Buffs.PrismaticDrown>(), 4, false);
            }

            for(int k = 0; k < Main.maxItems; k++)
            {
                Item item = Main.item[k];

                if (item.Hitbox.Intersects(new Rectangle((int)pos.X, (int)pos.Y + 8, 200 * 16, (int)npc.ai[0])) && item.velocity.Y > -4) item.velocity.Y -= 0.2f;

                if (item.Hitbox.Intersects(new Rectangle((int)pos.X, (int)pos.Y - 8, 200 * 16, 16)))
                {
                    item.position.Y = WaterLevel - 16 + (float)Math.Sin((npc.ai[1] + item.position.X) % 6.28f) * 4;

                    if (item.type == ItemType<SquidBossSpawn>() && npc.ai[0] == 150 && !Main.npc.Any(n => n.active && n.modNPC is SquidBoss)) //ready to spawn another squid              
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 630, NPCType<SquidBoss>());
                        item.TurnToAir();

                        for (int n = 0; n < 50; n++) Dust.NewDustPerfect(item.Center, DustType<Dusts.Starlight>(), Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(20));
                    }
                }
            }
        }

        public void DrawWater(SpriteBatch spriteBatch)
        {
            Vector2 pos = npc.Center + new Vector2(- 1600, 35 * 16) + new Vector2(0, -npc.ai[0]) - Main.screenPosition;

            pos += Main.screenPosition;
            for(int x = (int)pos.X / 16; x < (int)pos.X / 16 + 200; x++)
            {
                for (int y = (int)pos.Y / 16; y <= (int)pos.Y / 16 + (int)npc.ai[0] / 16; y++)
                {
                    if (WorldGen.InWorld(x, y + 1) && Helper.OnScreen(new Vector2(x, y + 1) * 16 - Main.screenPosition))
                    {
                        if (Main.tile[x, y + 1].active() && Lighting.Brightness(x, y + 1) <= 0.4f)
                        {
                            Color color = Color.Black * (1 - Lighting.Brightness(x, y + 1) * 4);
                            spriteBatch.Draw(Main.blackTileTexture, new Vector2(x, y + 1) * 16 - Main.screenPosition, color);
                        }

                        if (Main.tile[x, y + 1].wall == whitelistID)
                        {
                            Color color = Lighting.GetColor(x, y + 1).MultiplyRGB(new Color(100, 200, 255)) * (0.5f - Lighting.Brightness(x, y + 1) * 0.2f);
                            spriteBatch.Draw(Main.blackTileTexture, new Vector2(x, y + 1) * 16 - Main.screenPosition, color);
                        }

                        if (Main.tile[x, y].wall == whitelistID && y == (int)pos.Y / 16)
                        {
                            Color color = Lighting.GetColor(x, y + 1).MultiplyRGB(new Color(100, 200, 255)) * (0.5f - Lighting.Brightness(x, y + 1) * 0.2f);

                            float offset = npc.ai[0] % 16;
                            if (offset == 0) offset = 16;
                            offset += (float)Math.Sin(npc.ai[1] * (4) + x) * (2);

                            spriteBatch.Draw(Main.blackTileTexture, new Rectangle((int)(x * 16 - Main.screenPosition.X), (int)((y + 1) * 16 - Main.screenPosition.Y - (offset) + 1), 16, (int)(offset)), color);
                            spriteBatch.Draw(Main.magicPixel, new Rectangle((int)(x * 16 - Main.screenPosition.X), (int)((y + 1) * 16 - Main.screenPosition.Y - (offset) + 1), 16, 2), Main.magicPixel.Frame(), Color.White * 0.6f);
                        }
                    }
                }
            }
        }

        public void DrawWindow(SpriteBatch spriteBatch) 
        {
            Color color = new Color(230, 255, 255);

            Rectangle source = new Rectangle(0, 100, 512, 200);
            spriteBatch.Draw(TextureManager.Load("Images/Misc/Perlin"), new Rectangle((int)npc.Center.X - 520 - (int)Main.screenPosition.X, (int)npc.Center.Y - 400 - (int)Main.screenPosition.Y, 1040, 850), source, color * 0.8f);

            Texture2D tex = GetTexture("StarlightRiver/NPCs/Boss/SquidBoss/Window");
            for (int x = 0; x < tex.Width / 16; x++)
            {
                for (int y = 0; y < tex.Height / 16; y++)
                {
                    Vector2 pos = npc.Center - (tex.Size() / 2) + new Vector2(x, y - 7) * 16;

                    if(Helper.OnScreen(pos - Main.screenPosition))
                        spriteBatch.Draw(tex, pos - Main.screenPosition, new Rectangle(x * 16, y * 16, 16, 16), Lighting.GetColor((int)pos.X / 16, (int)pos.Y / 16), 0, Vector2.Zero, 1, 0, 0);
                }
            }

            Texture2D tex2 = GetTexture("StarlightRiver/NPCs/Boss/SquidBoss/WindowIn");
            spriteBatch.Draw(tex2, npc.Center + new Vector2(0, -7 * 16) - Main.screenPosition, null, Color.White * 0.4f, 0, tex2.Size() / 2, 1, 0, 0);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, default, default, default, default, Main.GameViewMatrix.ZoomMatrix);

            Texture2D tex3 = GetTexture("StarlightRiver/NPCs/Boss/SquidBoss/Godray");

            for (int k = 0; k < 4; k++)
            {
                spriteBatch.Draw(tex3, npc.Center + new Vector2(450, -250) - Main.screenPosition, null, Color.White * 0.5f, 0.9f + (float)Math.Sin(npc.ai[2] * 2 + k) * 0.13f, Vector2.Zero, 1.5f, 0, 0);
                spriteBatch.Draw(tex3, npc.Center + new Vector2(-450, -250) - Main.screenPosition, null, Color.White * 0.5f, 0.45f + (float)Math.Sin(npc.ai[2] * 2 + k) * 0.13f, Vector2.Zero, 1.5f, 0, 0);

                spriteBatch.Draw(tex3, npc.Center + new Vector2(0, -450) - Main.screenPosition, null, Color.White * 0.5f, 0.68f + (float)Math.Sin(npc.ai[2] * 2 + (k / 4f *  6.28f)) * 0.13f, Vector2.Zero, 1.9f, 0, 0);
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, default, default, default, default, default, Main.GameViewMatrix.ZoomMatrix);

            DrawWindow(spriteBatch, new Vector2(0, -70), new Color(255, 200, 255));

            DrawWindow(spriteBatch, new Vector2(-20, -65), new Color(200, 255, 200));
            DrawWindow(spriteBatch, new Vector2(-11, -105), new Color(200, 255, 255));

            DrawWindow(spriteBatch, new Vector2(20, -65), new Color(200, 255, 200));
            DrawWindow(spriteBatch, new Vector2(11, -105), new Color(200, 255, 255));
        }

        private void DrawWindow(SpriteBatch spriteBatch, Vector2 off, Color color)
        {
            Texture2D tex = GetTexture("StarlightRiver/NPCs/Boss/SquidBoss/SmallWindowIn");
            Texture2D tex2 = GetTexture("StarlightRiver/NPCs/Boss/SquidBoss/TentacleGlow");
            Texture2D tex3 = GetTexture("StarlightRiver/NPCs/Boss/SquidBoss/TentacleTop");
            Texture2D tex4 = GetTexture("StarlightRiver/NPCs/Boss/SquidBoss/TentacleBody");
            Texture2D tex5 = GetTexture("StarlightRiver/NPCs/Boss/SquidBoss/BodyUnder");

            spriteBatch.Draw(tex, npc.Center + new Vector2(off.X * 16, off.Y * 16) + Vector2.One.RotatedBy(npc.ai[1] * 2) * 2 - Main.screenPosition, null, new Color(70, 95, 125), 0, tex.Size() / 2, 1, 0, 0);

            if (!Main.npc.Any(n => n.active && n.modNPC is SquidBoss) && !StarlightWorld.SquidBossDowned)
            {
                if (off.X == 0)
                {
                    Vector2 tentaclePos = new Vector2(off.X * 16 + (float)Math.Cos(npc.ai[1] + off.X * 0.2f) * 5, off.Y * 16 + (float)Math.Sin(npc.ai[1] + off.X * 0.2f) * 2);
                    spriteBatch.Draw(tex5, npc.Center + tentaclePos + new Vector2(0, 210) - Main.screenPosition, new Rectangle(26, 0, 128, 150), Color.White, 0, new Vector2(64, 50), 1, 0, 0);
                }
                else
                {
                    Vector2 tentaclePos = new Vector2(off.X * 16 + (float)Math.Cos(npc.ai[1] + off.X * 0.2f) * 20, off.Y * 16 + (float)Math.Sin(npc.ai[1] + off.X * 0.2f) * 5);
                    float tentacleRot = ((npc.Center + tentaclePos) - (npc.Center + off * 16 + new Vector2(0, 200))).ToRotation() + 1.57f;

                    for (int k = 0; k < 32; k++)
                        spriteBatch.Draw(tex4, npc.Center + Vector2.Lerp(tentaclePos, off * 16 + new Vector2((float)Math.Sin(npc.ai[1] + k * 0.5f) * 5, 200), k / 20f) - Main.screenPosition, 
                            null, Color.White, 0, tex4.Size() / 2, 1, 0, 0);

                    spriteBatch.Draw(tex2, npc.Center + tentaclePos - Main.screenPosition, null, Color.White, tentacleRot, tex2.Size() / 2, 1, 0, 0);
                    spriteBatch.Draw(tex3, npc.Center + tentaclePos - Main.screenPosition, null, Color.White, tentacleRot, tex2.Size() / 2, 1, 0, 0);
                }
            }

            spriteBatch.Draw(tex, npc.Center + new Vector2(off.X * 16, off.Y * 16) - Main.screenPosition, null, color * 0.5f, 0, tex.Size() / 2, 1, 0, 0);

            for(int k = 0; k < 5; k++) Lighting.AddLight(npc.Center + new Vector2(off.X * 16, off.Y * 16) + new Vector2(0, -100 + k * 50), color.ToVector3() * 0.5f);

            Texture2D tex6 = GetTexture("StarlightRiver/NPCs/Boss/SquidBoss/SmallWindow");

            for (int x = 0; x < tex6.Width / 16; x++)
                for (int y = 0; y < tex6.Height / 16; y++)
                {
                    Vector2 pos = npc.Center - (tex6.Size() / 2) + new Vector2(x + off.X, y + off.Y) * 16;
                    spriteBatch.Draw(tex6, pos - Main.screenPosition, new Rectangle(x * 16, y * 16, 16, 16), Lighting.GetColor((int)pos.X / 16, (int)pos.Y / 16), 0, Vector2.Zero, 1, 0, 0);
                }

            Dust d = Dust.NewDustPerfect(npc.Center + off * 16 + new Vector2(Main.rand.Next(-60, 60), 300 - Main.rand.Next(400)), 257, new Vector2(0, -2), 200, color * 1.8f, 1);
            d.noGravity = true;
        }

        private void SpawnPlatform(int x, int y, bool small = false)
        {
            if(small) NPC.NewNPC((int)(npc.Center.X + x), (int)(npc.Center.Y + y), NPCType<IcePlatformSmall>());
            else NPC.NewNPC((int)(npc.Center.X + x), (int)(npc.Center.Y + y), NPCType<IcePlatform>() );
        }
    }
}

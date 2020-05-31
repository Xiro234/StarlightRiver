﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    public class VitricBackdropLeft : ModNPC
    {
        public const int Scrolltime = 1000;
        public const int Risetime = 360;
        public override bool CheckActive()
        {
            return false;
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return false;
        }

        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void SetDefaults()
        {
            npc.height = 1;
            npc.width = 560;
            npc.aiStyle = -1;
            npc.lifeMax = 2;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.dontTakeDamage = true;
            npc.dontCountMe = true;
        }
        public override void AI()
        {
            /* AI fields:
             * 0: timer
             * 1: activation state, im too lazy to create an enum for this so: (0 = hidden, 1 = rising, 2 = still, 3 = scrolling)
             * 2: scrolling timer
             * 3:
             */

            if (LegendWorld.GlassBossOpen && npc.ai[1] == 0)
            {
                npc.ai[1] = 1; //when the altar is hit, make the BG rise out of the ground
            }

            if (npc.ai[1] == 1)
            {
                SpawnPlatforms();

                if (npc.ai[0] == Risetime - 1) //hitting the top
                {
                    Main.LocalPlayer.GetModPlayer<StarlightPlayer>().Shake += 18;
                    Main.PlaySound(SoundID.NPCDeath9);
                }
                if (npc.ai[0]++ > Risetime)
                {
                    npc.ai[1] = 2;
                }

                if (npc.ai[0] % 10 == 0)
                {
                    Main.LocalPlayer.GetModPlayer<StarlightPlayer>().Shake += npc.ai[0] < 100 ? 5 : 3;
                }

                for (int k = 0; k < 18; k++)
                {
                    Dust.NewDust(npc.position, 560, 1, ModContent.DustType<Dusts.Sand>(), 0, Main.rand.NextFloat(-5f, -1f), Main.rand.Next(255), default, Main.rand.NextFloat(1.5f)); //spawns dust
                }
            }
            if (npc.ai[1] == 2)
            {
                npc.ai[0] = Risetime;
            }

            if (npc.ai[1] == 3)
            {
                npc.ai[2]++;
            }

            if (npc.ai[2] > Scrolltime)
            {
                npc.ai[2] = 0;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            return false;
        }
        public void SpecialDraw(SpriteBatch spriteBatch)
        {
            if (npc.ai[1] != 3) //animation for rising out of the sand
            {
                Texture2D tex = ModContent.GetTexture(Texture);
                int targetHeight = (int)(npc.ai[0] / Risetime * tex.Height);
                Rectangle target = new Rectangle((int)(npc.position.X - Main.screenPosition.X), (int)(npc.position.Y - targetHeight - Main.screenPosition.Y), tex.Width, targetHeight);
                Rectangle source = new Rectangle(0, 0, tex.Width, targetHeight);
                Color color = new Color(180, 225, 255);

                spriteBatch.Draw(tex, target, source, color, 0, Vector2.Zero, 0, 0);
            }
            else
            {
                ScrollDraw(spriteBatch);
            }
        }
        public virtual void ScrollDraw(SpriteBatch sb) //im lazy
        {
            Texture2D tex = ModContent.GetTexture(Texture);
            int height1 = (int)(npc.ai[2] / Scrolltime * tex.Height);
            int height2 = tex.Height - height1;
            Color color = new Color(180, 225, 255);

            Rectangle target1 = new Rectangle((int)(npc.position.X - Main.screenPosition.X), (int)(npc.position.Y - height1 - Main.screenPosition.Y), tex.Width, height1);
            Rectangle target2 = new Rectangle((int)(npc.position.X - Main.screenPosition.X), (int)(npc.position.Y - height1 - height2 - Main.screenPosition.Y), tex.Width, height2);
            Rectangle source1 = new Rectangle(0, 0, tex.Width, height1);
            Rectangle source2 = new Rectangle(0, tex.Height - height2, tex.Width, height2);

            sb.Draw(tex, target1, source1, color, 0, Vector2.Zero, 0, 0);
            sb.Draw(tex, target2, source2, color, 0, Vector2.Zero, 0, 0);
        }
        public virtual void SpawnPlatforms(bool rising = true)
        {
            PlacePlatform(205, 136, ModContent.NPCType<VitricBossPlatformUp>(), rising);
            PlacePlatform(140, 420, ModContent.NPCType<VitricBossPlatformUp>(), rising);
            PlacePlatform(440, 668, ModContent.NPCType<VitricBossPlatformUp>(), rising);
            PlacePlatform(210, 30, ModContent.NPCType<VitricBossPlatformUpSmall>(), rising);
            PlacePlatform(400, 230, ModContent.NPCType<VitricBossPlatformUpSmall>(), rising);
            PlacePlatform(280, 310, ModContent.NPCType<VitricBossPlatformUpSmall>(), rising);
            PlacePlatform(230, 570, ModContent.NPCType<VitricBossPlatformUpSmall>(), rising);
            PlacePlatform(260, 790, ModContent.NPCType<VitricBossPlatformUpSmall>(), rising);
        }
        public void PlacePlatform(int x, int y, int type, bool rising)
        {
            if (rising && npc.ai[0] == Risetime - (int)(y / 880f * Risetime))
            {
                NPC.NewNPC((int)npc.position.X + x, (int)npc.position.Y - 2, type, 0, 0, Risetime - npc.ai[0]); //When rising out of the ground, check for the appropriate time to spawn the platform based on y coord
            }
            else if (!rising)
            {
                NPC.NewNPC((int)npc.position.X + x, (int)npc.position.Y - y, type, 0, 2, Risetime); //otherwise spawn it instantly AT the y coord
            }
        }
    }
    public class VitricBackdropRight : VitricBackdropLeft //im lazy
    {
        public override void ScrollDraw(SpriteBatch sb)
        {
            Texture2D tex = ModContent.GetTexture(Texture);
            int height1 = (int)(npc.ai[2] / Scrolltime * tex.Height);
            int height2 = tex.Height - height1;
            Color color = new Color(180, 225, 255);

            Rectangle target1 = new Rectangle((int)(npc.position.X - Main.screenPosition.X), (int)(npc.position.Y - tex.Height * 2 + height1 + height2 - Main.screenPosition.Y), tex.Width, height1);
            Rectangle target2 = new Rectangle((int)(npc.position.X - Main.screenPosition.X), (int)(npc.position.Y - tex.Height + height1 - Main.screenPosition.Y), tex.Width, height2);
            Rectangle source2 = new Rectangle(0, 0, tex.Width, height2);
            Rectangle source1 = new Rectangle(0, tex.Height - height1, tex.Width, height1);

            sb.Draw(tex, target1, source1, color, 0, Vector2.Zero, 0, 0);
            sb.Draw(tex, target2, source2, color, 0, Vector2.Zero, 0, 0);
        }

        public override void SpawnPlatforms(bool rising = true)
        {
            PlacePlatform(150, 90, ModContent.NPCType<VitricBossPlatformDown>(), rising);
            PlacePlatform(260, 330, ModContent.NPCType<VitricBossPlatformDown>(), rising);
            PlacePlatform(180, 580, ModContent.NPCType<VitricBossPlatformDown>(), rising);
            PlacePlatform(380, 200, ModContent.NPCType<VitricBossPlatformDownSmall>(), rising);
            PlacePlatform(80, 440, ModContent.NPCType<VitricBossPlatformDownSmall>(), rising);
            PlacePlatform(410, 660, ModContent.NPCType<VitricBossPlatformDownSmall>(), rising);
            PlacePlatform(280, 760, ModContent.NPCType<VitricBossPlatformDownSmall>(), rising);
        }
    }
}

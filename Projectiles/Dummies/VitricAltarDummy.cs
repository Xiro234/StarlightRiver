using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.NPCs.Boss.VitricBoss;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.Dummies
{
    internal class VitricAltarDummy : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override string Texture => "StarlightRiver/Invisible";
        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 96;
            projectile.aiStyle = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            projectile.timeLeft = 2;
            Point16 parentPos = new Point16((int)projectile.position.X / 16, (int)projectile.position.Y / 16);
            Tile parent = Framing.GetTileSafely(parentPos.X, parentPos.Y);
            if (!parent.active()) projectile.timeLeft = 0;

            if (parent.frameX == 0 && Main.player.Any(n => Abilities.AbilityHelper.CheckDash(n, projectile.Hitbox)))
            {
                Main.PlaySound(Terraria.ID.SoundID.Shatter);
                for (int k = 0; k < 100; k++) Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.Glass2>(), 0, 0, 0, default, 1.2f);
                for (int x = parentPos.X; x < parentPos.X + 5; x++)
                {
                    for (int y = parentPos.Y; y < parentPos.Y + 7; y++)
                    {
                        Framing.GetTileSafely(x, y).frameX += 90;
                    }
                }
            }

            if (parent.frameX == 90 && !LegendWorld.GlassBossOpen)
            {
                Main.LocalPlayer.GetModPlayer<StarlightPlayer>().Shake += 1;
                Dust.NewDust(projectile.Center + new Vector2(-632, projectile.height / 2), 560, 1, ModContent.DustType<Dusts.Sand>(), 0, Main.rand.NextFloat(-5f, -1f), Main.rand.Next(255), default, Main.rand.NextFloat(1.5f));
                Dust.NewDust(projectile.Center + new Vector2(72, projectile.height / 2), 560, 1, ModContent.DustType<Dusts.Sand>(), 0, Main.rand.NextFloat(-5f, -1f), Main.rand.Next(255), default, Main.rand.NextFloat(1.5f));
                //Main.PlaySound(SoundID.); TODO: Rumble sound
                projectile.ai[1]++;
                if (projectile.ai[1] > 120)
                {
                    LegendWorld.GlassBossOpen = true;
                    if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneGlass)
                    {
                        Main.LocalPlayer.GetModPlayer<StarlightPlayer>().ScreenMovePan = projectile.Center + new Vector2(0, -400);
                        Main.LocalPlayer.GetModPlayer<StarlightPlayer>().ScreenMoveTarget = projectile.Center;
                        Main.LocalPlayer.GetModPlayer<StarlightPlayer>().ScreenMoveTime = VitricBackdropLeft.Risetime + 120;
                    }
                    projectile.ai[1] = 0;
                }
            }

            //This controls spawning the rest of the arena
            if (!Main.npc.Any(n => n.active && n.type == ModContent.NPCType<VitricBackdropLeft>())) //Need to find a better check
            {
                Main.NewText("Debug");
                Vector2 center = projectile.Center + new Vector2(0, 60);
                int timerset = LegendWorld.GlassBossOpen ? 360 : 0; //the arena should already be up if it was opened before

                int index = NPC.NewNPC((int)center.X + 352, (int)center.Y, ModContent.NPCType<VitricBackdropRight>(), 0, timerset);
                if (LegendWorld.GlassBossOpen && Main.npc[index].modNPC is VitricBackdropRight) (Main.npc[index].modNPC as VitricBackdropRight).SpawnPlatforms(false);

                index = NPC.NewNPC((int)center.X - 352, (int)center.Y, ModContent.NPCType<VitricBackdropLeft>(), 0, timerset);
                if (LegendWorld.GlassBossOpen && Main.npc[index].modNPC is VitricBackdropLeft) (Main.npc[index].modNPC as VitricBackdropLeft).SpawnPlatforms(false);
            }

            //controls the drawing of the barriers
            if (projectile.ai[0] < 120 && Main.npc.Any(n => n.active && n.type == ModContent.NPCType<VitricBoss>()))
            {
                projectile.ai[0]++;
                if (projectile.ai[0] % 3 == 0) Main.LocalPlayer.GetModPlayer<StarlightPlayer>().Shake += 2; //screenshake
                if (projectile.ai[0] == 119) //hitting the top
                {
                    Main.LocalPlayer.GetModPlayer<StarlightPlayer>().Shake += 25;
                    for (int k = 0; k < 5; k++) Main.PlaySound(Terraria.ID.SoundID.Tink);
                }
            }
            else if (!Main.npc.Any(n => n.active && n.type == ModContent.NPCType<VitricBoss>())) projectile.ai[0] = 0; //TODO fix this later
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) //actually drawing the barriers and item indicator
        {
            Point16 parentPos = new Point16((int)projectile.position.X / 16, (int)projectile.position.Y / 16);
            Tile parent = Framing.GetTileSafely(parentPos.X, parentPos.Y);

            if (parent.frameX >= 90 && !NPC.AnyNPCs(ModContent.NPCType<NPCs.Boss.VitricBoss.VitricBoss>()))
            {
                Helper.DrawSymbol(spriteBatch, projectile.Center - Main.screenPosition + new Vector2(0, (float)Math.Sin(LegendWorld.rottime) * 5 - 20), new Color(150, 220, 250));
            }
            else if (parent.frameX < 90)
            {
                Texture2D glow = ModContent.GetTexture("StarlightRiver/Tiles/Vitric/VitricBossAltarGlow");
                spriteBatch.Draw(glow, projectile.position - Main.screenPosition + new Vector2(3, -9), glow.Frame(), Color.White * (float)Math.Sin(LegendWorld.rottime), 0, Vector2.Zero, 1, 0, 0);
            }

            Vector2 center = projectile.Center + new Vector2(0, 56);
            Texture2D tex = ModContent.GetTexture("StarlightRiver/NPCs/Boss/VitricBoss/VitricBossBarrier");
            Color color = new Color(180, 225, 255);
            int off = (int)(projectile.ai[0] / 120f * 880);
            spriteBatch.Draw(tex, new Rectangle((int)center.X - 790 - (int)Main.screenPosition.X, (int)center.Y - off - (int)Main.screenPosition.Y, tex.Width, off),
                new Rectangle(0, 0, tex.Width, (int)(projectile.ai[0] / 120f * 880)), color);

            spriteBatch.Draw(tex, new Rectangle((int)center.X + 606 - (int)Main.screenPosition.X, (int)center.Y - off - (int)Main.screenPosition.Y, tex.Width, off),
                new Rectangle(0, 0, tex.Width, (int)(projectile.ai[0] / 120f * 880)), color);
        }
        public void SpawnBoss()
        {
            NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y + 500, ModContent.NPCType<VitricBoss>());
        }
    }
}

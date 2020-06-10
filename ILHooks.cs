using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using StarlightRiver.Configs;
using StarlightRiver.Dragons;
using StarlightRiver.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver
{
    public partial class StarlightRiver : Mod
    {
        private void HookIL()
        {
            // Vitric lighting
            IL.Terraria.Lighting.PreRenderPhase += VitricLighting;
            //IL.Terraria.Main.DrawInterface_14_EntityHealthBars += ForceRedDraw;
            //moonlord draw layer
            IL.Terraria.Main.DoDraw += DrawMoonlordLayer;
            //dragons
            IL.Terraria.Main.DrawMenu += DragonMenuAttach;
            //soulbound items
            IL.Terraria.UI.ChestUI.DepositAll += PreventSoulboundStack;
            //dynamic map icons
            IL.Terraria.Main.DrawMap += DynamicBossIcon;
            //jungle grass
            IL.Terraria.WorldGen.Convert += JungleGrassConvert;
            IL.Terraria.WorldGen.hardUpdateWorld += JungleGrassSpread;
            //title screen BGs
            //IL.Terraria.Main.DrawBG += DrawTitleScreen;
            //grappling hooks on moving platforms
            IL.Terraria.Projectile.VanillaAI += GrapplePlatforms;
        }
        #region IL edits
        private void GrapplePlatforms(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.TryGotoNext(i => i.MatchLdfld<Projectile>("aiStyle"), i => i.MatchLdcI4(7));
            c.TryGotoNext(i => i.MatchLdfld<Projectile>("ai"), i => i.MatchLdcI4(0), i => i.MatchLdelemR4(), i => i.MatchLdcR4(2));
            c.TryGotoNext(i => i.MatchLdloc(143)); //flag2 in source code
            c.Index++;
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<GrapplePlatformDelegate>(EmitGrapplePlatformDelegate);
            c.TryGotoNext(i => i.MatchStfld<Player>("grapCount"));
            c.Index++;
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<UngrapplePlatformDelegate>(EmitUngrapplePlatformDelegate);
        }
        private delegate bool GrapplePlatformDelegate(bool fail, Projectile proj);
        private bool EmitGrapplePlatformDelegate(bool fail, Projectile proj)
        {
            if (proj.timeLeft < 36000 - 3)
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    NPC n = Main.npc[k];
                    if (n.active && n.modNPC is NPCs.MovingPlatform && n.Hitbox.Intersects(proj.Hitbox))
                    {
                        proj.position += n.velocity;
                        return false;
                    }
                }
            return fail;
        }
        private delegate void UngrapplePlatformDelegate(Projectile proj);
        private void EmitUngrapplePlatformDelegate(Projectile proj)
        {
            Player player = Main.player[proj.owner];
            int numHooks = 3;
            //time to replicate retarded vanilla hardcoding, wheee
            if (proj.type == 165) numHooks = 8;
            if (proj.type == 256) numHooks = 2;
            if (proj.type == 372) numHooks = 2;
            if (proj.type == 652) numHooks = 1;
            if (proj.type >= 646 && proj.type <= 649) numHooks = 4;
            //end vanilla zoink

            ProjectileLoader.NumGrappleHooks(proj, player, ref numHooks);
            if (player.grapCount > numHooks) Main.projectile[player.grappling.OrderBy(n => (Main.projectile[n].active ? 0 : 999999) + Main.projectile[n].timeLeft).ToArray()[0]].Kill();
        }
        private void DrawTitleScreen(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.TryGotoNext(i => i.MatchLdsfld<Main>("gameMenu"));
            c.TryGotoNext(i => i.MatchLdsfld<Main>("quickBG"));
            c.Index++;
            c.EmitDelegate<TitleScreenDelegate>(EmitTitleScreenDelegate);
        }
        private delegate void TitleScreenDelegate();
        private void EmitTitleScreenDelegate()
        {
            if (Main.menuMode == 0)
            {
                switch (GetInstance<Config>().Style)
                {
                    case TitleScreenStyle.Starlight:
                        Main.bgStyle = 4;
                        break;
                    case TitleScreenStyle.CorruptJungle:
                        Main.bgStyle = GetSurfaceBgStyleSlot<Backgrounds.JungleCorruptBgStyle>();
                        break;
                }
            }
        }
        private void JungleGrassSpread(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            for (int k = 0; k < 3; k++)
            {
                int type;
                switch (k)
                {
                    case 0: type = ModContent.TileType<Tiles.JungleBloody.GrassJungleBloody>(); break;
                    case 2: type = ModContent.TileType<Tiles.JungleCorrupt.GrassJungleCorrupt>(); break;
                    case 1: type = ModContent.TileType<Tiles.JungleHoly.GrassJungleHoly>(); break;
                    default: type = 2; break;
                }

                for (int n = 0; n < 2; n++)
                {
                    c.TryGotoNext(i => i.MatchLdcI4(0), i => i.MatchStfld<Tile>("type"));
                    c.Index--;
                    c.Emit(OpCodes.Pop);
                    c.Emit(OpCodes.Ldc_I4, type);
                }
            }
        }
        private void JungleGrassConvert(ILContext il) //Fun stuff.
        {
            ILCursor c = new ILCursor(il);
            for (int k = 0; k < 3; k++)
            {
                int type;
                int index;
                switch (k)
                {
                    case 0: type = ModContent.TileType<Tiles.JungleBloody.GrassJungleBloody>(); break;
                    case 2: type = ModContent.TileType<Tiles.JungleCorrupt.GrassJungleCorrupt>(); break;
                    case 1: type = ModContent.TileType<Tiles.JungleHoly.GrassJungleHoly>(); break;
                    default: type = 2; break;
                }
                c.TryGotoNext(i => i.MatchLdsfld(typeof(TileID.Sets.Conversion).GetField("Grass")));
                c.TryGotoNext(i => i.MatchLdsfld(typeof(TileID.Sets.Conversion).GetField("Ice")));
                index = c.Index--;
                c.TryGotoPrev(i => i.MatchLdsfld(typeof(TileID.Sets.Conversion).GetField("Grass")));
                c.Index++;
                c.Emit(OpCodes.Pop);
                c.Emit(OpCodes.Ldc_I4, type);
                c.Emit(OpCodes.Ldloc_0);
                c.Emit(OpCodes.Ldloc_1);
                c.EmitDelegate<GrassConvertDelegate>(EmitGrassConvertDelegate);
                c.Emit(OpCodes.Brtrue, il.Instrs[index]);
                c.Emit(OpCodes.Ldsfld, typeof(TileID.Sets.Conversion).GetField("Grass"));
            }
            c.TryGotoNext(i => i.MatchLdfld<Tile>("wall"), i => i.MatchLdcI4(69));
            c.TryGotoPrev(i => i.MatchLdsfld<Main>("tile"));
            c.Index++;
            c.Emit(OpCodes.Ldc_I4, TileID.JungleGrass);
            c.Emit(OpCodes.Ldloc_0);
            c.Emit(OpCodes.Ldloc_1);
            c.EmitDelegate<GrassConvertDelegate>(EmitGrassConvertDelegate);
            c.Emit(OpCodes.Pop);
        }
        private delegate bool GrassConvertDelegate(int type, int x, int y);
        private bool EmitGrassConvertDelegate(int type, int x, int y)
        {
            Tile tile = Main.tile[x, y];
            if (tile.type == TileID.JungleGrass || tile.type == ModContent.TileType<Tiles.JungleBloody.GrassJungleBloody>() || tile.type == ModContent.TileType<Tiles.JungleCorrupt.GrassJungleCorrupt>() || tile.type == ModContent.TileType<Tiles.JungleHoly.GrassJungleHoly>())
            {
                tile.type = (ushort)type;
                return true;
            }
            return false;
        }
        private void DynamicBossIcon(ILContext il)
        {
            //Hillariously repetitive IL edit to draw custom icons dynamically. Funny. Fuck vanilla.
            ILCursor c = new ILCursor(il);
            //the overlay map comes first
            c.TryGotoNext(i => i.MatchCallvirt<NPC>("GetBossHeadTextureIndex")); //only gonna break down one of these, finds a point in vanilla where we can reasonably draw boss icons

            c.EmitDelegate<DynamicIconDelegate>(EmitDynamicIconDelegateOverlay);

            c.TryGotoNext(i => i.MatchCallvirt<NPC>("GetBossHeadTextureIndex"));
            c.Index++;
            c.TryGotoNext(i => i.MatchCallvirt<NPC>("GetBossHeadTextureIndex"));
            c.Index++;
            c.TryGotoNext(i => i.MatchCallvirt<NPC>("GetBossHeadTextureIndex"));

            c.EmitDelegate<DynamicIconDelegate>(EmitDynamicIconDelegateMinimap);

            c.TryGotoNext(i => i.MatchCallvirt<NPC>("GetBossHeadTextureIndex"));
            c.Index++;
            c.TryGotoNext(i => i.MatchCallvirt<NPC>("GetBossHeadTextureIndex"));
            c.Index++;
            c.TryGotoNext(i => i.MatchCallvirt<NPC>("GetBossHeadTextureIndex"));

            c.EmitDelegate<DynamicIconDelegate>(EmitDynamicIconDelegateFullmap);

        }
        private delegate NPC DynamicIconDelegate(NPC npc);
        private NPC EmitDynamicIconDelegateOverlay(NPC npc)
        {
            if (npc?.active == true && npc.modNPC is NPCs.IDynamicMapIcon)
            {
                Vector2 npcPos = npc.Center;
                Vector2 framePos = (Main.screenPosition + new Vector2(Main.screenWidth / 2, Main.screenHeight / 2));
                Vector2 target = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2) + (npcPos - framePos) / 16 * Main.mapOverlayScale;

                float scale = (Main.mapMinimapScale * 0.25f * 2f + 1f) / 3f;

                (npc.modNPC as NPCs.IDynamicMapIcon).DrawOnMap(Main.spriteBatch, target, scale, Color.White * Main.mapOverlayAlpha);
            }
            return npc;
        }
        private NPC EmitDynamicIconDelegateMinimap(NPC npc)
        {
            if (npc?.active == true && npc.modNPC is NPCs.IDynamicMapIcon)
            {
                Vector2 mapPos = new Vector2(Main.miniMapX, Main.miniMapY);
                Vector2 npcPos = npc.Center;
                Vector2 framePos = (Main.screenPosition + new Vector2(Main.screenWidth / 2, Main.screenHeight / 2));
                Vector2 target = mapPos + Vector2.One * 117 + (npcPos - framePos) / 16 * Main.mapMinimapScale;

                if (target.X > Main.miniMapX + 15 && target.Y > Main.miniMapY + 15 && target.X < Main.miniMapX + 235 && target.Y < Main.miniMapY + 235) //only draw on the minimap
                {
                    float scale = (Main.mapMinimapScale * 0.25f * 2f + 1f) / 3f;

                    (npc.modNPC as NPCs.IDynamicMapIcon).DrawOnMap(Main.spriteBatch, target, scale, Color.White);
                    if (new Rectangle((int)target.X - (int)(15 * scale), (int)target.Y - (int)(15 * scale), (int)(30 * scale), (int)(30 * scale)).Contains(Main.MouseScreen.ToPoint()))
                    {
                        Utils.DrawBorderString(Main.spriteBatch, npc.GivenOrTypeName, Main.MouseScreen + Vector2.One * 15, Main.mouseTextColorReal);
                    }
                }
            }
            return npc;
        }
        private NPC EmitDynamicIconDelegateFullmap(NPC npc)
        {
            if (npc?.active == true && npc.modNPC is NPCs.IDynamicMapIcon)
            {
                float mapScale = Main.mapFullscreenScale / Main.UIScale;

                float mapFullscrX = Main.mapFullscreenPos.X * mapScale;
                float mapFullscrY = Main.mapFullscreenPos.Y * mapScale;
                float mapX = -mapFullscrX + (Main.screenWidth / 2f);
                float mapY = -mapFullscrY + (Main.screenHeight / 2f);

                Vector2 mapPos = new Vector2(mapX, mapY);
                Vector2 npcPos = npc.Center;
                Vector2 target = mapPos + npcPos / 16 * Main.mapFullscreenScale;

                float scale = Main.UIScale;

                (npc.modNPC as NPCs.IDynamicMapIcon).DrawOnMap(Main.spriteBatch, target, scale, Color.White);
                if (new Rectangle((int)target.X - (int)(15 * scale), (int)target.Y - (int)(15 * scale), (int)(30 * scale), (int)(30 * scale)).Contains(Main.MouseScreen.ToPoint()))
                {
                    Utils.DrawBorderString(Main.spriteBatch, npc.GivenOrTypeName, Main.MouseScreen + Vector2.One * 15, Main.mouseTextColorReal);
                }
            }
            return npc;
        }
        private void PreventSoulboundStack(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            c.TryGotoNext(i => i.MatchLdloc(1), i => i.MatchLdcI4(1), i => i.MatchSub());
            Instruction target = c.Prev.Previous;

            c.TryGotoPrev(n => n.MatchLdfld<Item>("favorited"));
            c.Index++;

            c.Emit(OpCodes.Ldloc_0);
            c.EmitDelegate<SoulboundDelegate>(EmitSoulboundDel);
            c.Emit(OpCodes.Brtrue_S, target);
        }
        private delegate bool SoulboundDelegate(int index);
        private bool EmitSoulboundDel(int index)
        {
            return Main.LocalPlayer.inventory[index].modItem is Items.SoulboundItem;
        }
        //IL edit for dragon customization
        private void DragonMenuAttach(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.GotoNext(n => n.MatchLdsfld<Main>("menuMode") && n.Next.MatchLdcI4(2));
            c.Index++;

            c.EmitDelegate<DragonMenuDelegate>(EmitDragonDel);
        }
        private delegate void DragonMenuDelegate();
        private DragonMenu dragonMenu = new DragonMenu();
        private UserInterface dragonMenuUI = new UserInterface();
        private void EmitDragonDel()
        {
            if (Main.menuMode == 2 || DragonMenu.visible)
            {
                if (!DragonMenu.created)
                {
                    dragonMenu = new DragonMenu();
                    dragonMenu.OnInitialize();
                    Main.PendingPlayer.GetModPlayer<DragonHandler>().data.SetDefault();
                    dragonMenu.dragon = Main.PendingPlayer.GetModPlayer<DragonHandler>();
                    DragonMenu.created = true;

                    dragonMenuUI = new UserInterface();
                    dragonMenuUI.SetState(dragonMenu);
                }
                SpriteBatch spriteBatch = Main.spriteBatch;

                if (dragonMenu != null && dragonMenuUI != null)
                {
                    dragonMenu.Draw(spriteBatch);
                }

            }
            else
            {
                DragonMenu.created = false;
                dragonMenu = null;
                dragonMenuUI = null;
            }
        }

        // IL edit to get the overgrow boss window drawing correctly
        private delegate void DrawWindowDelegate();
        private void DrawMoonlordLayer(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.TryGotoNext(n => n.MatchLdfld<Main>("DrawCacheNPCsMoonMoon"));
            c.Index--;

            c.EmitDelegate<DrawWindowDelegate>(EmitMoonlordLayerDel);
        }
        //private readonly List<BootlegDust> WindowDust = new List<BootlegDust>();
        private void EmitMoonlordLayerDel()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (!Main.npc[i].active) return;

                if (Main.npc[i].type == ModContent.NPCType<Projectiles.Dummies.OvergrowBossWindowDummy>())
                {
                    NPC npc = Main.npc[i];
                    Vector2 pos = npc.Center;
                    Vector2 dpos = pos - Main.screenPosition;
                    SpriteBatch spriteBatch = Main.spriteBatch;

                    //background
                    Texture2D backtex1 = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/Window4");
                    spriteBatch.Draw(backtex1, dpos, new Rectangle(0, 0, 100, 100), new Color(205, 165, 70), 0, Vector2.One * 50, 10, 0, 0);

                    for (int k = -5; k < 5; k++)//back row
                    {
                        Texture2D backtex2 = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/Window3");
                        Vector2 thispos = dpos + new Vector2(k * backtex2.Width, 300) + FindOffset(pos, 0.4f);
                        if (Vector2.Distance(thispos, dpos) < 800)
                            spriteBatch.Draw(backtex2, thispos, backtex2.Frame(), Color.White, 0, backtex2.Frame().Size() / 2, 1, 0, 0);
                    }
                    for (int k = -5; k < 5; k++)//mid row
                    {
                        Texture2D backtex3 = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/Window2");
                        Vector2 thispos = dpos + new Vector2(k * backtex3.Width, 350) + FindOffset(pos, 0.3f);
                        if (Vector2.Distance(thispos, dpos) < 800)
                            spriteBatch.Draw(backtex3, thispos, backtex3.Frame(), Color.White, 0, backtex3.Frame().Size() / 2, 1, 0, 0);
                    }

                    //sun
                    spriteBatch.End();
                    spriteBatch.Begin(default, BlendState.Additive, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);

                    Texture2D tex = ModContent.GetTexture("StarlightRiver/Keys/Glow");

                    // Update + draw dusts
                    /*foreach (BootlegDust dust in WindowDust)
                    {
                        dust.SafeDraw(spriteBatch);
                        dust.Update();
                    }
                    WindowDust.RemoveAll(n => n.time == 0);

                    if (Main.rand.Next(10) == 0) WindowDust.Add(new WindowLightDust(npc.Center + new Vector2(Main.rand.Next(-350, 350), -650), new Vector2(0, Main.rand.NextFloat(0.8f, 1.6f))));
                    */

                    for (int k = -2; k < 3; k++)
                    {
                        Texture2D tex2 = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/PitGlow");
                        float rot = (float)Main.time / 50 % 6.28f;
                        float sin = (float)Math.Sin(rot + k);
                        float sin2 = (float)Math.Sin(rot + k * 1.4f);
                        float cos = (float)Math.Cos(rot + k * 1.8f);
                        Vector2 beampos = dpos + FindOffset(pos, 0.4f + Math.Abs(k) * 0.05f) + new Vector2(k * 85 + (k % 2 == 0 ? sin : sin2) * 30, -300);
                        Rectangle beamrect = new Rectangle((int)beampos.X - (int)(sin * 30), (int)beampos.Y + (int)(sin2 * 70), 90 + (int)(sin * 30), 700 + (int)(sin2 * 140));

                        spriteBatch.Draw(tex2, beamrect, tex2.Frame(), new Color(255, 255, 200) * (1.4f + cos) * 0.8f, 0, tex2.Frame().Size() / 2, SpriteEffects.FlipVertically, 0);
                    }

                    spriteBatch.End();
                    spriteBatch.Begin(default, default, SamplerState.PointWrap, default, default, default, Main.GameViewMatrix.TransformationMatrix);

                    for (int k = -10; k < 10; k++)// small waterfalls
                    {
                        Texture2D watertex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/Waterfall");
                        int frame = (int)Main.time % 16 / 2;
                        spriteBatch.Draw(watertex, dpos + new Vector2(100, k * 64) + FindOffset(pos, 0.22f), new Rectangle(0, frame * 32, watertex.Width, 32), Color.White * 0.3f, 0, Vector2.Zero, 2, 0, 0);
                    }

                    for (int k = -5; k < 5; k++) //front row
                    {
                        Texture2D backtex4 = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/Window1");
                        Vector2 thispos = dpos + new Vector2(k * backtex4.Width, 380) + FindOffset(pos, 0.2f);
                        if (Vector2.Distance(thispos, dpos) < 800)
                            spriteBatch.Draw(backtex4, thispos, backtex4.Frame(), Color.White, 0, backtex4.Frame().Size() / 2, 1, 0, 0);
                    }

                    for (int k = -5; k < 5; k++) //top row
                    {
                        Texture2D backtex4 = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/Window1");
                        Vector2 thispos = dpos + new Vector2(k * backtex4.Width, -450) + FindOffset(pos, 0.25f);
                        if (Vector2.Distance(thispos, dpos) < 800)
                            spriteBatch.Draw(backtex4, thispos, backtex4.Frame(), Color.White, 0, backtex4.Frame().Size() / 2, 1, 0, 0);
                    }

                    for (int k = -7; k < 7; k++) //big waterfall
                    {
                        Texture2D watertex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/Waterfall");
                        int frame = (int)Main.time % 16 / 2;
                        spriteBatch.Draw(watertex, dpos + new Vector2(300, k * 96) + FindOffset(pos, 0.1f), new Rectangle(0, frame * 32, watertex.Width, 32), Color.White * 0.3f, 0, Vector2.Zero, 3, 0, 0);
                    }

                    foreach (NPC boss in Main.npc.Where(n => n.active && n.type == ModContent.NPCType<NPCs.Boss.OvergrowBoss.OvergrowBoss>() && n.ai[0] == (int)NPCs.Boss.OvergrowBoss.OvergrowBoss.OvergrowBossPhase.FirstGuard)) //boss behind
                    {
                        Texture2D bosstex = ModContent.GetTexture(boss.modNPC.Texture);
                        spriteBatch.Draw(bosstex, boss.Center - Main.screenPosition, bosstex.Frame(), Color.White, boss.rotation, bosstex.Frame().Size() / 2, boss.scale, 0, 0);
                    }

                    if (npc.ai[0] <= 360) //wall
                    {
                        Texture2D walltex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/Dor2");
                        Texture2D walltex2 = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/Dor1");
                        Rectangle sourceRect = new Rectangle(0, 0, walltex.Width, walltex.Height - (int)(npc.ai[0] / 360 * 764));
                        Rectangle sourceRect2 = new Rectangle(0, (int)(npc.ai[0] / 360 * 564), walltex2.Width, walltex2.Height - (int)(npc.ai[0] / 360 * 564));
                        spriteBatch.Draw(walltex, dpos + new Vector2(0, 176 + npc.ai[0] / 360 * 764), sourceRect, new Color(255, 255, 200), 0, walltex.Frame().Size() / 2, 1, 0, 0); //frame
                        spriteBatch.Draw(walltex2, dpos + new Vector2(0, -282 - npc.ai[0] / 360), sourceRect2, new Color(255, 255, 200), 0, walltex2.Frame().Size() / 2, 1, 0, 0); //frame
                    }
                }

                if (Main.npc[i].modNPC is NPCs.Boss.VitricBoss.VitricBackdropLeft)
                {
                    (Main.npc[i].modNPC as NPCs.Boss.VitricBoss.VitricBackdropLeft).SpecialDraw(Main.spriteBatch);
                }
            }
        }

        //IL edit for vitric biome lighting
        private delegate void ModLightingStateDelegate(float from, ref float to);
        private delegate void ModColorDelegate(int i, int j, ref float r, ref float g, ref float b);

        private void VitricLighting(ILContext il)
        {
            // Create our cursor at the start of the void PreRenderPhase() method.
            ILCursor c = new ILCursor(il);

            // We insert our emissions right before the ModifyLight call (line 1963, CIL 0x3428)
            // Get the TileLoader.ModifyLight method. Then, using it,
            // find where it's called and place the cursor right before that call instruction.

            MethodInfo ModifyLight = typeof(TileLoader).GetMethod("ModifyLight", BindingFlags.Public | BindingFlags.Static);
            c.GotoNext(i => i.MatchCall(ModifyLight));

            // Emit the values of I and J.
            /* To emit local variables, you have to know the indeces of where those variables are stored.
             * These are stated at the very top of the method, in a format like below:
             * .locals init ( 
             *      [0] = float32 FstName, 
             *      [1] = ScdName, 
             *      [2] = ThdName
             * )
            */

            c.Emit(OpCodes.Ldloc, 27); // [27] = n
            c.Emit(OpCodes.Ldloc, 29); // [29] = num17

            /* Emit the addresses of R, G, and B.
             * It's important to emit their *addresses*, because we're passing them—
             *   by reference, not by value. Under the hood, "ref" tokens—
             *   pass a pointer to the object (even for managed types),
             *   and that's what we need to do here.
            */
            c.Emit(OpCodes.Ldloca, 32); // [32] = num18
            c.Emit(OpCodes.Ldloca, 33); // [33] = num19
            c.Emit(OpCodes.Ldloca, 34); // [34] = num20

            // Consume the values of I,J and the addresses of R,G,B by calling EmitVitricDel.
            c.EmitDelegate<ModColorDelegate>(EmitVitricDel);

            #region DEPRECATED
            //// This following code is hacky just because I dislike writing "if"s in IL :)
            //EmitLightingState3("r2", 32); // [32] = num18 (R)
            //EmitLightingState3("g2", 33); // [33] = num19 (G)
            //EmitLightingState3("b2", 34); // [34] = num20 (B)

            //void EmitLightingState3(string fieldname, int colorIndex)
            //{
            //    // Find the field info of Lighting.LightingState's r2/g2/b2 fields.
            //    Type LightingState = typeof(Lighting).GetNestedType("LightingState", BindingFlags.NonPublic);
            //    FieldInfo field = LightingState.GetField(fieldname, BindingFlags.Public | BindingFlags.Instance);

            //    // Emit R, B, and G from before
            //    c.Emit(OpCodes.Ldloc, colorIndex);

            //    // Emit LightingState, then its r2/g2/b2 address.
            //    c.Emit(OpCodes.Ldloc, 30); // [30] = lightingState3
            //    c.Emit(OpCodes.Ldflda, field);
            //    c.EmitDelegate<ModLightingStateDelegate>(EmitLightingStateDel);
            //}
            #endregion

            // Not much more than that.
            // EmitVitricDel has the actual logic inside of it.
        }

        private static void EmitVitricDel(int i, int j, ref float r, ref float g, ref float b)
        {
            if (Main.tile[i, j] == null)
            {
                return;
            }
            // If the tile is in the vitric biome and doesn't block light, emit light.
            bool tileBlock = Main.tile[i, j].active() && Main.tileBlockLight[Main.tile[i, j].type] && !(Main.tile[i, j].slope() != 0 || Main.tile[i, j].halfBrick());
            bool wallBlock = Main.wallLight[Main.tile[i, j].wall];
            if (LegendWorld.VitricBiome.Contains(i, j) && Main.tile[i, j] != null && !tileBlock && wallBlock)
            {
                r = .4f;
                g = .57f;
                b = .65f;
            }

            //underworld lighting
            if (Vector2.Distance(Main.LocalPlayer.Center, LegendWorld.RiftLocation) <= 1500 && j >= Main.maxTilesY - 200 && Main.tile[i, j] != null && !tileBlock && wallBlock)
            {
                r = 0;
                g = 0;
                b = (1500 / Vector2.Distance(Main.LocalPlayer.Center, LegendWorld.RiftLocation) - 1) / 2;
                if (b >= 0.8f) b = 0.8f;
            }

            //waters, probably not the most amazing place to do this but it works and dosent melt people's PCs
            if (!tileBlock && Main.tile[i, j].liquid != 0 && Main.tile[i, j].liquidType() == 0)
            {
                //the crimson
                if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleBloody || Main.LocalPlayer.GetModPlayer<BiomeHandler>().FountainJungleBloody)
                {
                    if (Main.tile[i, j - 1].liquid != 0 || Main.tile[i, j - 1].active())
                    {
                        r = 0.25f;
                        g = 0.14f;
                        b = 0.0f;

                        if (Main.rand.Next(40) == 0)
                            Dust.NewDustPerfect(new Vector2(i * 16 + Main.rand.Next(16), j * 16 + Main.rand.Next(16)), ModContent.DustType<Dusts.Stamina>(), new Vector2(0, Main.rand.NextFloat(-0.8f, -0.6f)), 0, default, 0.6f);
                    }
                    else
                    {
                        r = 0.4f;
                        g = 0.32f;
                        b = 0.0f;
                        if (Main.rand.Next(5) == 0)
                            Dust.NewDustPerfect(new Vector2(i * 16 + Main.rand.Next(16), j * 16 + Main.rand.Next(16)), ModContent.DustType<Dusts.Gold2>(), new Vector2(0, Main.rand.NextFloat(-1.4f, -1.2f)), 0, default, 0.3f);
                    }
                }
                // the corruption
                if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleCorrupt || Main.LocalPlayer.GetModPlayer<BiomeHandler>().FountainJungleCorrupt)
                {
                    if (Main.tile[i, j - 1].liquid != 0 || Main.tile[i, j - 1].active())
                    {
                        if (Main.rand.Next(80) == 0)
                            Dust.NewDustPerfect(new Vector2(i * 16 + Main.rand.Next(16), j * 16 + Main.rand.Next(16)), 186, new Vector2(0, Main.rand.NextFloat(0.6f, 0.8f)), 0, default, 1f);
                    }
                    else
                    {
                        r = 0.1f;
                        g = 0.1f;
                        b = 0.3f;
                        if (Main.rand.Next(10) == 0)
                            Dust.NewDustPerfect(new Vector2(i * 16 + Main.rand.Next(16), j * 16 - Main.rand.Next(-20, 20)), 112, new Vector2(0, Main.rand.NextFloat(1.2f, 1.4f)), 120, new Color(100, 100, 200) * 0.6f, 0.6f);
                    }
                }
                //the hallow
                if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleHoly || Main.LocalPlayer.GetModPlayer<BiomeHandler>().FountainJungleHoly)
                {
                    if (Main.tile[i, j - 1].liquid != 0 || Main.tile[i, j - 1].active())
                    {
                        r = 0.1f;
                        g = 0.3f;
                        b = 0.3f;
                        if (Main.rand.Next(80) == 0)
                            Dust.NewDustPerfect(new Vector2(i * 16 + Main.rand.Next(16), j * 16 + Main.rand.Next(16)), ModContent.DustType<Dusts.Starlight>(), Vector2.One.RotatedByRandom(6.28f) * 10, 0, default, 0.5f);
                    }
                    else
                    {
                        r = 0.1f;
                        g = 0.5f;
                        b = 0.3f;
                        if (Main.rand.Next(100) == 0)
                            Dust.NewDustPerfect(new Vector2(i * 16 + Main.rand.Next(16), j * 16 + Main.rand.Next(-1, 20)), ModContent.DustType<Dusts.AirDash>(), new Vector2(0, Main.rand.NextFloat(-1, -0.1f)), 120, default, Main.rand.NextFloat(1.1f, 2.4f));
                    }
                }

            }

            //trees, 100% not the right place to do this. I should probably move this later. I wont. Kill me.
            if (Main.tile[i, j].type == Terraria.ID.TileID.Trees && Main.tile[i, j - 1].type != Terraria.ID.TileID.Trees && Main.tile[i, j + 1].type == Terraria.ID.TileID.Trees
                && Helper.ScanForTypeDown(i, j, ModContent.TileType<Tiles.JungleHoly.GrassJungleHoly>())) //at the top of trees in the holy jungle
            {
                Color color = new Color();
                switch (i % 3)
                {
                    case 0: color = new Color(150, 255, 230); break;
                    case 1: color = new Color(255, 180, 255); break;
                    case 2: color = new Color(200, 150, 255); break;
                }

                if (Main.rand.Next(5) == 0)
                {
                    Dust d = Dust.NewDustPerfect(new Vector2(i, j - 3) * 16 + Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(32), ModContent.DustType<Dusts.BioLumen>(), new Vector2(0.9f, 0.3f), 0, color, 1);
                    d.fadeIn = Main.rand.NextFloat(3.14f);
                }
                r = color.R / 555f; //lazy value tuning
                g = color.G / 555f;
                b = color.B / 555f;
            }
        }
        #endregion
    }
}

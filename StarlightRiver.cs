using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.UI;
using StarlightRiver.GUI;
using System.IO;
using StarlightRiver.Abilities;
using System.Linq;
using StarlightRiver.Items.CursedAccessories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent.UI.Elements;
using System.Reflection;
using UICharacter = Terraria.GameContent.UI.Elements.UICharacter;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using StarlightRiver.RiftCrafting;
using Terraria.Graphics.Shaders;
using StarlightRiver.Items.Prototypes;
using StarlightRiver.Configs;
using static Terraria.ModLoader.ModContent;
using StarlightRiver.BootlegDusts;
using StarlightRiver.Keys;
using Terraria.Graphics;
using ReLogic.Graphics;
using StarlightRiver.Dragons;
using Terraria.GameInput;

namespace StarlightRiver
{
    public partial class StarlightRiver : Mod
    {
        public Stamina stamina;
        public Collection collection;
        public Overlay overlay;
        public Infusion infusion;
        public Cooking cooking;
        public KeyInventory keyinventory;
        public AbilityText abilitytext;
        public GUI.Codex codex;

        public UserInterface customResources;
        public UserInterface customResources2;
        public UserInterface customResources3;
        public UserInterface customResources4;
        public UserInterface customResources5;
        public UserInterface customResources6;
        public UserInterface customResources7;
        public UserInterface customResources8;

        public static ModHotKey Dash;
        public static ModHotKey Superdash;
        public static ModHotKey Smash;
        public static ModHotKey Wisp;
        public static ModHotKey Purify;

        public List<RiftRecipe> RiftRecipes;

        public static float Rotation;

        public enum AbilityEnum : int {dash, wisp, purify, smash, superdash };

        public static StarlightRiver Instance { get; set; }
        public StarlightRiver()
        {
            Instance = this;
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneGlass)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/GlassPassive");
                    priority = MusicPriority.BiomeMedium;
                }

                if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneOvergrow)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/Overgrow");
                    priority = MusicPriority.BiomeHigh;
                }

                if(Main.npc.Any(n => n.active && n.type==ModContent.NPCType<Projectiles.Dummies.OvergrowBossWindowDummy>()))
                {
                    NPC npc = Main.npc.FirstOrDefault(n => n.active && n.type == ModContent.NPCType<Projectiles.Dummies.OvergrowBossWindowDummy>());
                    Rectangle arenaRect = new Rectangle((int)npc.Center.X - 52 * 16, (int)npc.Center.Y - 38 * 16, 104 * 16, 83 * 16);
                    if (Main.LocalPlayer.Hitbox.Intersects(arenaRect) && !Main.npc.Any(n => n.active && n.type == ModContent.NPCType<NPCs.Boss.OvergrowBoss.OvergrowBoss>() && n.ai[0] > 0) && !LegendWorld.OvergrowBossFree)
                    {
                        music = GetSoundSlot(SoundType.Music, "Sounds/Music/VoidPre");
                        priority = MusicPriority.BossLow;
                    }
                }

                if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneVoidPre)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/VoidPre");
                    priority = MusicPriority.BossLow;
                }

                if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleCorrupt)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/JungleCorrupt");
                    priority = MusicPriority.BiomeHigh;
                }

                if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleBloody)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/WhipAndNaenae");
                    priority = MusicPriority.BiomeHigh;
                }

                if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleHoly)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/WhipAndNaenae");
                    priority = MusicPriority.BiomeHigh;
                }

                if (Main.LocalPlayer.ZoneOverworldHeight && LegendWorld.starfall)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/Starlight");
                    priority = MusicPriority.BiomeHigh;
                }
            }

            return;           
        }
        public static void AutoloadRiftRecipes(List<RiftRecipe> target)
        {
            Mod mod = ModContent.GetInstance<StarlightRiver>();
            if (mod.Code != null)
            {
                foreach (Type type in mod.Code.GetTypes().Where(t => t.IsSubclassOf(typeof(RiftRecipe))))
                {
                    target.Add((RiftRecipe)Activator.CreateInstance(type));
                }
            }
        }
        public override void Load()
        {
            //Calls to add achievements.
            Achievements.Achievements.CallAchievements(this);

            //Shaders
            GameShaders.Misc["StarlightRiver:Distort"] = new MiscShaderData(new Ref<Effect>(GetEffect("Effects/Distort")), "Distort");

            RiftRecipes = new List<RiftRecipe>();
            AutoloadRiftRecipes(RiftRecipes);

            Dash = RegisterHotKey("Dash", "LeftShift");
            Wisp = RegisterHotKey("Wisp Form", "F");
            Purify = RegisterHotKey("Purify", "N");
            Smash = RegisterHotKey("Smash", "Z");
            Superdash = RegisterHotKey("Void Dash", "Q");

            if (!Main.dedServ)
            {
                customResources = new UserInterface();
                customResources2 = new UserInterface();
                customResources3 = new UserInterface();
                customResources4 = new UserInterface();
                customResources5 = new UserInterface();
                customResources6 = new UserInterface();
                customResources7 = new UserInterface();
                customResources8 = new UserInterface();

                stamina = new Stamina();
                collection = new Collection();
                overlay = new Overlay();
                infusion = new Infusion();
                cooking = new Cooking();
                keyinventory = new KeyInventory();
                abilitytext = new AbilityText();
                codex = new GUI.Codex();

                customResources.SetState(stamina);
                customResources2.SetState(collection);
                customResources3.SetState(overlay);
                customResources4.SetState(infusion);
                customResources5.SetState(cooking);
                customResources6.SetState(keyinventory);
                customResources7.SetState(abilitytext);
                customResources8.SetState(codex);
            }

            // Cursed Accessory Control Override
            On.Terraria.UI.ItemSlot.LeftClick_ItemArray_int_int += HandleSpecialItemInteractions;
            On.Terraria.UI.ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += DrawSpecial;
            On.Terraria.UI.ItemSlot.RightClick_ItemArray_int_int += NoSwapCurse;
            // Prototype Item Background
            On.Terraria.UI.ItemSlot.Draw_SpriteBatch_refItem_int_Vector2_Color += DrawProto;
            // Character Slot Addons
            On.Terraria.GameContent.UI.Elements.UICharacterListItem.DrawSelf += DrawSpecialCharacter;
            // Seal World Indicator
            On.Terraria.GameContent.UI.Elements.UIWorldListItem.GetIcon += VoidIcon;
            // Vitric background
            On.Terraria.Main.DrawBackgroundBlackFill += DrawVitricBackground;
            //Rift fading
            On.Terraria.Main.DrawUnderworldBackground += DrawBlackFade;
            //Mines
            On.Terraria.Main.drawWaters += DrawUnderwaterNPCs;
            //Keys
            On.Terraria.Main.DrawItems += DrawKeys;
            //Tile draws infront of the player
            On.Terraria.Main.DrawPlayer += PostDrawPlayer;
            //Foreground elements
            On.Terraria.Main.DrawInterface += DrawForeground;
            //Menu themes
            //On.Terraria.Main.DrawMenu += TestMenu;
            //Tilt
            On.Terraria.Graphics.SpriteViewMatrix.ShouldRebuild += UpdateMatrixFirst;
            //Moving Platforms
            On.Terraria.Player.Update_NPCCollision += PlatformCollision;
            //Dergon menu
            On.Terraria.Main.DoUpdate += UpdateDragonMenu;
            //Soulbound Items, ech these are a pain
            On.Terraria.Player.DropSelectedItem += DontDropSoulbound;
            On.Terraria.Player.dropItemCheck += SoulboundPriority;
            On.Terraria.Player.ItemFitsItemFrame += NoSoulboundFrame;
            On.Terraria.Player.ItemFitsWeaponRack += NoSoulboundRack;

            // Vitric lighting
            IL.Terraria.Lighting.PreRenderPhase += VitricLighting;
            //IL.Terraria.Main.DrawInterface_14_EntityHealthBars += ForceRedDraw;
            IL.Terraria.Main.DoDraw += DrawWindow;
            IL.Terraria.Main.DrawMenu += DragonMenuAttach;
            IL.Terraria.UI.ChestUI.DepositAll += PreventSoulboundStack;
        }



        //IL edits-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
        private void DrawWindow(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.GotoNext(n => n.MatchLdfld<Main>("DrawCacheNPCsMoonMoon"));
            c.Index--;

            c.EmitDelegate<DrawWindowDelegate>(EmitWindowDel);
        }
        private List<BootlegDust> WindowDust = new List<BootlegDust>();
        private void EmitWindowDel()
        {
            foreach (NPC npc in Main.npc.Where(n => n.active && n.type == ModContent.NPCType<Projectiles.Dummies.OvergrowBossWindowDummy>()))
            {
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
                foreach (BootlegDust dust in WindowDust)
                {
                    dust.Draw(spriteBatch);
                    dust.Update();
                }
                WindowDust.RemoveAll(n => n.time == 0);

                if (Main.rand.Next(10) == 0) WindowDust.Add(new WindowLightDust(npc.Center + new Vector2(Main.rand.Next(-350, 350), -650), new Vector2(0, Main.rand.NextFloat(0.8f, 1.6f))));

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
                    Texture2D walltex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/WindowFill");
                    Rectangle sourceRect = new Rectangle(0, 0, walltex.Width, walltex.Height - (int)(npc.ai[0] / 360 * 564));
                    Rectangle sourceRect2 = new Rectangle(0, 0, walltex.Width, walltex.Height - (int)(npc.ai[0] / 360 * 564));
                    spriteBatch.Draw(walltex, dpos + new Vector2(0, 282 + npc.ai[0] / 360 * 564), sourceRect, new Color(255, 255, 200), 0, walltex.Frame().Size() / 2, 1, 0, 0); //frame
                    spriteBatch.Draw(walltex, dpos + new Vector2(0, -282 - npc.ai[0] / 360), sourceRect2, new Color(255, 255, 200), 0, walltex.Frame().Size() / 2, 1, SpriteEffects.FlipVertically, 0); //frame
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
            bool tileBlock = Main.tile[i, j].active() && Main.tileBlockLight[Main.tile[i, j].type];
            bool wallBlock = Main.wallLight[Main.tile[i, j].wall];
            if (LegendWorld.vitricBiome.Contains(i, j) && Main.tile[i, j] != null && !tileBlock && wallBlock)
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
        }

        // On.hooks ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private bool NoSoulboundFrame(On.Terraria.Player.orig_ItemFitsItemFrame orig, Player self, Item i)
        {
            if (i.modItem is Items.SoulboundItem) return false;
            return orig(self, i);
        }
        private bool NoSoulboundRack(On.Terraria.Player.orig_ItemFitsWeaponRack orig, Player self, Item i)
        {
            if (i.modItem is Items.SoulboundItem) return false;
            return orig(self, i);
        }
        private void SoulboundPriority(On.Terraria.Player.orig_dropItemCheck orig, Player self)
        {
            if (Main.mouseItem.type > 0 && !Main.playerInventory && Main.mouseItem.modItem != null && Main.mouseItem.modItem is Items.SoulboundItem)
            {
                for (int k = 49; k > 0; k--)
                {
                    Item item = self.inventory[49];
                    if (!(self.inventory[49].modItem is Items.SoulboundItem) || k == 0)
                    {
                        int index = Item.NewItem(self.position, item.type, item.stack, false, item.prefix, false, false);
                        Main.item[index] = item.Clone();
                        Main.item[index].position = self.position;
                        item.TurnToAir();
                        break;
                    }
                }
            }
            orig(self);
        }
        private void DontDropSoulbound(On.Terraria.Player.orig_DropSelectedItem orig, Terraria.Player self)
        {
            if (self.inventory[self.selectedItem].modItem is Items.SoulboundItem || Main.mouseItem.modItem is Items.SoulboundItem) return;
            else orig(self);
        }
        private void UpdateDragonMenu(On.Terraria.Main.orig_DoUpdate orig, Terraria.Main self, GameTime gameTime)
        {
            if (dragonMenuUI != null)
            {
                dragonMenuUI.Update(gameTime);
            }
            orig(self, gameTime);
        }
        private void PlatformCollision(On.Terraria.Player.orig_Update_NPCCollision orig, Player self)
        {
            foreach (NPC npc in Main.npc.Where(n => n.active && n.modNPC != null && n.modNPC is NPCs.MovingPlatform))
            {
                if (new Rectangle((int)self.position.X, (int)self.position.Y + (self.height - 2), self.width, 4).Intersects
                (new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, 4)) && self.position.Y <= npc.position.Y)
                {
                    if (!self.justJumped && self.velocity.Y >= 0)
                    {
                        self.gfxOffY = npc.gfxOffY;
                        self.velocity.Y = 0;
                        self.fallStart = (int)(self.position.Y / 16f);
                        return;
                    }
                }
            }

            orig(self);
        }
        private bool UpdateMatrixFirst(On.Terraria.Graphics.SpriteViewMatrix.orig_ShouldRebuild orig, SpriteViewMatrix self)
        {
            return false;
        }
        private void PostDrawPlayer(On.Terraria.Main.orig_DrawPlayer orig, Main self, Player drawPlayer, Vector2 Position, float rotation, Vector2 rotationOrigin, float shadow)
        {
            orig(self, drawPlayer, Position, rotation, rotationOrigin, shadow);
            for (int i = (int)Main.screenPosition.X / 16; i < (int)Main.screenPosition.X / 16 + Main.screenWidth / 16; i++)
                for (int j = (int)Main.screenPosition.Y / 16; j < (int)Main.screenPosition.Y / 16 + Main.screenWidth / 16; j++)
                {
                    if (i >  0 && j > 0 && i < Main.maxTilesX && j < Main.maxTilesY && Main.tile[i, j] != null && Main.tile[i, j].type == ModContent.TileType<Tiles.Overgrow.GrassOvergrow>())
                    {
                        (ModContent.GetModTile(ModContent.TileType<Tiles.Overgrow.GrassOvergrow>()) as Tiles.Overgrow.GrassOvergrow).CustomDraw(i, j, Main.spriteBatch);
                    }
                }
        }
        private void DrawKeys(On.Terraria.Main.orig_DrawItems orig, Main self)
        {
            foreach (Key key in LegendWorld.Keys)
            {
                key.Draw(Main.spriteBatch);
            }
            orig(self);
        }
        public Vector2 FindOffset(Vector2 basepos, float factor)
        {
            Vector2 origin = Main.screenPosition + new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            float x = (origin.X - basepos.X) * factor;
            float y = (origin.Y - basepos.Y) * factor * 0.4f;
            return new Vector2(x, y);
        }

        List<BootlegDust> foregroundDusts = new List<BootlegDust>();
        private void DrawForeground(On.Terraria.Main.orig_DrawInterface orig, Main self, GameTime gameTime)
        {
            Main.spriteBatch.Begin();
            //This is where foreground shiznit is drawn
            List<BootlegDust> removals = new List<BootlegDust>();

            foreach (BootlegDust dus in foregroundDusts)
            {
                dus.Draw(Main.spriteBatch);
                dus.Update();
                if (dus.time <= 0) removals.Add(dus);
            }
            foreach (BootlegDust dus in removals) foregroundDusts.Remove(dus);

            //Overgrow magic wells
            if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneOvergrow)
            {
                int direction = Main.dungeonX > Main.spawnTileX ? -1 : 1;
                if (Main.rand.Next(9) == 0)
                    for (int k = 0; k < 10; k++)
                    {
                        foregroundDusts.Add(new OvergrowForegroundDust(direction * 800 * k + Main.rand.Next(80), 1.4f, new Vector2(0, -Main.rand.NextFloat(2.5f, 3)), Color.White * 0.005f, Main.rand.NextFloat(1, 2)));
                        if(Main.rand.Next(2) == 0)
                        foregroundDusts.Add(new OvergrowForegroundDust(direction * 900 * k + Main.rand.Next(30), 0.6f, new Vector2(0, -Main.rand.NextFloat(1.3f, 1.5f)), Color.White * 0.05f, Main.rand.NextFloat(0.4f, 0.6f)));
                    }
            }
            else foreach (BootlegDust dus in foregroundDusts) (dus as OvergrowForegroundDust).fadein = 101;
                    Main.spriteBatch.End();
            orig(self, gameTime);
        }

        internal static readonly List<BootlegDust> MenuDust = new List<BootlegDust>();
        private void TestMenu(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
        {
            orig(self, gameTime);
            if (ModLoader.GetMod("StarlightRiver") == null) return;

            Main.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive);
            Main.spriteBatch.DrawString(Main.fontMouseText, "Menu Mode: " + Main.menuMode, Vector2.One * 200, Color.White);

            switch (GetInstance<TitleScreenConfig>().Style)
            {
                case TitleScreenStyle.None:
                break;

                case TitleScreenStyle.Starlight:
                    if (Main.rand.Next(2) == 0)
                    {
                        MenuDust.Add(new EvilDust(ModContent.GetTexture("StarlightRiver/GUI/Light"), new Vector2(Main.rand.Next(Main.screenWidth), Main.screenHeight + 40), new Vector2(0, -Main.rand.NextFloat(0.8f))));
                    }
                    Main.spriteBatch.Draw(ModContent.GetTexture("Terraria/Extra_60"), new Rectangle(0, Main.screenHeight - 200, Main.screenWidth, 500), new Rectangle(50, 0, 32, 152), new Color(100, 160, 190) * 0.75f);
                    break;

                case TitleScreenStyle.Overgrow:
                    if(Main.rand.Next(1) == 0)
                    {
                        MenuDust.Add(new HolyDust(ModContent.GetTexture("StarlightRiver/GUI/Holy"), new Vector2(Main.rand.Next(Main.screenWidth), Main.screenHeight - Main.rand.Next(Main.screenHeight / 4)), Vector2.Zero));
                    }
                    Main.spriteBatch.Draw(ModContent.GetTexture("Terraria/Extra_60"), new Rectangle(0, Main.screenHeight - 200, Main.screenWidth, 500), new Rectangle(50, 0, 32, 152), new Color(180, 170, 100) * 0.75f);
                    break;

                case TitleScreenStyle.Rift:
                    if (Main.rand.Next(1) == 0)
                    {
                        MenuDust.Add(new VoidDust(ModContent.GetTexture("StarlightRiver/GUI/Fire"), new Vector2(Main.rand.Next(Main.screenWidth), Main.screenHeight + 10), new Vector2(0, -Main.rand.NextFloat(0.6f, 1f))));
                    }
                    Main.spriteBatch.Draw(ModContent.GetTexture("Terraria/Extra_60"), new Rectangle(0, Main.screenHeight - 200, Main.screenWidth, 500), new Rectangle(50, 0, 32, 152), new Color(180, 50, 240) * 0.9f);
                    break;

                case TitleScreenStyle.Mario:

                    MenuDust.Add(new EvilDust(ModContent.GetTexture("StarlightRiver/MarioCumming"), new Vector2(Main.rand.Next(Main.screenWidth), Main.screenHeight + 40), new Vector2(0, -10)));
                   
                    break;
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
            foreach (BootlegDust dus in MenuDust) dus.Draw(Main.spriteBatch);
            foreach (BootlegDust dus in MenuDust) dus.Update();

            List<BootlegDust> Removals = new List<BootlegDust>();
            foreach (BootlegDust dus in MenuDust.Where(dus => dus.time <= 0)) Removals.Add(dus);
            foreach (BootlegDust dus in Removals) MenuDust.Remove(dus);
            Main.spriteBatch.End();
        }
        private void DrawProto(On.Terraria.UI.ItemSlot.orig_Draw_SpriteBatch_refItem_int_Vector2_Color orig, SpriteBatch spriteBatch, ref Item inv, int context, Vector2 position, Color lightColor)
        {
             orig(spriteBatch, ref inv, context, position, lightColor);
        }
        private Texture2D VoidIcon(On.Terraria.GameContent.UI.Elements.UIWorldListItem.orig_GetIcon orig, UIWorldListItem self)
        {
            /*FieldInfo datainfo = self.GetType().GetField("_data", BindingFlags.NonPublic | BindingFlags.Instance);
            WorldFileData data = (WorldFileData)datainfo.GetValue(self);
            string path = data.Path.Replace(".wld", ".twld");

            byte[] buf = FileUtilities.ReadAllBytes(path, data.IsCloudSave);
            TagCompound tag = TagIO.FromStream(new MemoryStream(buf), true);
            TagCompound tag2 = tag.GetList<TagCompound>("modData").FirstOrDefault(k => k.ContainsKey());
            ModContent.GetInstance<LegendWorld>().Load(tag.GetCompound("data"));

            bool riftopen = false;
            if (tag2 != null && tag2.HasTag(nameof(LegendWorld.SealOpen))) riftopen = tag2.GetBool(nameof(LegendWorld.SealOpen));

            if (riftopen)
            {
                return ModContent.GetTexture("StarlightRiver/GUI/Fire");
            }*/

            return orig(self);
        }
        private void DrawBlackFade(On.Terraria.Main.orig_DrawUnderworldBackground orig, Main self, bool flat)
        {
            orig(self, flat);
            if (Main.gameMenu) return;
            Texture2D tex = ModContent.GetTexture("StarlightRiver/GUI/Fire");

            float distance = Vector2.Distance(Main.LocalPlayer.Center, LegendWorld.RiftLocation);
            float val = ((1500 / distance - 1) / 3);
            if (val > 0.8f) val = 0.8f;
            Color color = Color.Black * (distance <= 1500 ? val : 0);
                   
            Main.spriteBatch.Draw(tex, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), tex.Frame(), color);
        }
        private void DrawUnderwaterNPCs(On.Terraria.Main.orig_drawWaters orig, Main self, bool bg, int styleOverride, bool allowUpdate)
        {
            orig(self, bg, styleOverride, allowUpdate);
            foreach(NPC npc in Main.npc.Where(npc => npc.type == ModContent.NPCType<NPCs.Hostile.BoneMine>() && npc.active))
            {
                SpriteBatch spriteBatch = Main.spriteBatch;
                Color drawColor = Lighting.GetColor((int)npc.position.X / 16, (int)npc.position.Y / 16) * 0.3f;

                spriteBatch.Draw(ModContent.GetTexture(npc.modNPC.Texture), npc.position - Main.screenPosition + Vector2.One * 16 * 12 + new Vector2((float)Math.Sin(npc.ai[0]) * 4f, 0), drawColor);
                for (int k = 0; k >= 0; k++)
                {
                    if (Main.tile[(int)npc.position.X / 16, (int)npc.position.Y / 16 + k + 2].active()) break;
                    spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Projectiles/WeaponProjectiles/ShakerChain"),
                        npc.Center - Main.screenPosition + Vector2.One * 16 * 12 + new Vector2(-4 + (float)Math.Sin(npc.ai[0] + k) * 4, 18 + k * 16), drawColor);
                }
            }
        }

        internal static readonly List<BootlegDust> VitricBackgroundDust = new List<BootlegDust>();
        internal static readonly List<BootlegDust> VitricForegroundDust = new List<BootlegDust>();

        private void DrawVitricBackground(On.Terraria.Main.orig_DrawBackgroundBlackFill orig, Main self)
        {
            orig(self);
            Player player = null;
            if (Main.playerLoaded) { player = Main.LocalPlayer; }

            VitricBackgroundDust.ForEach(BootlegDust => BootlegDust.Update());
            VitricBackgroundDust.RemoveAll(BootlegDust => BootlegDust.time <= 0);

            VitricForegroundDust.ForEach(BootlegDust => BootlegDust.Update());
            VitricForegroundDust.RemoveAll(BootlegDust => BootlegDust.time <= 0);

            if (player != null && LegendWorld.vitricBiome.Contains((player.Center / 16).ToPoint()))
            {
                Vector2 basepoint = (LegendWorld.vitricBiome != null) ? LegendWorld.vitricBiome.TopLeft() * 16 + new Vector2(-2000, 0) : Vector2.Zero;
                for (int k = 5; k >= 0; k--)
                {
                    DrawLayer(basepoint, ModContent.GetTexture("StarlightRiver/Backgrounds/Glass" + k), k + 1);
                    if (k == 5)
                    {
                        VitricBackgroundDust.ForEach(BootlegDust => BootlegDust.Draw(Main.spriteBatch));
                    }
                    if (k == 2)
                    {
                        VitricForegroundDust.ForEach(BootlegDust => BootlegDust.Draw(Main.spriteBatch));
                    }
                }

                for (int k = (int)(player.position.X - basepoint.X) - (int)(Main.screenWidth * 1.5f); k <= (int)(player.position.X - basepoint.X) + (int)(Main.screenWidth * 1.5f); k += 30)
                {
                    if (Main.rand.Next(500) == 0)
                    {
                        BootlegDust dus = new VitricDust(ModContent.GetTexture("StarlightRiver/GUI/Light"), basepoint + new Vector2(-2000, 1000), k, 1.5f, 0.3f, 0.1f);
                        VitricBackgroundDust.Add(dus);
                    }

                    if (Main.rand.Next(400) == 0)
                    {
                        BootlegDust dus2 = new VitricDust(ModContent.GetTexture("StarlightRiver/GUI/Light"), basepoint + new Vector2(-2000, 1000), k, 2.25f, 0.6f, 0.4f);
                        VitricForegroundDust.Add(dus2);
                    }
                }

                for (int i = -2 + (int)(player.position.X - Main.screenWidth / 2) / 16; i <= 2 + (int)(player.position.X + Main.screenWidth / 2) / 16; i++)
                {
                    for (int j = -2 + (int)(player.position.Y - Main.screenHeight) / 16; j <= 2 + (int)(player.position.Y + Main.screenHeight) / 16; j++)
                    {
                        if (Lighting.Brightness(i,j) == 0 || ((Main.tile[i, j].active() && Main.tile[i, j].collisionType == 1) || Main.tile[i, j].wall != 0))
                        {
                            Color color = Color.Black * (1 - Lighting.Brightness(i, j) * 2);
                            Main.spriteBatch.Draw(Main.blackTileTexture, new Vector2(i * 16, j * 16) - Main.screenPosition, color);
                        }
                    }
                }
            }           
        }
        public void DrawLayer(Vector2 basepoint, Texture2D texture, int parallax)
        {
            for (int k = 0; k <= 5; k++)
            {
                Main.spriteBatch.Draw(texture,
                    new Vector2(basepoint.X + (k * 739 * 4) + GetParallaxOffset(basepoint.X, parallax * 0.1f) - (int)Main.screenPosition.X, basepoint.Y - (int)Main.screenPosition.Y),
                    new Rectangle(0, 0, 2956, 1528), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            }
        }
        public int GetParallaxOffset(float startpoint, float factor)
        {
            return (int)((Main.LocalPlayer.position.X - startpoint) * factor);
        }
        private void DrawSpecialCharacter(On.Terraria.GameContent.UI.Elements.UICharacterListItem.orig_DrawSelf orig, UICharacterListItem self, SpriteBatch spriteBatch)
        {
            orig(self, spriteBatch);
            Vector2 origin = new Vector2(self.GetDimensions().X, self.GetDimensions().Y);
            Rectangle box = new Rectangle((int)(origin + new Vector2(86, 66)).X, (int)(origin + new Vector2(86, 66)).Y, 80, 25);
            int playerStamina = 0;

            //horray double reflection, fuck you vanilla
            Type typ = self.GetType();
            FieldInfo playerInfo = typ.GetField("_playerPanel", BindingFlags.NonPublic | BindingFlags.Instance);
            UICharacter character = (UICharacter)playerInfo.GetValue(self);

            Type typ2 = character.GetType();
            FieldInfo playerInfo2 = typ2.GetField("_player", BindingFlags.NonPublic | BindingFlags.Instance);
            Player player = (Player)playerInfo2.GetValue(character);
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

            if (mp == null) { return; }

            playerStamina = mp.StatStaminaMax;

            Texture2D wind = !mp.dash.Locked ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wind1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wind0");
            Texture2D wisp = !mp.wisp.Locked ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wisp1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wisp0");
            Texture2D pure = !mp.pure.Locked ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Purity1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Purity0");
            Texture2D smash = !mp.smash.Locked ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Smash1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Smash0");
            Texture2D shadow = !mp.sdash.Locked ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Cloak1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Cloak0");            

            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/box"), box, Color.White); //Stamina box

            mp.SetList();//update ability list
            if (mp.Abilities.Any(a => !a.Locked))//Draw stamina if any unlocked
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/Stamina"), origin + new Vector2(91, 68), Color.White);
                Utils.DrawBorderString(spriteBatch, playerStamina + " SP", origin + new Vector2(118, 68), Color.White);
            }
            else//Myserious if locked
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/Stamina3"), origin + new Vector2(91, 68), Color.White);
                Utils.DrawBorderString(spriteBatch,"???", origin + new Vector2(118, 68), Color.White);
            }

            //Draw ability Icons
            spriteBatch.Draw(wind, origin + new Vector2(390, 62), Color.White);
            spriteBatch.Draw(wisp, origin + new Vector2(426, 62), Color.White);
            spriteBatch.Draw(pure, origin + new Vector2(462, 62), Color.White);
            spriteBatch.Draw(smash, origin + new Vector2(498, 62), Color.White);
            spriteBatch.Draw(shadow, origin + new Vector2(534, 62), Color.White);

            if (player.statLifeMax > 400) //why vanilla dosent do this I dont know
            {
                spriteBatch.Draw(Main.heart2Texture, origin + new Vector2(80, 37), Color.White);
            }
        }
        private void HandleSpecialItemInteractions(On.Terraria.UI.ItemSlot.orig_LeftClick_ItemArray_int_int orig, Terraria.Item[] inv, int context, int slot)
        {
            if((inv[slot].modItem is CursedAccessory || inv[slot].modItem is Blocker) && context == 10)
            {
                return;
            }
            if(Main.mouseItem.modItem is Items.SoulboundItem && (context != 0 || inv != Main.LocalPlayer.inventory))
            {
                return;
            }
            if(inv[slot].modItem is Items.SoulboundItem && Main.keyState.PressingShift())
            {
                return;
            }
            Main.NewText(context);
            orig(inv, context, slot);
        }

        private void NoSwapCurse(On.Terraria.UI.ItemSlot.orig_RightClick_ItemArray_int_int orig, Terraria.Item[] inv, int context, int slot)
        {           
            Player player = Main.player[Main.myPlayer];
            for (int i = 0; i < player.armor.Length; i++)
            {
                if ((player.armor[i].modItem is CursedAccessory || player.armor[i].modItem is Blocker) && ItemSlot.ShiftInUse && inv[slot].accessory)
                {
                    return;
                }              
            }

            if (inv == player.armor)
            {               
                Item swaptarget = player.armor[slot - 10];
                Main.NewText(swaptarget + "  /  " + slot);
                if (context == 11 && (swaptarget.modItem is CursedAccessory || swaptarget.modItem is Blocker || swaptarget.modItem is InfectedAccessory)) return;
            }

            orig(inv, context, slot);
           
        }

        private void DrawSpecial(On.Terraria.UI.ItemSlot.orig_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color orig, SpriteBatch sb, Terraria.Item[] inv, int context, int slot, Vector2 position, Color color)
        {
            if ((inv[slot].modItem is CursedAccessory || inv[slot].modItem is BlessedAccessory) && context == 10)
            {
                Texture2D back = inv[slot].modItem is CursedAccessory ? ModContent.GetTexture("StarlightRiver/GUI/CursedBack") : ModContent.GetTexture("StarlightRiver/GUI/BlessedBack");
                Color backcolor = (!Main.expertMode && slot == 8) ? Color.White * 0.25f : Color.White * 0.75f;
                sb.Draw(back, position, null, backcolor, 0f, default, Main.inventoryScale, SpriteEffects.None, 0f);
                RedrawItem(sb, inv, back, position, slot, color);
            }
            else if ((inv[slot].modItem is InfectedAccessory || inv[slot].modItem is Blocker) && context == 10)
            {
                Texture2D back = ModContent.GetTexture("StarlightRiver/GUI/InfectedBack");
                Color backcolor = (!Main.expertMode && slot == 8) ? Color.White * 0.25f : Color.White * 0.75f;
                sb.Draw(back, position, null, backcolor, 0f, default, Main.inventoryScale, SpriteEffects.None, 0f);
                RedrawItem(sb, inv, back, position, slot, color);
            }
            else if (inv[slot].modItem is PrototypeWeapon && inv[slot] != Main.mouseItem)
            {
                Texture2D back = ModContent.GetTexture("StarlightRiver/GUI/ProtoBack");
                Color backcolor = Main.LocalPlayer.HeldItem != inv[slot] ? Color.White * 0.75f : Color.Yellow;
                sb.Draw(back, position, null, backcolor, 0f, default, Main.inventoryScale, SpriteEffects.None, 0f);
                RedrawItem(sb, inv, back, position, slot, color);
            }
            else
            {
                orig(sb, inv, context, slot, position, color);
            }
        }
        private void RedrawItem(SpriteBatch sb, Item[] inv, Texture2D back, Vector2 position, int slot, Color color)
        {
            Item item = inv[slot];
            Vector2 vector = back.Size() * Main.inventoryScale;
            Texture2D texture2D3 = ModContent.GetTexture(item.modItem.Texture);
            Rectangle rectangle2 = (texture2D3.Frame(1, 1, 0, 0));
            Color currentColor = color;
            float scale3 = 1f;
            ItemSlot.GetItemLight(ref currentColor, ref scale3, item, false);
            float num8 = 1f;
            if (rectangle2.Width > 32 || rectangle2.Height > 32)
            {
                num8 = ((rectangle2.Width <= rectangle2.Height) ? (32f / (float)rectangle2.Height) : (32f / (float)rectangle2.Width));
            }
            num8 *= Main.inventoryScale;
            Vector2 position2 = position + vector / 2f - rectangle2.Size() * num8 / 2f;
            Vector2 origin = rectangle2.Size() * (scale3 / 2f - 0.5f);
            if (ItemLoader.PreDrawInInventory(item, sb, position2, rectangle2, item.GetAlpha(currentColor), item.GetColor(color), origin, num8 * scale3))
            {
                sb.Draw(texture2D3, position2, rectangle2, Color.White, 0f, origin, num8 * scale3, SpriteEffects.None, 0f);
            }
            ItemLoader.PostDrawInInventory(item, sb, position2, rectangle2, item.GetAlpha(currentColor), item.GetColor(color), origin, num8 * scale3);
        }

        public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
        {
            var type = typeof(SpriteViewMatrix);
            var field = type.GetField("_transformationMatrix", BindingFlags.NonPublic | BindingFlags.Instance);

            Matrix rotation = Matrix.CreateRotationZ(Rotation);
            Matrix translation = Matrix.CreateTranslation(new Vector3(Main.screenWidth / 2, Main.screenHeight / 2, 0));
            Matrix translation2 = Matrix.CreateTranslation(new Vector3(Main.screenWidth / -2, Main.screenHeight / -2, 0));

            field.SetValue(Transform, (translation2 * rotation) * translation);
            base.ModifyTransformMatrix(ref Transform);
            Helper.UpdateTilt();
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {

            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (MouseTextIndex != -1)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("StarlightRiver: Stamina",
                delegate
                {
                    if (Stamina.visible)
                    {
                        customResources.Update(Main._drawInterfaceGameTime);
                        stamina.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));

                layers.Insert(MouseTextIndex + 1, new LegacyGameInterfaceLayer("StarlightRiver: Collection",
                delegate
                {
                    if (Collection.visible)
                    {
                        customResources2.Update(Main._drawInterfaceGameTime);
                        collection.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));

                layers.Insert(0, new LegacyGameInterfaceLayer("StarlightRiver: Overlay",
                delegate
                {
                    if (Overlay.visible)
                    {
                        customResources3.Update(Main._drawInterfaceGameTime);
                        overlay.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));

                layers.Insert(MouseTextIndex + 2, new LegacyGameInterfaceLayer("StarlightRiver: Infusions",
                delegate
                {
                    if (Infusion.visible)
                    {
                        customResources4.Update(Main._drawInterfaceGameTime);
                        infusion.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));

                layers.Insert(MouseTextIndex + 3, new LegacyGameInterfaceLayer("StarlightRiver: Cooking",
                delegate
                {
                    if (Cooking.visible)
                    {
                        customResources5.Update(Main._drawInterfaceGameTime);
                        cooking.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));

                layers.Insert(MouseTextIndex + 4, new LegacyGameInterfaceLayer("StarlightRiver: Keys",
                delegate
                {
                    if (KeyInventory.visible)
                    {
                        customResources6.Update(Main._drawInterfaceGameTime);
                        keyinventory.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));

                layers.Insert(MouseTextIndex + 5, new LegacyGameInterfaceLayer("StarlightRiver: Ability Text",
                delegate
                {
                    if (AbilityText.Visible)
                    {
                        customResources7.Update(Main._drawInterfaceGameTime);
                        abilitytext.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));

                layers.Insert(MouseTextIndex + 6, new LegacyGameInterfaceLayer("StarlightRiver: Codex",
                delegate
                {
                    if (GUI.Codex.Visible)
                    {
                        customResources8.Update(Main._drawInterfaceGameTime);
                        codex.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                RiftRecipes = null;

                customResources = null;
                customResources2 = null;
                customResources3 = null;
                customResources4 = null;
                customResources5 = null;
                customResources7 = null;
                customResources8 = null;

                stamina = null;
                collection = null;
                overlay = null;
                infusion = null;
                cooking = null;
                abilitytext = null;
                codex = null;

                VitricBackgroundDust.Clear();
                VitricForegroundDust.Clear();
                CursedAccessory.Bootlegdust.Clear();
                BlessedAccessory.Bootlegdust.Clear();
                BlessedAccessory.Bootlegdust2.Clear();
                Collection.Bootlegdust.Clear();
                Overlay.Bootlegdust.Clear();

                Instance = null;
                Dash = null;
                Superdash = null;
                Wisp = null;
                Smash = null;
                Purify = null;
            }
            IL.Terraria.Lighting.PreRenderPhase -= VitricLighting;
        }
    }
}	

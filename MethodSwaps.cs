using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using StarlightRiver.Abilities;
using StarlightRiver.BootlegDusts;
using StarlightRiver.Codex;
using StarlightRiver.Configs;
using StarlightRiver.GUI;
using StarlightRiver.Items.CursedAccessories;
using StarlightRiver.Items.Prototypes;
using StarlightRiver.Keys;
using StarlightRiver.Tiles.Overgrow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using UICharacter = Terraria.GameContent.UI.Elements.UICharacter;

namespace StarlightRiver
{

    public partial class StarlightRiver : Mod
    {
        private void HookOn()
        {
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
        }
        #region hooks
        private bool NoSoulboundFrame(On.Terraria.Player.orig_ItemFitsItemFrame orig, Player self, Item i)
        {
            return i.modItem is Items.SoulboundItem ? false : orig(self, i);
        }
        private bool NoSoulboundRack(On.Terraria.Player.orig_ItemFitsWeaponRack orig, Player self, Item i)
        {
            return i.modItem is Items.SoulboundItem ? false : orig(self, i);
        }
        private void SoulboundPriority(On.Terraria.Player.orig_dropItemCheck orig, Player self)
        {
            if (Main.mouseItem.type > ItemID.None && !Main.playerInventory && Main.mouseItem.modItem != null && Main.mouseItem.modItem is Items.SoulboundItem)
            {
                for (int k = 49; k > 0; k--)
                {
                    Item item = self.inventory[k];
                    if (!(self.inventory[k].modItem is Items.SoulboundItem) || k == 0)
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
            dragonMenuUI?.Update(gameTime);
            orig(self, gameTime);
        }
        private void PlatformCollision(On.Terraria.Player.orig_Update_NPCCollision orig, Player self)
        {
            if (self.controlDown) self.GetModPlayer<StarlightPlayer>().platformTimer = 5;
            if (self.controlDown || self.GetModPlayer<StarlightPlayer>().platformTimer > 0 || self.GoingDownWithGrapple) { orig(self); return; }
            foreach (NPC npc in Main.npc.Where(n => n.active && n.modNPC != null && n.modNPC is NPCs.MovingPlatform))
            {
                if (new Rectangle((int)self.position.X, (int)self.position.Y + (self.height), self.width, 1).Intersects
                (new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, 8 + (self.velocity.Y > 0 ? (int)self.velocity.Y : 0))) && self.position.Y <= npc.position.Y)
                {
                    if (!self.justJumped && self.velocity.Y >= 0)
                    {
                        self.gfxOffY = npc.gfxOffY;
                        self.velocity.Y = 0;
                        self.fallStart = (int)(self.position.Y / 16f);
                        self.position.Y = npc.position.Y - self.height + 4;
                        orig(self);
                    }
                }
            }

            orig(self);
        }
        private bool UpdateMatrixFirst(On.Terraria.Graphics.SpriteViewMatrix.orig_ShouldRebuild orig, SpriteViewMatrix self)
        {
            return Rotation != 0 ? false : orig(self);
        }
        private void PostDrawPlayer(On.Terraria.Main.orig_DrawPlayer orig, Main self, Player drawPlayer, Vector2 Position, float rotation, Vector2 rotationOrigin, float shadow)
        {
            orig(self, drawPlayer, Position, rotation, rotationOrigin, shadow);
            for (int i = (int)Main.screenPosition.X / 16; i < (int)Main.screenPosition.X / 16 + Main.screenWidth / 16; i++)
                for (int j = (int)Main.screenPosition.Y / 16; j < (int)Main.screenPosition.Y / 16 + Main.screenWidth / 16; j++)
                {
                    if (i > 0 && j > 0 && i < Main.maxTilesX && j < Main.maxTilesY && Main.tile[i, j] != null && Main.tile[i, j].type == ModContent.TileType<Tiles.Overgrow.GrassOvergrow>())
                    {
                        GrassOvergrow.CustomDraw(i, j, Main.spriteBatch);
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
        public static Vector2 FindOffset(Vector2 basepos, float factor)
        {
            Vector2 origin = Main.screenPosition + new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            float x = (origin.X - basepos.X) * factor;
            float y = (origin.Y - basepos.Y) * factor * 0.4f;
            return new Vector2(x, y);
        }

        private readonly List<BootlegDust> foregroundDusts = new List<BootlegDust>();
        private void DrawForeground(On.Terraria.Main.orig_DrawInterface orig, Main self, GameTime gameTime)
        {
            Main.spriteBatch.Begin();
            //This is where foreground shiznit is drawn
            List<BootlegDust> removals = new List<BootlegDust>();

            foreach (BootlegDust dus in foregroundDusts)
            {
                dus.SafeDraw(Main.spriteBatch);
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
                        if (Main.rand.Next(2) == 0)
                            foregroundDusts.Add(new OvergrowForegroundDust(direction * 900 * k + Main.rand.Next(30), 0.6f, new Vector2(0, -Main.rand.NextFloat(1.3f, 1.5f)), Color.White * 0.05f, Main.rand.NextFloat(0.4f, 0.6f)));
                    }
            }
            else foreach (BootlegDust dus in foregroundDusts) (dus as OvergrowForegroundDust).fadein = 101;
            Main.spriteBatch.End();
            orig(self, gameTime);

            if (!Main.playerInventory)
            {
                Main.spriteBatch.Begin();
                Main.spriteBatch.DrawString(Main.fontItemStack, PatchString, new Vector2(20, 120), Color.White);
                Main.spriteBatch.DrawString(Main.fontItemStack, MessageString, new Vector2(20, 140), Color.White);
                Main.spriteBatch.End();
            }
        }

        internal static readonly List<BootlegDust> MenuDust = new List<BootlegDust>();
        private void TestMenu(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
        {
            orig(self, gameTime);

            Main.spriteBatch.Begin();
            Main.spriteBatch.DrawString(Main.fontItemStack, PatchString, new Vector2(20, 20), Color.White);
            Main.spriteBatch.DrawString(Main.fontItemStack, MessageString, new Vector2(20, 40), Color.White);
            Main.spriteBatch.End();

            try
            {
                bool canDraw = Main.menuMode == 0;

                if (canDraw)
                {
                    Main.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive);

                    switch (GetInstance<Config>().Style)
                    {
                        case TitleScreenStyle.None:
                            break;

                        case TitleScreenStyle.Starlight:
                            Main.time = 0;
                            if (Main.rand.Next(3) >= 1 && canDraw)
                            {
                                MenuDust.Add(new EvilDust(ModContent.GetTexture("StarlightRiver/GUI/Light"), new Vector2(Main.rand.Next(Main.screenWidth), Main.screenHeight + 40), new Vector2(0, -Main.rand.NextFloat(1.4f))));
                            }
                            if (canDraw) Main.spriteBatch.Draw(ModContent.GetTexture("Terraria/Extra_60"), new Rectangle(0, Main.screenHeight - 200, Main.screenWidth, 500), new Rectangle(50, 0, 32, 152), new Color(100, 160, 190) * 0.75f);
                            break;

                        case TitleScreenStyle.Vitric:
                            if (Main.rand.Next(10) == 0 && canDraw)
                                MenuDust.Add(new VitricDust(ModContent.GetTexture("StarlightRiver/Dusts/Mist"), new Vector2(Main.rand.Next(Main.screenWidth), Main.screenHeight + 40), 0, 0.35f, 0.4f, 0));
                            if (canDraw) Main.spriteBatch.Draw(ModContent.GetTexture("Terraria/Extra_60"), new Rectangle(0, Main.screenHeight - 200, Main.screenWidth, 500), new Rectangle(50, 0, 32, 152), new Color(100, 180, 180) * 0.75f);
                            break;

                        case TitleScreenStyle.Overgrow:
                            if (Main.rand.Next(3) >= 1 && canDraw)
                            {
                                MenuDust.Add(new HolyDust(ModContent.GetTexture("StarlightRiver/GUI/Holy"), new Vector2(Main.rand.Next(Main.screenWidth), Main.screenHeight - Main.rand.Next(Main.screenHeight / 3)), Vector2.Zero));
                            }
                            if (canDraw) Main.spriteBatch.Draw(ModContent.GetTexture("Terraria/Extra_60"), new Rectangle(0, Main.screenHeight - 200, Main.screenWidth, 500), new Rectangle(50, 0, 32, 152), new Color(180, 170, 100) * 0.75f);
                            break;

                        case TitleScreenStyle.CorruptJungle:
                            Main.time = 51000;
                            if (Main.rand.Next(2) == 0 && canDraw)
                            {
                                MenuDust.Add(new EvilDust(ModContent.GetTexture("StarlightRiver/GUI/Corrupt"), new Vector2(Main.rand.Next(Main.screenWidth), Main.screenHeight), new Vector2(0, -1.4f)));
                            }
                            if (canDraw) Main.spriteBatch.Draw(ModContent.GetTexture("Terraria/Extra_60"), new Rectangle(0, Main.screenHeight - 200, Main.screenWidth, 500), new Rectangle(50, 0, 32, 152), new Color(160, 110, 220) * 0.75f);
                            break;

                        case TitleScreenStyle.CrimsonJungle:
                            Main.time = 51000;
                            if (Main.rand.Next(2) == 0 && canDraw)
                            {
                                MenuDust.Add(new BloodDust(ModContent.GetTexture("StarlightRiver/GUI/Blood"), new Vector2(Main.rand.Next(Main.screenWidth), -40), new Vector2(0, -1.4f), Main.rand.NextFloat(1, 2), Main.rand.NextFloat(0.2f)));
                            }
                            if (canDraw) Main.spriteBatch.Draw(ModContent.GetTexture("Terraria/Extra_60"), new Rectangle(0, -220, Main.screenWidth, 500), new Rectangle(50, 0, 32, 152), new Color(200, 70, 70) * 0.75f, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;

                    }

                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin();

                    foreach (BootlegDust dus in MenuDust) dus.SafeDraw(Main.spriteBatch);
                    foreach (BootlegDust dus in MenuDust) dus.Update();

                    List<BootlegDust> Removals = new List<BootlegDust>();
                    foreach (BootlegDust dus in MenuDust.Where(dus => dus.time <= 0)) Removals.Add(dus);
                    foreach (BootlegDust dus in Removals) MenuDust.Remove(dus);
                    Main.spriteBatch.End();
                }
            }
            catch
            {

            }
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
            foreach (NPC npc in Main.npc.Where(npc => npc.type == ModContent.NPCType<NPCs.Hostile.BoneMine>() && npc.active))
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

            if (Main.gameMenu) return;

            Player player = null;
            if (Main.playerLoaded) { player = Main.LocalPlayer; }

            VitricBackgroundDust.ForEach(BootlegDust => BootlegDust.Update());
            VitricBackgroundDust.RemoveAll(BootlegDust => BootlegDust.time <= 0);

            VitricForegroundDust.ForEach(BootlegDust => BootlegDust.Update());
            VitricForegroundDust.RemoveAll(BootlegDust => BootlegDust.time <= 0);

            if (player != null && LegendWorld.VitricBiome.Contains((player.Center / 16).ToPoint()))
            {
                Vector2 basepoint = (LegendWorld.VitricBiome != null) ? LegendWorld.VitricBiome.TopLeft() * 16 + new Vector2(-2000, 0) : Vector2.Zero;

                DrawLayer(basepoint, ModContent.GetTexture("StarlightRiver/Backgrounds/Glass5"), 0, 300); //the background

                DrawLayer(basepoint, ModContent.GetTexture("StarlightRiver/Backgrounds/Glass1"), 5, 170, new Color(150, 175, 190)); //the back sand
                DrawLayer(basepoint, ModContent.GetTexture("StarlightRiver/Backgrounds/Glass1"), 5.5f, 400, new Color(120, 150, 170), true); //the back sand on top

                foreach (BootlegDust dust in VitricBackgroundDust.Where(n => n.pos.X > 0 && n.pos.X < Main.screenWidth + 30 && n.pos.Y > 0 && n.pos.Y < Main.screenHeight + 30))
                {
                    dust.SafeDraw(Main.spriteBatch); //back particles
                }

                for (int k = 4; k >= 0; k--)
                {
                    int off = 140 + (440 - k * 110);
                    if (k == 4) off = 400;
                    DrawLayer(basepoint, ModContent.GetTexture("StarlightRiver/Backgrounds/Glass" + k), k + 1, off); //the crystal layers and front sand
                    if (k == 0) DrawLayer(basepoint, ModContent.GetTexture("StarlightRiver/Backgrounds/Glass1"), 0.5f, 100, new Color(180, 220, 235), true); //the sand on top
                    if (k == 2)
                    {
                        foreach (BootlegDust dust in VitricForegroundDust.Where(n => n.pos.X > 0 && n.pos.X < Main.screenWidth + 30 && n.pos.Y > 0 && n.pos.Y < Main.screenHeight + 30))
                        {
                            dust.SafeDraw(Main.spriteBatch); //front particles
                        }
                    }

                }

                int screenCenterX = (int)(Main.screenPosition.X + Main.screenWidth / 2);
                for (int k = (int)(screenCenterX - basepoint.X) - (int)(Main.screenWidth * 1.5f); k <= (int)(screenCenterX - basepoint.X) + (int)(Main.screenWidth * 1.5f); k += 30)
                {
                    if (Main.rand.Next(800) == 0)
                    {
                        BootlegDust dus = new VitricDust(ModContent.GetTexture("StarlightRiver/Dusts/Mist"), basepoint + new Vector2(-2000, 1550), k, 0.75f, 0.2f, 0.1f);
                        VitricBackgroundDust.Add(dus);
                    }

                    if (Main.rand.Next(700) == 0)
                    {
                        BootlegDust dus2 = new VitricDust(ModContent.GetTexture("StarlightRiver/Dusts/Mist"), basepoint + new Vector2(-2000, 1550), k, 0.95f, 0.5f, 0.4f);
                        VitricForegroundDust.Add(dus2);
                    }
                }

                for (int i = -2 + (int)(Main.screenPosition.X) / 16; i <= 2 + (int)(Main.screenPosition.X + Main.screenWidth) / 16; i++)
                {
                    for (int j = -2 + (int)(Main.screenPosition.Y) / 16; j <= 2 + (int)(Main.screenPosition.Y + Main.screenHeight) / 16; j++)
                    {
                        if (Lighting.Brightness(i, j) == 0 || ((Main.tile[i, j].active() && Main.tile[i, j].collisionType == 1) || Main.tile[i, j].wall != 0))
                        {
                            Color color = Color.Black * (1 - Lighting.Brightness(i, j) * 2);
                            Main.spriteBatch.Draw(Main.blackTileTexture, new Vector2(i * 16, j * 16) - Main.screenPosition, color);
                        }
                    }
                }
            }
        }
        public static void DrawLayer(Vector2 basepoint, Texture2D texture, float parallax, int offY = 0, Color color = default, bool flip = false)
        {
            if (color == default) color = Color.White;
            for (int k = 0; k <= 5; k++)
            {
                float x = basepoint.X + (k * 739 * 4) + GetParallaxOffset(basepoint.X, parallax * 0.1f) - (int)Main.screenPosition.X;
                float y = basepoint.Y + offY - (int)Main.screenPosition.Y + GetParallaxOffsetY(basepoint.Y + LegendWorld.VitricBiome.Height * 8, parallax * 0.04f);
                if (x > -texture.Width && x < Main.screenWidth + 30)
                {
                    Main.spriteBatch.Draw(texture, new Vector2(x, y), new Rectangle(0, 0, 2956, 1528), color, 0f, Vector2.Zero, 1f, flip ? SpriteEffects.FlipVertically : 0, 0);
                }
            }
        }
        public static int GetParallaxOffset(float startpoint, float factor)
        {
            float vanillaParallax = 1 - (Main.caveParallax - 0.8f) / 0.2f;
            return (int)((Main.screenPosition.X + Main.screenWidth / 2 - startpoint) * factor * vanillaParallax);
        }
        public static int GetParallaxOffsetY(float startpoint, float factor)
        {
            //float vanillaParallax = 1 - (Main.caveParallax - 0.8f) / 0.2f;
            return (int)((Main.screenPosition.Y + Main.screenHeight / 2 - startpoint) * factor /* vanillaParallax*/);
        }
        private void DrawSpecialCharacter(On.Terraria.GameContent.UI.Elements.UICharacterListItem.orig_DrawSelf orig, UICharacterListItem self, SpriteBatch spriteBatch)
        {
            orig(self, spriteBatch);
            Vector2 origin = new Vector2(self.GetDimensions().X, self.GetDimensions().Y);
            int playerStamina = 0;

            //horray double reflection, fuck you vanilla
            Type typ = self.GetType();
            FieldInfo playerInfo = typ.GetField("_playerPanel", BindingFlags.NonPublic | BindingFlags.Instance);
            UICharacter character = (UICharacter)playerInfo.GetValue(self);

            Type typ2 = character.GetType();
            FieldInfo playerInfo2 = typ2.GetField("_player", BindingFlags.NonPublic | BindingFlags.Instance);
            Player player = (Player)playerInfo2.GetValue(character);
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            CodexHandler mp2 = player.GetModPlayer<CodexHandler>();

            if (mp == null || mp2 == null) { return; }

            playerStamina = mp.StatStaminaMax;

            List<Texture2D> textures = new List<Texture2D>()
                {
                    !mp.dash.Locked ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wind1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wind0"),
                    !mp.wisp.Locked ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wisp1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wisp0"),
                    !mp.pure.Locked ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Purity1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Purity0"),
                    !mp.smash.Locked ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Smash1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Smash0"),
                    //!mp.sdash.Locked ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Cloak1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Cloak0"),
                };

            Rectangle box = new Rectangle((int)(origin + new Vector2(86, 66)).X, (int)(origin + new Vector2(86, 66)).Y, 80, 25);
            Rectangle box2 = new Rectangle((int)(origin + new Vector2(172, 66)).X, (int)(origin + new Vector2(86, 66)).Y, 104, 25);
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/box"), box, Color.White); //Stamina box
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/box"), box2, Color.White); //Codex box

            mp.SetList();//update ability list

            if (mp.Abilities.Any(a => !a.Locked))//Draw stamina if any unlocked
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/Stamina"), origin + new Vector2(91, 68), Color.White);
                Utils.DrawBorderString(spriteBatch, playerStamina + " SP", origin + new Vector2(118, 68), Color.White);
            }
            else//Myserious if locked
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/Stamina3"), origin + new Vector2(91, 68), Color.White);
                Utils.DrawBorderString(spriteBatch, "???", origin + new Vector2(118, 68), Color.White);
            }

            if (mp2.CodexState != 0)//Draw codex percentage if unlocked
            {
                Texture2D bookTex = mp2.CodexState == 2 ? ModContent.GetTexture("StarlightRiver/GUI/Book2Closed") : ModContent.GetTexture("StarlightRiver/GUI/Book1Closed");
                int percent = (int)(mp2.Entries.Count(n => !n.Locked) / (float)mp2.Entries.Count * 100f);
                spriteBatch.Draw(bookTex, origin + new Vector2(178, 60), Color.White);
                Utils.DrawBorderString(spriteBatch, percent + "%", origin + new Vector2(212, 68), percent >= 100 ? new Color(255, 205 + (int)(Math.Sin(Main.time / 50000 * 100) * 40), 50) : Color.White);
            }
            else//Mysterious if locked
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/BookLocked"), origin + new Vector2(178, 60), Color.White * 0.4f);
                Utils.DrawBorderString(spriteBatch, "???", origin + new Vector2(212, 68), Color.White);
            }


            //Draw ability Icons
            for (int k = 0; k < textures.Count; k++)
            {
                spriteBatch.Draw(textures[(textures.Count - 1) - k], origin + new Vector2(536 - k * 32, 62), Color.White);
            }


            if (player.statLifeMax > 400) //why vanilla dosent do this I dont know
            {
                spriteBatch.Draw(Main.heart2Texture, origin + new Vector2(80, 37), Color.White);
            }
        }
        private void HandleSpecialItemInteractions(On.Terraria.UI.ItemSlot.orig_LeftClick_ItemArray_int_int orig, Terraria.Item[] inv, int context, int slot)
        {
            if ((inv[slot].modItem is CursedAccessory || inv[slot].modItem is Blocker) && context == 10) return;

            if (Main.mouseItem.modItem is Items.SoulboundItem && (context != 0 || inv != Main.LocalPlayer.inventory)) return;

            if (inv[slot].modItem is Items.SoulboundItem && Main.keyState.PressingShift()) return;

            orig(inv, context, slot);
        }
        private void NoSwapCurse(On.Terraria.UI.ItemSlot.orig_RightClick_ItemArray_int_int orig, Terraria.Item[] inv, int context, int slot)
        {
            Player player = Main.player[Main.myPlayer];
            for (int i = 0; i < player.armor.Length; i++)
            {
                if ((player.armor[i].modItem is CursedAccessory || player.armor[i].modItem is Blocker) && ItemSlot.ShiftInUse && inv[slot].accessory) return;
            }

            if (inv == player.armor)
            {
                Item swaptarget = player.armor[slot - 10];
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
        private static void RedrawItem(SpriteBatch sb, Item[] inv, Texture2D back, Vector2 position, int slot, Color color)
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
                num8 = ((rectangle2.Width <= rectangle2.Height) ? (32f / rectangle2.Height) : (32f / rectangle2.Width));
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
        #endregion
    }
}

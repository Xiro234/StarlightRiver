using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using StarlightRiver.Codex;
using StarlightRiver.Core;
using StarlightRiver.Items.CursedAccessories;
using StarlightRiver.Items.Prototypes;
using StarlightRiver.Keys;
using StarlightRiver.Tiles.Overgrow.Blocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using Terraria.Utilities;
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
            On.Terraria.GameContent.UI.Elements.UIWorldListItem.DrawSelf += VoidIcon;
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
            //Additive Batching
            On.Terraria.Main.DrawDust += DrawAdditive;
            //Particle System Batching for Inventory
            On.Terraria.Main.DrawInterface_27_Inventory += DrawInventoryParticles;
            //Astral metoers
            On.Terraria.WorldGen.meteor += AluminumMeteor;

            ForegroundSystem = new ParticleSystem("StarlightRiver/GUI/Assets/HolyBig", UpdateOvergrowWells); //TODO: Move this later
        }



        #region hooks
        private bool AluminumMeteor(On.Terraria.WorldGen.orig_meteor orig, int i, int j)
        {
            if (StarlightWorld.AluminumMeteors)
            {
                Point16 target = new Point16();

                while (!CheckAroundMeteor(target))
                {
                    int x = Main.rand.Next(Main.maxTilesX);

                    for (int y = 0; y < Main.maxTilesY; y++)
                    {
                        if (Framing.GetTileSafely(x, y).active())
                        {
                            target = new Point16(x, y);
                            break;
                        }
                    }
                }

                for (int x = -35; x < 35; x++)
                    for (int y = -35; y < 35; y++)
                    {
                        if (WorldGen.InWorld(target.X + x, target.Y + y) && Framing.GetTileSafely(target.X + x, target.Y + y).collisionType == 1)
                        {
                            float dist = new Vector2(x, y).Length();
                            if (dist < 8) WorldGen.KillTile(target.X + x, target.Y + y);

                            if (dist > 8 && dist < 15)
                            {
                                WorldGen.PlaceTile(target.X + x, target.Y + y, ModContent.TileType<Tiles.OreAluminum>(), true, true);
                                WorldGen.SlopeTile(target.X + x, target.Y + y, 0);
                            }

                            if (dist > 15 && dist < 30 && Main.rand.Next((int)dist - 15) == 0)
                            {
                                WorldGen.PlaceTile(target.X + x, target.Y + y, ModContent.TileType<Tiles.OreAluminum>(), true, true);
                                WorldGen.SlopeTile(target.X + x, target.Y + y, 0);
                            }
                        }
                    }

                if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText("An asteroid has landed!", new Color(107, 233, 231));
                else if (Main.netMode == NetmodeID.Server) NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("An asteroid has landed!"), new Color(107, 233, 231));

                return true;
            }

            else return orig(i, j);
        }

        private bool CheckAroundMeteor(Point16 test)
        {
            if (test == Point16.Zero) return false;

            for(int x = -35; x < 35; x++)
                for (int y = -35; y < 35; y++)
                {
                    if(WorldGen.InWorld(test.X + x, test.Y + y))
                    {
                        Tile tile = Framing.GetTileSafely(test + new Point16(x, y));
                        if (tile.type == TileID.Containers || tile.type == TileID.Containers2) return false;
                    }
                }

            if (Main.npc.Any(n => n.active && n.friendly && Vector2.Distance(n.Center, test.ToVector2() * 16) <= 35 * 16)) return false;

            return true;
        }

        private void DrawInventoryParticles(On.Terraria.Main.orig_DrawInterface_27_Inventory orig, Main self)
        {
            orig(self);
            CursedAccessory.CursedSystem.DrawParticles(Main.spriteBatch);
        }

        private void DrawAdditive(On.Terraria.Main.orig_DrawDust orig, Main self)
        {
            orig(self);
            Main.spriteBatch.Begin(default, BlendState.Additive, default, default, default, default, Main.GameViewMatrix.ZoomMatrix);

            for (int k = 0; k < Main.maxProjectiles; k++) //projectiles
                if (Main.projectile[k].active && Main.projectile[k].modProjectile is IDrawAdditive) (Main.projectile[k].modProjectile as IDrawAdditive).DrawAdditive(Main.spriteBatch);

            for (int k = 0; k < Main.maxNPCs; k++) //NPCs
                if (Main.npc[k].active && Main.npc[k].modNPC is IDrawAdditive) (Main.npc[k].modNPC as IDrawAdditive).DrawAdditive(Main.spriteBatch);

            Main.spriteBatch.End();
        }

        private bool NoSoulboundFrame(On.Terraria.Player.orig_ItemFitsItemFrame orig, Player self, Item i) => i.modItem is Items.SoulboundItem ? false : orig(self, i);

        private bool NoSoulboundRack(On.Terraria.Player.orig_ItemFitsWeaponRack orig, Player self, Item i) => i.modItem is Items.SoulboundItem ? false : orig(self, i);

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

        private void DontDropSoulbound(On.Terraria.Player.orig_DropSelectedItem orig, Player self)
        {
            if (self.inventory[self.selectedItem].modItem is Items.SoulboundItem || Main.mouseItem.modItem is Items.SoulboundItem) return;
            else orig(self);
        }

        private void UpdateDragonMenu(On.Terraria.Main.orig_DoUpdate orig, Main self, GameTime gameTime)
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

        private bool UpdateMatrixFirst(On.Terraria.Graphics.SpriteViewMatrix.orig_ShouldRebuild orig, SpriteViewMatrix self) => Rotation != 0 ? false : orig(self);

        private void PostDrawPlayer(On.Terraria.Main.orig_DrawPlayer orig, Main self, Player drawPlayer, Vector2 Position, float rotation, Vector2 rotationOrigin, float shadow)
        {
            orig(self, drawPlayer, Position, rotation, rotationOrigin, shadow);
            for (int i = (int)Main.screenPosition.X / 16; i < (int)Main.screenPosition.X / 16 + Main.screenWidth / 16; i++)
                for (int j = (int)Main.screenPosition.Y / 16; j < (int)Main.screenPosition.Y / 16 + Main.screenWidth / 16; j++)
                {
                    if (i > 0 && j > 0 && i < Main.maxTilesX && j < Main.maxTilesY && Main.tile[i, j] != null && Main.tile[i, j].type == ModContent.TileType<GrassOvergrow>())
                    {
                        GrassOvergrow.CustomDraw(i, j, Main.spriteBatch);
                    }
                }
        }

        private void DrawKeys(On.Terraria.Main.orig_DrawItems orig, Main self)
        {
            foreach (Key key in StarlightWorld.Keys)
            {
                key.Draw(Main.spriteBatch);
            }
            orig(self);
        }

        internal static ParticleSystem.Update UpdateOvergrowWells => UpdateOvergrowWellsBody;

        internal ParticleSystem ForegroundSystem;

        private static void UpdateOvergrowWellsBody(Particle particle)
        {
            particle.Position.Y = particle.Velocity.Y * (600 - particle.Timer) + particle.StoredPosition.Y - Main.screenPosition.Y + (particle.StoredPosition.Y - Main.screenPosition.Y) * particle.Velocity.X * 0.5f;
            particle.Position.X = particle.StoredPosition.X - Main.screenPosition.X + (particle.StoredPosition.X - Main.screenPosition.X) * particle.Velocity.X;

            particle.Color = Color.White * (particle.Timer > 300 ? ((300 - (particle.Timer - 300)) / 300f) : (particle.Timer / 300f)) * particle.Velocity.X * 0.4f;

            particle.Timer--;
        }

        private void DrawForeground(On.Terraria.Main.orig_DrawInterface orig, Main self, GameTime gameTime)
        {
            Main.spriteBatch.Begin();
            ForegroundSystem.DrawParticles(Main.spriteBatch);

            //Overgrow magic wells
            if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneOvergrow)
            {
                int direction = Main.dungeonX > Main.spawnTileX ? -1 : 1;
                if (StarlightWorld.rottime == 0)
                    for (int k = 0; k < 10; k++)
                    {
                        for (int i = (int)Main.worldSurface; i < Main.maxTilesY - 200; i += 20)
                        {
                            ForegroundSystem.AddParticle(new Particle(new Vector2(0, 0), new Vector2(0.4f, Main.rand.NextFloat(-2, -1)), 0, Main.rand.NextFloat(1.5f, 2),
                                Color.White * 0.05f, 600, new Vector2(Main.dungeonX * 16 + k * (800 * direction) + Main.rand.Next(30), i * 16)));

                            ForegroundSystem.AddParticle(new Particle(new Vector2(0, 0), new Vector2(0.15f, Main.rand.NextFloat(-2, -1)), 0, Main.rand.NextFloat(0.5f, 0.8f),
                                Color.White * 0.05f, 600, new Vector2(Main.dungeonX * 16 + k * (900 * direction) + Main.rand.Next(15), i * 16)));
                        }
                    }
            }
            Main.spriteBatch.End();
            orig(self, gameTime);
        }

        /*private void TestMenu(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
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
                                MenuDust.Add(new EvilDust(ModContent.GetTexture("StarlightRiver/GUI/Assets/Light"), new Vector2(Main.rand.Next(Main.screenWidth), Main.screenHeight + 40), new Vector2(0, -Main.rand.NextFloat(1.4f))));
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
                                MenuDust.Add(new HolyDust(ModContent.GetTexture("StarlightRiver/GUI/Assets/Holy"), new Vector2(Main.rand.Next(Main.screenWidth), Main.screenHeight - Main.rand.Next(Main.screenHeight / 3)), Vector2.Zero));
                            }
                            if (canDraw) Main.spriteBatch.Draw(ModContent.GetTexture("Terraria/Extra_60"), new Rectangle(0, Main.screenHeight - 200, Main.screenWidth, 500), new Rectangle(50, 0, 32, 152), new Color(180, 170, 100) * 0.75f);
                            break;

                        case TitleScreenStyle.CorruptJungle:
                            Main.time = 51000;
                            if (Main.rand.Next(2) == 0 && canDraw)
                            {
                                MenuDust.Add(new EvilDust(ModContent.GetTexture("StarlightRiver/GUI/Assets/Corrupt"), new Vector2(Main.rand.Next(Main.screenWidth), Main.screenHeight), new Vector2(0, -1.4f)));
                            }
                            if (canDraw) Main.spriteBatch.Draw(ModContent.GetTexture("Terraria/Extra_60"), new Rectangle(0, Main.screenHeight - 200, Main.screenWidth, 500), new Rectangle(50, 0, 32, 152), new Color(160, 110, 220) * 0.75f);
                            break;

                        case TitleScreenStyle.CrimsonJungle:
                            Main.time = 51000;
                            if (Main.rand.Next(2) == 0 && canDraw)
                            {
                                MenuDust.Add(new BloodDust(ModContent.GetTexture("StarlightRiver/GUI/Assets/Blood"), new Vector2(Main.rand.Next(Main.screenWidth), -40), new Vector2(0, -1.4f), Main.rand.NextFloat(1, 2), Main.rand.NextFloat(0.2f)));
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
        }*/

        private void DrawProto(On.Terraria.UI.ItemSlot.orig_Draw_SpriteBatch_refItem_int_Vector2_Color orig, SpriteBatch spriteBatch, ref Item inv, int context, Vector2 position, Color lightColor)
        {
            orig(spriteBatch, ref inv, context, position, lightColor);
        }

        private void VoidIcon(On.Terraria.GameContent.UI.Elements.UIWorldListItem.orig_DrawSelf orig, UIWorldListItem self, SpriteBatch spriteBatch)
        {
            orig(self, spriteBatch);
            Vector2 pos = self.GetDimensions().ToRectangle().TopRight();

            FieldInfo datainfo = self.GetType().GetField("_data", BindingFlags.NonPublic | BindingFlags.Instance);
            WorldFileData data = (WorldFileData)datainfo.GetValue(self);

            string path = data.Path.Replace(".wld", ".twld");

            TagCompound tag;

            try
            {
                byte[] buf = FileUtilities.ReadAllBytes(path, data.IsCloudSave);
                tag = TagIO.FromStream(new MemoryStream(buf), true);
            }

            catch
            {
                tag = null;
            }

            TagCompound tag2 = tag?.GetList<TagCompound>("modData").FirstOrDefault(k => k.GetString("mod") == "StarlightRiver" && k.GetString("name") == "StarlightWorld");
            TagCompound tag3 = tag2?.Get<TagCompound>("data");

            float chungosity = 0;
            if (tag3 != null) chungosity = tag3.GetFloat("Chungus");

            Texture2D tex = ModContent.GetTexture("StarlightRiver/GUI/Assets/ChungusMeter");
            Texture2D tex2 = ModContent.GetTexture("StarlightRiver/GUI/Assets/ChungusMeterFill");
            spriteBatch.Draw(tex, pos + new Vector2(-122, 6), Color.White);
            spriteBatch.Draw(tex2, pos + new Vector2(-108, 10), new Rectangle(0, 0, (int)(tex2.Width * chungosity), tex2.Height), Color.White);
            spriteBatch.Draw(Main.magicPixel, new Rectangle((int)pos.X - 108 + (int)(tex2.Width * chungosity), (int)pos.Y + 10, 2, 10), Color.White);

            Rectangle rect = new Rectangle((int)pos.X - 122, (int)pos.Y + 6, tex.Width, tex.Height);
            if (rect.Contains(Main.MouseScreen.ToPoint()))
            {
                Utils.DrawBorderString(spriteBatch, "Chungosity: " + (int)(chungosity * 100) + "%", self.GetDimensions().Position() + new Vector2(110, 70), Color.White);
            }
        }

        private void DrawBlackFade(On.Terraria.Main.orig_DrawUnderworldBackground orig, Main self, bool flat)
        {
            orig(self, flat);
            if (Main.gameMenu) return;
            Texture2D tex = ModContent.GetTexture("StarlightRiver/GUI/Assets/Fire");

            float distance = Vector2.Distance(Main.LocalPlayer.Center, StarlightWorld.RiftLocation);
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
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/Assets/box"), box, Color.White); //Stamina box
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/Assets/box"), box2, Color.White); //Codex box

            mp.SetList();//update ability list

            if (mp.Abilities.Any(a => !a.Locked))//Draw stamina if any unlocked
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/Assets/Stamina"), origin + new Vector2(91, 68), Color.White);
                Utils.DrawBorderString(spriteBatch, playerStamina + " SP", origin + new Vector2(118, 68), Color.White);
            }
            else//Myserious if locked
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/Assets/Stamina3"), origin + new Vector2(91, 68), Color.White);
                Utils.DrawBorderString(spriteBatch, "???", origin + new Vector2(118, 68), Color.White);
            }

            if (mp2.CodexState != 0)//Draw codex percentage if unlocked
            {
                Texture2D bookTex = mp2.CodexState == 2 ? ModContent.GetTexture("StarlightRiver/GUI/Assets/Book2Closed") : ModContent.GetTexture("StarlightRiver/GUI/Assets/Book1Closed");
                int percent = (int)(mp2.Entries.Count(n => !n.Locked) / (float)mp2.Entries.Count * 100f);
                spriteBatch.Draw(bookTex, origin + new Vector2(178, 60), Color.White);
                Utils.DrawBorderString(spriteBatch, percent + "%", origin + new Vector2(212, 68), percent >= 100 ? new Color(255, 205 + (int)(Math.Sin(Main.time / 50000 * 100) * 40), 50) : Color.White);
            }
            else//Mysterious if locked
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/Assets/BookLocked"), origin + new Vector2(178, 60), Color.White * 0.4f);
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

        private void HandleSpecialItemInteractions(On.Terraria.UI.ItemSlot.orig_LeftClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
        {
            if ((inv[slot].modItem is CursedAccessory || inv[slot].modItem is Blocker) && context == 10) return;

            if (Main.mouseItem.modItem is Items.SoulboundItem && (context != 0 || inv != Main.LocalPlayer.inventory)) return;

            if (inv[slot].modItem is Items.SoulboundItem && Main.keyState.PressingShift()) return;

            orig(inv, context, slot);
        }

        private void NoSwapCurse(On.Terraria.UI.ItemSlot.orig_RightClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
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

        private void DrawSpecial(On.Terraria.UI.ItemSlot.orig_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color orig, SpriteBatch sb, Item[] inv, int context, int slot, Vector2 position, Color color)
        {
            if ((inv[slot].modItem is CursedAccessory || inv[slot].modItem is BlessedAccessory) && context == 10)
            {
                Texture2D back = inv[slot].modItem is CursedAccessory ? ModContent.GetTexture("StarlightRiver/GUI/Assets/CursedBack") : ModContent.GetTexture("StarlightRiver/GUI/Assets/BlessedBack");
                Color backcolor = (!Main.expertMode && slot == 8) ? Color.White * 0.25f : Color.White * 0.75f;
                sb.Draw(back, position, null, backcolor, 0f, default, Main.inventoryScale, SpriteEffects.None, 0f);
                RedrawItem(sb, inv, back, position, slot, color);
            }
            else if ((inv[slot].modItem is InfectedAccessory || inv[slot].modItem is Blocker) && context == 10)
            {
                Texture2D back = ModContent.GetTexture("StarlightRiver/GUI/Assets/InfectedBack");
                Color backcolor = (!Main.expertMode && slot == 8) ? Color.White * 0.25f : Color.White * 0.75f;
                sb.Draw(back, position, null, backcolor, 0f, default, Main.inventoryScale, SpriteEffects.None, 0f);
                RedrawItem(sb, inv, back, position, slot, color);
            }
            else if (inv[slot].modItem is PrototypeWeapon && inv[slot] != Main.mouseItem)
            {
                Texture2D back = ModContent.GetTexture("StarlightRiver/GUI/Assets/ProtoBack");
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

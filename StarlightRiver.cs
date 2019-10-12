using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.UI;
using StarlightRiver.GUI;
using System.IO;
using StarlightRiver.Abilities;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Linq;
using StarlightRiver.Items.CursedAccessories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using On.Terraria.GameContent.UI.Elements;
using System;
using Terraria.GameContent.UI.Elements;
using System.Reflection;
using UICharacter = Terraria.GameContent.UI.Elements.UICharacter;
//using On.Terraria;

namespace StarlightRiver
{
    public class StarlightRiver : Mod
    {
        public Stamina stamina;
        public Collection collection;
        public Overlay overlay;
        public Infusion infusion;
        public Cooking cooking;
        public LinkHP linkhp;
        public UserInterface customResources;
        public UserInterface customResources2;
        public UserInterface customResources3;
        public UserInterface customResources4;
        public UserInterface customResources5;
        public UserInterface customResources6;

        public static ModHotKey Dash;
        public static ModHotKey Superdash;
        public static ModHotKey Smash;
        public static ModHotKey Float;
        public static ModHotKey Purify;

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

        public override void Load()
        {
            Dash = RegisterHotKey("Dash", "LeftShift");
            Superdash = RegisterHotKey("Void Dash", "Q");
            Smash = RegisterHotKey("Smash", "Z");
            Float = RegisterHotKey("Float", "F");
            Purify = RegisterHotKey("Purify", "N");

            if (!Main.dedServ)
            {
                customResources = new UserInterface();
                customResources2 = new UserInterface();
                customResources3 = new UserInterface();
                customResources4 = new UserInterface();
                customResources5 = new UserInterface();
                customResources6 = new UserInterface();
                stamina = new Stamina();
                collection = new Collection();
                overlay = new Overlay();
                infusion = new Infusion();
                cooking = new Cooking();
                linkhp = new LinkHP();

                customResources.SetState(stamina);
                customResources2.SetState(collection);
                customResources3.SetState(overlay);
                customResources4.SetState(infusion);
                customResources5.SetState(cooking);
                customResources6.SetState(linkhp);
            }
            // Cursed Accessory Control Override
            On.Terraria.UI.ItemSlot.LeftClick_ItemArray_int_int += NoClickCurse;
            On.Terraria.UI.ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += DrawSpecial;
            On.Terraria.UI.ItemSlot.RightClick_ItemArray_int_int += NoSwapCurse;
            //Character Slot Addons
            On.Terraria.GameContent.UI.Elements.UICharacterListItem.DrawSelf += DrawSpecialCharacter;
            //Link mode healthbar
            On.Terraria.Main.DrawInterface_Resources_Life += LinkModeHealth;
        }

        private void LinkModeHealth(On.Terraria.Main.orig_DrawInterface_Resources_Life orig)
        {
            if (LinkMode.Enabled)
            {
                return;
            }
            else
            {
                orig();
            }
        }

        private void DrawSpecialCharacter(On.Terraria.GameContent.UI.Elements.UICharacterListItem.orig_DrawSelf orig, Terraria.GameContent.UI.Elements.UICharacterListItem self, SpriteBatch spriteBatch)
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

            playerStamina = mp.staminamax + mp.permanentstamina;


            Texture2D wind = mp.unlock[0] == 1 ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wind1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wind0");
            Texture2D wisp = mp.unlock[1] == 1 ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wisp1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wisp0");
            Texture2D pure = mp.unlock[2] == 1 ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Purity1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Purity0");
            Texture2D smash = mp.unlock[3] == 1 ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Smash1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Smash0");
            Texture2D shadow = mp.unlock[4] == 1 ? ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Cloak1") : ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Cloak0");            

            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/box"), box, Color.White); //Stamina box

            if (mp.unlock.Any(k => k > 0))//Draw stamina if unlocked
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

        private void NoClickCurse(On.Terraria.UI.ItemSlot.orig_LeftClick_ItemArray_int_int orig, Terraria.Item[] inv, int context, int slot)
        {
            if(inv[slot].modItem is CursedAccessory && context == 10)
            {
                return;
            }
            orig(inv, context, slot);
        }

        private void NoSwapCurse(On.Terraria.UI.ItemSlot.orig_RightClick_ItemArray_int_int orig, Terraria.Item[] inv, int context, int slot)
        {
            Player player = Main.player[Main.myPlayer];
            for (int i = 0; i < player.armor.Length; i++)
            {
                if (player.armor[i].modItem is CursedAccessory && ItemSlot.ShiftInUse && inv[slot].accessory)
                {
                    return;
                }              
            }
            orig(inv, context, slot);
        }

        private void DrawSpecial(On.Terraria.UI.ItemSlot.orig_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color orig, SpriteBatch sb, Terraria.Item[] inv, int context, int slot, Vector2 position, Color color)
        {
            if ((inv[slot].modItem is CursedAccessory || inv[slot].modItem is BlessedAccessory) && context == 10)
            {
                Texture2D back = inv[slot].modItem is CursedAccessory ? ModContent.GetTexture("StarlightRiver/GUI/CursedBack") : ModContent.GetTexture("StarlightRiver/GUI/BlessedBack");
                Color backcolor = (!Main.expertMode && slot == 8) ? Color.White * 0.25f : Color.White * 0.75f;
                sb.Draw(back, position, null, backcolor, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);

                //Zoinked from vanilla code
                Item item = inv[slot];
                Vector2 vector = back.Size() * Main.inventoryScale;
                Texture2D texture2D3 = ModContent.GetTexture("StarlightRiver/Items/CursedAccessories/"+inv[slot].modItem.Name);
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
                //End zoink
            }
            else
            {
                orig(sb, inv, context, slot, position, color);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {

            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (MouseTextIndex != -1)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("[PH]MODNAME: Cooldown",
                delegate
                {
                    if (Stamina.visible)
                    {
                        customResources.Update(Main._drawInterfaceGameTime);
                        stamina.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));

                layers.Insert(MouseTextIndex + 1, new LegacyGameInterfaceLayer("[PH]MODNAME: Collection",
                delegate
                {
                    if (Collection.visible)
                    {
                        customResources2.Update(Main._drawInterfaceGameTime);
                        collection.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));

                layers.Insert(0, new LegacyGameInterfaceLayer("[PH]MODNAME: Overlay",
                delegate
                {
                    if (Overlay.visible)
                    {
                        customResources3.Update(Main._drawInterfaceGameTime);
                        overlay.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));

                layers.Insert(MouseTextIndex + 2, new LegacyGameInterfaceLayer("[PH]MODNAME: Infusions",
                delegate
                {
                    if (Infusion.visible)
                    {
                        customResources4.Update(Main._drawInterfaceGameTime);
                        infusion.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));

                layers.Insert(MouseTextIndex + 3, new LegacyGameInterfaceLayer("[PH]MODNAME: Cooking",
                delegate
                {
                    if (Cooking.visible)
                    {
                        customResources5.Update(Main._drawInterfaceGameTime);
                        cooking.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));

                layers.Insert(MouseTextIndex + 4, new LegacyGameInterfaceLayer("[PH]MODNAME: LinkHP",
                delegate
                {
                    if (LinkHP.visible)
                    {
                        customResources6.Update(Main._drawInterfaceGameTime);
                        linkhp.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));
            }
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            var target = new Abilities.Ability(0);
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(reader.ReadString()));
            var ser = new DataContractJsonSerializer(target.GetType());
            target = ser.ReadObject(ms) as Abilities.Ability;

            Main.player[whoAmI].GetModPlayer<AbilityHandler>().ability = target;
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                customResources = null;
                customResources2 = null;
                customResources3 = null;
                customResources4 = null;
                customResources5 = null;
                customResources6 = null;
                stamina = null;
                collection = null;
                overlay = null;
                infusion = null;
                cooking = null;
                linkhp = null;
            }
        }

    }
}	

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.GUI;
using StarlightRiver.Items.CursedAccessories;
using StarlightRiver.RiftCrafting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.UI;

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
        public TextCard textcard;
        public GUI.Codex codex;
        public CodexPopup codexpopup;
        public LootUI lootUI;

        public UserInterface StaminaUserInterface;
        public UserInterface CollectionUserInterface;
        public UserInterface OverlayUserInterface;
        public UserInterface InfusionUserInterface;
        public UserInterface CookingUserInterface;
        public UserInterface KeyInventoryUserInterface;
        public UserInterface TextCardUserInterface;
        public UserInterface CodexUserInterface;
        public UserInterface CodexPopupUserInterface;
        public UserInterface LootUserInterface;

        public static ModHotKey Dash;
        public static ModHotKey Wisp;
        public static ModHotKey Purify;
        public static ModHotKey Smash;
        public static ModHotKey Superdash;

        public List<RiftRecipe> RiftRecipes;

        public static float Rotation;

        public const string PatchString = "Starlight River Test Build #23     6/2/2020 - 01:45 EST";
        public readonly string MessageString = Helper.WrapString("Poop.", Main.screenWidth / 4, Main.fontDeathText, 1);

        public enum AbilityEnum : int { dash, wisp, purify, smash, superdash };
        public static StarlightRiver Instance { get; set; }
        public StarlightRiver() { Instance = this; }
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
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/JungleBloody");
                    priority = MusicPriority.BiomeHigh;
                }

                if (Main.LocalPlayer.GetModPlayer<BiomeHandler>().ZoneJungleHoly)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/JungleHoly");
                    priority = MusicPriority.BiomeHigh;
                }

                if (Main.LocalPlayer.ZoneOverworldHeight && StarlightWorld.starfall)
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
            //Shaders
            if (!Main.dedServ)
            {
                GameShaders.Misc["StarlightRiver:Distort"] = new MiscShaderData(new Ref<Effect>(GetEffect("Effects/Distort")), "Distort");

                Ref<Effect> screenRef4 = new Ref<Effect>(GetEffect("Effects/Shockwave"));
                Terraria.Graphics.Effects.Filters.Scene["ShockwaveFilter"] = new Terraria.Graphics.Effects.Filter(new ScreenShaderData(screenRef4, "ShockwavePass"), Terraria.Graphics.Effects.EffectPriority.VeryHigh);
                Terraria.Graphics.Effects.Filters.Scene["ShockwaveFilter"].Load();

                Ref<Effect> screenRef3 = new Ref<Effect>(GetEffect("Effects/WaterEffect"));
                Terraria.Graphics.Effects.Filters.Scene["WaterFilter"] = new Terraria.Graphics.Effects.Filter(new ScreenShaderData(screenRef3, "WaterPass"), Terraria.Graphics.Effects.EffectPriority.VeryHigh);
                Terraria.Graphics.Effects.Filters.Scene["WaterFilter"].Load();

                Ref<Effect> screenRef2 = new Ref<Effect>(GetEffect("Effects/AuraEffect"));
                Terraria.Graphics.Effects.Filters.Scene["AuraFilter"] = new Terraria.Graphics.Effects.Filter(new ScreenShaderData(screenRef2, "AuraPass"), Terraria.Graphics.Effects.EffectPriority.VeryHigh);
                Terraria.Graphics.Effects.Filters.Scene["AuraFilter"].Load();

                Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/BlurEffect"));
                Terraria.Graphics.Effects.Filters.Scene["BlurFilter"] = new Terraria.Graphics.Effects.Filter(new ScreenShaderData(screenRef, "BlurPass"), Terraria.Graphics.Effects.EffectPriority.High);
                Terraria.Graphics.Effects.Filters.Scene["BlurFilter"].Load();

                Ref<Effect> screenRef5 = new Ref<Effect>(GetEffect("Effects/Purity"));
                Terraria.Graphics.Effects.Filters.Scene["PurityFilter"] = new Terraria.Graphics.Effects.Filter(new ScreenShaderData(screenRef5, "PurityPass"), Terraria.Graphics.Effects.EffectPriority.High);
                Terraria.Graphics.Effects.Filters.Scene["PurityFilter"].Load();
            }

            //Autoload Rift Recipes
            RiftRecipes = new List<RiftRecipe>();
            AutoloadRiftRecipes(RiftRecipes);

            //Hotkeys
            Dash = RegisterHotKey("Forbidden Winds", "LeftShift");
            Wisp = RegisterHotKey("Faeflame", "F");
            Purify = RegisterHotKey("[PH]Purify Crown", "N");
            Smash = RegisterHotKey("Gaia's Fist", "Z");
            Superdash = RegisterHotKey("Zzelera's Cloak", "Q");

            //UI
            if (!Main.dedServ)
            {
                StaminaUserInterface = new UserInterface();
                CollectionUserInterface = new UserInterface();
                OverlayUserInterface = new UserInterface();
                InfusionUserInterface = new UserInterface();
                CookingUserInterface = new UserInterface();
                KeyInventoryUserInterface = new UserInterface();
                TextCardUserInterface = new UserInterface();
                CodexUserInterface = new UserInterface();
                CodexPopupUserInterface = new UserInterface();
                LootUserInterface = new UserInterface();

                stamina = new Stamina();
                collection = new Collection();
                overlay = new Overlay();
                infusion = new Infusion();
                cooking = new Cooking();
                keyinventory = new KeyInventory();
                textcard = new TextCard();
                codex = new GUI.Codex();
                codexpopup = new CodexPopup();
                lootUI = new LootUI();

                StaminaUserInterface.SetState(stamina);
                CollectionUserInterface.SetState(collection);
                OverlayUserInterface.SetState(overlay);
                InfusionUserInterface.SetState(infusion);
                CookingUserInterface.SetState(cooking);
                KeyInventoryUserInterface.SetState(keyinventory);
                TextCardUserInterface.SetState(textcard);
                CodexUserInterface.SetState(codex);
                CodexPopupUserInterface.SetState(codexpopup);
                LootUserInterface.SetState(lootUI);
            }

            //particle systems
            if (!Main.dedServ)
            {
                LoadVitricBGSystems();
            }

            //Hooking
            HookOn();
            HookIL();
        }
        public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
        {
            if (Rotation != 0)
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
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (MouseTextIndex != -1)
            {
                AddLayer(layers, StaminaUserInterface, stamina, MouseTextIndex, Stamina.visible);
                AddLayer(layers, CollectionUserInterface, collection, MouseTextIndex, Collection.visible);
                AddLayer(layers, OverlayUserInterface, overlay, 0, Overlay.visible);
                AddLayer(layers, InfusionUserInterface, infusion, MouseTextIndex, Infusion.visible);
                AddLayer(layers, CookingUserInterface, cooking, MouseTextIndex, Cooking.Visible);
                AddLayer(layers, KeyInventoryUserInterface, keyinventory, MouseTextIndex, KeyInventory.visible);
                AddLayer(layers, TextCardUserInterface, textcard, MouseTextIndex, TextCard.Visible);
                AddLayer(layers, CodexUserInterface, codex, MouseTextIndex, GUI.Codex.ButtonVisible);
                AddLayer(layers, CodexPopupUserInterface, codexpopup, MouseTextIndex, codexpopup.Timer > 0);
                AddLayer(layers, LootUserInterface, lootUI, MouseTextIndex, LootUI.Visible);
            }
        }
        private void AddLayer(List<GameInterfaceLayer> layers, UserInterface userInterface, UIState state, int index, bool visible)
        {
            layers.Insert(index, new LegacyGameInterfaceLayer("StarlightRiver: " + state.ToString(),
                delegate
                {
                    if (visible)
                    {
                        userInterface.Update(Main._drawInterfaceGameTime);
                        state.Draw(Main.spriteBatch);
                    }
                    return true;
                }, InterfaceScaleType.UI));
        }
        public override void Unload()
        {
            if (!Main.dedServ)
            {
                RiftRecipes = null;

                StaminaUserInterface = null;
                CollectionUserInterface = null;
                OverlayUserInterface = null;
                InfusionUserInterface = null;
                CookingUserInterface = null;
                TextCardUserInterface = null;
                CodexUserInterface = null;
                CodexPopupUserInterface = null;
                LootUserInterface = null;

                stamina = null;
                collection = null;
                overlay = null;
                infusion = null;
                cooking = null;
                textcard = null;
                codex = null;
                codexpopup = null;
                lootUI = null;

                Instance = null;
                Dash = null;
                Superdash = null;
                Wisp = null;
                Smash = null;
                Purify = null;
            }
            IL.Terraria.Lighting.PreRenderPhase -= VitricLighting;
        }

        #region NetEasy
        public override void PostSetupContent()
        {
            NetEasy.NetEasy.Register(this);
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            NetEasy.NetEasy.HandleModule(reader, whoAmI);
        }
        #endregion
    }
}

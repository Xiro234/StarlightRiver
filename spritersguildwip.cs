using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.UI;
using spritersguildwip.GUI;

namespace spritersguildwip
{
    public class spritersguildwip : Mod
    {
        public Stamina stamina;
        public Collection collection;
        public UserInterface customResources;
        public UserInterface customResources2;

        public static ModHotKey Dash;
        public static ModHotKey Superdash;
        public static ModHotKey Smash;
        public static ModHotKey Float;
        public spritersguildwip()
        {

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
            }
            return;           
        }

        public override void Load()
        {
            Dash = RegisterHotKey("Dash", "LeftShift");
            Superdash = RegisterHotKey("Void Dash", "Q");
            Smash = RegisterHotKey("Smash", "Z");
            Float = RegisterHotKey("Float", "F");

            if (!Main.dedServ)
            {
                customResources = new UserInterface();
                customResources2 = new UserInterface();
                stamina = new Stamina();
                collection = new Collection();

                Stamina.visible = true;

                customResources.SetState(stamina);
                customResources2.SetState(collection);
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
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                customResources = null;
                customResources2 = null;
                stamina = null;
                collection = null;
            }
        }

    }
}	

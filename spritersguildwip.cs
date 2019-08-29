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
        public UserInterface customResources;

        public static ModHotKey Dash;
        public static ModHotKey Superdash;
        public static ModHotKey Smash;
        public spritersguildwip()
        {

        }
        public override void Load()
        {
            Dash = RegisterHotKey("Dash", "L:Shift");
            Superdash = RegisterHotKey("Energy Dash", "Q");
            Smash = RegisterHotKey("Smash", "Z");

            if (!Main.dedServ)
            {
                customResources = new UserInterface();
                stamina = new Stamina();

                Stamina.visible = true;

                customResources.SetState(stamina);
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
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                customResources = null;
                stamina = null;
            }
        }

    }
}	

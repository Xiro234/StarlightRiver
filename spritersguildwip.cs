using Terraria.ModLoader;

namespace spritersguildwip
{
    public class spritersguildwip : Mod
    {
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
        }
    }
	}
}
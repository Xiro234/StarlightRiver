using Terraria.ModLoader.Config;

namespace StarlightRiver.Configs
{
    public enum TitleScreenStyle
    {
        None = 0,
        Starlight = 1,
        Overgrow = 2,
        Rift = 3,
        Mario = 4
    }
    public class TitleScreenConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Menu Theme")]
        [Tooltip("Changes or disables the menu theme")]
        public TitleScreenStyle Style;
    }
}

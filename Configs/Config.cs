﻿using Terraria.ModLoader.Config;

namespace StarlightRiver.Configs
{
    public enum TitleScreenStyle
    {
        Starlight = 0,
        Vitric = 1,
        Overgrow = 2,
        CorruptJungle = 3,
        CrimsonJungle = 4,
        HallowJungle = 5,
        None = 6
    }

    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Menu Theme")]
        [Tooltip("Changes or disables the menu theme")]
        public TitleScreenStyle Style;

        [Label("Extra Particles")]
        [Tooltip("Enables/Disables special particles. Disable this if you have performance issues.")]
        public bool Active = true;

        [Label("Smooth Lighting Coarseness")]
        [Tooltip("Sample spacing between verticies for certain objects drawn with lighting. Higher = better performance but lower quality.")]
        [Range(1, 8)]
        public int Coarseness = 4;
    }
}
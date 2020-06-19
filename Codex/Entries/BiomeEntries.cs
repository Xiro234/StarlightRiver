﻿using static Terraria.ModLoader.ModContent;
using Terraria;

namespace StarlightRiver.Codex.Entries
{
    internal class VitricEntry : CodexEntry
    {
        public VitricEntry()
        {
            Category = Categories.Biomes;
            Title = "Vitric Desert";
            Body = Helper.WrapString("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tempor nec feugiat nisl pretium fusce id velit. Quam nulla porttitor massa id neque aliquam. Orci phasellus egestas tellus rutrum tellus. Ut placerat orci nulla pellentesque. Magnis dis parturient montes nascetur. Eu augue ut lectus arcu bibendum at. Donec ultrices tincidunt arcu non sodales. Pulvinar mattis nunc sed blandit libero volutpat sed. Lacus suspendisse faucibus interdum posuere lorem. Augue lacus viverra vitae congue eu consequat ac felis donec. Nisl condimentum id venenatis a condimentum vitae sapien pellentesque. Sit amet volutpat consequat mauris. Egestas tellus rutrum tellus pellentesque eu tincidunt tortor. In dictum non consectetur a erat. Lectus magna fringilla urna porttitor rhoncus dolor purus non enim. Facilisi cras fermentum odio eu feugiat. Elit sed vulputate mi sit. Integer malesuada nunc vel risus commodo viverra maecenas accumsan. Diam vulputate ut pharetra sit.",
                500, Main.fontDeathText, 0.8f);
            Hint = "Found beneath the underground desert...";
            Image = GetTexture("StarlightRiver/Codex/Entries/BiomeImageVitric");
            Icon = GetTexture("StarlightRiver/Codex/Entries/BiomeIconVitric");
        }
    }

    internal class OvergrowEntry : CodexEntry
    {
        public OvergrowEntry()
        {
            Category = Categories.Biomes;
            Title = "The Overgrowth";
            Body = Helper.WrapString("NO TEXT",
                500, Main.fontDeathText, 0.8f);
            Hint = "Found beyond a sealed door in the dungeon...";
            Image = GetTexture("StarlightRiver/Codex/Entries/BiomeImageOvergrow");
            Icon = GetTexture("StarlightRiver/Codex/Entries/BiomeIconOvergrow");
        }
    }
}
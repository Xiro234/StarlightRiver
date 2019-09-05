using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace spritersguildwip
{
    public class BiomeHandler : ModPlayer
    {
        public bool ZoneGlass= false;
        public override void UpdateBiomes()
        {
            ZoneGlass = (LegendWorld.glassTiles > 50);
        }

        public override bool CustomBiomesMatch(Player other)
        {
            BiomeHandler modOther = other.GetModPlayer<BiomeHandler>(mod);
            return ZoneGlass == modOther.ZoneGlass;
            // If you have several Zones, you might find the &= operator or other logic operators useful:
            // bool allMatch = true;
            // allMatch &= ZoneGlass == modOther.ZoneGlass;
            // allMatch &= ZoneModel == modOther.ZoneModel;
            // return allMatch;
            // Here is an example just using && chained together in one statemeny 
            // return ZoneGlass == modOther.ZoneGlass && ZoneModel == modOther.ZoneModel;
        }

        public override void CopyCustomBiomesTo(Player other)
        {
            BiomeHandler modOther = other.GetModPlayer<BiomeHandler>(mod);
            modOther.ZoneGlass = ZoneGlass;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = ZoneGlass;
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            ZoneGlass = flags[0];
        }

        public override Texture2D GetMapBackgroundImage()
        {
            if (ZoneGlass)
            {
                //return mod.GetTexture("ExampleBiomeMapBackground");
                return null;
            }
            return null;
        }
    }

    public partial class LegendWorld
    {
        public static int glassTiles;
        public override void TileCountsAvailable(int[] tileCounts)
        {
            glassTiles = tileCounts[mod.TileType("SandGlass")] + tileCounts[mod.TileType("GlassCrystal")];
        }
    }
}

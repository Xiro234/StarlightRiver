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
using spritersguildwip.GUI;

namespace spritersguildwip
{
    public class BiomeHandler : ModPlayer
    {
        public bool ZoneGlass = false;
        public bool ZoneVoidPre = false;
        public override void UpdateBiomes()
        {
            ZoneGlass = (LegendWorld.glassTiles > 50);
            ZoneVoidPre = (LegendWorld.voidTiles > 50);
        }

        public override bool CustomBiomesMatch(Player other)
        {
            BiomeHandler modOther = other.GetModPlayer<BiomeHandler>(mod);
            bool allMatch = true;
            allMatch &= ZoneGlass == modOther.ZoneGlass;
            allMatch &= ZoneVoidPre == modOther.ZoneVoidPre;
            return allMatch;
        }

        public override void CopyCustomBiomesTo(Player other)
        {
            BiomeHandler modOther = other.GetModPlayer<BiomeHandler>(mod);
            modOther.ZoneGlass = ZoneGlass;
            modOther.ZoneVoidPre = ZoneVoidPre;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = ZoneGlass;
            flags[1] = ZoneVoidPre;
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            ZoneGlass = flags[0];
            ZoneVoidPre = flags[1];
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

        public override void PreUpdate()
        {
            if (ZoneVoidPre)
            {
                Overlay.visible = true;
            }
            else
            {
                Overlay.visible = false;
            }
        }
    }

    public partial class LegendWorld
    {
        public static int glassTiles;
        public static int voidTiles;
        public override void TileCountsAvailable(int[] tileCounts)
        {
            glassTiles = tileCounts[mod.TileType("SandGlass")] + tileCounts[mod.TileType("GlassCrystal")];
            voidTiles = tileCounts[mod.TileType("Void1")] + tileCounts[mod.TileType("Void2")];
        }
    }
}

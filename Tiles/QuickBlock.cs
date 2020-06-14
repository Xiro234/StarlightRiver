using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace StarlightRiver.Tiles
{
    internal static class QuickBlock
    {
        public static void QuickSet(this ModTile tile, int MinPick, int DustType, int SoundType, Color mapColor, int Drop, bool dirtMerge = false, bool stone = false, string mapName = "")
        {
            tile.minPick = MinPick;
            tile.dustType = DustType;
            tile.soundType = SoundType;
            tile.drop = Drop;
            Main.tileMergeDirt[tile.Type] = dirtMerge;
            Main.tileStone[tile.Type] = stone;

            Main.tileSolid[tile.Type] = true;
            Main.tileLighted[tile.Type] = true;
            Main.tileBlockLight[tile.Type] = true;

            ModTranslation name = tile.CreateMapEntryName();
            name.SetDefault(mapName);
            tile.AddMapEntry(mapColor, name);
        }
    }
}

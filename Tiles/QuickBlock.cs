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
        public static void QuickSet(this ModTile tile, int minPick, int dustType, int soundType, Color mapColor, int drop, bool dirtMerge = false, bool stone = false, string mapName = "")
        {
            tile.minPick = minPick;
            tile.dustType = dustType;
            tile.soundType = soundType;
            tile.drop = drop;
            Main.tileMergeDirt[tile.Type] = dirtMerge;
            Main.tileStone[tile.Type] = stone;

            Main.tileSolid[tile.Type] = true;
            Main.tileLighted[tile.Type] = true;
            Main.tileBlockLight[tile.Type] = true;

            ModTranslation name = tile.CreateMapEntryName();
            name.SetDefault(mapName);
            tile.AddMapEntry(mapColor, name);
        }
        public static void QuickSetWall(this ModWall wall, int dustType, int soundType, int drop, bool safe, Color mapColor)
        {
            wall.dustType = dustType;
            wall.soundType = soundType;
            wall.drop = drop;
            Main.wallHouse[wall.Type] = safe;
            wall.AddMapEntry(mapColor);
        }
    }
}

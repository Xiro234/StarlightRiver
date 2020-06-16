using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;
using Terraria.DataStructures;

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
        /// <summary>
        /// Note that this should be called at the END of setdefaults so that any additional TileObjectData takes effect.
        /// </summary>
        public static void QuickSetFurniture(this ModTile tile, int width, int height, int dustType, int soundType, bool tallBottom, int drop, Color mapColor, bool solidTop = false, bool solid = false, string mapName = "")
        {
            Main.tileLavaDeath[tile.Type] = false;
            Main.tileFrameImportant[tile.Type] = true;
            Main.tileSolidTop[tile.Type] = solidTop;
            Main.tileSolid[tile.Type] = solid;

            tile.drop = drop;

            TileObjectData.newTile.Width = width;
            TileObjectData.newTile.Height = height;
            TileObjectData.newTile.CoordinateHeights = new int[height];

            for (int k = 0; k < height; k++)
            {
                TileObjectData.newTile.CoordinateHeights[k] = 16;
            }   

            if (tallBottom) TileObjectData.newTile.CoordinateHeights[height - 1] = 18;
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(tile.Type);

            ModTranslation name = tile.CreateMapEntryName();
            name.SetDefault(mapName);
            tile.AddMapEntry(mapColor, name);
            tile.dustType = dustType;
            tile.soundType = soundType;
            tile.disableSmartCursor = true;
        }
    }
}

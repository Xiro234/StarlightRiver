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
    class QuickBlock : ModTile
    {
        private readonly int MinPick;
        private readonly int DustType;
        private readonly int SoundType;
        private readonly Color MapColor;
        private readonly int ThisDrop;
        private readonly bool DirtMerge;
        private readonly bool Stone;
        private readonly string MapName;
        public QuickBlock(int minPick, int dustType, int soundType, Color mapColor, int drop = 0, bool dirtMerge = false, bool stone = false, string mapName = "")
        {
            MinPick = minPick;
            DustType = dustType;
            SoundType = soundType;
            MapColor = mapColor;
            ThisDrop = drop;
            DirtMerge = dirtMerge;
            Stone = stone;
            MapName = mapName;
        }
        public virtual void SafeSetDefaults() { }
        public sealed override void SetDefaults()
        {
            minPick = MinPick;
            dustType = DustType;
            soundType = SoundType;
            drop = ThisDrop;
            Main.tileMergeDirt[Type] = DirtMerge;
            Main.tileStone[Type] = Stone;

            Main.tileSolid[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault(MapName);
            AddMapEntry(MapColor, name);

            SafeSetDefaults();
        }
    }
}

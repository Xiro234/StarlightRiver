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
        private readonly int SoundStyle;
        private readonly Color MapColor;
        private readonly int Drop;
        private readonly bool DirtMerge;
        private readonly bool Stone;
        private readonly string MapName;
        public QuickBlock(int minPick, int dustType, int soundStyle, Color mapColor, int drop = 0, bool dirtMerge = false, bool stone = false, string mapName = "")
        {
            MinPick = minPick;
            DustType = dustType;
            SoundStyle = soundStyle;
            MapColor = mapColor;
            Drop = drop;
            DirtMerge = dirtMerge;
            Stone = stone;
            MapName = mapName;
        }
        public virtual void SafeSetDefaults() { }
        public sealed override void SetDefaults()
        {
            minPick = MinPick;
            dustType = DustType;
            soundStyle = SoundStyle;
            drop = Drop;
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

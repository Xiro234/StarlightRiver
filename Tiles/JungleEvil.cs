using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace StarlightRiver.Tiles
{
    public class GrassJungleCorrupt : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            TileID.Sets.Grass[Type] = true;
            TileID.Sets.GrassSpecial[Type] = true;
            TileID.Sets.ChecksForMerge[Type] = true;
            drop = ItemID.MudBlock;
            AddMapEntry(new Color(98, 82, 148));
        }
        public override void RandomUpdate(int i, int j)
        {
            if (Main.tile[i, j + 1].active() == false && Main.tile[i, j].slope() == 0)
            {
                WorldGen.PlaceTile(i, j + 1, mod.TileType<VineJungleCorrupt>(), true);
            }
        }
    }
    public class VineJungleCorrupt : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileCut[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                mod.TileType<GrassJungleCorrupt>(),
                mod.TileType<VineJungleCorrupt>()
            };
            TileObjectData.addTile(Type);
        }

        public override void RandomUpdate(int i, int j)
        {
            if (Main.tile[i, j + 1].active() == false)
            {
                WorldGen.PlaceTile(i, j + 1, mod.TileType<VineJungleCorrupt>(), true);
            }
        }
    }
}
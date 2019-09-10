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
using Terraria.DataStructures;

namespace spritersguildwip.Tiles
{
    /*class HallowThistle : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileNoFail[Type] = true;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
				mod.TileType<Trellis>()
            };
        }
    }*/

    class ForestIvy : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileCut[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.RandomStyleRange = 3;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                mod.TileType<ForestIvy>(),
                mod.TileType<Planter>()
            };
            TileObjectData.addTile(Type);
            drop = mod.ItemType("Ivy");
        }

        public override void RandomUpdate(int i, int j)
        {
            if(Main.tile[i, j + 1].active() == false)
            {
                WorldGen.PlaceTile(i, j + 1, mod.TileType("ForestIvy"));
            }
        }
    }
    class Deathstalk : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.RandomStyleRange = 3;
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                mod.TileType<Soil>()               
            };
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
{
                mod.TileType<Deathstalk>()
};
            TileObjectData.addTile(Type);
            drop = mod.ItemType("Deathstalk");
        }

        public override void RandomUpdate(int i, int j)
        {
            if (Main.tile[i, j - 1].active() == false)
            {
                WorldGen.PlaceTile(i, j - 1, mod.TileType("Deathstalk"));
            }
        }
    }
}

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
        public int x = 0;
        public int y = 0;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            TileID.Sets.Grass[Type] = true;
            TileID.Sets.GrassSpecial[Type] = true;
            TileID.Sets.ChecksForMerge[Type] = true;
            SetModTree(new TreeJungleCorrupt());
            drop = ItemID.MudBlock;
            AddMapEntry(new Color(98, 82, 148));
            soundType = 0;
            dustType = 38;
        }
        public override void RandomUpdate(int i, int j)//grappling hook breaks the grass, its running killtile for some reason?
        {
            x = Main.rand.Next(-4, 4);
            y = Main.rand.Next(-4, 4);
            //Main.NewText("tick");

            if (Main.tile[i + x, j + y].active() && Main.hardMode)//spread
            {
                if (Main.tile[i + x, j + y].type == TileID.JungleGrass)
                {
                    //Main.NewText("Tile at: " + i + ", " + j + ". x/y: " + x + ", " + y + ". Placing at: " + (i + x) + ", " + (j + y));
                    WorldGen.PlaceTile(i + x, j + y, ModContent.TileType<GrassJungleCorrupt>(), true, true);
                }
                else if (Main.tile[i + x, j + y].type == TileID.Mud)
                {
                    if (!Main.tileSolid[Main.tile[i + x + 1, j + y].type] || !Main.tileSolid[Main.tile[i + x - 1, j + y].type] || !Main.tileSolid[Main.tile[i + x, j + y + 1].type] || !Main.tileSolid[Main.tile[i + x, j + y - 1].type])
                    {
                        WorldGen.PlaceTile(i + x, j + y, ModContent.TileType<GrassJungleCorrupt>(), true, true);
                    }
                }
                else if (Main.tile[i + x, j + y].type == TileID.Stone)
                {
                    WorldGen.PlaceTile(i + x, j + y, TileID.Ebonstone, true, true);
                }
                else if (Main.tile[i + x, j + y].type == TileID.Grass)
                {
                    WorldGen.PlaceTile(i + x, j + y, TileID.CorruptGrass, true, true);
                }
                else if (Main.tile[i + x, j + y].type == TileID.Sand)
                {
                    WorldGen.PlaceTile(i + x, j + y, TileID.Ebonsand, true, true);
                }
                else if (Main.tile[i + x, j + y].type == TileID.IceBlock)
                {
                    WorldGen.PlaceTile(i + x, j + y, TileID.CorruptIce, true, true);
                }
            }

            if (!Main.tile[i, j + 1].active() && Main.tile[i, j].slope() == 0 && !Main.tile[i, j].halfBrick())//vines (Maybe add the corruption thorns too?)
            {
                if (Main.rand.Next(1) == 0)
                {
                    WorldGen.PlaceTile(i, j + 1, ModContent.TileType<VineJungleCorrupt>(), true);
                }
            }

            if (!Main.tile[i, j - 1].active() && Main.tile[i, j].slope() == 0 && !Main.tile[i, j].halfBrick())//grass
            {
                if (Main.rand.Next(2) == 0)
                {
                    WorldGen.PlaceTile(i, j - 1, ModContent.TileType<TallgrassJungleCorrupt>(), true);
                    Main.tile[i, j - 1].frameY = (short)(Main.rand.Next(9) * 18);
                }
            }

            if (!Main.tile[i, j - 1].active() && !Main.tile[i, j - 2].active() && Main.tile[i, j].slope() == 0 && !Main.tile[i, j].halfBrick())//double grass
            {
                if (Main.rand.Next(4) == 0)
                {
                    WorldGen.PlaceTile(i, j - 2, ModContent.TileType<TallgrassJungleCorrupt2>(), true);
                    int rand = Main.rand.Next(6);
                    Main.tile[i, j - 1].frameY = (short)(rand * 36);
                    Main.tile[i, j - 2].frameY = (short)(18 + rand * 36);
                }
            }
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            effectOnly = true;
            WorldGen.PlaceTile(i, j, TileID.Mud, false, true);
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {

            if (Main.rand.Next(600) == 0 && !Main.tile[i, j + 1].active() && Main.tile[i, j].slope() == 0)
            {
                Dust.NewDustPerfect(new Vector2(i, j) * 16, mod.DustType("Corrupt2"), new Vector2(0, 0.6f) );
            }

            if (!Main.tile[i, j - 1].active() && Main.tile[i, j].slope() == 0 && !Main.tile[i, j].halfBrick())//grass quickly if nearby
            {
                if (Main.rand.Next(120) == 0)
                {
                    WorldGen.PlaceTile(i, j - 1, ModContent.TileType<TallgrassJungleCorrupt>(), true);
                    Main.tile[i, j - 1].frameY = (short)(Main.rand.Next(9) * 18);
                }
            }

            if (!Main.tile[i, j - 1].active() && !Main.tile[i, j - 2].active() && Main.tile[i, j].slope() == 0 && !Main.tile[i, j].halfBrick())//double grass quickly if nearby
            {
                if (Main.rand.Next(180) == 0)
                {
                    WorldGen.PlaceTile(i, j - 2, ModContent.TileType<TallgrassJungleCorrupt2>(), true);
                    int rand = Main.rand.Next(6);
                    Main.tile[i, j - 1].frameY = (short)(18 + rand * 36);
                    Main.tile[i, j - 2].frameY = (short)(rand * 36);
                }
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
                ModContent.TileType<GrassJungleCorrupt>(),
                ModContent.TileType<VineJungleCorrupt>()
            };
            TileObjectData.addTile(Type);
            soundType = 6;
            dustType = 14;
            AddMapEntry(new Color(64, 57, 94));
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Main.tile[i, j + 1].active() && Main.tile[i, j - 9].type != Type)
            {
                if (Main.rand.Next(1) == 0)
                {
                    WorldGen.PlaceTile(i, j + 1, ModContent.TileType<VineJungleCorrupt>(), true);
                }
            }
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (!Main.tile[i, j + 1].active() && Main.tile[i, j - 9].type != Type) //spawns Vines quickly if nearby
            {
                if (Main.rand.Next(120) == 0)
                {
                    WorldGen.PlaceTile(i, j + 1, ModContent.TileType<VineJungleCorrupt>(), true);
                }
            }
        }
        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            if(!Main.tile[i, j - 1].active())
            {
                WorldGen.KillTile(i, j, false, false, false);
                WorldGen.SquareTileFrame(i, j, true);
            }
            else if (Main.tile[i, j - 1].type != ModContent.TileType<GrassJungleCorrupt>() && Main.tile[i, j - 1].type != ModContent.TileType<VineJungleCorrupt>())
            {
                WorldGen.KillTile(i, j, false, false, false);
                WorldGen.SquareTileFrame(i, j, true);
            }
            return true;
        }
    }
    public class TallgrassJungleCorrupt : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileCut[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.RandomStyleRange = 9;
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                ModContent.TileType<GrassJungleCorrupt>()
            };
            TileObjectData.addTile(Type);
            soundType = 6;
            dustType = 14;
            AddMapEntry(new Color(64, 57, 94));
        }
    }

    public class TallgrassJungleCorrupt2 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileCut[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.RandomStyleRange = 6;
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                ModContent.TileType<GrassJungleCorrupt>()
            };
            TileObjectData.addTile(Type);
            soundType = 6;
            dustType = 14;
            AddMapEntry(new Color(64, 57, 94));
        }
    }

    public class WallJungleCorrupt : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            dustType = 14;
            drop = mod.ItemType("WallJungleCorruptItem");
            AddMapEntry(new Color(42, 36, 52));
        }

        public override void RandomUpdate(int i, int j)
        {
            for(int x = i - 4; x <= i+4; x++)
            {
                for (int y = j - 4; y <= j + 4; y++)
                {
                    if (Main.tile[x, y].active())
                    {
                        return;
                    }
                }
            }
            if (!Main.tile[i, j].active())
            {
                if (Main.rand.Next(30) == 0)
                {
                    WorldGen.PlaceTile(i, j, ModContent.TileType<SporeJungleCorrupt>(), true);
                }
            }
        }
    }
}
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

namespace spritersguildwip.Tiles
{
    class Soil : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileMerge[Type][mod.GetTile("Trellis").Type] = true;
            drop = mod.ItemType("Soil");
            AddMapEntry(new Color(172, 131, 105));
        }
    }


    class Trellis : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileMerge[Type][mod.GetTile("Soil").Type] = true;
            drop = mod.ItemType("Soil");
            AddMapEntry(new Color(172, 131, 105));
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            spriteBatch.Draw(ModContent.GetTexture("spritersguildwip/Tiles/Post"), new Vector2((i + 12) * 16, (j+9) * 16) - (Main.screenPosition), Color.White);
        }
    }

    public class Planter : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);
            
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Planter");
            AddMapEntry(new Color(196, 107, 59), name);
            disableSmartCursor = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 32, mod.ItemType("Planter"));
        }
        public override void PlaceInWorld(int i, int j, Item item)
        {
            Main.tile[i, j].frameX = 0;
        }
        public override void RandomUpdate(int i, int j)
        {
            if (Main.tile[i, j + 1].active() == false)
            {
               switch (Main.tile[i,j].frameX / 18)
                {
                    case 0: break;
                    case 1: WorldGen.PlaceTile(i, j + 1, mod.TileType("ForestIvy")); break;
                }                 
            }
        }
        public override void RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            if (player.HeldItem.type == mod.ItemType("IvySeeds") && Main.tile[i, j].frameX == 0)
            {
                Main.tile[i, j].frameX = 18;               
            }
        }

    }

    class GreenhouseWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            drop = mod.ItemType("Greenhouse");
        }
    }
}

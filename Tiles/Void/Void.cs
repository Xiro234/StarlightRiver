using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Void
{
    internal class Void1 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("Void1Item");
            dustType = mod.DustType("Darkness");
            AddMapEntry(new Color(35, 40, 20));
        }
    }

    internal class Void2 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("Void2Item");
            dustType = mod.DustType("Darkness");
            AddMapEntry(new Color(45, 50, 30));
        }
    }

    internal class VoidTorch1 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(45, 50, 30));
            dustType = mod.DustType("Darkness");
            disableSmartCursor = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("VoidTorch1Item"));
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                for (int k = 0; k <= 3; k++)
                {
                    Dust.NewDust(new Vector2((i * 16) - 1, (j - 1) * 16), 12, 16, mod.DustType("Darkness"));
                }
            }
        }
    }

    internal class VoidTorch2 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.AnchorWall = true;
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(45, 50, 30));
            dustType = mod.DustType("Darkness");
            disableSmartCursor = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("VoidTorch2Item"));
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                for (int k = 0; k <= 6; k++)
                {
                    Dust.NewDust(new Vector2((i * 16) + 5, (j - 0.8f) * 16), 18, 14, mod.DustType("Darkness"));
                }
            }
        }
    }

    internal class VoidPillarB : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(45, 50, 30));
            dustType = mod.DustType("Darkness");
            disableSmartCursor = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("VoidPillarBItem"));
        }
    }

    internal class VoidPillarM : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                ModContent.TileType<VoidPillarB>(),
                ModContent.TileType<VoidPillarM>()
            };
            TileObjectData.addTile(Type);
            dustType = mod.DustType("Darkness");
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("VoidPillarMItem"));
        }
    }

    internal class VoidPillarT : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0);

            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                ModContent.TileType<VoidPillarM>()
            };
            TileObjectData.addTile(Type);
            dustType = mod.DustType("Darkness");
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("VoidPillarTItem"));
        }
    }

    internal class VoidPillarP : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0);

            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                ModContent.TileType<VoidPillarM>()
            };
            TileObjectData.addTile(Type);
            dustType = mod.DustType("Darkness");
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("VoidPillarPItem"));
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Vitric
{
    public class VitricRoundCactus : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorValidTiles = new int[] { ModContent.TileType<VitricSand>(), ModContent.TileType<VitricSoftSand>() };
            TileObjectData.newTile.RandomStyleRange = 4;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("");
            AddMapEntry(new Color(80, 131, 142), name);
            dustType = ModContent.DustType<Dusts.Air>();
            soundType = SoundID.Shatter;
            disableSmartCursor = true;
            minPick = 0;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new Vector2(i * 16, j * 16), 32, 48, ModContent.ItemType<Items.Vitric.VitricOre>(), Main.rand.Next(2, 5));
        }
    }

    public class VitricSmolCactus : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorValidTiles = new int[] { ModContent.TileType<VitricSand>(), ModContent.TileType<VitricSoftSand>() };
            TileObjectData.newTile.RandomStyleRange = 4;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("");
            AddMapEntry(new Color(80, 131, 142), name);
            dustType = ModContent.DustType<Dusts.Air>();
            soundType = SoundID.Shatter;
            disableSmartCursor = true;
            minPick = 0;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new Vector2(i * 16, j * 16), 32, 48, ModContent.ItemType<Items.Vitric.VitricOre>(), 1);
        }
    }

    public class VitricCactus : ModCactus
    {
        public override Texture2D GetTexture()
        {
            return ModContent.GetTexture("StarlightRiver/Tiles/Vitric/VitricCactus");
        }
    }
}
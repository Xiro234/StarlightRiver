using Microsoft.Xna.Framework;
using StarlightRiver.GUI;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Crafting
{
    internal class Oven : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);
            dustType = DustID.Stone;
            disableSmartCursor = true;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Oven");
            AddMapEntry(new Color(113, 113, 113), name);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.4f;
            g = 0.2f;
            b = 0.05f;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, ItemType<Items.Crafting.OvenItem>());
        }
    }

    internal class OvenAstral : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);
            dustType = DustID.Stone;
            disableSmartCursor = true;
            adjTiles = new int[] { TileType<Oven>() };

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Astral Oven");
            AddMapEntry(new Color(125, 125, 125), name);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.5f;
            g = 0.3f;
            b = 0.15f;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, ItemType<Items.Crafting.OvenAstralItem>());
        }
    }

    internal class HerbStation : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);
            dustType = DustID.t_LivingWood;
            disableSmartCursor = true;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Herbologist's Bench");
            AddMapEntry(new Color(151, 107, 75), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, ItemType<Items.Crafting.HerbStationItem>());
        }
    }

    internal class CookStation : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.Width = 6;
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 3);
            TileObjectData.addTile(Type);

            dustType = DustID.t_LivingWood;
            disableSmartCursor = true;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Prep Station");
            AddMapEntry(new Color(151, 107, 75), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, ItemType<Items.Crafting.CookStationItem>());
        }

        public override bool NewRightClick(int i, int j)
        {
            if (!Cooking.Visible) { Cooking.Visible = true; Main.PlaySound(SoundID.MenuOpen); }
            else { Cooking.Visible = false; Main.PlaySound(SoundID.MenuClose); }
            return true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (Vector2.Distance(Main.LocalPlayer.Center, new Vector2(i, j) * 16) > 128 && Cooking.Visible) { Cooking.Visible = false; Main.PlaySound(SoundID.MenuClose); }
        }
    }
}
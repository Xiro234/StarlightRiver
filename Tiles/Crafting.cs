using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using StarlightRiver.GUI;

namespace StarlightRiver.Tiles
{
    class Oven : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);
            //AddMapEntry(new Color(113, 113, 113)); this goes after ModTranslation & name.SetDefault, not before.
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
            Item.NewItem(i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Crafting.OvenItem>());
        }
    }

    class OvenAstral : ModTile
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
            adjTiles = new int[] { ModContent.TileType<Oven>() };

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
            Item.NewItem(i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Crafting.OvenAstralItem>());
        }
    }

    class HerbStation : ModTile
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
            Item.NewItem(i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Crafting.HerbStationItem>());
        }
    }

    class CookStation : ModTile
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
            name.SetDefault("Prep Station");
            AddMapEntry(new Color(151, 107, 75), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Crafting.CookStationItem>());
        }

        public override bool NewRightClick(int i, int j)
        {
            if (Vector2.Distance(Main.LocalPlayer.Center, new Vector2(i, j) * 16) <= 64 && !Cooking.visible) { Cooking.visible = true; Main.PlaySound(SoundID.MenuOpen); }
            else { Cooking.visible = false; Main.PlaySound(SoundID.MenuClose); }
            return true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (Vector2.Distance(Main.LocalPlayer.Center, new Vector2(i, j) * 16) > 64 && Cooking.visible) { Cooking.visible = false; }
        }
    }
}

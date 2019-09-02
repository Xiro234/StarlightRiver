using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spritersguildwip.Ability;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace spritersguildwip.Tiles
{
    public class VitricOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("");//Map name
            AddMapEntry(new Color(0, 0, 0), name);
            dustType = mod.DustType("Wind");
            disableSmartCursor = true;
            minPick = int.MaxValue;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public static Texture2D glow = ModContent.GetTexture("spritersguildwip/Tiles/VitricOreGlow");
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color color = new Color(255, 255, 255) * (float)Math.Sin(LegendWorld.rottime);
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                spriteBatch.Draw(glow, new Vector2((i + 12) * 16 - 1, (j + 12) * 16 - 1) - Main.screenPosition, color);
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new Vector2(i * 16, j * 16), 32, 48, mod.ItemType("Glassore"), Main.rand.Next(4,12));
        }
    }

    public class VitricOreFloat : ModTile
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
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("");//Map name
            AddMapEntry(new Color(0, 0, 0), name);
            dustType = mod.DustType("Wind");
            disableSmartCursor = true;
            minPick = int.MaxValue;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public static Texture2D glow = ModContent.GetTexture("spritersguildwip/Tiles/VitricOreFloatGlow");
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color color = new Color(255, 255, 255) * (float)Math.Sin(LegendWorld.rottime);
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                spriteBatch.Draw(glow, new Vector2((i + 12) * 16 - 1, (j + 12) * 16 - 1) - Main.screenPosition, color);
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new Vector2(i * 16, j * 16), 32, 48, mod.ItemType("Glassore"), Main.rand.Next(2, 6));
        }
    }

    public class OreCollide : ModPlayer
    {

        public override void PreUpdate()
        { 
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

            for (int j = (int)(player.position.Y / 16) - 3; j <= (int)(player.position.Y / 16) + 3; j++)
            {
                for (int i = (int)(player.position.X / 16) - 3; i <= (int)(player.position.X / 16) + 3; i++)
                {
                    if ((Main.tile[i, j].type == mod.TileType("VitricOre") || Main.tile[i, j].type == mod.TileType("VitricOreFloat")) && Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
                    {
                        for (float f = 0; f <= 1; f += 1/30f)
                        {
                            Vector2 lerped = Vector2.Lerp(player.position, player.oldPosition, f);
                            if (Collision.CheckAABBvAABBCollision(lerped, new Vector2(32, 48), new Vector2(i * 16, j * 16), new Vector2(32, 48)) && mp.dashcd >= 1)
                            {
                                f = 2;
                                WorldGen.KillTile(i, j);                                                                 
                            }
                        }
                    }
                }
            }
        }
    }
}

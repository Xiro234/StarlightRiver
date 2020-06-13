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
            AddMapEntry(new Color(0, 255, 255), name);
            dustType = ModContent.DustType<Dusts.Air>();
            soundType = SoundID.Shatter;
            disableSmartCursor = true;
            minPick = int.MaxValue;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public static Texture2D glow = ModContent.GetTexture("StarlightRiver/Tiles/Vitric/VitricOreGlow");

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color color = new Color(255, 255, 255) * (float)Math.Sin(StarlightWorld.rottime);
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                spriteBatch.Draw(glow, new Vector2((i + 12) * 16 - 1, (j + 12) * 16 - 1) - Main.screenPosition, color);
            }
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                if (!Main.projectile.Any(proj => proj.Hitbox.Intersects(new Rectangle(i * 16 + 4, j * 16 + 4, 1, 1)) && proj.type == ModContent.ProjectileType<Projectiles.Dummies.VitricOreDummy>() && proj.active))
                {
                    Projectile.NewProjectile(new Vector2(i + 1, j + 1) * 16, Vector2.Zero, ModContent.ProjectileType<Projectiles.Dummies.VitricOreDummy>(), 0, 0);
                }
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new Vector2(i * 16, j * 16), 32, 48, ModContent.ItemType<Items.Vitric.VitricOre>(), Main.rand.Next(4, 12));
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
            dustType = ModContent.DustType<Dusts.Air>();
            soundType = SoundID.Shatter;
            disableSmartCursor = true;
            minPick = int.MaxValue;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color color = new Color(255, 255, 255) * (float)Math.Sin(StarlightWorld.rottime);
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/Vitric/VitricOreFloatGlow"), new Vector2((i + 12) * 16 - 1, (j + 12) * 16 - 1) - Main.screenPosition, color);
            }
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                if (!Main.projectile.Any(proj => proj.Hitbox.Intersects(new Rectangle(i * 16 + 4, j * 16 + 4, 1, 1)) && proj.type == ModContent.ProjectileType<Projectiles.Dummies.VitricOreDummy>() && proj.active))
                {
                    Projectile.NewProjectile(new Vector2(i + 1, j + 1) * 16, Vector2.Zero, ModContent.ProjectileType<Projectiles.Dummies.VitricOreDummy>(), 0, 0);
                }
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new Vector2(i * 16, j * 16), 32, 48, ModContent.ItemType<Items.Vitric.VitricOre>(), Main.rand.Next(2, 6));
        }
    }
}
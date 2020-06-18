﻿using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Items;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using StarlightRiver.Projectiles.Dummies;
using StarlightRiver.Abilities;

namespace StarlightRiver.Tiles.Overgrow
{
    internal class BulbFruit : DummyTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }

        public override int DummyType => ProjectileType<BulbFruitDummy>();

        public override bool SpawnConditions(int i, int j)
        {
            Tile tile = Main.tile[i, j];

            if (tile.frameY == 0 && (tile.frameX == 0 || tile.frameX == 34)) return true;
            else return false;
        }

        public override void SetDefaults() => QuickBlock.QuickSetFurniture(this, 2, 2, DustType<Dusts.Gold>(), SoundID.Grass, false, new Color(255, 255, 200));

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.frameY == 0 && tile.frameX == 0) { r = 0.8f; g = 0.7f; b = 0.4f; }
            else { r = 0; g = 0; b = 0; }
        }

        public override void RandomUpdate(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            if (tile.frameY == 0 && tile.frameX == 34)
            {
                if (Main.rand.Next(8) == 0)
                {
                    for (int x = i; x <= i + 1; x++)
                        for (int y = j; y <= j + 1; y++)
                            Main.tile[x, y].frameX -= 34;
                }
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new Vector2(i * 16, j * 16), 32, 48, ItemType<Items.Debug.DebugPotion>());
    }

    internal class BulbFruitDummy : Dummy
    {
        public BulbFruitDummy() : base(TileType<BulbFruit>(), 32, 32) { }

        public override void Collision(Player player)
        {
            Tile tile = Main.tile[ParentX - 1, ParentY - 1];
            if (tile.frameX == 0 && tile.frameY == 0 && AbilityHelper.CheckWisp(player, projectile.Hitbox))
            {
                for (int k = 0; k < 40; k++) Dust.NewDustPerfect(projectile.Center, DustType<Dusts.Gold2>(), Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(1.2f, 1.4f));
                tile.frameX = 34;
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Tile tile = Main.tile[ParentX - 1, ParentY - 1];

            Texture2D tex2 = GetTexture("StarlightRiver/Tiles/Overgrow/BulbFruit"); //Draws the bulb itself
            Rectangle frame = new Rectangle((tile.frameX == 0 && tile.frameY == 0) ? 0 : 32, 0, 32, 32);
            float offset = (float)Math.Sin(StarlightWorld.rottime) * 3;

            spriteBatch.Draw(tex2, projectile.Center + new Vector2(offset, 0) - Main.screenPosition, frame, Lighting.GetColor(ParentX, ParentY), 0, Vector2.One * 16, 1, 0, 0);

            if (tile.frameX == 0 && tile.frameY == 0) //Draws the glowing indicator
            {
                Texture2D tex = GetTexture("StarlightRiver/Tiles/Overgrow/BulbFruitGlow");

                spriteBatch.Draw(tex, projectile.Center + new Vector2(offset, 6) - Main.screenPosition, tex.Frame(), Color.White * (float)Math.Sin(StarlightWorld.rottime), 0, tex.Size() / 2, 1, 0, 0);
                Dust.NewDust(projectile.position, 32, 32, DustType<Dusts.Gold>(), 0, 0, 0, default, 0.3f);
                Lighting.AddLight(projectile.Center, new Vector3(1, 0.8f, 0.4f));
            }

            for (int k = 2; k <= 30; k++) //Draws the vine
            {
                if (Main.tile[ParentX, ParentY - k].active()) break;
                Texture2D tex = GetTexture("StarlightRiver/Tiles/Overgrow/VineOvergrowFlow");
                float sway = (float)Math.Sin(StarlightWorld.rottime + k * 0.2f) * 3;

                spriteBatch.Draw(tex, projectile.Center + new Vector2(sway - 8, k * -16) - Main.screenPosition, new Rectangle(16 * k % 3, 0, 16, 16), Lighting.GetColor(ParentX, ParentY - k));

                if (Main.rand.Next(5) == 0 && tile.frameX == 0 && tile.frameY == 0) Dust.NewDust(projectile.Center - new Vector2(10, k * 16 - 8), 16, 16, DustType<Dusts.Gold2>(), 0, -3, 0, default, 0.3f);
            }
        }
    }

    internal class BulbFruitItem : QuickTileItem
    {
        public override string Texture => "StarlightRiver/MarioCumming";
        public BulbFruitItem() : base("Bulb Fruit", "", TileType<BulbFruit>(), 1) { }
    }

}
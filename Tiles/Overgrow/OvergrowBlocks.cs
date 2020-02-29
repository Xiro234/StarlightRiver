using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using System;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;
using System.Linq;

namespace StarlightRiver.Tiles.Overgrow
{
    class BrickOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileMerge[Type][mod.GetTile("GrassOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("CrusherTile").Type] = true;
            Main.tileMerge[Type][mod.GetTile("GlowBrickOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("LeafOvergrow").Type] = true;

            Main.tileMerge[Type][TileID.BlueDungeonBrick] = true;
            Main.tileMerge[Type][TileID.GreenDungeonBrick] = true;
            Main.tileMerge[Type][TileID.PinkDungeonBrick] = true;
            minPick = 210;
            drop = mod.ItemType("BrickOvergrowItem");
            AddMapEntry(new Color(79, 76, 71));
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Random rand = new Random(i * j * j);
            if (rand.Next(30) == 0 && i != 1 && j != 1)
            {
                Main.specX[nextSpecialDrawIndex] = i;
                Main.specY[nextSpecialDrawIndex] = j;
                nextSpecialDrawIndex++;
            }
        }
        public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Random rand = new Random(i * j * j);
            if (rand.Next(30) == 0 && i != 1 && j != 1)
            {
                Texture2D tex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/Blob");
                spriteBatch.Draw(tex, (Helper.TileAdj + new Vector2(i, j)) * 16 + Vector2.One * 8 - Main.screenPosition, new Rectangle(rand.Next(4) * 40, 0, 40, 50), Lighting.GetColor(i, j), 0, new Vector2(20, 25), 1, 0, 0);
            }
        }
    }
    class GlowBrickOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileMerge[Type][mod.GetTile("GrassOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("BrickOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("LeafOvergrow").Type] = true;
            minPick = 210;
            drop = mod.ItemType("BrickOvergrowItem");
            AddMapEntry(new Color(79, 76, 71));

            animationFrameHeight = 270;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 5)
            {
                frameCounter = 0;
                if (++frame >= 5)
                {
                    frame = 0;
                }
            }
        }

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            frameYOffset = 270 * (( j + Main.tileFrame[type]) % 6);   
        }
    }
    class LeafOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileMerge[Type][mod.GetTile("GrassOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("BrickOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("GlowBrickOvergrow").Type] = true;
            minPick = 210;
            drop = mod.ItemType("BrickOvergrowItem");
            AddMapEntry(new Color(221, 211, 67));
        }
    }

    class GrassOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileMerge[Type][mod.GetTile("BrickOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("LeafOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("CrusherTile").Type] = true;
            Main.tileMerge[Type][mod.GetTile("GlowBrickOvergrow").Type] = true;
            TileID.Sets.Grass[Type] = true;
            drop = mod.ItemType("BrickOvergrowItem");
            AddMapEntry(new Color(202, 157, 49));
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Tile tile = Main.tile[i, j];
            if ((tile.frameX >= 10 && tile.frameX < 70 && tile.frameY == 0) )
            {
                Main.specX[nextSpecialDrawIndex] = i;
                Main.specY[nextSpecialDrawIndex] = j;
                nextSpecialDrawIndex++;
            }
        }
        public void CustomDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Texture2D tex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/GrassOvergrowMoss");
            Rectangle source = new Rectangle(0 + i % 5 * 8, 0, 8, 16);
            Color color = Lighting.GetColor(i, j);

            Vector2 crunch = new Vector2(0, -5);
            if (Main.player.Any(n => n.Hitbox.Intersects(new Rectangle(i * 16 - 8, j * 16 - 1, 16, 1)))) crunch.Y += 2;
            if (Main.player.Any(n => n.Hitbox.Intersects(new Rectangle(i * 16, j * 16 - 1, 8, 1)))) crunch.Y += 3;

            Vector2 crunch2 = new Vector2(0, -4);
            if (Main.player.Any(n => n.Hitbox.Intersects(new Rectangle(i * 16 + 8, j * 16 - 1, 16, 1)))) crunch2.Y += 2;
            if (Main.player.Any(n => n.Hitbox.Intersects(new Rectangle(i * 16 + 8, j * 16 - 1, 8, 1)))) crunch2.Y += 3;


            if (tile.frameX >= 10 && tile.frameX < 70 && tile.frameY == 0)
            {
                if(Main.tile[i - 1, j].type == Type)
                    spriteBatch.Draw(tex, new Vector2(i, j) * 16 + crunch - Main.screenPosition, source, color);
                if (Main.tile[i + 1, j].type == Type)
                    spriteBatch.Draw(tex, new Vector2(i + 0.5f, j) * 16 + crunch2 - Main.screenPosition, source, color);
            }

            if (Main.player.Any(n => n.Hitbox.Intersects(new Rectangle(i * 16, j * 16 - 1, 16, 1)) && n.velocity.X != 0))
            {
                Player player = Main.player.FirstOrDefault(n => n.Hitbox.Intersects(new Rectangle(i * 16, j * 16 - 1, 16, 1)) && n.velocity.X != 0);
                if (Main.rand.Next(3) == 0)Dust.NewDust(new Vector2(i, j - 0.5f) * 16, 16, 1, ModContent.DustType<Dusts.Stamina>(), -player.velocity.X * 0.5f, -2);
                if(Main.rand.Next(10) == 0)Dust.NewDust(new Vector2(i, j + 0.5f) * 16, 16, 1, ModContent.DustType<Dusts.Leaf>(), 0, 0.6f);

                if (player.GetModPlayer<Abilities.AbilityHandler>().dash.Cooldown == 90)
                {
                    for (int k = 0; k < 20; k++)
                    {
                        Dust.NewDust(new Vector2(i, j + 0.5f) * 16, 16, 1, ModContent.DustType<Dusts.Leaf>(), 0, -2);
                    }
                }
            }
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Main.tile[i, j - 1].active())
            {
                if (Main.rand.Next(1) == 0)
                {
                    WorldGen.PlaceTile(i, j - 1, ModContent.TileType<TallgrassOvergrow>(), true);
                }
            }
        }
    }

    class VineOvergrow : ModTile
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
                ModContent.TileType<GrassOvergrow>(),
                ModContent.TileType<VineOvergrow>()
            };
            TileObjectData.addTile(Type);
            soundType = 6;
            dustType = 14;
            AddMapEntry(new Color(202, 157, 49));
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            float sway = 0;
            float rot = LegendWorld.rottime + (i % 4) * 0.3f;
            for(int k = 1; k > 0; k++)
            {
                if (Main.tile[i, j - k].type == Type && sway <= 2.4f) { sway += 0.3f; }
                else { break; }
            }
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/VineOvergrowFlow"), 
                (new Vector2(i, j)  + Helper.TileAdj) * 16 - Main.screenPosition + new Vector2((float) (1 + Math.Cos(rot * 2) + Math.Sin(rot)) * sway * sway, 0),
                new Rectangle(Main.tile[i, j + 1].type != ModContent.TileType<VineOvergrow>() ? 32 : j % 2 * 16, 0, 16, 16), Lighting.GetColor(i, j));
            return false;
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Main.NewText("PENIS");
        }
    }

    class TallgrassOvergrow : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileCut[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                ModContent.TileType<GrassOvergrow>(),
                ModContent.TileType<VineOvergrow>()
            };
            TileObjectData.addTile(Type);
            soundType = 6;
            dustType = 14;
            AddMapEntry(new Color(202, 157, 49));
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/TallgrassOvergrowFlow"), new Rectangle(((i + (int)Helper.TileAdj.X) * 16) - (int)Main.screenPosition.X + 8, 
                ((j + (int)Helper.TileAdj.Y + 1) * 16) - (int)Main.screenPosition.Y + 2, 16, 16), new Rectangle((i % 2) * 16, 0, 16, 16), drawColor, (float)Math.Sin(LegendWorld.rottime + i % 6.28f) * 0.25f, 
                new Vector2(8, 16), 0, 0);
        }
    }
}

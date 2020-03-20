using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Herbology
{
    /*class HallowThistle : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileNoFail[Type] = true;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
				ModContent.TileType<Trellis>()
            };
        }
    }*/

    class ForestIvy : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileCut[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.RandomStyleRange = 3;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                ModContent.TileType<ForestIvy>(),
                ModContent.TileType<Planter>()
            };
            TileObjectData.addTile(Type);
            drop = mod.ItemType("Ivy");
        }

        public override void RandomUpdate(int i, int j)
        {
            if(Main.tile[i, j + 1].active() == false)
            {
                WorldGen.PlaceTile(i, j + 1, mod.TileType("ForestIvy"), true);
            }
        }
    }
    class Deathstalk : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.RandomStyleRange = 3;
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                ModContent.TileType<Soil>()               
            };
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
			{
                ModContent.TileType<Deathstalk>()
			};
            TileObjectData.addTile(Type);
            drop = mod.ItemType("Deathstalk");
        }

        public override void RandomUpdate(int i, int j)
        {
            if (Main.tile[i, j - 1].active() == false)
            {
                WorldGen.PlaceTile(i, j - 1, mod.TileType("Deathstalk"));
            }
        }
    }
	class GiantDeathweed : ModTile
	{
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoFail[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                ModContent.TileType<Soil>()
            };
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                TileID.PlanterBox
            };
            TileObjectData.addTile(Type);
        }
			
			public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
			{
				/*if (i % 2 == 1)
				{
					spriteEffects = SpriteEffects.FlipHorizontally;
				}*/
			}
			
			public override void RandomUpdate(int i, int j)
			{ 
				if (Main.tile[i, j].frameY == 0 || Main.tile[i, j].frameY == 32)
				{ 
					Main.tile[i, j].frameY += 32;
				} 
			}
			
			public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
			{
				Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
				
				Texture2D texture = mod.GetTexture("Tiles/Herbology/GiantDeathweed"); //Overlay

				Main.spriteBatch.Draw(texture, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(0, 0, texture.Width, texture.Height * Main.tile[i, j].frameY), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			}
	}
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Enums;



namespace StarlightRiver.Tiles
{
	
	
	
	
	class GiantDeathweed : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			//Main.tileNoFail[Type] = true; //crops usually have this
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            /*TileObjectData.newTile.AnchorValidTiles = new int[] //anchoring multitiles is ech
			{
				2,
				109,
			};
			TileObjectData.newTile.AnchorAlternateTiles = new int[]
			{
				78,
				TileID.PlanterBox
			};*/
            TileObjectData.addTile(Type);
		}

		/*public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects) //makes the sprite face a random direction
		{
			if (i % 2 == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
		}*/

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
			int stage = Main.tile[i, j].frameY / 44;
			if (stage == 2)
			{
				Item.NewItem(i * 16, j * 16, 0, 0, ModContent.ItemType<Items.Herbology.TestSeedItem>());
                Item.NewItem(i * 16, j * 16, 0, 0, ItemID.CopperOre, 2);//testing
			}
			else
			{
				Item.NewItem(i * 16, j * 16, 0, 0, ModContent.ItemType<Items.Herbology.TestSeedItem>());
			}
		}

		public override void RandomUpdate(int i, int j)//not quite sure if this is right, prob not.
		{ 
			if (Main.tile[i, j].frameY == 0 || Main.tile[i, j].frameY == 44)
			{ 
				Main.tile[i, j].frameY += 44; WorldGen.SquareTileFrame(i, j, true);
				NetMessage.SendTileSquare(-1, i, j, 2, TileChangeType.None); 
			} 
		}
		
		 
	}
}

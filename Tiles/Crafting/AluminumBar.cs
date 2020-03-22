using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;


namespace StarlightRiver.Tiles.Crafting
{
	public class AluminumBar : ModTile
	{	
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
			//dustType = 0;
			drop = ModContent.ItemType<Items.Crafting.AluminumBar>();
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Metal Bar");
			AddMapEntry(new Color(156, 172, 177), name);
		}
	}
}
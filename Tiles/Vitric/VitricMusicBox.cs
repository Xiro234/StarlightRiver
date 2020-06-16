using Microsoft.Xna.Framework;
using StarlightRiver.Items;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Vitric
{
	internal class VitricMusicBox : ModTile
	{
        public override void SetDefaults()
        { QuickBlock.QuickSetFurniture(this, 2, 2, ModContent.DustType<Dusts.Air>(), SoundID.Shatter, false, ItemType<VitricMusicBoxItem>(), new Color(200, 255, 255), false, false, "Music Box"); }

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = ItemType<VitricMusicBoxItem>();
		}
	}
    internal class VitricMusicBoxItem : QuickTileItem { public VitricMusicBoxItem() : base("Music Box (Vitric Desert)", "", ModContent.TileType<VitricMusicBox>(), 2) { } }
}
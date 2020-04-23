using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ObjectData;
using Terraria.Enums;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace StarlightRiver.Tiles.Vitric
{
    class VitricRock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileCut[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.RandomStyleRange = 6;
            TileObjectData.newTile.AnchorAlternateTiles = new int[]
            {
                ModContent.TileType<VitricSand>()
            };
            TileObjectData.addTile(Type);
            soundType = SoundID.Shatter;
            dustType = ModContent.DustType<Dusts.Glass2>();
            AddMapEntry(new Color(115, 182, 158));
        }
    }
}

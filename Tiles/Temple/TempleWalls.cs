using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using StarlightRiver.Items;

namespace StarlightRiver.Tiles.Temple
{
    class TempleWall : ModWall { public override void SetDefaults() { QuickBlock.QuickSetWall(this, DustID.Stone, SoundID.Dig, ModContent.ItemType<TempleWallItem>(), true, new Color(20, 20,  20)); } }
    class TempleWallItem : QuickWallItem { public TempleWallItem() : base("Ancient Temple Brick Wall", "", ModContent.WallType<TempleWall>(), ItemRarityID.White ) { } }

    class TempleWallBig : ModWall{ public override void SetDefaults(){ QuickBlock.QuickSetWall(this, DustID.Stone, SoundID.Dig, ModContent.ItemType<TempleWallBigItem>(), true, new Color(20, 20, 20)); } }
    class TempleWallBigItem : QuickWallItem { public TempleWallBigItem() : base("Large Ancient Temple Brick Wall", "", ModContent.WallType<TempleWallBig>(), ItemRarityID.White) { } }
}

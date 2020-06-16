using Microsoft.Xna.Framework;
using StarlightRiver.Items;
using StarlightRiver.Tiles.Vitric.Blocks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Vitric
{
    internal class AncientSandstone : ModTile
    { public override void SetDefaults() => QuickBlock.QuickSet(this, 200, DustID.Copper, SoundID.Tink, new Color(150, 105, 65), ItemType<AncientSandstoneItem>()); }

    public class AncientSandstoneItem : QuickTileItem { public AncientSandstoneItem() : base("Ancient Sandstone Brick", "", TileType<AncientSandstone>(), 0) { } }

    internal class AncientSandstonePlatform : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileBlockLight[Type] = false;
            minPick = 200;
            AddMapEntry(new Color(150, 105, 65));
        }
        
    }

    internal class AncientSandstoneWall : ModWall
    { public override void SetDefaults() => QuickBlock.QuickSetWall(this, DustID.Copper, SoundID.Dig, ItemType<AncientSandstoneWallItem>(), false, new Color(62, 68, 55)); }

    internal class AncientSandstoneWallItem : QuickWallItem { public AncientSandstoneWallItem() : base("Ancient Sandstone Wall", "", WallType<AncientSandstoneWall>(), 0) { } }

}
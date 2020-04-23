using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Purified
{
    class StonePure : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileStone[Type] = true;
            drop = ModContent.ItemType<Items.Pure.StonePureItem>();
            dustType = mod.DustType("Purify");
            AddMapEntry(new Color(208, 201, 199));
        }
    }
    class StonePure2 : StonePure
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            name = "StonePure2";
            texture = "StarlightRiver/Tiles/Purified/StonePure";
            return true;
        }
    }
}

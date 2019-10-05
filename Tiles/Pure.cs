using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles
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
            drop = mod.ItemType<Items.Pure.StonePureItem>();
            dustType = mod.DustType("Purify");
            AddMapEntry(new Color(255, 255, 255));
        }
    }
    class StonePure2 : StonePure
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            name = "StonePure2";
            texture = "StarlightRiver/Tiles/StonePure";
            return true;
        }
    }
}

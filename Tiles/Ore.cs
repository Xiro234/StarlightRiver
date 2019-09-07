using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spritersguildwip.Ability;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;

namespace spritersguildwip.Tiles
{
    class OreEbony : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileStone[Type] = true;
            drop = mod.ItemType("OreEbonyItem");
            AddMapEntry(new Color(80, 80, 90));
        }
    }
    class OreIvory : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileStone[Type] = true;
            drop = mod.ItemType("OreIvoryItem");
            AddMapEntry(new Color(245, 245, 230));
        }
    }
}

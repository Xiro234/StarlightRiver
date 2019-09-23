using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles
{
    public class VoidWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            dustType = mod.DustType("Darkness");
            drop = mod.ItemType("VoidWallItem");
            AddMapEntry(new Color(17, 22, 21));
        }
    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles
{
    internal class CheeseTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("CheeseTileItem");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Cheese");
            AddMapEntry(new Color(270, 270, 200), name);
        }
    }
}
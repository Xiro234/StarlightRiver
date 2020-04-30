using Microsoft.Xna.Framework;
using StarlightRiver.GUI;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles
{
	class CheeseTile : ModTile
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
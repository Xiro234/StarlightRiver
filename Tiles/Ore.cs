using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles
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

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Ebony");
            AddMapEntry(new Color(80, 80, 90), name);
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

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Ivory");
            AddMapEntry(new Color(245, 245, 230), name);
        }
    }
    class OreCelumite : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileStone[Type] = true;
            drop = mod.ItemType("OreCelumiteItem");

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Celumite");
            AddMapEntry(new Color(108, 134, 120), name);
        }
    }
}

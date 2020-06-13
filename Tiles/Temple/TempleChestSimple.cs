using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Temple
{
    class TempleChestSimple : LootChest
    {
        internal override List<Loot> GoldLootPool
        {
            get => new List<Loot>
            {
                new Loot(ModContent.ItemType<Items.Temple.TemplePick>(), 1),
                new Loot(ModContent.ItemType<Items.Temple.TempleSpear>(), 1),
                new Loot(ModContent.ItemType<Items.Temple.TempleRune>(), 1)
            };               
        }
        internal override List<Loot> SmallLootPool
        {
            get => new List<Loot>
            {
                new Loot(ItemID.LesserHealingPotion, 4, 8),
                new Loot(ItemID.LesserManaPotion, 3, 6),
                new Loot(ItemID.JestersArrow, 40, 60),
                new Loot(ItemID.SilverBullet, 20, 30),
                new Loot(ItemID.Dynamite, 2, 4),
                new Loot(ItemID.SpelunkerGlowstick, 15),
                new Loot(ModContent.ItemType<Items.Herbology.IvySeeds>(), 4, 8)
            };
        }
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.addTile(Type);
            dustType = DustID.GoldCoin;
            disableSmartCursor = true;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Ancient Chest");
            AddMapEntry(new Color(151, 151, 151), name);
        }

    }
}

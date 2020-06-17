﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Tiles.Hell
{
    class HellChest : LootChest
    {
        internal override List<Loot> GoldLootPool
        {
            get => new List<Loot>
            {
                new Loot(ItemType<Items.Temple.TemplePick>(), 1)
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
                new Loot(ItemType<Items.Herbology.IvySeeds>(), 5, 8)
            };
        }

        public override void SafeSetDefaults() => QuickBlock.QuickSetFurniture(this, 2, 2, DustID.AmberBolt, SoundID.Unlock, false, new Color(255, 255, 155));

        public override bool CanOpen(Player player)
        {
            for(int k = 0; k < player.inventory.Length; k++)
            {
                Item item = player.inventory[k];
                if (item.type == ItemType<Items.Hell.HellKey>())
                {
                    if (item.stack > 1) item.stack--;
                    else item.TurnToAir();
                    return true;
                }
            }
            return false;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = ItemType<Items.Hell.HellKey>();
        }

    }
}

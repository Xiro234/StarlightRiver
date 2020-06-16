﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.Core;
using System;

namespace StarlightRiver.Items.Overgrow
{
    internal class MossSalve : SmartAccessory
    {
        public MossSalve() : base("Moss Salve", "Health potions grant a short regeneration effect") { }

        public override void SafeSetDefaults()
        {
            item.rare = ItemRarityID.Green;
            item.value = 10000;
        }

        public override bool Autoload(ref string name)
        {
            StarlightItem.GetHealLifeEvent += HealMoss;
            return true;
        }

        private void HealMoss(Item item, Player player, bool quickHeal, ref int healValue)
        {
            if(item.potion && Equipped(player)) player.AddBuff(ModContent.BuffType<Buffs.MossRegen>(), 60 * 6);
        }
    }
}
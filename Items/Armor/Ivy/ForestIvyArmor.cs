﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace StarlightRiver.Items.Armor.ForestIvy
{
    [AutoloadEquip(EquipType.Head)]
    public class ForestIvyHead : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forest Ivy Helm");
            Tooltip.SetDefault("2% increased ranged critial strike change");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 2;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class ForestIvyChest : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forest Ivy Chestplate");
            Tooltip.SetDefault("2% increased ranged critial strike change");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 1;
            item.rare = ItemRarityID.Green;
            item.defense = 4;
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 2;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<ForestIvyHead>() && legs.type == ModContent.ItemType<ForestIvyLegs>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "After five (5) seconds of not taking damage, your next attack will ensnare and cause bleeding.";
            StarlightPlayer starlightPlayer = player.GetModPlayer<StarlightPlayer>();
            starlightPlayer.ivyArmorComplete = true;
        }
    }
    [AutoloadEquip(EquipType.Legs)]
    public class ForestIvyLegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forest Ivy Leggings");
            Tooltip.SetDefault("Slightly increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 1;
            item.rare = ItemRarityID.Green;
            item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.2f;
        }
    }
}
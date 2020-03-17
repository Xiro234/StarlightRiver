using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using StarlightRiver.Abilities;
using Microsoft.Xna.Framework;

namespace StarlightRiver.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class MechaHead : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mecha Helmet");
            Tooltip.SetDefault("Placeholder");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 1;
            item.rare = 3;
            item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {

        }

        public override void AddRecipes()
        {

        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class MechaChest : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mecha Chestplate");
            Tooltip.SetDefault("Placeholder");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 1;
            item.rare = 3;
            item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {

        }

        public override void UpdateArmorSet(Player player)
        {

        }

        public override void AddRecipes()
        {

        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class MechaLegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mecha Boots");
            Tooltip.SetDefault("Placeholder");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 1;
            item.rare = 3;
            item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {

        }

        public override void AddRecipes()
        {

        }
    }
}
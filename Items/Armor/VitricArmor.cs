using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VitricHead : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("vitric hat haha");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 1;
            item.rare = 1;
            item.defense = 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("VitricChest") && legs.type == mod.ItemType("VitricLegs");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "trollface.jpg";
            player.allDamage -= 0.2f;
            /* Here are the individual weapon class bonuses.
			player.meleeDamage -= 0.2f;
			player.thrownDamage -= 0.2f;
			player.rangedDamage -= 0.2f;
			player.magicDamage -= 0.2f;
			player.minionDamage -= 0.2f;
			*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Items.Vitric.VitricGem>(), 10);
            recipe.AddIngredient(mod.ItemType<Items.Vitric.Sandglass>(), 20);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
namespace StarlightRiver.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class VitricChest : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("vitric chest haha");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 1;
            item.rare = 1;
            item.defense = 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == mod.ItemType("VitricHead") && legs.type == mod.ItemType("VitricLegs");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "trollface.jpg";
            player.allDamage -= 0.2f;
            /* Here are the individual weapon class bonuses.
			player.meleeDamage -= 0.2f;
			player.thrownDamage -= 0.2f;
			player.rangedDamage -= 0.2f;
			player.magicDamage -= 0.2f;
			player.minionDamage -= 0.2f;
			*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Items.Vitric.VitricGem>(), 10);
            recipe.AddIngredient(mod.ItemType<Items.Vitric.Sandglass>(), 20);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
namespace StarlightRiver.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class VitricLegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("vitric legs haha");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 1;
            item.rare = 1;
            item.defense = 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == mod.ItemType("VitricHead") && body.type == mod.ItemType("VitricChest");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "trollface.jpg";
            player.allDamage -= 0.2f;
            /* Here are the individual weapon class bonuses.
			player.meleeDamage -= 0.2f;
			player.thrownDamage -= 0.2f;
			player.rangedDamage -= 0.2f;
			player.magicDamage -= 0.2f;
			player.minionDamage -= 0.2f;
			*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Items.Vitric.VitricGem>(), 10);
            recipe.AddIngredient(mod.ItemType<Items.Vitric.Sandglass>(), 20);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
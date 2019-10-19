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
    public class VitricHead : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased ranged damage");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 1;
            item.rare = 2;
            item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
			player.rangedDamage += 0.1f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("VitricChest") && legs.type == mod.ItemType("VitricLegs");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Gain up to 12 defense at high HP\nRelease glass shards at low HP";

            for (float k = 0.2f; k <= 0.8f; k += 0.2f)
            {
                if ((float)player.statLife / player.statLifeMax2 > k)
                {
                    player.statDefense += 3;


                }
                if ((float)player.statLife / player.statLifeMax2 < k)
                {
                    if (!Main.projectile.Any(projectile => projectile.type == ModContent.ProjectileType<Projectiles.WeaponProjectiles.VitricArmorProjectile>() && projectile.active && projectile.localAI[0] == (int)(k * 5) && projectile.owner == player.whoAmI))
                    {
                        int proj = Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.WeaponProjectiles.VitricArmorProjectile>(), 15, 0);
                        Main.projectile[proj].localAI[0] = (int)(k * 5);
                        Main.projectile[proj].owner = player.whoAmI;
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Items.Vitric.VitricGem>(), 10);
            recipe.AddIngredient(ModContent.ItemType<Items.Vitric.VitricSandItem>(), 20);
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
            Tooltip.SetDefault("5% increased ranged critical strike chance");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 1;
            item.rare = 2;
            item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 5;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Items.Vitric.VitricGem>(), 10);
            recipe.AddIngredient(ModContent.ItemType<Items.Vitric.VitricSandItem>(), 20);
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
            Tooltip.SetDefault("Slightly improved stamina regeneration");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 1;
            item.rare = 2;
            item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AbilityHandler>().staminaTickerMax -= 20;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Items.Vitric.VitricGem>(), 10);
            recipe.AddIngredient(ModContent.ItemType<Items.Vitric.VitricSandItem>(), 20);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
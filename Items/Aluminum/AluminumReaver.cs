﻿using Microsoft.Xna.Framework;
using StarlightRiver.Projectiles.WeaponProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Items.Aluminum
{
    internal class AluminumReaver : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Reaver");
            Tooltip.SetDefault("Occasionally zaps nearby enemies on use");
        }

        public override void SetDefaults()
        {
            item.damage = 15;
            item.melee = true;
            item.width = 38;
            item.height = 38;
            item.useTime = 14;
            item.useAnimation = 14;
            item.pick = 75;
            item.axe = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5f;
            item.value = Item.sellPrice(0, 0, 30, 0);
            item.rare = ItemRarityID.Blue;
            item.autoReuse = true;
            item.UseSound = SoundID.Item18;
            item.useTurn = true;
        }

        public override bool UseItem(Player player)
        {
            if (Main.rand.Next(10) == 0)
            {
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    NPC target = Main.npc[k];
                    if (target.active && Vector2.Distance(target.Center, player.Center) < 100)
                    {
                        Projectile.NewProjectile(target.Center, Vector2.Zero, ProjectileType<LightningNode>(), 20, 0, 0, 2, 100);
                        Helper.DrawElectricity(player.Center, target.Center, DustType<Dusts.Electric>());
                    }
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<AluminumBar>(), 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

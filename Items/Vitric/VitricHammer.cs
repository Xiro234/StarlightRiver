/*using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Items.Vitric
{
    class VitricHammer : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 10;
            item.melee = true;
            item.width = 60;
            item.height = 60;
            item.useTime = 15;
            item.useAnimation = 35;
            item.hammer = 65;
            item.useStyle = 1;
            item.knockBack = 10f;
            item.value = 1000;
            item.rare = 2;
            item.autoReuse = true;
            item.UseSound = SoundID.Item18;
            item.useTurn = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitreous Hammer");
            Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Items.Vitric.OverseerCore>());
            recipe.AddIngredient(mod.ItemType<Items.Vitric.VitricBar>(), 7);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }

}*/

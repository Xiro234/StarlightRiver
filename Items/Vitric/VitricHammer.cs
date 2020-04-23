using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Vitric
{
    class VitricHammer : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 28;
            item.melee = true;
            item.width = 30;
            item.height = 30;
            item.useTime = 30;
            item.useAnimation = 30;
            item.hammer = 75;
            item.useStyle = 1;
            item.knockBack = 6f;
            item.value = 1000;
            item.rare = 2;
            item.autoReuse = true;
            item.UseSound = SoundID.Item18;
            item.useTurn = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitric Hammer");
            Tooltip.SetDefault("Attracts dropped item to it's user");
        }

        public override bool UseItem(Player player)
        {
            foreach (Item item in Main.item.Where(item => Vector2.Distance(item.Center, player.Center) <= 120 && item.active))
            {
                item.velocity = Vector2.Normalize(item.Center - player.Center) * -2;
                Dust.NewDust(item.Center, 4, 4, ModContent.DustType<Dusts.Air>());
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FossilOre, 8);
            recipe.AddIngredient(ModContent.ItemType<Items.Vitric.VitricGem>(), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }

}

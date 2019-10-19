using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Crafting
{
    public class AluminumOre : QuickMaterial { public AluminumOre() : base("Astral Aluminum Chunk", "Smelt into bars at an Oven", 999, 500, 2) { } }
    public class AluminumBar : QuickMaterial
    {
        public AluminumBar() : base("Astral Aluminum Bar", "'Shimmering with Beautiful Light'", 999, 2000, 3) { }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<AluminumOre>(), 5);
            recipe.AddTile(ModContent.TileType<Tiles.Oven>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class Starlight : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fading starlight");
            Tooltip.SetDefault("A fading fragment of brilliance");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 9));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 14;
            item.maxStack = 999;
            item.value = 100;
            item.rare = 1;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, .25f, .45f, .45f);
            item.position.Y += (float)Math.Sin(LegendWorld.rottime) / 3;
        }
    }
}

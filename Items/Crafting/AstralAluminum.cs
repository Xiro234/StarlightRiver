using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Crafting
{
    public class AluminumOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Aluminum Fragments");
            Tooltip.SetDefault("They give off a faint glow");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(40, 5));
            ItemID.Sets.ItemNoGravity[item.type] = true;


        }
        public override void SetDefaults()
        {
            item.width = 11;
            item.height = 11;
            item.maxStack = 999;
            item.value = 100;
            item.rare = 1;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, .0f, .1f, .1f);
            item.position.Y += (float)Math.Sin(LegendWorld.rottime) / Main.rand.Next(1, 10);
            item.position.X += (float)Math.Sin(LegendWorld.rottime) / Main.rand.Next(1, 10);
            item.position.Y -= (float)Math.Sin(LegendWorld.rottime) / Main.rand.Next(1, 10);
            item.position.X -= (float)Math.Sin(LegendWorld.rottime) / Main.rand.Next(1, 10);
        }
    }
    public class AluminumBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Aluminum Bar");
            Tooltip.SetDefault("It shimmers with energy");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 999;
            item.value = 100;
            item.rare = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<AluminumOre>(), 5);
            recipe.AddIngredient(mod.ItemType<StarFragment>(), 1);
            recipe.AddTile(mod.TileType<Tiles.Oven2>());
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<StarFragment>(), 2);
            recipe.AddIngredient(ItemID.MeteoriteBar, 2);
            recipe.AddTile(mod.TileType<Tiles.Oven2>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class StarFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fading Starlight");
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

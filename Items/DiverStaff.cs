using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace spritersguildwip.Items
{
    public class DiverStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 34;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.useAnimation = 20;
            item.useTime = 20;
            item.shootSpeed = 6f;
            item.knockBack = 2f;
            item.damage = 18;
            item.shoot = mod.ProjectileType("Diver");
            item.rare = 2;
            item.noMelee = true;
            item.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Diver Staff");
            Tooltip.SetDefault("He Swim!");
        }
    }
}

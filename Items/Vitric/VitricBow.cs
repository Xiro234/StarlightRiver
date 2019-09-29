using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace StarlightRiver.Items.Vitric
{
    public class VitricBow : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 34;
            item.useStyle = 5;
            item.useAnimation = 28;
            item.useTime = 28;
            item.shootSpeed = 8f;
            item.shoot = 1;
            item.knockBack = 2f;
            item.damage = 12;
            item.useAmmo = AmmoID.Arrow;
            item.rare = 2;
            item.UseSound = SoundID.Item5;
            item.noMelee = true;
            item.ranged = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 aim = Vector2.Normalize(Main.MouseWorld - player.Center);
            Vector2 offset = aim.RotatedBy(Math.PI / 2) * 8f;
            Projectile.NewProjectile(player.Center + aim * 0.25f + offset, aim * 8, type, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(player.Center + aim * 0.25f - offset, aim * 8, type, damage, knockBack, player.whoAmI);
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitric Bow");
            Tooltip.SetDefault("Shoots two arrows at once");
        }
    }
}

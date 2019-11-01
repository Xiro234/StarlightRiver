using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using StarlightRiver.Projectiles.WeaponProjectiles;

namespace StarlightRiver.Items.Overgrow
{
    class VineWhip : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vine Whip");
        }
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 40;
            item.damage = 20;
            item.melee = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.rare = 4;
            item.useStyle = 5;
            item.useAnimation = 1;
            item.useTime = 1;
            item.channel = true;
            item.shoot = ModContent.ProjectileType<WhipSegment1>(); 
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int proj = Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, ModContent.ProjectileType<WhipSegment1>(), damage, 0, Main.myPlayer);
            WhipSegment1 whip = Main.projectile[proj].modProjectile as WhipSegment1;
            Main.projectile[proj].rotation = (Main.MouseWorld - player.Center).ToRotation();
            Main.projectile[proj].netUpdate = true;
            return true;
        }
    }
}

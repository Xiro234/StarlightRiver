using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.Projectiles.WeaponProjectiles;

namespace StarlightRiver.Items.Overgrow
{
    public class OvergrowBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Overgrown Bow");
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.ranged = true;
            item.width = 40;
            item.height = 20;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4;
            item.rare = 2;
            item.UseSound = SoundID.Item5;
            item.shoot = 10;
            item.shootSpeed = 14f;
            item.useAmmo = AmmoID.Arrow;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int proj = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<LeafSpawner>(), damage, knockBack, player.whoAmI);
            LeafSpawner spawner = Main.projectile[proj].modProjectile as LeafSpawner;
            spawner.proj = Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            return false;
        }
    }
}

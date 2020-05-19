using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace StarlightRiver.Items.Misc
{
    class Sandscript : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Scripts");
            Tooltip.SetDefault("Manifests a blade of sand");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.useStyle = 5;
            item.useAnimation = 43;
            item.useTime = 43;
            item.shootSpeed = 1f;
            item.knockBack = 7f;
            item.damage = 12;
            item.shoot = ModContent.ProjectileType<Projectiles.WeaponProjectiles.SandSlash>();
            item.rare = 1;
            item.noMelee = true;
            item.magic = true;
            item.mana = 10;

            item.UseSound = Terraria.ID.SoundID.Item45;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int i = Projectile.NewProjectile(player.Center, Vector2.Normalize(Main.MouseWorld - player.Center) * 25, type, damage, knockBack, player.whoAmI);
            Main.projectile[i].rotation = (Main.MouseWorld - player.Center).ToRotation();
            return false;
        }
    }
}

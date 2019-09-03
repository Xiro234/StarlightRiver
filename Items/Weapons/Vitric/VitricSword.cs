using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.Items.Weapons.Vitric
{
    class VitricSword : ModItem
    {
        bool Broken = false;
        public override void SetDefaults()
        {
            item.damage = 30;
            item.melee = true;
            item.width = 36;
            item.height = 38;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 1;
            item.knockBack = 0.5f;
            item.value = 1000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitreous Blade");
            Tooltip.SetDefault("Shatters into enchanted glass shards. \nUnable to attack while the blade is shattered.");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (!Broken)
            {
                Main.PlaySound(SoundID.Shatter);
                Projectile.NewProjectile(target.Center, Vector2.Normalize(player.Center - target.Center) * -24, mod.ProjectileType("VitricSwordProjectile"), 20, 0, player.whoAmI);
                Projectile.NewProjectile(target.Center, Vector2.Normalize(player.Center - target.Center).RotatedBy(0.3) * -13, mod.ProjectileType("VitricSwordProjectile"), 20, 0, player.whoAmI);
                Projectile.NewProjectile(target.Center, Vector2.Normalize(player.Center - target.Center).RotatedBy(-0.25) * -18, mod.ProjectileType("VitricSwordProjectile"), 20, 0, player.whoAmI);
                for (int k = 0; k <= 20; k++)
                {
                    Dust.NewDust(Vector2.Lerp(player.Center, target.Center, 0.4f), 8, 8, mod.DustType("Air"), (Vector2.Normalize(player.Center - target.Center) * -2).X, (Vector2.Normalize(player.Center - target.Center) * -2).Y);

                    float vel = Main.rand.Next(-300, -100) * 0.1f;
                    Dust.NewDust(Vector2.Lerp(player.Center, target.Center, 0.4f), 16, 16, mod.DustType("Glass"), (Vector2.Normalize(player.Center - target.Center) * vel).X, (Vector2.Normalize(player.Center - target.Center) * vel).Y);
                }
                Broken = true;
            }
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.projectile.Any(projectile => (projectile.type == mod.ProjectileType("VitricSwordProjectile") && projectile.owner == player.whoAmI && projectile.active)))
            {
                return false;
            }
            else
            {
                Broken = false;
            }
            return true;
            
        }
    }

    public class LayerHandler : ModPlayer
    {
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if (Main.projectile.Any(projectile => (projectile.type == mod.ProjectileType("VitricSwordProjectile") && projectile.owner == player.whoAmI && projectile.active)))
            {
                foreach(PlayerLayer layer in layers)
                {
                    if(layer == PlayerLayer.HeldItem)
                    {
                        layer.visible = false;
                    }
                }
            }
        }
    }
}

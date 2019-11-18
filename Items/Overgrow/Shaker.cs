using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace StarlightRiver.Items.Overgrow
{
    class Shaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Shaker");
        }

        public override void SetDefaults()
        {
            item.damage = 10;
            item.melee = true;
            item.width = 40;
            item.height = 20;
            item.useTime = 60;
            item.useAnimation = 1;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4;
            item.rare = 3;
        }

        public override bool CanUseItem(Player player)
        {
            return !Main.projectile.Any(proj => proj.type == ModContent.ProjectileType<Projectiles.WeaponProjectiles.ShakerBall>() && proj.active && Main.player[proj.owner] == player);
        }

        public override bool UseItem(Player player)
        {
            int proj = Projectile.NewProjectile(player.position + new Vector2(0, -30), Vector2.Zero, ModContent.ProjectileType<Projectiles.WeaponProjectiles.ShakerBall>(), item.damage, item.knockBack);
            player.channel = true;
            Main.projectile[proj].owner = player.whoAmI;
            return true;
        }

        public override void HoldItem(Player player)
        {
            if (player.channel)
            {
                player.velocity.X *= 0.9f;
                player.GetModPlayer<AnimationHandler>().Lifting = true;
            }
        }
    }

    public class AnimationHandler : ModPlayer
    {
        public bool Lifting = false;
        public override void PostUpdate()
        {
            if (Lifting)
            {
                player.bodyFrame = new Rectangle(0, 56 * 5, 40, 56);
            }
        }

        public override void ResetEffects()
        {
            Lifting = false;
        }
    }
}

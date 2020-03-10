
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.Projectiles.WeaponProjectiles;

namespace StarlightRiver.Items.Overgrow
{
    public class OvergrowStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Entanglement Rod");
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.magic = true;
            item.width = 40;
            item.height = 20;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4;
            item.rare = 2;
            item.shoot = ModContent.ProjectileType<EntangleThorn>();
            item.shootSpeed = 5;
        }
    }
}

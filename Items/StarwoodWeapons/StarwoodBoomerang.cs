using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.StarwoodWeapons
{
    public class StarwoodBoomerang : StarwoodItem
    {
        public StarwoodBoomerang() : base(ModContent.GetTexture("StarlightRiver/Items/StarwoodWeapons/StarwoodBoomerang_Alt")) { }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starwood Boomerang");
            Tooltip.SetDefault("Tooltip");
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.magic = true;
            item.width = 18;
            item.height = 34;
            item.useTime = 10;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shootSpeed = 10f;
            item.knockBack = 4f;
            item.UseSound = SoundID.Item19;
            item.shoot = ModContent.ProjectileType<Projectiles.WeaponProjectiles.StarwoodBoomerangProjectile>();
            item.useAnimation = 10;
            item.noMelee = true;
        }
    }
}
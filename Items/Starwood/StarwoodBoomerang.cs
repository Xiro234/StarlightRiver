using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Items.Starwood
{
    public class StarwoodBoomerang : StarwoodItem
    {
        public StarwoodBoomerang() : base(GetTexture("StarlightRiver/Items/Starwood/StarwoodBoomerang_Alt"))
        {
        }

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
            item.shoot = ProjectileType<Projectiles.WeaponProjectiles.StarwoodBoomerangProjectile>();
            item.useAnimation = 10;
        }
    }
}
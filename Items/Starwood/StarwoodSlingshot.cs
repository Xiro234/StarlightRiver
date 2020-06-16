using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Starwood
{
    public class StarwoodSlingshot : StarwoodItem
    {
        public StarwoodSlingshot() : base(ModContent.GetTexture("StarlightRiver/Items/Starwood/StarwoodSlingshot_Alt")) { }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starwood Slingshot");
            Tooltip.SetDefault("Shoots fallen stars \n5% chance to consume ammo");
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.magic = true;
            item.width = 18;
            item.height = 34;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;;
            item.knockBack = 4f;
            item.UseSound = SoundID.Item19;
            item.shoot = ModContent.ProjectileType<Projectiles.WeaponProjectiles.StarwoodSlingshotProjectile>();
            item.shootSpeed = 16f;
            item.useAmmo = ItemID.FallenStar;
        }

        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() >= .95f;
        }
    }
}
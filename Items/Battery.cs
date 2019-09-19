using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    public class Battery : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 34;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.useAnimation = 5;
            item.useTime = 5;
            item.shootSpeed = 2f;
            item.knockBack = 2f;
            item.damage = 50;
            item.UseSound = SoundID.Item43;
            item.shoot = mod.ProjectileType("Battery");
            item.rare = 2;
            item.noMelee = true;
            item.magic = true;
            item.autoReuse = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("piss battery");
            Tooltip.SetDefault("Rapidly shoots homing piss missiles");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(60));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }
    }
}

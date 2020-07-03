using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.Core;

namespace StarlightRiver.Items.StarwoodWeapons
{
    public class StarwoodStaff : StarwoodItem
    {
        public StarwoodStaff() : base(ModContent.GetTexture("StarlightRiver/Items/StarwoodWeapons/StarwoodSlingshot_Alt")) { }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starwood Staff");
            Tooltip.SetDefault("Yabba Dabba Doo");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.magic = true;
            item.width = 18;
            item.height = 34;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 4f;
            item.UseSound = SoundID.Item19;
            item.shoot = ModContent.ProjectileType<Projectiles.WeaponProjectiles.StarwoodStaffProjectile>();
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.autoReuse = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int amount = Main.player[player.whoAmI].GetModPlayer<StarlightPlayer>().Empowered ? 4 : 3;
            for (int k = 0; k < amount; k++)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedBy(Main.rand.NextFloat(-0.1f, 0.1f) * ((k * 0.15f) + 1)) * Main.rand.NextFloat(0.9f, 1.1f) * ((k * 0.15f) + 1), type, damage, knockBack, player.whoAmI, Main.rand.NextFloat(-0.025f, 0.025f));
            }
            return false;
        }

    }
}
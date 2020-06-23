using Microsoft.Xna.Framework;
using StarlightRiver.Trails;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Trails
{
    public class TrailTest3Staff : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Trail Test 3 Staff");
            Tooltip.SetDefault("S.");
        }


        public override void SetDefaults() {
            item.damage = 64;
            item.magic = true;
            item.mana = 11;
            item.width = 70;
            item.height = 70;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 120000;
            item.rare = 8;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<TrailTest3>();
            item.shootSpeed = 16f;
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            if(Main.myPlayer == player.whoAmI) {
                Vector2 mouse = Main.MouseWorld;

                for(int i = 0; i < 3; ++i) {
                    int p = Projectile.NewProjectile(mouse.X + Main.rand.Next(-80, 80), mouse.Y - 50 + Main.rand.Next(-10, 10), 0, Main.rand.Next(2, 4), type, damage, knockBack, player.whoAmI);
                }

            }
            return false;
        }

    }
}

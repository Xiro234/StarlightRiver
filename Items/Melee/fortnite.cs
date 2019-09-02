using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace spritersguildwip.Items.Melee
{
	public class fortnite : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 500;
			item.melee = true;
			item.width = 22;
			item.height = 25;
			item.useTime = 1;
			item.useAnimation = 6;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 270000;
			item.rare = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = 712;
			item.shootSpeed = 50f;
			item.useTurn = true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 10 + Main.rand.Next(3); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(10);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

        public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Bookban");
      Tooltip.SetDefault("Great for impersonating retards");
    }
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
			if (target.type != 488)
			{
				for (int i = 0; i <= 1; i++)
				{
					float sX = 2f;
					float sY = 2f;
					sX += (float)Main.rand.Next(-60, 61) * 0.2f;
					sY += (float)Main.rand.Next(-60, 61) * 0.2f;
					Projectile.NewProjectile(target.Center.X, target.Center.Y, sX, sY, ProjectileID.Bee, damage, knockback, player.whoAmI, 0f, 0f);
					Projectile.NewProjectile(target.Center.X, target.Center.Y, -sX, -sY, ProjectileID.Wasp, damage, knockback, player.whoAmI, 0f, 0f);
				}
			}
        }
	}
}

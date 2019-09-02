using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace spritersguildwip.Items.Melee
{
	public class VitricBlade : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 50;
			item.melee = true;
			item.width = 22;
			item.height = 25;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 270000;
			item.rare = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}

        public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Vitric Blade");
      Tooltip.SetDefault("Attacks release a shower of damaging crystals. \nRight-clicking will cause the blade to fragment. \nFragmenting the blade creates multiple shards that will return to the blade on impact. \nIt is impossible to attack while the blade is fragmented.");
    } //still tryna figure out how to get fragmentation working
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) //spawns projectiles on hit
        {
			if (target.type != 488)
			{
				for (int i = 0; i <= 1; i++)
				{
					float sX = 4f;
					float sY = 4f;
					sX += (float)Main.rand.Next(-60, 61) * 0.2f;
                    sY += (float)Main.rand.Next(-60, 61) * 0.2f;
                    sY += (float)Main.rand.Next(-60, 61) * 0.2f;
                    sY += (float)Main.rand.Next(-60, 61) * 0.2f;
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, sX, sY, ProjectileID.CrystalShard, damage, knockback, player.whoAmI, 0f, 0f); //using crystal shards and storms as a placeholder until i just make a custom version
					Projectile.NewProjectile(target.Center.X, target.Center.Y, -sX, -sY, ProjectileID.CrystalStorm, damage, knockback, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, sX, sY, ProjectileID.CrystalStorm, damage, knockback, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, -sX, -sY, ProjectileID.CrystalShard, damage, knockback, player.whoAmI, 0f, 0f);
                }
			}
        }
	}
}

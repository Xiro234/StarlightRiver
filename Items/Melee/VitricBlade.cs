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

            item.damage = 14;
            item.melee = true;
            item.width = 58;
            item.height = 58;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 2500;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitric Blade");
            Tooltip.SetDefault("Attacks release a shower of damaging crystals. \nRight-clicking will cause the blade to fragment. \nFragmenting the blade creates multiple shards that will return to the blade on impact. \nIt is impossible to attack while the blade is fragmented.");
        } //still tryna figure out how to get fragmentation working

        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("VitricShard")] != 0)
            {
              //  return false;
            }
            return true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            int rand = Main.rand.Next(2, 3);
            for (int i = 0; i <= rand; i++)
            {
                float sX = Main.rand.NextFloat(-8f, 8f) * i + 1;
                float sY = Main.rand.NextFloat(-8f, 8f) * i + 1;
                Vector2 velocity = new Vector2(sX, sY);
                Projectile projectile = Main.projectile[Projectile.NewProjectile(target.Center, velocity, mod.ProjectileType("VitricShard"), damage, knockback, player.whoAmI, 0f, 0f)];
                projectile.ai[1] = target.whoAmI;
            }
            if (target.type != 488)
            {
                for (int i = 0; i <= 4; i++)
                {
                    float sX = 4f;
                    float sY = 4f;
                    sX += (float)Main.rand.Next(-60, 61) * 0.2f;
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

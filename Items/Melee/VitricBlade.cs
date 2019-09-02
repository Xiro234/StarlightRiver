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
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitric Sword");
            Tooltip.SetDefault("Hitting an enemy causes the blade to shatter\nShattered parts of the blade home in on enemies and then return to you letting you attack again");
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("VitricShard")] != 0)
            {
                return false;
            }
            return true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            int rand = Main.rand.Next(2, 3);
            for (int i = 0; i <= rand; i++)
            {
                float sX = Main.rand.NextFloat(-4f, 4f) * i;
                float sY = Main.rand.NextFloat(-4f, 4f) * i;
                Vector2 velocity = new Vector2(sX, sY);
                Projectile.NewProjectile(player.Center, velocity, ProjectileID.CrystalShard, damage, knockback, player.whoAmI, 0f, 0f);
            }

        }
    }
}

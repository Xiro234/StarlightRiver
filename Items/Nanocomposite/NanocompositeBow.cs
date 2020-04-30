using Microsoft.Xna.Framework;
using StarlightRiver.Projectiles.WeaponProjectiles.Nanocomposite;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Nanocomposite
{
    public class NanocompositeBow : ModItem
    {
        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 32;
            item.useTime = 6;
            item.reuseDelay = 6;
            item.knockBack = 4f;
            item.width = 60;
            item.height = 30;
            item.damage = 92;
            item.rare = 7;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.noMelee = true;
            item.autoReuse = true;
            item.ranged = true;
            item.shoot = 10;
            item.shootSpeed = 12f;
            item.UseSound = SoundID.Item75;
            item.useAmmo = AmmoID.Arrow;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nanocomposite Bow");
            Tooltip.SetDefault("It go like PEW PEW PEW PEW PEW PEW");
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            NPC target = Main.npc[0];
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            for (int k = 0; k < Main.npc.Length; k++)
            {
                if (Vector2.Distance(Main.npc[k].Center, new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y)) < Vector2.Distance(target.Center, new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y)) && Main.npc[k].active && !Main.npc[k].friendly)
                {
                    target = Main.npc[k];
                }
            }

            Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(80)), ModContent.ProjectileType<NanocompositeArrow>(), damage, knockBack, player.whoAmI, target.whoAmI);
            return false;
        }
    }
}
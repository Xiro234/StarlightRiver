using Microsoft.Xna.Framework;
using StarlightRiver.Projectiles.WeaponProjectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Misc
{
    public class ThrowawayJoke : ModItem
    {
        public int ammo = 30;
        public int cooldown = 120;
        public bool crit = false;
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 6;
            item.useTime = 6;
            item.knockBack = 2f;
            item.width = 60;
            item.height = 30;
            item.damage = 11;
            item.rare = ItemRarityID.LightRed;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.noMelee = true;
            item.autoReuse = true;
            item.ranged = true;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 20f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Throwaway Joke");
            Tooltip.SetDefault("The thing goes like pewpewpewpewpewpewpewpewpewpewpewpew\nand then you throw it\nif you smack somethings ass with it you get to go pewpewpewpewpewpewpewpewpewpew again \nalso if you hit somethign and its a crit you get like have more ammo\nbut if you dont you gotta wait to go pewpewpewpewpewpewpewpewpewpew again\nit shoot faster the less ammo it has out of the total of the ammo in the magazine");
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-30, 0);
        }
        public override float UseTimeMultiplier(Player player)
        {
            return 1f + ((crit ? 60 : 30) - ammo) / 30f;
        }
        public override bool CanUseItem(Player player)
        {
            if (ammo <= 1) //one less cause this method dumb
            {
                item.autoReuse = false;
                item.noUseGraphic = true;
            }
            else
            {
                item.autoReuse = true;
                item.noUseGraphic = false;
            }
            return ammo > 0f;
        }
        public void reload(bool crit)
        {
            cooldown = 120;
            this.crit = crit;
            ammo = crit ? 60 : 30;
            Main.PlaySound(SoundID.Item, -1, -1, 99, 1);
            Main.PlaySound(SoundID.Item, -1, -1, 98, 1);
        }
        public override void HoldItem(Player player)
        {
            if (ammo <= 0)
            {
                cooldown--;
                if (cooldown <= 0)
                {
                    reload(false);
                }
            }
            base.HoldItem(player);
        }
        public override bool ConsumeAmmo(Player player)
        {
            return ammo <= 0;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            ammo--;
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            if (ammo <= 0)
            {
                Main.PlaySound(SoundID.Item, -1, -1, 1, 1);
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), ModContent.ProjectileType<ThrowawayJokeProjectile>(), damage * 8, knockBack, player.whoAmI);
                return false;
            }
            else
            {
                Main.PlaySound(SoundID.Item, -1, -1, 11, 1);
            }
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(6));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }
    }
}

using StarlightRiver.Projectiles.WeaponProjectiles;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.StormCorrupt
{
    public class CursedRondure : ModItem
    {
        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 24;
            item.useTime = 24;
            item.shootSpeed = 10f;
            item.knockBack = 2f;
            item.width = 26;
            item.height = 24;
            item.damage = 72;
            item.rare = 4;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.noMelee = true;
            item.autoReuse = true;
            item.melee = true;
            item.channel = true;
            item.shoot = ModContent.ProjectileType<CursedRondureProjectile>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed rendure");
            Tooltip.SetDefault("It go like whoom whoom and you like shoot fire projectiles\nAnd like, if you hit an enemy with th efire projectile and like\nIt will give birth and like, its child will home in on like, not the thing you hit\nIf the target of the child dies it goes back to the one hit by its parent\nThat also happens like when it takes too long to find anyone");
        }
    }
}

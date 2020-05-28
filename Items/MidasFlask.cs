using StarlightRiver.Projectiles.WeaponProjectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    public class MidasFlask : ModItem
    {
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 28;
            item.useTime = 28;
            item.maxStack = 999;
            item.width = 60;
            item.height = 30;
            item.rare = ItemRarityID.LightRed;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.noMelee = true;
            item.autoReuse = false;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item106;
            item.consumable = true;
            item.shoot = ModContent.ProjectileType<MidasFlaskProjectile>();
            item.shootSpeed = 8f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midas' Flask");
            Tooltip.SetDefault("You throw the thing\nAnd it like hits an enemy maybę?\nAnd if it do it like, makes AOE and like anything inside becomes pure fucking gold\nPure fucking gold enemies drop 2 times as much gold and like, are tanky-ier");
        }
    }
}

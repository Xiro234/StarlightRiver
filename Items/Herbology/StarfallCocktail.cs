using StarlightRiver.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{
    public class StarfallCocktail : ModItem
    {
        public override void SetDefaults()
        {
            item.useStyle = 2;
            item.useAnimation = 28;
            item.useTime = 28;
            item.maxStack = 999;
            item.width = 20;
            item.height = 32;
            item.rare = 4;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.noMelee = true;
            item.autoReuse = false;
            item.UseSound = SoundID.Item3;
            item.consumable = true;
            item.buffTime = 36000;
            item.buffType = ModContent.BuffType<StarfallCocktailBuff>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starfall COCKtail");
            Tooltip.SetDefault("Get high on star juice");
        }
    }
}

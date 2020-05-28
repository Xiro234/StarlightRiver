using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    public class Garlic : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 34;
            item.useStyle = ItemUseStyleID.HoldingOut;
            Item.staff[item.type] = true;
            item.useAnimation = 5;
            item.useTime = 5;
            item.shootSpeed = 3f;
            item.knockBack = 2f;
            item.damage = 18;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/WhipAndNaenae");
            item.rare = ItemRarityID.Cyan;
            item.noMelee = true;
            item.magic = true;
            item.autoReuse = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Garlic");
            Tooltip.SetDefault("Garlic");
        }
    }
}

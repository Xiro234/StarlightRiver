using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Overgrow
{
    internal class MossSalve : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moss Salve");
            Tooltip.SetDefault("Health potions grant a short regeneration effect");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = ItemRarityID.Green;
            item.value = 10000;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
        }
    }

    internal class OnHealItem : GlobalItem
    {
        public override void OnConsumeItem(Item item, Player player)
        {
            if (item.healLife > 0 && item.potion)
            {
                if (Helper.HasEquipped(player, ModContent.ItemType<MossSalve>())) player.AddBuff(ModContent.BuffType<Buffs.MossRegen>(), 60 * 6);
            }
        }
    }
}
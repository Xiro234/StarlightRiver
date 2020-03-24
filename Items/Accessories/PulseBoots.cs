using StarlightRiver.Abilities;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Accessories
{
	[AutoloadEquip(EquipType.Shoes)]
    public class PulseBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Pulse Boots");
            DisplayName.SetDefault("Unimplimented Function");
        }

        public override void SetDefaults()
        {
            item.width = 21;
            item.height = 19;
            item.accessory = true;
        }

        public override void PostUpdate()
        {
            base.PostUpdate();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
        }
        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
        }
    }
}
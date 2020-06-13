using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.CursedAccessories
{
    internal class AnthemDagger : CursedAccessory
    {
        public AnthemDagger() : base(ModContent.GetTexture("StarlightRiver/Items/CursedAccessories/AnthemDaggerGlow")) { }

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Consume mana to absorb damage\n90% Reduced defense");
            DisplayName.SetDefault("Anthem Dagger");
        }

        public override void SafeUpdateEquip(Player player)
        {
            player.statDefense /= 10;
            player.manaFlower = false;
        }
    }
}
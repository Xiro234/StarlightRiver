using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Overgrow
{
    class ExecutionersAxe : ModItem
    {
        public override string Texture => "StarlightRiver/MarioCumming";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Executioner's Axe");
        }
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 40;
            item.damage = 20;
            item.melee = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.rare = 4;
            item.useStyle = 5;
            item.useAnimation = 30;
            item.useTime = 30;
            item.channel = true;
        }

        public override bool CanUseItem(Player player)
        {
            return true;
        }
    }
}

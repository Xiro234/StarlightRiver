using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Accessories
{
    public class MirageBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mirage Boots");
            Tooltip.SetDefault("Allows extended flight" + "\nUnimplimented Function");
        }

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Orange;
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.rocketBoots = 4;
            player.rocketTimeMax = 10;
            if (player.rocketFrame)
            {
            }
            //if (player.velocity.Y == 0) player.rocketTime = 600;
            //Main.NewText("Rocket Power: " + player.rocketTime + "/" + player.rocketTimeMax);
        }
    }
}
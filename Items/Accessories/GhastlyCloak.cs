using StarlightRiver.Buffs;
using StarlightRiver.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Accessories
{
    public class GhastlyCloak : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ghastly Cloak");
            Tooltip.SetDefault("Avoiding damage cloaks you, increasing most stats");
        }

        public override void UpdateEquip(Player player)
        {
            StarlightPlayer modplayer = player.GetModPlayer<StarlightPlayer>();
            if (modplayer.Timer - modplayer.LastHit >= 1200)
            {
                if (!player.HasBuff(ModContent.BuffType<GhastlyCloakBuff>())) //activation thing
                {
                    Main.PlaySound(SoundID.Item123, player.position);
                    for (int i = 0; i <= 30; i++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, 62);
                    }
                }
                player.AddBuff(ModContent.BuffType<GhastlyCloakBuff>(), 2, false);
            }
            base.UpdateEquip(player);
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.accessory = true;
        }
    }
}
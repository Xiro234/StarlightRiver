using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.IceandFire
{
    public class FireBreath : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.useStyle = 1;
            item.useAnimation = 12;
            item.useTime = 6;
            item.autoReuse = true;
            item.useTurn = false;
            item.damage = 10;
            item.rare = 2;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.magic = true;

            item.UseSound = SoundID.Item45;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon's breath");
            Tooltip.SetDefault("Hot");
        }

        public override bool CanUseItem(Player player)
        {
            return player.velocity.Y == 0;
        }

        public override bool UseItem(Player player)
        {
            player.channel = true;

            for (int k = 0; k <= 20; k++)
            {
                Dust.NewDustPerfect(player.Center + new Vector2(6 * player.direction, -8), ModContent.DustType<Dusts.DragonFire>(),
                    new Vector2(player.direction, 0.15f).RotatedBy(Main.rand.NextFloat(-0.3f, 0.3f)) * Main.rand.NextFloat(12), 0, default, 3);
            }
            return true;
        }

        public override void HoldItem(Player player)
        {
            if (player.channel)
            {
                player.jump = 2;
                player.velocity.X *= 0.8f;
            }

        }
    }
}

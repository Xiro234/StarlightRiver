using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Effects
{
    public class VertexTest : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 34;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 28;
            item.useTime = 28;
            item.shootSpeed = 8f;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.knockBack = 2f;
            item.damage = 12;
            item.useAmmo = AmmoID.Arrow;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item5;
            item.noMelee = true;
            item.ranged = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 aim = Vector2.Normalize(Main.MouseWorld - player.Center);
            return true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vertex Strip Test");
            Tooltip.SetDefault("strip, now");
        }
    }
}
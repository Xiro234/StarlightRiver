using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    public class StarwoodBoomerang : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starwood Boomerang");
            Tooltip.SetDefault("Tooltip");
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.magic = true;
            item.width = 18;
            item.height = 34;
            item.useTime = 10;
            item.useAnimation = 10;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shootSpeed = 10f;
            item.knockBack = 4f;
            item.UseSound = SoundID.Item19;
            item.shoot = ModContent.ProjectileType<Projectiles.WeaponProjectiles.StarwoodBoomerangProjectile>();
        }

        private Texture2D MainTexture => ModContent.GetTexture("StarlightRiver/Items/StarwoodBoomerangFull");

        private int itemFrame = 0;

        public override void UpdateInventory(Player player)
        {
            if (player.GetModPlayer<StarlightPlayer>().Empowered) { itemFrame = 1; }
            else { itemFrame = 0; }
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            spriteBatch.Draw(MainTexture, position, new Rectangle(0, (MainTexture.Height / 2) * itemFrame, MainTexture.Width, MainTexture.Height / 2), drawColor, default, origin, scale, default, default);
            return false;
        }

        //public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        //{
        //    spriteBatch.Draw(MainTexture, item.position - Main.screenPosition, new Rectangle(0, (MainTexture.Height / 2) * 1, MainTexture.Width, MainTexture.Height / 2), lightColor, rotation, default, scale, default, default);
        //    return false;
        //}//disabled because it should always use the normal sprite on the ground


    }
}
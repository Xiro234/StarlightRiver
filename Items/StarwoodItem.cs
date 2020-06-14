using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    public abstract class StarwoodItem : ModItem
    {
        protected Texture2D EmpoweredTexture;
        public StarwoodItem(Texture2D AltTexture)
        {
            EmpoweredTexture = AltTexture;
        }

        protected bool isEmpowered;
        public override void UpdateInventory(Player player)
        {
            isEmpowered = player.GetModPlayer<StarlightPlayer>().Empowered;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (isEmpowered)
            {
                spriteBatch.Draw(EmpoweredTexture, position, frame, drawColor, default, origin, scale, default, default);
            }            
            return !isEmpowered;
        }

        //public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        //{
        //    spriteBatch.Draw(MainTexture, item.position - Main.screenPosition, new Rectangle(0, (MainTexture.Height / 2) * 1, MainTexture.Width, MainTexture.Height / 2), lightColor, rotation, default, scale, default, default);
        //    return false;
        //}//disabled because it should always use the normal sprite on the ground


    }
}
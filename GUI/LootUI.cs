using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace StarlightRiver.GUI
{
    public class LootUI : UIState
    {
        public static bool Visible = false;
        private Item BigItem = new Item();
        internal Item[] Selections = new Item[2];

        public override void OnInitialize()
        {
            base.OnInitialize();
        }

    }
    class LootSelection : UIElement
    {
        internal Item Item;
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.GetTexture("StarlightRiver/GUI/CookSlotY");

            spriteBatch.Draw(tex, GetDimensions().Position(), tex.Frame(), Color.White, 0, Vector2.Zero, 1, 0, 0);
            if (!Item.IsAir)
            {
                Texture2D tex2 = Item.type > ItemID.Count ? ModContent.GetTexture(Item.modItem.Texture) : ModContent.GetTexture("Terraria/Item_" + Item.type);
                spriteBatch.Draw(tex2, new Rectangle((int)GetDimensions().X + 16, (int)GetDimensions().Y + 16, 28, 28), tex2.Frame(), Color.White);
                if (Item.stack > 1) spriteBatch.DrawString(Main.fontItemStack, Item.stack.ToString(), GetDimensions().Position() + Vector2.One * 28, Color.White);
            }
        }
    }
}

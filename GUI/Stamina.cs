using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;
using spritersguildwip.Ability;

namespace spritersguildwip.GUI
{
    public class Stamina : UIState
    {
        public UIPanel abicon;
        public static bool visible = false;
        public static int stamina = 0;
        

        UIImage Stam1 = new UIImage(ModContent.GetTexture("spritersguildwip/GUI/Stamina"));
        UIImage Stam2 = new UIImage(ModContent.GetTexture("spritersguildwip/GUI/Stamina"));
        UIImage Stam3 = new UIImage(ModContent.GetTexture("spritersguildwip/GUI/Stamina"));


        public override void OnInitialize()
        {          
            Stam1.Left.Set(-303, 1);
            Stam1.Top.Set(110, 0);
            Stam1.Width.Set(22, 0f);
            Stam1.Height.Set(22, 0f);
            base.Append(Stam1);

            Stam2.Left.Set(0, 0);
            Stam2.Top.Set(26, 0);
            Stam2.Width.Set(22, 0f);
            Stam2.Height.Set(22, 0f);
            Stam1.Append(Stam2);

            Stam3.Left.Set(0, 0);
            Stam3.Top.Set(26, 0);
            Stam3.Width.Set(22, 0f);
            Stam3.Height.Set(22, 0f);
            Stam2.Append(Stam3);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

            if (mp.stamina >= 1)
            {
                Stam1.ImageScale = 1;
            }
            else
            {
                Stam1.ImageScale = 0.5f;
            }

            if (mp.stamina >= 2)
            {
                Stam2.ImageScale = 1;
            }
            else
            {
                Stam2.ImageScale = 0.5f;
            }

            if (mp.stamina >= 3)
            {
                Stam3.ImageScale = 1;
            }
            else
            {
                Stam3.ImageScale = 0.5f;
            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Recalculate();
        }
    }
}
  
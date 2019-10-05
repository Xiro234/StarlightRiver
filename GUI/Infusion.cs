using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;
using StarlightRiver.Abilities;
using System.Collections.Generic;

namespace StarlightRiver.GUI
{
    public class Infusion : UIState
    {
        public Circle circle1 = new Circle(ModContent.GetTexture("StarlightRiver/GUI/Circle1"));
        public Circle circle2 = new Circle(ModContent.GetTexture("StarlightRiver/GUI/Circle2"));
        public Circle circle3 = new Circle(ModContent.GetTexture("StarlightRiver/GUI/Circle3"));
        public Circle circle4 = new Circle(ModContent.GetTexture("StarlightRiver/GUI/Circle4"));
        public Circle circle5 = new Circle(ModContent.GetTexture("StarlightRiver/GUI/Circle5"));

        public UIImageButton none = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/blank2"));

        public UIImageButton wind1 = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/blank"));
        public UIImageButton wind2 = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/blank"));
        public UIImageButton wisp1 = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/blank"));
        public UIImageButton wisp2 = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/blank"));

        public UIText Name = new UIText("", 1);
        public UIText Line1 = new UIText("", 0.75f);

        public static bool visible = false;
        public static int timer = 0;

        public override void OnInitialize()
        {
            Name.Left.Set(Name.Width.GetValue(0) / -2, 0.5f);
            Name.Top.Set(-240, 0.5f);
            base.Append(Name);

            Line1.Left.Set(Line1.Width.GetValue(0) / -2, 0.5f);
            Line1.Top.Set(-220, 0.5f);
            base.Append(Line1);


            none.Left.Set(0 - 16, 0.5f);
            none.Top.Set(-128 - 16, 0.5f);
            none.Width.Set(32, 0);
            none.Height.Set(32, 0);
            none.OnClick += new MouseEvent(Select);
            base.Append(none);

            wind1.Left.Set(69 - 16, 0.5f);
            wind1.Top.Set(-107 - 16, 0.5f);
            wind1.Width.Set(32, 0);
            wind1.Height.Set(32, 0);
            wind1.OnClick += new MouseEvent(Select);
            base.Append(wind1);

            wind2.Left.Set(116 - 16, 0.5f);
            wind2.Top.Set(-53 - 16, 0.5f);
            wind2.Width.Set(32, 0);
            wind2.Height.Set(32, 0);
            wind2.OnClick += new MouseEvent(Select);
            base.Append(wind2);

            wisp1.Left.Set(126 - 16, 0.5f);
            wisp1.Top.Set(18 - 16, 0.5f);
            wisp1.Width.Set(32, 0);
            wisp1.Height.Set(32, 0);
            //wisp1.OnClick += new MouseEvent(Select);
            base.Append(wisp1);

            wisp2.Left.Set(97 - 16, 0.5f);
            wisp2.Top.Set(84 - 16, 0.5f);
            wisp2.Width.Set(32, 0);
            wisp2.Height.Set(32, 0);
            //wisp2.OnClick += new MouseEvent(Select);
            base.Append(wisp2);


            circle1.Left.Set(0, 0.5f);
            circle1.Top.Set(0, 0.5f);
            circle1.Width.Set(0, 0);
            circle1.Height.Set(0, 0);

            circle2.Left.Set(0, 0);
            circle2.Top.Set(0, 0);
            circle2.Width.Set(400, 0);
            circle2.Height.Set(400, 0);
            circle1.Append(circle2);

            circle3.Left.Set(0, 0);
            circle3.Top.Set(0, 0);
            circle3.Width.Set(400, 0);
            circle3.Height.Set(400, 0);
            circle1.Append(circle3);

            circle4.Left.Set(0, 0);
            circle4.Top.Set(0, 0);
            circle4.Width.Set(400, 0);
            circle4.Height.Set(400, 0);
            circle1.Append(circle4);

            circle5.Left.Set(0, 0);
            circle5.Top.Set(0, 0);
            circle5.Width.Set(400, 0);
            circle5.Height.Set(400, 0);
            circle1.Append(circle5);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            //Comet Dash
            if (mp.upgradeUnlock[0] == 1 && mp.upgrade[0] != 1 && mp.upgrade[1] != 1) { wind1.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Wind2")); } //unlocked texture
            else if (mp.upgrade[0] == 1 || mp.upgrade[1] == 1) { wind1.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Wind2Bad")); } //in use texture
            else { wind1.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Wind2Lock")); }//locked texture
            //Poop
            if (mp.upgradeUnlock[1] == 1 && mp.upgrade[0] != 2 && mp.upgrade[1] != 2) { wind2.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Wind3")); }
            else if (mp.upgrade[0] == 2 || mp.upgrade[1] == 2) { wind2.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Wind3Bad")); }
            else { wind2.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Wind3Lock")); }

            //Text--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            Name.Left.Set(Name.Text.Length * 6 / -2, 0.5f);
            Line1.Left.Set(Line1.Text.Length * 5 / -2, 0.5f);

            if (none.IsMouseHovering) { Name.SetText("Unequip"); Line1.SetText("Unequip an infusion"); }//unequip

            //if----(ability hovered----------unlocked----------------) { Name is name of ability-----Description of ability}
            else if (wind1.IsMouseHovering && mp.upgradeUnlock[0] == 1) { Name.SetText("Comet Dash"); Line1.SetText("Dash farther"); }//comet dash
            else if (wind2.IsMouseHovering && mp.upgradeUnlock[1] == 1) { Name.SetText("Flaming Slash"); Line1.SetText("Deal up to 200 damage on dash"); }//poop

            else { Name.SetText(""); Line1.SetText(""); }

            //Circle Textures------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            if (mp.unlock[0] == 1) { circle1.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Circle1")); } else { circle1.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Circle1Off")); }
            if (mp.unlock[1] == 1) { circle2.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Circle2")); } else { circle2.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Circle2Off")); }
            if (mp.unlock[2] == 1) { circle3.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Circle3")); } else { circle3.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Circle3Off")); }
            if (mp.unlock[3] == 1) { circle4.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Circle4")); } else { circle4.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Circle4Off")); }
            if (mp.unlock[4] == 1) { circle5.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Circle5")); } else { circle5.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Circle5Off")); }

            //Animation-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            if(timer == 0)
            {
                none.Remove();
                wind1.Remove();
                wind2.Remove();
                wisp1.Remove();
                wisp2.Remove();

                circle1.Remove();
            }
            if(timer > 1)
            {
                base.Append(none);
                base.Append(wind1);
                base.Append(wind2);
                base.Append(wisp1);
                base.Append(wisp2);

                base.Append(circle1);
            }
            if (visible && timer < 15)
            {
                timer++;

                //circle
                int scale = (int)(timer / 15f * 400f);
                circle1.Width.Set(scale, 0); circle1.Height.Set(scale, 0); circle1.Left.Set(-scale / 2, 0.5f); circle1.Top.Set(-scale / 2, 0.5f);

                //icons
                float scale2 = (timer / 15f * 116f);
                float angle(int ticks){ return ((float)(Math.PI / 5.5f * (ticks + (-5.5f + timer/15f * 5.5f)))); }

                none.Left.Set((int)(Math.Sin(angle(0)) * scale2) - 16, 0.5f); none.Top.Set((int)(Math.Cos(angle(0)) * -scale2) - 16, 0.5f);
                wind1.Left.Set((int)(Math.Sin(angle(1)) * scale2) - 16, 0.5f); wind1.Top.Set((int)(Math.Cos(angle(1)) * -scale2) - 16, 0.5f);
                wind2.Left.Set((int)(Math.Sin(angle(2)) * scale2) - 16, 0.5f); wind2.Top.Set((int)(Math.Cos(angle(2)) * -scale2) - 16, 0.5f);
                wisp1.Left.Set((int)(Math.Sin(angle(3)) * scale2) - 16, 0.5f); wisp1.Top.Set((int)(Math.Cos(angle(3)) * -scale2) - 16, 0.5f);
                wisp2.Left.Set((int)(Math.Sin(angle(4)) * scale2) - 16, 0.5f); wisp2.Top.Set((int)(Math.Cos(angle(4)) * -scale2) - 16, 0.5f);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Recalculate();
        }


        

        public static int slot = 0;
        private void Select(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            /*  Template:
             *  
             * (listeningElement == A && mp.upgradeUnlock[B] == 1 && mp.upgrade[0] != C && mp.upgrade[1] != C) { mp.upgrade[slot] = C; }
             * 
             * A = UI element to check if clicked
             * B = index of that upgrade in the upgradeUnlocks array (see AbilityHandler.cs)
             * c = the ID of the ability to set the correct index in the upgrade array to
             * 
             * upgrade IDs:
             * 1: Comet Dash
             * 2: Poop
             */

            // (the UI element clicked ------If upgrade is unlocked------not already picked in either slot-------) {Set the upgrade slot selected}
            if (listeningElement == wind1 && mp.upgradeUnlock[0] == 1 && mp.upgrade[0] != 1 && mp.upgrade[1] != 1) { mp.upgrade[slot] = 1; visible = false; } //Comet Dash
            if (listeningElement == wind2 && mp.upgradeUnlock[1] == 1 && mp.upgrade[0] != 2 && mp.upgrade[1] != 2) { mp.upgrade[slot] = 2; visible = false; } //Poop

            //Special case, unequip button
            if (listeningElement == none) { mp.upgrade[slot] = 0; visible = false; } //Unequip
            
        }
    }
    public class Circle : UIElement
    {
        Texture2D Texture;
        public Circle(Texture2D texture)
        {
            Texture = texture;
        }
        public void SetImage(Texture2D texture)
        {
            Texture = texture;
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(Texture, new Rectangle((int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height), Color.White);
        }
    }
}
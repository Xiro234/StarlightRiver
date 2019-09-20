using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;
using StarlightRiver.Ability;
using System.Collections.Generic;

namespace StarlightRiver.GUI
{
    public class Infusion : UIState
    {
        public UIImage back = new UIImage(ModContent.GetTexture("StarlightRiver/GUI/back"));

        public UIImageButton none = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/blank"));

        public UIImageButton wind1 = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/blank"));
        public UIImageButton wind2 = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/blank"));
        public UIImageButton wisp1 = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/blank"));
        public UIImageButton wisp2 = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/blank"));

        public UIText Name = new UIText("", 1);
        public UIText Line1 = new UIText("", 0.75f);

        public static bool visible = false;

        public override void OnInitialize()
        {
            Name.Left.Set(Name.Width.GetValue(0) / -2, 0.5f);
            Name.Top.Set(-170, 0.5f);
            base.Append(Name);

            Line1.Left.Set(Line1.Width.GetValue(0) / -2, 0.5f);
            Line1.Top.Set(-150, 0.5f);
            base.Append(Line1);

            none.Left.Set(0, 0.5f);
            none.Top.Set(-128, 0.5f);
            none.Width.Set(32, 0);
            none.Height.Set(32, 0);
            none.OnClick += new MouseEvent(Select);
            base.Append(none);

            wind1.Left.Set(69, 0.5f);
            wind1.Top.Set(-107, 0.5f);
            wind1.Width.Set(32, 0);
            wind1.Height.Set(32, 0);
            wind1.OnClick += new MouseEvent(Select);
            base.Append(wind1);

            wind2.Left.Set(116, 0.5f);
            wind2.Top.Set(-53, 0.5f);
            wind2.Width.Set(32, 0);
            wind2.Height.Set(32, 0);
            wind2.OnClick += new MouseEvent(Select);
            base.Append(wind2);

            wisp1.Left.Set(126, 0.5f);
            wisp1.Top.Set(18, 0.5f);
            wisp1.Width.Set(32, 0);
            wisp1.Height.Set(32, 0);
            //wisp1.OnClick += new MouseEvent(Select);
            base.Append(wisp1);

            wisp2.Left.Set(97, 0.5f);
            wisp2.Top.Set(84, 0.5f);
            wisp2.Width.Set(32, 0);
            wisp2.Height.Set(32, 0);
            //wisp2.OnClick += new MouseEvent(Select);
            base.Append(wisp2);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            //Comet Dash
            if (mp.upgradeUnlock[0] == 1 && mp.upgrade[0] != 1 && mp.upgrade[1] != 1) { wind1.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Wind2")); } //unlocked texture
            else if (mp.upgrade[0] == 1 || mp.upgrade[1] == 1) { wind1.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Wind2Bad")); } //in use texture
            else { wind1.SetImage(ModContent.GetTexture("StarlightRiver/GUI/blank")); }//locked texture
            //Poop
            if (mp.upgradeUnlock[1] == 1 && mp.upgrade[0] != 2 && mp.upgrade[1] != 2) { wind2.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Wind3")); }
            else if (mp.upgrade[0] == 2 || mp.upgrade[1] == 2) { wind2.SetImage(ModContent.GetTexture("StarlightRiver/GUI/Wind3Bad")); }
            else { wind2.SetImage(ModContent.GetTexture("StarlightRiver/GUI/blank")); }

            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            Name.Left.Set(Name.Text.Length * 6 / -2, 0.5f);
            Line1.Left.Set(Line1.Text.Length * 5 / -2, 0.5f);

            if (none.IsMouseHovering) { Name.SetText("Unequip"); Line1.SetText("Unequip an infusion"); }//unequip

            //if----(ability hovered----------unlocked----------------) { Name is name of ability-----Description of ability}
            else if (wind1.IsMouseHovering && mp.upgradeUnlock[0] == 1) { Name.SetText("Comet Dash"); Line1.SetText("Dash farther, deal damage"); }//comet dash
            else if (wind2.IsMouseHovering && mp.upgradeUnlock[1] == 1) { Name.SetText("Poop"); Line1.SetText("Poop +1"); }//poop

            else { Name.SetText(""); Line1.SetText(""); }
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
}
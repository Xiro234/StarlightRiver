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

        public UIImageButton wind1 = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/blank"));
        public UIImageButton wind2 = new UIImageButton(ModContent.GetTexture("StarlightRiver/GUI/blank"));

        public static bool visible = false;

        public override void OnInitialize()
        {
            wind1.Left.Set(0, 0.5f);
            wind1.Top.Set(-64, 0.5f);
            wind1.Width.Set(32, 0);
            wind1.Height.Set(32, 0);
            wind1.OnClick += new MouseEvent(Select);
            base.Append(wind1);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

            if (mp.upgradeUnlock[0] == 1) { wind1.SetImage(ModContent.GetTexture("StarlightRiver/NPCs/Pickups/Wind1")); } else { wind1.SetImage(ModContent.GetTexture("StarlightRiver/GUI/blank")); }          
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

            if (listeningElement == wind1 && mp.upgradeUnlock[0] == 1) { mp.upgrade[slot] = 1; }

            visible = false;
        }
    }
}
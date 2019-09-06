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
using System.Collections.Generic;

namespace spritersguildwip.GUI
{
    public class Collection : UIState
    {
        public UIImage back = new UIImage(ModContent.GetTexture("spritersguildwip/GUI/back"));
        public UIImage charm = new UIImage(ModContent.GetTexture("spritersguildwip/GUI/charm"));

        public UIImageButton wind = new UIImageButton(ModContent.GetTexture("spritersguildwip/GUI/blank"));
        public UIImageButton wisp = new UIImageButton(ModContent.GetTexture("spritersguildwip/GUI/blank"));
        public UIImageButton smash = new UIImageButton(ModContent.GetTexture("spritersguildwip/GUI/blank"));
        public UIImageButton shadow = new UIImageButton(ModContent.GetTexture("spritersguildwip/GUI/blank"));
         
        public UIText Name = new UIText("ERROR", 1.2f);
        public UIText Line1 = new UIText("This is an error!", 0.9f);
        public UIText Line2 = new UIText("This is an error also!", 0.8f);
        public UIText Line3 = new UIText("This is, yet again, an error!", 0.8f);

        public UIImage stamina = new UIImage(ModContent.GetTexture("spritersguildwip/GUI/Stamina"));

        public static bool visible = false;
        int select = 0;
        
        public override void OnInitialize()
        {
            back.Left.Set(20, 0);
            back.Top.Set(260, 0);
            back.Width.Set(132, 0f);
            back.Height.Set(132, 0f);
            base.Append(back);

            charm.Left.Set(58, 0);
            charm.Top.Set(58, 0);
            charm.Width.Set(16, 0f);
            charm.Height.Set(16, 0f);
            charm.ImageScale = 0;
            back.Append(charm);

            stamina.Left.Set(360, 0);
            stamina.Top.Set(20, 0);
            stamina.Width.Set(22, 0f);
            stamina.Height.Set(22, 0f);
            stamina.ImageScale = 0;
            back.Append(stamina);

            wind.Left.Set(0, 0);
            wind.Top.Set(45, 0);
            wind.Width.Set(32, 0);
            wind.Height.Set(32, 0);
            wind.OnClick += new MouseEvent(Select);
            back.Append(wind);

            wisp.Left.Set(22, 0);
            wisp.Top.Set(85, 0);
            wisp.Width.Set(32, 0);
            wisp.Height.Set(32, 0);
            wisp.OnClick += new MouseEvent(Select);
            back.Append(wisp);

            smash.Left.Set(78, 0);
            smash.Top.Set(85, 0);
            smash.Width.Set(32, 0);
            smash.Height.Set(32, 0);
            smash.OnClick += new MouseEvent(Select);
            back.Append(smash);

            shadow.Left.Set(100, 0);
            shadow.Top.Set(45, 0);
            shadow.Width.Set(32, 0);
            shadow.Height.Set(32, 0);
            shadow.OnClick += new MouseEvent(Select);
            back.Append(shadow);

            //-------------------------

            Name.Left.Set(190, 0);
            Name.Top.Set(280, 0);
            base.Append(Name);

            Line1.Left.Set(0, 0);
            Line1.Top.Set(30, 0);
            Name.Append(Line1);

            Line2.Left.Set(0, 0);
            Line2.Top.Set(50, 0);
            Name.Append(Line2);

            Line3.Left.Set(0, 0);
            Line3.Top.Set(65, 0);
            Name.Append(Line3);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

            if (Main.hardMode) { charm.ImageScale = 1; }
            else { charm.ImageScale = 0; }

            if (select != 0) { stamina.ImageScale = 1; }
            else { stamina.ImageScale = 0; }

            if (!Main.playerInventory)
            {
                select = 0;
            }

            if (mp.unlock[0] == 1) { wind.SetImage(ModContent.GetTexture("spritersguildwip/GUI/ab1")); } else { wind.SetImage(ModContent.GetTexture("spritersguildwip/GUI/blank")); }
            if (mp.unlock[3] == 1) { wisp.SetImage(ModContent.GetTexture("spritersguildwip/GUI/ab2")); } else { wisp.SetImage(ModContent.GetTexture("spritersguildwip/GUI/blank")); }
            if (mp.unlock[2] == 1) { smash.SetImage(ModContent.GetTexture("spritersguildwip/GUI/ab3")); } else { smash.SetImage(ModContent.GetTexture("spritersguildwip/GUI/blank")); }
            if (mp.unlock[1] == 1) { shadow.SetImage(ModContent.GetTexture("spritersguildwip/GUI/ab4")); } else { shadow.SetImage(ModContent.GetTexture("spritersguildwip/GUI/blank")); }

            switch (select)
            {
                case 0: Name.SetText("");
                    Line1.SetText("");
                    Line2.SetText("");
                    Line3.SetText(""); break;
                case 1: Name.SetText("Forbidden Winds      x1");
                    Line1.SetText("Press Shift");
                    Line2.SetText("Dash forward A short");
                    Line3.SetText("distance, breaks crystals"); break;
                case 2: Name.SetText("[PH] wisp               1/s");
                    Line1.SetText("Hold F");
                    Line2.SetText("Shrink and float in the air,");
                    Line3.SetText("using your mouse to steer"); break;
                case 3: Name.SetText("[PH] smash             x2");
                    Line1.SetText("Press Z");
                    Line2.SetText("Dive downwards, shattering");
                    Line3.SetText("solid rock and steel"); break;
                case 4: Name.SetText("Zzelera's Cloak        x3");
                    Line1.SetText("Press Q");
                    Line2.SetText("Become invincible and quickly");
                    Line3.SetText("fly to a targeted location"); break;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch); 
            Bootlegdust.ForEach(BootlegDust => BootlegDust.Draw(spriteBatch));
            Recalculate();
        }

        internal static readonly List<BootlegDust> Bootlegdust = new List<BootlegDust>();
        public override void Update(GameTime gameTime)
        {
            Bootlegdust.ForEach(BootlegDust => BootlegDust.Update());
            Bootlegdust.RemoveAll(BootlegDust => BootlegDust.time <= 0);

            if(Main.expertMode && visible)
            {
                BootlegDust dus = new BootlegDust(ModContent.GetTexture("spritersguildwip/GUI/Fire"), new Vector2(78, 318) + new Vector2(Main.rand.Next(0,16), Main.rand.Next(0, 16)), new Vector2(0, -1), new Color(255,255,100), 2f, 60);
                Bootlegdust.Add(dus);
            }
        }

        private void Select(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            if (listeningElement == wind && mp.unlock[0] == 1) { select = 1; }
            if (listeningElement == wisp && mp.unlock[3] == 1) { select = 2; }
            if (listeningElement == smash && mp.unlock[2] == 1) { select = 3; }
            if (listeningElement == shadow && mp.unlock[1] == 1) { select = 4; }
        }
    }

    public class CollectionHandler : ModPlayer
    {
        public override void PreUpdate()
        {
            if (Main.playerInventory)
            {
                Collection.visible = true;
            }
            else
            {
                Collection.visible = false;
            }
        }
    }

    public class BootlegDust
    {     
        Texture2D tex;
        Vector2 pos;
        Vector2 vel;
        Color col;
        float scl;
        public int time;

        public BootlegDust(Texture2D texture, Vector2 position, Vector2 velocity, Color color, float scale, int timeleft)
        {
            tex = texture;
            pos = position;
            vel = velocity;
            col = color;
            scl = scale;
            time = timeleft;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, default, col, 0, default, scl, default, 0);
        }

        public virtual void Update()
        {
            pos += vel;
            col.G -= 4;
            scl *= 0.94f;
            time--;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Dragons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace StarlightRiver.GUI
{
    public enum ActivePart
    {
        none = 0,
        horn = 1,
        scale = 2,
        belly = 3,
        eye = 4,
        name = 5
    };

    public class DragonMenu : UIState
    {
        public static bool created = false;
        public static bool visible = false;
        public Color currentColor;
        private ActivePart part;

        public DragonHandler dragon;

        public override void OnInitialize()
        {
            QuickAddButton("Customize Dragon", new Vector2(Main.screenWidth / 2, 700), new MouseEvent(ChangeToDragon));
        }

        private void ChangeToHuman(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.menuMode = 2;
            visible = false;
            base.RemoveAllChildren();
            QuickAddButton("Customize Dragon", new Vector2(Main.screenWidth / 2, 700), new MouseEvent(ChangeToDragon));

            Main.PlaySound(SoundID.MenuClose);
        }

        private void ChangeToDragon(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.menuMode = 888;
            visible = true;
            base.RemoveAllChildren();

            QuickAddButton("Customize Player", new Vector2(Main.screenWidth / 2, 700), new MouseEvent(ChangeToHuman));
            QuickAddButton("Name", new Vector2(Main.screenWidth / 2, 300), new MouseEvent(Customize0));
            QuickAddButton("Horns", new Vector2(Main.screenWidth / 2, 340), new MouseEvent(Customize1));
            QuickAddButton("Scales", new Vector2(Main.screenWidth / 2, 380), new MouseEvent(Customize2));
            QuickAddButton("Belly", new Vector2(Main.screenWidth / 2, 420), new MouseEvent(Customize3));
            QuickAddButton("Eyes", new Vector2(Main.screenWidth / 2, 460), new MouseEvent(Customize4));
            QuickAddButton("Roar", new Vector2(Main.screenWidth / 2, 500) /*, new MouseEvent(Customize5)*/);
            QuickAddButton("Random", new Vector2(Main.screenWidth / 2, 580), new MouseEvent(Randomize));

            currentColor = new Color(0, 0, 0);
            part = ActivePart.none;

            Main.PlaySound(SoundID.MenuOpen);
        }

        private void Customize0(UIMouseEvent evt, UIElement listeningElement)
        {
            base.RemoveAllChildren();
            part = ActivePart.name;

            QuickAddButton("Back", new Vector2(Main.screenWidth / 2, 700), new MouseEvent(ChangeToDragon));
        }

        private void Customize1(UIMouseEvent evt, UIElement listeningElement)
        {
            base.RemoveAllChildren();
            QuickAddColor(ColorChannel.r, new Vector2(Main.screenWidth / 2, 300), dragon.data.hornColor.R);
            QuickAddColor(ColorChannel.g, new Vector2(Main.screenWidth / 2, 340), dragon.data.hornColor.G);
            QuickAddColor(ColorChannel.b, new Vector2(Main.screenWidth / 2, 380), dragon.data.hornColor.B);
            part = ActivePart.horn;

            QuickAddButton("Back", new Vector2(Main.screenWidth / 2, 700), new MouseEvent(ChangeToDragon));

            Main.PlaySound(SoundID.MenuOpen);
        }

        private void Customize2(UIMouseEvent evt, UIElement listeningElement)
        {
            base.RemoveAllChildren();
            QuickAddColor(ColorChannel.r, new Vector2(Main.screenWidth / 2, 300), dragon.data.scaleColor.R);
            QuickAddColor(ColorChannel.g, new Vector2(Main.screenWidth / 2, 340), dragon.data.scaleColor.G);
            QuickAddColor(ColorChannel.b, new Vector2(Main.screenWidth / 2, 380), dragon.data.scaleColor.B);
            part = ActivePart.scale;

            QuickAddButton("Back", new Vector2(Main.screenWidth / 2, 700), new MouseEvent(ChangeToDragon));

            Main.PlaySound(SoundID.MenuOpen);
        }

        private void Customize3(UIMouseEvent evt, UIElement listeningElement)
        {
            base.RemoveAllChildren();
            QuickAddColor(ColorChannel.r, new Vector2(Main.screenWidth / 2, 300), dragon.data.bellyColor.R);
            QuickAddColor(ColorChannel.g, new Vector2(Main.screenWidth / 2, 340), dragon.data.bellyColor.G);
            QuickAddColor(ColorChannel.b, new Vector2(Main.screenWidth / 2, 380), dragon.data.bellyColor.B);
            part = ActivePart.belly;

            QuickAddButton("Back", new Vector2(Main.screenWidth / 2, 700), new MouseEvent(ChangeToDragon));

            Main.PlaySound(SoundID.MenuOpen);
        }

        private void Customize4(UIMouseEvent evt, UIElement listeningElement)
        {
            base.RemoveAllChildren();
            QuickAddColor(ColorChannel.r, new Vector2(Main.screenWidth / 2, 300), dragon.data.eyeColor.R);
            QuickAddColor(ColorChannel.g, new Vector2(Main.screenWidth / 2, 340), dragon.data.eyeColor.G);
            QuickAddColor(ColorChannel.b, new Vector2(Main.screenWidth / 2, 380), dragon.data.eyeColor.B);
            part = ActivePart.eye;

            QuickAddButton("Back", new Vector2(Main.screenWidth / 2, 700), new MouseEvent(ChangeToDragon));

            Main.PlaySound(SoundID.MenuOpen);
        }

        private void Randomize(UIMouseEvent evt, UIElement listeningElement)
        {
            dragon.data.hornColor = new Color(Main.rand.Next(255), Main.rand.Next(255), Main.rand.Next(255));
            dragon.data.scaleColor = new Color(Main.rand.Next(255), Main.rand.Next(255), Main.rand.Next(255));
            dragon.data.bellyColor = new Color(Main.rand.Next(255), Main.rand.Next(255), Main.rand.Next(255));
            dragon.data.eyeColor = new Color(Main.rand.Next(255), Main.rand.Next(255), Main.rand.Next(255));

            Main.PlaySound(SoundID.MenuTick);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                Texture2D tex = ModContent.GetTexture("StarlightRiver/Dragons/DragonHorn");
                Texture2D tex2 = ModContent.GetTexture("StarlightRiver/Dragons/DragonScale");
                Texture2D tex3 = ModContent.GetTexture("StarlightRiver/Dragons/DragonBelly");
                Texture2D tex4 = ModContent.GetTexture("StarlightRiver/Dragons/DragonEye");
                spriteBatch.Draw(tex, new Vector2(Main.screenWidth / 2, 220), tex.Frame(), dragon.data.hornColor, 0, tex.Frame().Size() / 2, 1, 0, 0);
                spriteBatch.Draw(tex2, new Vector2(Main.screenWidth / 2, 220), tex.Frame(), dragon.data.scaleColor, 0, tex.Frame().Size() / 2, 1, 0, 0);
                spriteBatch.Draw(tex3, new Vector2(Main.screenWidth / 2, 220), tex.Frame(), dragon.data.bellyColor, 0, tex.Frame().Size() / 2, 1, 0, 0);
                spriteBatch.Draw(tex4, new Vector2(Main.screenWidth / 2, 220), tex.Frame(), dragon.data.eyeColor, 0, tex.Frame().Size() / 2, 1, 0, 0);

                currentColor.A = 255;

                if (part == ActivePart.name)
                {
                    Vector2 adj = new Vector2((int)Main.fontMouseText.MeasureString(dragon.data.name).X, (int)Main.fontMouseText.MeasureString(dragon.data.name).Y * 1.2f);
                    Utils.DrawBorderStringBig(spriteBatch, dragon.data.name, new Vector2(Main.screenWidth / 2, 300) - adj, Color.White, 0.675f);
                }
            }

            base.Draw(spriteBatch);
            Recalculate();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (part)
            {
                case ActivePart.horn: dragon.data.hornColor = currentColor; break;
                case ActivePart.scale: dragon.data.scaleColor = currentColor; break;
                case ActivePart.belly: dragon.data.bellyColor = currentColor; break;
                case ActivePart.eye: dragon.data.eyeColor = currentColor; break;
                case ActivePart.name: dragon.data.name = Main.GetInputText(dragon.data.name); break;
            }
        }

        private void QuickAddButton(string text, Vector2 pos, MouseEvent OnClick = null)
        {
            TextButton button = new TextButton(text);
            button.Left.Set(pos.X - (int)Main.fontMouseText.MeasureString(text).X * 2f / 2, 0);
            button.Top.Set(pos.Y - (int)Main.fontMouseText.MeasureString(text).Y * 1.2f / 2, 0);
            button.OnClick += OnClick;
            base.Append(button);
        }

        private void QuickAddColor(ColorChannel channel, Vector2 pos, int initialValue = 0)
        {
            ColorSlider slider = new ColorSlider(channel);
            slider.Left.Set(pos.X - 126, 0);
            slider.Top.Set(pos.Y - 10, 0);
            slider.Width.Set(255, 0);
            slider.Height.Set(32, 0);
            slider.sliderPos = initialValue;

            base.Append(slider);
        }
    }

    public class TextButton : UIElement
    {
        private readonly string Text;
        private int Fade;

        public TextButton(string text)
        {
            Text = text;
        }

        public override void OnInitialize()
        {
            Width.Set((int)Main.fontMouseText.MeasureString(Text).X * 1.2f, 0);
            Height.Set((int)Main.fontMouseText.MeasureString(Text).Y * 1.2f, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            bool hover = GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint());
            if (!hover && Fade > 0) Fade--;
            if (hover && Fade < 10) Fade++;

            int basecol = 140;
            int intensity = basecol + Fade * 11;
            Color color = new Color(intensity, intensity, basecol - Fade * 14);

            Utils.DrawBorderStringBig(spriteBatch, Text, GetDimensions().Position() + GetDimensions().ToRectangle().Size() / 2, color * (0.8f + Fade / 50f), 0.675f + Fade / 100f, 0.5f, 0.5f);

            Width.Set((int)Main.fontMouseText.MeasureString(Text).X * 2f, 0);
            Height.Set((int)Main.fontMouseText.MeasureString(Text).Y * 1.2f, 0);
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            Main.PlaySound(SoundID.MenuTick);
        }

        public override void Click(UIMouseEvent evt)
        {
            base.Click(evt);
        }
    }

    public enum ColorChannel
    {
        r = 0,
        g = 1,
        b = 2
    };

    public class ColorSlider : UIElement
    {
        public int sliderPos = 0;
        private readonly ColorChannel Channel;
        private Rectangle SliderBox => new Rectangle((int)GetDimensions().X + sliderPos - 9, (int)GetDimensions().Y + 2, 18, 28);

        public ColorSlider(ColorChannel channel)
        {
            Channel = channel;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex0 = ModContent.GetTexture("StarlightRiver/GUI/Assets/SliderBack");
            Texture2D tex1 = ModContent.GetTexture("StarlightRiver/GUI/Assets/SliderGradient");
            Texture2D tex2 = ModContent.GetTexture("StarlightRiver/GUI/Assets/Slider");
            Texture2D tex3 = ModContent.GetTexture("StarlightRiver/GUI/Assets/SliderOver");

            Color backColor = (Parent as DragonMenu).currentColor;
            if (Channel == ColorChannel.r) backColor.R = 0;
            if (Channel == ColorChannel.g) backColor.G = 0;
            if (Channel == ColorChannel.b) backColor.B = 0;

            int off = (int)Channel * 2;

            spriteBatch.Draw(tex0, GetDimensions().ToRectangle(), tex0.Frame(), backColor);

            spriteBatch.End();
            spriteBatch.Begin(default, BlendState.Additive, SamplerState.PointWrap, default, default);

            spriteBatch.Draw(tex1, new Rectangle((int)GetDimensions().X, (int)GetDimensions().Y + 8, 255, 16), new Rectangle(0, off, 255, 1), Color.White);

            spriteBatch.End();
            spriteBatch.Begin();

            spriteBatch.Draw(tex2, GetDimensions().ToRectangle(), tex2.Frame(), Color.White);
            spriteBatch.Draw(tex3, SliderBox, tex3.Frame(), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && Main.mouseLeft)
            {
                sliderPos = (int)(Main.MouseScreen.ToPoint().X - GetDimensions().X);
            }

            if (sliderPos < 0) sliderPos = 0;
            if (sliderPos > 255) sliderPos = 255;

            if (Channel == ColorChannel.r) (Parent as DragonMenu).currentColor.R = (byte)sliderPos;
            if (Channel == ColorChannel.g) (Parent as DragonMenu).currentColor.G = (byte)sliderPos;
            if (Channel == ColorChannel.b) (Parent as DragonMenu).currentColor.B = (byte)sliderPos;
        }
    }
}
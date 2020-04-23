using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace StarlightRiver.GUI
{
    public class Stamina : UIState
    {
        public UIPanel abicon;
        public static bool visible = false;

        Stam Stam1 = new Stam(ModContent.GetTexture("StarlightRiver/GUI/Stamina2"));
        Stam Stam2 = new Stam(ModContent.GetTexture("StarlightRiver/GUI/Stamina"));
        public override void OnInitialize()
        {
            Stam1.Left.Set(-303, 1);
            Stam1.Top.Set(110, 0);
            Stam1.Width.Set(22, 0f);
            Stam1.Height.Set(22, 0f);
            Stam1.color = Color.White * 0.75f;
            base.Append(Stam1);

            Stam2.Left.Set(0, 0);
            Stam2.Top.Set(0, 0);
            Stam2.Width.Set(22, 0f);
            Stam2.Height.Set(22, 0f);
            Stam2.color = Color.White;
            Stam1.Append(Stam2);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            Stam1.Copies = mp.StatStaminaMax;
            Stam2.Copies = mp.StatStamina;

            if (Main.mapStyle != 1)
            {
                if (Main.playerInventory)
                {
                    Stam1.Left.Set(-220, 1);
                    Stam1.Top.Set(90, 0);
                }
                else
                {
                    Stam1.Left.Set(-66, 1);
                    Stam1.Top.Set(90, 0);
                }
            }
            else
            {
                Stam1.Left.Set(-303, 1);
                Stam1.Top.Set(110, 0);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Recalculate();
        }

    }

    class Stam : UIElement
    {
        public int Copies;
        public Texture2D Texture;
        public Color color;

        public Stam(Texture2D texture)
        {
            Texture = texture;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            Player player = Main.LocalPlayer;
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();

            for (int k = 0; k < Copies; k++)
            {
                if (k == Copies - 1 && color == Color.White)
                {
                    spriteBatch.Draw(Texture, new Vector2((int)dimensions.X, (int)dimensions.Y + k * 24) + new Vector2(dimensions.Width / 2, dimensions.Height / 2), null, color, 0f, new Vector2(dimensions.Width / 2, dimensions.Height / 2), 1f + ((float)Math.Sin(LegendWorld.rottime * 2) / 8), SpriteEffects.None, 0f);
                    spriteBatch.Draw(Texture, new Vector2((int)dimensions.X, (int)dimensions.Y + (k + 1) * 24) + new Vector2(dimensions.Width / 2, dimensions.Height / 2), null, Color.White * (1 - (mp.StatStaminaRegen / (float)mp.StatStaminaRegenMax)), 0f, new Vector2(dimensions.Width / 2, dimensions.Height / 2), (1 - (mp.StatStaminaRegen / (float)mp.StatStaminaRegenMax)), SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(Texture, new Rectangle((int)dimensions.X, (int)dimensions.Y + k * 24, (int)dimensions.Width, (int)dimensions.Height), color);
                }
            }
            //special case to draw the first crystal regenerating
            if (color == Color.White && mp.StatStamina == 0)
            {
                spriteBatch.Draw(Texture, new Vector2((int)dimensions.X, (int)dimensions.Y) + new Vector2(dimensions.Width / 2, dimensions.Height / 2), null, Color.White * (1 - (mp.StatStaminaRegen / (float)mp.StatStaminaRegenMax)), 0f, new Vector2(dimensions.Width / 2, dimensions.Height / 2), (1 - (mp.StatStaminaRegen / (float)mp.StatStaminaRegenMax)), SpriteEffects.None, 0f);
            }

        }
    }
}

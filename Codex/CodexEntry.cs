using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace StarlightRiver.Codex
{
    public class CodexEntry
    {
        public bool Locked = true;
        public bool New = false;
        public bool RequiresUpgradedBook = false;
        public int Category;

        public string Title;
        public string Body;
        public Texture2D Image;
        public Texture2D Icon;

        public enum Categories
        {
            Abilities = 0,
            Biomes = 1,
            Relics = 2,
            Bosses = 3,
            Misc = 4,
            RiftCrafting = 5
        }

        public virtual void Draw(Vector2 pos, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, pos + new Vector2(-50 + (310 - Image.Width) / 2, 36), Color.White);
            spriteBatch.Draw(Icon, pos + new Vector2(-38, -5), Color.White);
            Utils.DrawBorderString(spriteBatch, Title, pos, Color.White, 1.2f);
            Utils.DrawBorderString(spriteBatch, Body, pos + new Vector2(-30, 50 + Image.Height), Color.White, 0.8f);
        }
    }
}

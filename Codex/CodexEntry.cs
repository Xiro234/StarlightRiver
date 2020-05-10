using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader.IO;

namespace StarlightRiver.Codex
{
    public class CodexEntry : TagSerializable
    {
        public bool Locked = true;
        public bool New = false;
        public bool RequiresUpgradedBook = false;
        public Categories Category;

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
            None = 5
        }

        public virtual void Draw(Vector2 pos, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, pos + new Vector2(-50 + (310 - Image.Width) / 2, 36), Color.White);
            spriteBatch.Draw(Icon, pos + new Vector2(-38, -5), Color.White);
            Utils.DrawBorderString(spriteBatch, Title, pos, Color.White, 1.2f);
            Utils.DrawBorderString(spriteBatch, Helper.WrapString(Body, 550, Main.fontDeathText, 0.8f), pos + new Vector2(-30, 50 + Image.Height), Color.White, 0.8f);
        }

        public TagCompound SerializeData()
        {
            return new TagCompound()
            {
                ["Name"] = GetType().ToString(),
                ["Locked"] = Locked
            };
        }
        public static CodexEntry DeserializeData(TagCompound tag)
        {
            Type t = Type.GetType(tag.GetString("Name"));
            CodexEntry entry = (CodexEntry)Activator.CreateInstance(t);
            entry.Locked = tag.GetBool("Locked");
            return entry;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Void
{
    public class VoidWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            dustType = mod.DustType("Darkness");
            drop = mod.ItemType("VoidWallItem");
            AddMapEntry(new Color(17, 22, 21));
        }
    }
    public class VoidWallPillar : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            dustType = mod.DustType("Darkness");
            drop = mod.ItemType("VoidWallItem");
            AddMapEntry(new Color(17, 22, 21));
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Texture2D pillar = ModContent.GetTexture("StarlightRiver/Tiles/Void/BackPillar");
            for (int x = 0; x <= pillar.Width / 16; x++)
            {
                for (int y = 0; y <= pillar.Height / 16; y++)
                {
                    spriteBatch.Draw(pillar, new Vector2((i + 9) * 16 + 6 + 16 * x, (j - 6) * 16 - 296 + 16 * y) - Main.screenPosition, new Rectangle(x * 16, y * 16, 16, 16), Lighting.GetColor(((i - 2) * 16 + 6 + 16 * x) / 16, ((j - 18) * 16 - 296 + 16 * y) / 16));
                }
            }

            return false;
        }
    }
    public class VoidWallPillarS : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            dustType = mod.DustType("Darkness");
            drop = mod.ItemType("VoidWallItem");
            AddMapEntry(new Color(17, 22, 21));
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Texture2D pillar = ModContent.GetTexture("StarlightRiver/Tiles/Void/BackPillarS");
            for (int x = 0; x <= pillar.Width / 16; x++)
            {
                for (int y = 0; y <= pillar.Height / 16; y++)
                {
                    spriteBatch.Draw(pillar, new Vector2((i + 9) * 16 + 6 + 16 * x, (j - 6) * 16 - 240 + 16 * y) - Main.screenPosition, new Rectangle(x * 16, y * 16, 16, 16), Lighting.GetColor(((i - 2) * 16 + 6 + 16 * x) / 16, ((j - 14) * 16 - 296 + 16 * y) / 16));
                }
            }

            return false;
        }
    }
}
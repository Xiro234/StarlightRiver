using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spritersguildwip.Ability;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace spritersguildwip.Tiles
{
    class Void1 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;         
            drop = mod.ItemType("Void1Item");
            dustType = mod.DustType("Darkness");
            AddMapEntry(new Color(35, 40, 20));
        }
    }
    class Void2 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("Void2Item");
            dustType = mod.DustType("Darkness");
            AddMapEntry(new Color(45, 50, 30));
        }
    }
    class VoidTorch1 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(45, 50, 30));
            dustType = mod.DustType("Darkness");
            disableSmartCursor = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("VoidTorch1Item"));
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                for(int k = 0; k <= 3; k++)
                {
                    Dust.NewDust(new Vector2((i * 16) - 1, (j - 1) * 16), 12, 16, mod.DustType("Darkness"));
                }              
            }
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.JungleCorrupt
{
    internal class TreeJungleCorrupt : ModTree
    {
        private Mod mod => ModLoader.GetMod("StarlightRiver");

        public override int CreateDust()
        {
            return mod.DustType("Corrupt");
        }

        public override int GrowthFXGore()
        {
            return mod.GetGoreSlot("Gores/Ward0");
        }

        public override int DropWood()
        {
            return ItemID.Wood;
        }

        public override Texture2D GetTexture()
        {
            return mod.GetTexture("Tiles/JungleCorrupt/TreeJungleCorrupt");
        }

        public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
        {
            frameWidth = 116;
            frameHeight = 98;
            xOffsetLeft = 48;
            yOffset = 2;
            return mod.GetTexture("Tiles/JungleCorrupt/TreeJungleCorrupt_Tops");

        }

        public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
        {
            return mod.GetTexture("Tiles/JungleCorrupt/TreeJungleCorrupt_Branches");
        }
    }
}

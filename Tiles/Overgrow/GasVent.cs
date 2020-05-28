using Microsoft.Xna.Framework;
using StarlightRiver.Gases;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Overgrow
{
    internal class GasVent : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = true;

            dustType = ModContent.DustType<Dusts.Gas>();
            AddMapEntry(new Color(255, 186, 66));
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (Main.rand.Next(2) == 0)
            {
                ModGas.SpawnGas(i, j, ModContent.DustType<Dusts.Gas>(), 200);
            }
        }
    }
}


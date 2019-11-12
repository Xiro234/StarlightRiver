using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Gases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Overgrow
{
    class GasVent : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = true;

            drop = mod.ItemType("StaminaOrbItem");
            dustType = ModContent.DustType<Dusts.Gas>();
            AddMapEntry(new Color(255, 186, 66));
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if(Main.rand.Next(2) == 0)
            {
                ModGas.SpawnGas(i, j, ModContent.DustType<Dusts.Gas>(), 200);
            }
        }
    }
}


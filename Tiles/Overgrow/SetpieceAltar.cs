using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarlightRiver.Tiles.Overgrow
{
    internal class SetpieceAltar : ModTile { public override void SetDefaults() { QuickBlock.QuickSetFurniture(this, 10, 7, DustID.Stone, SoundID.Tink, true, -1, new Color(100, 100, 80)); } }
}
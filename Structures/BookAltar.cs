﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;

namespace StarlightRiver.Structures
{
    public partial class GenHelper
    {
        public static void BookAltarGen(GenerationProgress progress)
        {
            progress.Message = "Hiding Codex...";

            Vector2 spawn = FindSand();
            StructureHelper.StructureHelper.GenerateStructure("Structures/CodexTemple", spawn.ToPoint16(), StarlightRiver.Instance);
            LegendWorld.BookSP = spawn * 16 + new Vector2(312, 530);

        }

        private static Vector2 FindSand()
        {
            for (int i = WorldGen.UndergroundDesertLocation.X; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    if (Main.tile[i, j].type == TileID.Sand)
                    {
                        if (Helper.AirScanUp(new Vector2(i, j), 40))
                        {
                            return new Vector2(i, j - 30);
                        }
                        else break;
                    }
                }
            }
            return new Vector2(WorldGen.UndergroundDesertLocation.X, 400);
        }
    }
}

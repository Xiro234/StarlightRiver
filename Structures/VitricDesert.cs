using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using StarlightRiver.Tiles;
using System.Collections.Generic;
using StarlightRiver.Tiles.Vitric;

namespace StarlightRiver.Structures
{
    public partial class GenHelper
    {
        private const int TotalHeight = 96;
        private const int HalfHeight = 48;

        /// <summary>
        /// Generates a crystal cavern at position topCentre, where topCentre is exactly what it is called.
        /// </summary>
        /// <param name="centre">The top centre point of the cavern.</param>
        public static void VitricDesertGen(GenerationProgress progress)
        {
            progress.Message = "Vitrifying Desert";
            int size = (int)(Main.maxTilesX / 28f); //Width of the cavern; value shown here is half the size. So, functional size is actually size * 2.
            Point centre = new Point(WorldGen.UndergroundDesertLocation.X + size, WorldGen.UndergroundDesertLocation.Y + WorldGen.UndergroundDesertLocation.Height + 30);

            DigVitricCave(centre, size);
            for (int i = -4 - WorldGen.genRand.Next(7); i < 10 + WorldGen.genRand.Next(7); ++i) //Digs opening
            {
                for (int j = -12 - WorldGen.genRand.Next(5); j < 14 + WorldGen.genRand.Next(5); ++j)
                {
                    Main.tile[centre.X + i, centre.Y - 20 - j].active(false);
                }
            }

            Vector2 sandMid = GetGroundDirectional(new Vector2(0, 1), centre.ToVector2(), TileID.Platforms);

            Mod m = ModLoader.GetMod("StarlightRiver");

            GenerateSandDunes(new Point((int)sandMid.X, (int)sandMid.Y - 10), (int)(size * 1.15f)); //Generates SAND under the crystals. Wacky!
            GenerateCrystals(centre); //I wonder what this does
            GenerateFloatingOre(centre, 30, (int)(size * 0.7f));

            LegendWorld.vitricBiome = new Rectangle(centre.X - size, centre.Y - TotalHeight / 2, size * 2, 96);
        }

        private static void DigVitricCave(Point centre, int size)
        {
            int spikeOut = 0;
            int spikeIn = 0;

            for (int i = -size - HalfHeight; i < size + HalfHeight; ++i) //Digs out a cave, mayhaps placeholder
            {
                int distI = Math.Abs(i); //Absolute start for end/startpoints. Range is size to (size - 48). Fits within that range only
                int modI = distI - (size - HalfHeight);
                int height = (int)(48 * (Math.Sin((1 / 30.57d) * modI + 1.57f) + 1)) / 2;

                if (distI < size - HalfHeight)
                    height = HalfHeight;

                for (int j = -height - 2 - spikeOut; j < height + 2 + spikeIn; ++j) //Place walls
                {
                    int x = centre.X + i; //X and Y variables
                    int y = centre.Y + j;

                    Main.tile[x, y].active(false);
                    Main.tile[x, y].liquid = 0;

                    if (j < -height + 6 || j > height - 6)
                    {
                        int type = j < -height + 2 || j > height - 2 ? (WorldGen.genRand.NextBool() ? ModContent.TileType<VitricSand>() : TileID.Stone) : ModContent.TileType<VitricSand>();
                        WorldGen.TileRunner(x, y, 7, 2, type, true, 0, 0, false, true);
                        continue;
                    }

                    WorldGen.KillWall(x, y, false);
                }
            }
        }

        private static void GenerateFloatingOre(Point centre, int reps, int width)
        {
            for (int i = 0; i < reps; ++i)
            {
                if (WorldGen.genRand.Next(3) == 0)
                {
                    Vector2 pos = centre.ToVector2() + new Vector2(WorldGen.genRand.Next(-width, width), WorldGen.genRand.Next((int)(HalfHeight * 0.8f)));
                    while (Main.tile[(int)pos.X, (int)pos.Y].active())
                        pos = centre.ToVector2() + new Vector2(WorldGen.genRand.Next(-width, width), WorldGen.genRand.Next((int)(HalfHeight * 0.8f)));
                    Helper.PlaceMultitile(2, 3, (int)pos.X, (int)pos.Y, ModContent.TileType<VitricOreFloat>());
                }
            }
        }

        private const int MinArenaSide = -46; //Left side of the middle dune
        private const int MaxArenaSide = 56; //Right side of the middle dune

        private static void GenerateSandDunes(Point midPoint, int size)
        {
            for (int i = (int)(-size * 0.89f); i < (int)(size * 0.9f); ++i) //Places sand.
            {
                int sHei = (int)(Math.Sin((i / 18f) + (3.14f / 3)) * 8.0f); //Sin wave placement for Y height
                if (i > MinArenaSide && i < MaxArenaSide)
                    sHei = (int)(8f * (Math.Sin((i / 18f) - 2f)) - 16f); //Middle dune

                for (int j = (int)(sHei); j < (10 + WorldGen.genRand.Next(12, 19)); ++j)
                {
                    int off = (int)((i / 30f) * ((i >= 0) ? -1 : 1));
                    if (i > MinArenaSide && i < MaxArenaSide) off = 0;
                    if (Main.tile[midPoint.X + i, (midPoint.Y + j) + off].type == (ushort)ModContent.TileType<VitricGlassCrystal>())
                        continue;
                    WorldGen.KillTile(midPoint.X + i, (midPoint.Y + j) + off, true, false, true);
                    WorldGen.PlaceTile(midPoint.X + i, (midPoint.Y + j) + off, ModContent.TileType<VitricSand>(), true, true, -1, 0);
                }
            }
        }

        private const float CrystalOffsetCoefficient = 3f; //Used for easy changing of the offset of the crystals

        private static void GenerateCrystals(Point tC)
        {
            float rot = 0f; //Rotation of crystal/placement used later
            int totalReps = (int)(125 * WorldSize()); //Total repeats
            float shortTau = 6.28f; //Helper variable

            //Add those FRICKING dumb crystals you BUFFOON.
        }

        /// <summary>
        /// Places crystal at placePosition, length of dist * 3, direction of dir, at the size determined by smol.
        /// </summary>
        private static bool PlaceCrystal(int dist, Vector2 placePosition, Vector2 dir, bool smol)
        {
            Mod m = ModLoader.GetMod("StarlightRiver");

            int width = WorldGen.genRand.Next(3, 7);
            int negWidth = WorldGen.genRand.Next(3, 7);
            if (smol)
            {
                width = WorldGen.genRand.Next(1, 4);
                negWidth = WorldGen.genRand.Next(1, 4);
            }

            width -= 2;
            negWidth -= 2;

            for (int j = 0; j < dist * 3; ++j)
            {
                if (j == 0 || j == 2)
                    width++;
                if (j == 1 || j == 3)
                    negWidth++;

                if (placePosition.X < 0 || placePosition.X > Main.maxTilesX || placePosition.Y < 0 || placePosition.Y > Main.maxTilesY)
                    return false;
                Vector2 negDir = Vector2.Normalize(new Vector2(1 / dir.X, 1 / -dir.Y));
                Vector2 actualPlacePos = placePosition - (negDir * 2);

                if ((dist * 3) - j < width)
                    width--;
                if ((dist * 3) - j < negWidth)
                    negWidth--;

                for (int k = -negWidth; k < width; ++k)
                {
                    Main.tile[(int)actualPlacePos.X, (int)actualPlacePos.Y].active(false);
                    WorldGen.PlaceTile((int)actualPlacePos.X, (int)actualPlacePos.Y, ModContent.TileType<VitricGlassCrystal>());
                    actualPlacePos += (negDir / 4);
                }

                placePosition += dir;
            }
            return true;
        }

        /// <summary>
        /// Moves from starting point p to the first solid block it touches according to direction dir. Skips tiles of types included the ignoredTileIDs array.
        /// </summary>
        /// <returns></returns>
        private static Vector2 GetGroundDirectional(Vector2 dir, Vector2 p, params int[] ignoredTileIDs)
        {
            Vector2 actualPos = p;
            while (!Main.tile[(int)actualPos.X, (int)actualPos.Y].active() || ignoredTileIDs.Any(x => x == Main.tile[(int)actualPos.X, (int)actualPos.Y].type))
                actualPos += dir;
            return actualPos;
        }

        /// <summary>
        /// Returns the world size difference: 1f for small, 1.5f for medium, 2f for large. Will return valid results for abnormal worlds.
        /// </summary>
        public static float WorldSize() => Main.maxTilesX / 4200f;

        /// <summary>
        /// Returns the world size difference, adjusted: 1 for small, 2 for medium, 3 for large.
        /// </summary>
        public static int WorldSizeAdj() {
            float siz = WorldSize();
            switch (siz)
            {
                case 1:
                    return 1;
                case 1.5f:
                    return 2;
                case 2f:
                    return 3;
            }
            return (int)siz; //This shouldn't be used for abnormal world sizes.
        }
    }
}
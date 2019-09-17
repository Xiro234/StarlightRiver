using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using System;
using StarlightRiver.Projectiles;

namespace StarlightRiver
{
    
    public partial class LegendWorld : ModWorld
    {
        public Vector2 PureSpawnPoint;

        public static List<Vector2> PureTiles = new List<Vector2> { };
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1)
            {
                //tasks.Insert(ShiniesIndex + 1, new PassLegacy("Vitrifying Desert", GenerateCrystalCaverns));
                tasks.Insert(ShiniesIndex + 2, new PassLegacy("Making the World Impure", EbonyGen));
            }
        }
        /// <summary>
        /// Generates a crystal cavern at position topCentre, where topCentre is exactly what it is called.
        /// </summary>
        /// <param name="centre">The top centre point of the cavern.</param>
        /*private void GenerateCrystalCaverns(GenerationProgress progress)
        {
            progress.Message = "Vitrifying Desert";
            int MaxCrystalCaveDepth = 0;
            Point centre = new Point(WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width / 2, WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Height);
            int size = Main.maxTilesX / 10; //Width of the cavern; value shown here is half the size. So, functional size is actually Main.maxTilesX / 5.
            float depth = 0; //Depth of the cave
            float height = 0; //Height of cave
            for (int i = -size; i < size; ++i) //Digs out a cave, mayhaps placeholder
            {
                int x = i + centre.X;

                if (i < -size / 3)
                {
                    height += WorldGen.genRand.Next(10) * 0.1f;
                    depth += 0.5f;
                }
                if (i > size / 3)
                {
                    height -= WorldGen.genRand.Next(10) * 0.1f;
                    depth -= 0.5f;
                }

                if (depth > MaxCrystalCaveDepth) 
                    MaxCrystalCaveDepth = (int)depth;

                for (int j = (-(int)height / 2); j < ((int)depth / 2); ++j)
                {
                    int y = centre.Y + j;
                    WorldGen.KillTile(x, y, true, false, true);
                }
            }
            for (int i = 0; i < 4; ++i)
            {
                WorldGen.KillTile(centre.X - 2 + i, centre.Y + (MaxCrystalCaveDepth / 5), true, false, true);
                WorldGen.PlaceTile(centre.X - 2 + i, centre.Y + (MaxCrystalCaveDepth / 5), TileID.Platforms, true, true, -1);
            }
            GenerateCrystals(centre); //I wonder what this does
        }

        private void GenerateCrystals(Point tC)
        {
            float rot = 0f; //Rotation of crystal/placement used later
            int totalReps = 100; //Total repeats
            float shortTau = 6.28f; //Helper variable
            float side = shortTau / 4; //Helper variable
            for (int i = 0; i < totalReps; ++i)
            {
                rot += shortTau / totalReps; //Increases rotation uniformly

                if (rot > shortTau) //Caps angle
                    rot = 0;

                Vector2 randomWallLocation = GetGroundDirectional(new Vector2(0, -1).RotatedBy(rot), tC.ToVector2(), WallID.Dirt)
                    + (new Vector2(0, -1).RotatedBy(rot) * 3); //Position of a wall. Starts off going UP, then goes clockwise.

                if (WorldGen.genRand.Next(Math.Abs((int)randomWallLocation.X - tC.X) + WorldGen.genRand.Next(30)) < Main.maxTilesX / 40) //Biases crystals towards the sides
                    continue;

                float adjRot = Vector2.Normalize(tC.ToVector2() - randomWallLocation).ToRotation() + side; //Rotation direction of the direction of the crystals, starting from the wall.
                adjRot += WorldGen.genRand.Next((int)(-side * 18), (int)(side * 18)) * 0.01f; //Randomization of angle
                Vector2 direction = new Vector2(0, -1).RotatedBy(adjRot); //Angle velocity of the crystal

                int wid = WorldGen.genRand.Next(1, 3); //Partial width of the crystal
                int negWid = -WorldGen.genRand.Next(1, 3); //Partial width of the crystal - formatted in this way to make odd numbered widths possible

                int reps = (Math.Abs((int)randomWallLocation.X - tC.X) / 6) + (WorldGen.genRand.Next(6, 21));
                for (int j = 0; j < reps; ++j) //Places crystal, replace PlaceTile with TileRunner or other method of choice
                {
                    WorldGen.PlaceTile((int)randomWallLocation.X, (int)randomWallLocation.Y, mod.TileType("GlassCrystal"), true, true, -1, 0);
                    randomWallLocation += direction;

                    Vector2 newDir = new Vector2(0, -1).RotatedBy(Vector2.Normalize(tC.ToVector2() - randomWallLocation).ToRotation() - (side * 2));
                    Vector2 widthPos = randomWallLocation - (newDir * ((wid + negWid) / 2f));
                    for (float k = negWid; k < wid; k += 0.5f) //Widens the crystal
                    {
                        WorldGen.PlaceTile((int)widthPos.X, (int)widthPos.Y, mod.TileType("GlassCrystal"), true, true, -1, 0);
                        widthPos += newDir / 2;
                    }
                }
            }
        }

        /// <summary>
        /// Moves from starting point p to the first solid block it touches according to direction dir. Skips tiles of types included the ignoredTileIDs array.
        /// </summary>
        /// <returns></returns>
        private Vector2 GetGroundDirectional(Vector2 dir, Vector2 p, params int[] ignoredTileIDs)
        {
            Vector2 actualPos = p;
            while (!Main.tile[(int)actualPos.X, (int)actualPos.Y].active() || ignoredTileIDs.Any(x => x == Main.tile[(int)actualPos.X, (int)actualPos.Y].type))
                actualPos += dir;
            return actualPos;
        }*/
        private void EbonyGen(GenerationProgress progress)
        {
            progress.Message = "Making the World Impure";

            for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * .0015); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next(0, (int)WorldGen.worldSurfaceHigh);

                if (Main.tile[x, y].type == TileID.Dirt)
                {
                    WorldGen.TileRunner(x, y, (double)WorldGen.genRand.Next(10, 11), 1, mod.TileType("OreEbony"), false, 0f, 0f, false, true);
                }
            }
        }

        public override void PostWorldGen()
        {
            // Top-Left Position
            Vector2 PureAltarSP = new Vector2(Main.spawnTileX, Main.spawnTileY - 50);
            PureSpawnPoint = PureAltarSP + new Vector2(7, 18);

            // Slopes in order: full=0, BL, BR, TL, TR, half
            // Example: bottom-left thick slope is 001X_XXXX
            const byte a = 32, b = 33, c = 34, d = 35, e = 36;

            byte[][] PureAltar = new byte[][] //Tiles
            {
                new byte[] { 1, 3, 3, 3, 3, 0, 0, 0, 3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 3, 2, 2, 3, 0, 0, 0, 3, 2, 2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 3, 1, 2, 3, 0, 0, 0, 3, 2, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 3, 1, 2, 3, 0, 0, 0, 3, 2, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 2, 3, 0, 0, 0, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 2, 3, 0, 0, 0, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 2, 3, 0, 0, 0, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 2, 3, 0, 0, 0, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                new byte[] { 1, 1, 1, 2, 3, 0, 0, 0, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                new byte[] { 1, 1, 3, 3, 3, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new byte[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 3, 3, 3, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                new byte[] { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 3, 2, 3, 2, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3 },
                new byte[] { 3, 3, 2, 2, 2, 2, 2, 2, 2, 3, 3, 2, 2, 2, 2, 2, 3, 3, 2, 2, 2, 2, 2, 2, 2, 3, 3 },
                new byte[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            };

            byte[][] PureAltarWalls = new byte[][] //Walls
            {
                new byte[] { 7, 7, 7, 7, 7, 1, 1, 1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                new byte[] { 7, 7, 7, 7, 7, 1, 1, 1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                new byte[] { 7, 7, 7, 7, 7, 1, 1, 1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                new byte[] { 7, 7, 7, 7, 7, 1, 1, 1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                new byte[] { 7, 7, 7, 7, 7, 1, 1, 1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                new byte[] { 7, 7, 7, 7, 7, 1, 1, 1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                new byte[] { 7, 7, 7, 7, 7, 1, 1, 1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                new byte[] { 7, 7, 7, 7, 7, 1, 1, 1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                new byte[] { 7, 7, 7, 7, 7, 1, 1, 1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                new byte[] { 7, 7, 7, 7, 7, 1, 1, 1, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 1, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new byte[] { 8, 8, 8, 8, 7, 7, 7, 7, 7, 8, 8, 8, 8, 9, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                new byte[] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                new byte[] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                new byte[] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
            };

            //---------------------------------------------------------------------------------------------------------

            for (int y = 0; y < PureAltar.Length; y++)
            {
                for (int x = 0; x < PureAltar[0].Length; x++)
                {

                    ushort placeType = TileID.Dirt;
                    ushort placeWall = WallID.Dirt;

                    switch (PureAltar[y][x] & 0b0001_1111)
                    {
                        //This is your block pallete
                        case 1: placeType = TileID.Ash; break;
                        case 2: placeType = (ushort)mod.TileType("Void1"); break;
                        case 3: placeType = (ushort)mod.TileType("Void2"); break;                       
                    }

                    if ((PureAltar[y][x] & 0b0001_1111) < 7)
                    {
                        WorldGen.PlaceTile((int)PureAltarSP.X + x, (int)PureAltarSP.Y + y, placeType, false, true);
                    }

                    if((PureAltar[y][x] & 0b0001_1111) >= 7)//multitiles
                    {
                        if((PureAltar[y][x] & 0b0001_1111) == 7){ WorldGen.Place3x2((int)PureAltarSP.X + x, (int)PureAltarSP.Y + y, (ushort)mod.TileType("VoidPillarB")); }
                    }

                    if((PureAltar[y][x] & 0b0001_1111) == 0)
                    {
                        Main.tile[(int)PureAltarSP.X + x, (int)PureAltarSP.Y + y].active(false);
                    }

                    //---------------------------------------------------------------------------------------------------------

                    switch (PureAltarWalls[y][x] & 0b0001_1111)
                    {
                        //This is your wall pallete
                        case 1: placeWall = WallID.GrayBrick; break;
                        case 2: placeWall = WallID.ObsidianBrick; break;
                    }

                    WorldGen.PlaceWall((int)PureAltarSP.X + x, (int)PureAltarSP.Y + y, placeWall, false);

                    if ((PureAltarWalls[y][x] & 0b0001_1111) == 0)
                    {
                        WorldGen.KillWall((int)PureAltarSP.X + x, (int)PureAltarSP.Y + y);
                    }

                    //---------------------------------------------------------------------------------------------------------

                    if (PureAltar[y][x] >> 5 > 0)
                    {
                        if (PureAltar[y][x] >> 5 == 4)
                            Main.tile[(int)PureAltarSP.X + x, (int)PureAltarSP.Y + y].halfBrick(true);
                        else
                            WorldGen.SlopeTile((int)PureAltarSP.X + x, (int)PureAltarSP.Y + y, PureAltar[y][x] >> 5);
                    }
                }
            }           
        }

        public static float rottime = 0;
        public static bool starfall = false;
        public override void PreUpdate()
        {
            rottime += (float)Math.PI / 60;
            if (rottime >= Math.PI * 2)
            {
                rottime = 0;
            }

            if(Main.time == 12 && !Main.bloodMoon && Main.rand.Next(2) == 0)
            {
                starfall = true;
                Main.NewText("The Starlight River is Passing Through!");
            }

            if (starfall)
            {
                Player player = Main.LocalPlayer;
                
                if (Main.time % 2 == 0)
                {
                    Projectile.NewProjectile(new Vector2(Main.rand.Next(0, Main.maxTilesX) * 16 + Main.rand.Next(-16, 16), 100), Vector2.Zero, mod.ProjectileType("StarShard"), 500, 0.5f);
                }
                if(Main.dayTime)
                {
                    starfall = false;
                    foreach(Projectile proj in Main.projectile)
                    {
                        if(proj.type == mod.ProjectileType("StarShard"))
                        {
                            proj.timeLeft = 0;
                        }
                    }
                }
            }
        }

        public override void PostUpdate()
        {
            if (!Main.projectile.Any(proj => proj.type == mod.ProjectileType("Purifier")) && PureTiles != null)
            {
                PureTiles.Clear();
            }
            if (!Main.npc.Any(n => n.type == mod.NPCType("Purity") && n.active == true))
            {
                NPC.NewNPC((int)PureSpawnPoint.X * 16, (int)PureSpawnPoint.Y * 16, mod.NPCType("Purity"));
            }
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(PureTiles)] = PureTiles,
                [nameof(PureSpawnPoint)] = PureSpawnPoint
            };
        }
        public override void Load(TagCompound tag)
        {
            PureTiles = (List<Vector2>)tag.GetList<Vector2>(nameof(PureTiles));
            PureSpawnPoint = tag.Get<Vector2>(nameof(PureSpawnPoint));


            for (int k = 0; k <= PureTiles.Count - 1;  k++)
            {
                for(int i = (int)PureTiles[k].X - 16; i <= (int)PureTiles[k].X + 16; i++)
                {
                    for (int j = (int)PureTiles[k].Y - 16; j <= (int)PureTiles[k].Y + 16; j++)
                    {
                        Tile target = Main.tile[i, j];
                        if (target != null)
                        {
                            if (target.type == (ushort)mod.TileType("StonePure")) { target.type = TileID.Stone; }
                            if (target.type == (ushort)mod.TileType("OreIvory")) { target.type = (ushort)mod.TileType("OreEbony"); }
                        }
                    }
                }
            }
            PureTiles.Clear();
            PureTiles = new List<Vector2> { };
        }   
    }
}

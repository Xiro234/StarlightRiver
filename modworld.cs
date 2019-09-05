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

namespace spritersguildwip
{
    public partial class LegendWorld : ModWorld
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1)
            {
                tasks.Insert(ShiniesIndex + 1, new PassLegacy("Vitrifying Desert", GenerateCrystalCaverns));
            }
        }
        /// <summary>
        /// Generates a crystal cavern at position topCentre, where topCentre is exactly what it is called.
        /// </summary>
        /// <param name="centre">The top centre point of the cavern.</param>
        private void GenerateCrystalCaverns(GenerationProgress progress)
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
        }
        public static float rottime = 0;
        public override void PreUpdate()
        {
            rottime += (float)Math.PI / 60;
            if(rottime >= Math.PI * 2)
            {
                rottime = 0;
            }
        }
        public override void PostWorldGen()
        {
            // Top-Left Position
            Vector2 startpoint = new Vector2(Main.spawnTileX, Main.spawnTileY - 30);

            // Slopes in order: full=0, BL, BR, TL, TR, half
            // Example: bottom-left thick slope is 001X_XXXX
            const byte a = 32, b = 33, c = 34, d = 35, e = 36;

            byte[][] altar = new byte[][] //Tiles
            {
                new byte[] { 0+a, 000, 001, 000, 0+b },
                new byte[] { 000, 001, 001, 001, 000 },
                new byte[] { 001, 001, 001, 001, 001 },
                new byte[] { 000, 001, 001, 001, 000 },
                new byte[] { 0+c, 000, 001, 000, 0+d }
            };

            ushort[][] altarWalls = new ushort[][] //Walls
            {
                new ushort[] { c, c, c, c, c },
                new ushort[] { c, c, c, c, c },
                new ushort[] { c, c, c, c, c },
                new ushort[] { c, c, c, c, c },
                new ushort[] { c, c, c, c, c }
            };
            

            for (int y = 0; y < altar.Length; y++)
            {
                for (int x = 0; x < altar[0].Length; x++)
                {

                    ushort placeType = TileID.Dirt;

                    switch (altar[y][x] & 0b0001_1111)
                    {
                        //This is your block pallete
                        case 0: placeType = TileID.Dirt; break;
                        case 1: placeType = TileID.Stone; break;
                        case 2: placeType = TileID.Dirt; break;
                        case 3: placeType = TileID.Dirt; break;
                        case 4: placeType = TileID.Dirt; break;
                        case 5: placeType = TileID.Dirt; break;
                        case 6: placeType = TileID.Dirt; break;
                        case 7: placeType = TileID.Dirt; break;
                    }

                    WorldGen.PlaceTile((int)startpoint.X + x, (int)startpoint.Y + y, placeType, false, true);
                    WorldGen.PlaceWall((int)startpoint.X + x, (int)startpoint.Y + y, altarWalls[y][x], false);

                    if (altar[y][x] >> 5 > 0)
                    {
                        if (altar[y][x] >> 5 == 4)
                            Main.tile[(int)startpoint.X + x, (int)startpoint.Y + y].halfBrick(true);
                        else
                            WorldGen.SlopeTile((int)startpoint.X + x, (int)startpoint.Y + y, altar[y][x] >> 5);
                    }
                }
            }           
        }
    }
}

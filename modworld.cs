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

        public static bool ForceStarfall = false; 

        //Boss Flags
        public static bool AnyBossDowned = false;
        public static bool GlassBossDowned = false;

        public static bool SealOpen = false;

        //Voidsmith
        public static int[] NPCUpgrades = new int[] { 0,0 };

        public static List<Vector2> PureTiles = new List<Vector2> { };
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            int SurfaceIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Sunflowers"));
            if (ShiniesIndex != -1)
            {
                //tasks.Insert(ShiniesIndex + 1, new PassLegacy("Vitrifying Desert", GenerateCrystalCaverns));
                tasks.Insert(ShiniesIndex + 2, new PassLegacy("Starlight River Ores", EbonyGen));
                //tasks.Insert(ShiniesIndex + 3, new PassLegacy("Starlight River Void Altar", VoidAltarGen));

                tasks.Insert(SurfaceIndex + 1, new PassLegacy("Starlight River Ruins", RuinsGen));
            }
        }
        public override void PostWorldGen()
        {
            VoidAltarGen();
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
            progress.Message = "Making the World Impure...";

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

        private void VoidAltarGen()
        {
            //progress.Message = "Opening the Gates...";

            // Top-Left Position
            Vector2 PureAltarSP = new Vector2(Main.spawnTileX - Main.maxTilesX / 3, Main.maxTilesY - 101);
            PureSpawnPoint = PureAltarSP + new Vector2(202, 57);

            Texture2D Courtyard = ModContent.GetTexture("StarlightRiver/Structures/VoidAltar");

            for(int y = 0; y < Courtyard.Height; y++) // for every row
            {
                Color[] rawData = new Color[Courtyard.Width]; //array of colors
                Rectangle row = new Rectangle(0, y, Courtyard.Width, 1); //one row of the image
                Courtyard.GetData<Color>(0, row, rawData, 0, Courtyard.Width); //put the color data from the image into the array

                for (int x = 0; x < Courtyard.Width; x++) //every entry in the row
                {
                    Main.tile[(int)PureAltarSP.X + x, (int)PureAltarSP.Y + y].ClearEverything(); //clear the tile out
                    Main.tile[(int)PureAltarSP.X + x, (int)PureAltarSP.Y + y].liquidType(0); // clear liquids

                    ushort placeType = 0;
                    ushort wallType = 0;
                    switch (rawData[x].R) //select block
                    {
                        case 10: placeType = TileID.Ash; break;
                        case 20: placeType = (ushort)mod.TileType<Tiles.Void1>(); break;
                        case 30: placeType = (ushort)mod.TileType<Tiles.Void2>(); break;
                        case 40: placeType = (ushort)mod.TileType<Tiles.VoidDoorOn>(); break;
                    }
                    switch (rawData[x].B) //select wall
                    {
                        case 10: wallType = (ushort)mod.WallType<Tiles.VoidWall>(); break;
                        case 20: wallType = (ushort)mod.WallType<Tiles.VoidWallPillar>(); break;
                        case 30: wallType = (ushort)mod.WallType<Tiles.VoidWallPillarS>(); break;
                    }

                    if (placeType != 0) { WorldGen.PlaceTile((int)PureAltarSP.X + x, (int)PureAltarSP.Y + y, placeType, true, true); } //place block
                    if (wallType != 0) { WorldGen.PlaceWall((int)PureAltarSP.X + x, (int)PureAltarSP.Y + y, wallType, true); } //place wall
                }
            }           
        }

        private void RuinsGen(GenerationProgress progress)
        {
            progress.Message = "Spicing up Forests...";
            Texture2D Ruins = ModContent.GetTexture("StarlightRiver/Structures/Ruins");

            for (int x = 0; x + 16 < Main.maxTilesX; x += Main.rand.Next(8, 16))
            {
                if (Main.rand.Next(6) == 0) //1/7 chance to generate
                {
                    for (int y = 0; y < Main.maxTilesY; y++) //find the highest grass block
                    {
                        if (Main.tile[x, y].type == TileID.Grass && Math.Abs(x - Main.maxTilesX / 2) >= 120 && Main.tile[x+ 4,y].active() && Main.tile[x + 8, y].active())// valid placement
                        {
                            int variant = Main.rand.Next(5);

                            //Generation Block
                            for (int y2 = 0; y2 < Ruins.Height; y2++) // for every row
                            {
                                Color[] rawData = new Color[8]; //array of colors
                                Rectangle row = new Rectangle(8 * variant, y2, 8, 1); //one row of the image
                                Ruins.GetData<Color>(0, row, rawData, 0, 8); //put the color data from the image into the array

                                for (int x2 = 0; x2 < 8; x2++) //every entry in the row
                                {
                                    Main.tile[x + x2, y + y2].slope(0);

                                    ushort placeType = 0;
                                    ushort wallType = 0;
                                    switch (rawData[x2].R) //select block
                                    {
                                        case 10: placeType = TileID.GrayBrick; break;
                                        case 20: placeType = TileID.LeafBlock; break;
                                    }
                                    switch (rawData[x2].B) //select wall
                                    {
                                        case 10: wallType = WallID.GrayBrick; break;
                                    }

                                    if (placeType != 0) { WorldGen.PlaceTile(x + x2, y - 15 + y2, placeType, true, true); } //place block
                                    if (wallType != 0) { WorldGen.PlaceWall(x + x2, y - 15 + y2, wallType, true); } //place wall
                                }
                            }
                            break;
                        }
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
            if(Main.time == 12 && ((AnyBossDowned && !Main.bloodMoon && Main.rand.Next(11) == 0) || ForceStarfall))
            {
                starfall = true;
                Main.bloodMoon = false;
                ForceStarfall = false;
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
                NPC.NewNPC((int)PureSpawnPoint.X * 16 - 20, (int)PureSpawnPoint.Y * 16 - 20, mod.NPCType("Purity"));
            }
        }

        public override void Initialize()
        {
            AnyBossDowned = false;

            GlassBossDowned = false;
            ForceStarfall = false;
            SealOpen = false;

            NPCUpgrades = new int[] { 0, 0 };
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(AnyBossDowned)] = AnyBossDowned,
                [nameof(GlassBossDowned)] = GlassBossDowned,
                [nameof(SealOpen)] = SealOpen,

                [nameof(ForceStarfall)] = ForceStarfall,

                [nameof(NPCUpgrades)] = NPCUpgrades,

                [nameof(PureTiles)] = PureTiles,
                [nameof(PureSpawnPoint)] = PureSpawnPoint               
            };
        }
        public override void Load(TagCompound tag)
        {
            AnyBossDowned = tag.GetBool(nameof(AnyBossDowned));
            GlassBossDowned = tag.GetBool(nameof(GlassBossDowned));
            SealOpen = tag.GetBool(nameof(SealOpen));

            ForceStarfall = tag.GetBool(nameof(ForceStarfall));

            NPCUpgrades = tag.GetIntArray(nameof(NPCUpgrades));           

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
                            if (target.type == (ushort)mod.TileType("VoidDoorOff")) { target.type = (ushort)mod.TileType("VoidDoorOn"); }
                        }
                    }
                }
            }

            foreach(NPC npc in Main.npc)
            {
                if (npc.townNPC)
                {
                    npc.life = 250 + NPCUpgrades[0] * 50;
                }
            }
            PureTiles.Clear();
            PureTiles = new List<Vector2> { };
        }   
    }
}

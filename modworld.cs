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
            int HellIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Underworld"));
            int DesertIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            if (ShiniesIndex != -1)
            {
                tasks.Insert(DesertIndex + 1, new PassLegacy("Vitrifying Desert", GenerateCrystalCaverns));
                tasks.Insert(ShiniesIndex + 1, new PassLegacy("Starlight River Ores", EbonyGen));
                //tasks.Insert(HellIndex + 1, new PassLegacy("Starlight River Void Altar", VoidAltarGen));

                tasks.Insert(SurfaceIndex + 1, new PassLegacy("Starlight River Ruins", RuinsGen));
            }
        }
        public override void PostWorldGen()
        {
        }
        
        public int MaxCrystalCaveDepth = 0;
        public static Vector2 vitricTopLeft = new Vector2(); //Initialized after gen. Also don't know how to network, sorry.
        
        /// <summary>
        /// Generates a crystal cavern at position topCentre, where topCentre is exactly what it is called.
        /// </summary>
        /// <param name="centre">The top centre point of the cavern.</param>
        private void GenerateCrystalCaverns(GenerationProgress progress)
        {
            progress.Message = "Vitrifying Desert";
            int size = (int)(Main.maxTilesX / 26f); //Width of the cavern; value shown here is half the size. So, functional size is actually size * 2.
            Point centre = new Point(WorldGen.UndergroundDesertLocation.X + size, WorldGen.UndergroundDesertLocation.Y + 400);
            float depth = 0; //Depth of the cave
            float height = 0; //Height of cave
            int minHeight = 0;
            for (int i = -size; i < size; ++i) //Digs out a cave, mayhaps placeholder
            {
                int x = i + centre.X;

                if (i < -size / 2.5f) //Dig up
                {
                    height += WorldGen.genRand.Next(10, 20) * 0.1f;
                    depth += 1f;
                }
                if (i > size / 2.5f) //Dig back down
                {
                    height -= WorldGen.genRand.Next(10, 20) * 0.1f;
                    depth -= 1f;
                }

                if (height > minHeight)
                    minHeight = (int)height;
                if (depth > MaxCrystalCaveDepth)
                    MaxCrystalCaveDepth = (int)depth;

                int minHei = -(int)(height / 2.5f);
                if (minHei < -56f) //Clamp height
                    minHei = -56;
                int maxHei = (int)(depth / 2.5f) + 20;
                if (maxHei > 74) //Clamp height
                    maxHei = 74;
                for (int j = minHei; j < maxHei; ++j) //Place walls
                {
                    int y = centre.Y + j;
                    Main.tile[x, y].active(false);
                    if ((j > maxHei - 8 || j < minHei + 8 || i < -size + 8 || i > size - 8) && WorldGen.genRand.Next(4) == 0)
                    {
                        WorldGen.TileRunner(x, y, 7, 2, j < maxHei - 8 ? TileID.HardenedSand : ModContent.TileType<Tiles.VitricSand>(), true, 0, 0, false, true);
                        continue;
                    }
                    WorldGen.KillWall(x, y, false);
                    Main.tile[x, y].liquid = 0;
                }
            }
            for (int i = 0; i < 4; ++i) //Places platforms
            {
                Main.tile[centre.X - 12 + i, centre.Y + (MaxCrystalCaveDepth / 5)].active(false);
                WorldGen.PlaceTile(centre.X - 12 + i, centre.Y + (MaxCrystalCaveDepth / 5), TileID.Platforms, true, true, 2);
            }
            for (int i = -4 - WorldGen.genRand.Next(7); i < 10 + WorldGen.genRand.Next(7); ++i) //Digs opening
            {
                for (int j = -12 - WorldGen.genRand.Next(5); j < 14 + WorldGen.genRand.Next(5); ++j)
                {
                    Main.tile[centre.X + i, centre.Y - 20 - j].active(false);
                }
            }
            Vector2 sandMid = GetGroundDirectional(new Vector2(0, 1), centre.ToVector2(), TileID.Platforms);
            GenerateSandDunes(new Point((int)sandMid.X, (int)sandMid.Y + 0), (int)(size * 1.15f)); //Generates SAND under the crystals. Wacky!
            GenerateCrystals(centre); //I wonder what this does
            GenerateMiniCrystals(centre, 30, (int)(size * 0.7f));

            vitricTopLeft = centre.ToVector2() - new Vector2(size, 100);
        }

        ///TODO: Add crystal stairs

        private void GenerateMiniCrystals(Point centre, int reps, int width)
        {
            for (int i = 0; i < reps; ++i)
            {
                if (WorldGen.genRand.Next(3) == 0)
                {
                    Vector2 pos = centre.ToVector2() + new Vector2(WorldGen.genRand.Next(-width, width), WorldGen.genRand.Next((int)(MaxCrystalCaveDepth * 0.8f)));
                    while (Main.tile[(int)pos.X, (int)pos.Y].active())
                        pos = centre.ToVector2() + new Vector2(WorldGen.genRand.Next(-width, width), WorldGen.genRand.Next((int)(MaxCrystalCaveDepth * 0.8f)));
                    Vector2 offset = pos + new Vector2(WorldGen.genRand.Next(1, 20) * (WorldGen.genRand.NextBool() ? 1 : -1), WorldGen.genRand.Next(1, 20) * (WorldGen.genRand.NextBool() ? 1 : -1));
                    Vector2 plPos = pos;
                    int siz = WorldGen.genRand.Next(7, 12);
                    for (int j = 0; j < siz; ++j)
                    {
                        WorldGen.PlaceTile((int)plPos.X, (int)plPos.Y, ModContent.TileType<Tiles.VitricGlassCrystal>(), true, true);
                        WorldGen.KillTile((int)plPos.X, (int)plPos.Y, false, true);
                        int wid = WorldGen.genRand.Next(1, 4);
                        for (int k = 0; k < wid; ++k)
                        {

                        }
                        plPos += Vector2.Normalize(pos - offset) * 0.75f;
                    }
                }
            }
        }

        private void GenerateSandDunes(Point midPoint, int size)
        {
            for (int i = (int)(-size * 0.85f); i < (int)(size * 0.86f); ++i) //Places sand.
            {
                int sHei = (int)(Math.Sin((i / 18f) + (3.14f / 3)) * 8.0f); //Sin wave placement for Y height
                for (int j = sHei; j < 10 + WorldGen.genRand.Next(12, 19); ++j)
                {
                    int off = (int)(i / 5f) * ((i >= 0) ? -1 : 1);
                    if (Main.tile[midPoint.X + i, (midPoint.Y + j) + off].type == (ushort)mod.TileType("GlassCrystal"))
                        continue;
                    WorldGen.KillTile(midPoint.X + i, (midPoint.Y + j) + off, true, false, true);
                    WorldGen.PlaceTile(midPoint.X + i, (midPoint.Y + j) + off, mod.TileType("SandGlass"), true, true, -1, 0);
                }
            }
        }

        private void GenerateCrystals(Point tC)
        {
            float rot = 0f; //Rotation of crystal/placement used later
            int totalReps = (int)(70 * (Main.maxTilesX / 4000f)); //Total repeats
            float shortTau = 6.28f; //Helper variable
            float side = shortTau / 4; //Helper variable
            for (int i = 0; i < totalReps; ++i)
            {
                rot += shortTau / totalReps; //Increases rotation uniformly

                if (rot > shortTau) //Caps angle
                    rot = 0;
                Vector2 randomWallLocation = GetGroundDirectional(new Vector2(0, -1f).RotatedBy(rot), tC.ToVector2(), ModContent.TileType<Tiles.VitricGlassCrystal>())
                    + (new Vector2(0, -1).RotatedBy(rot) * 3); //Position of a wall. Starts off going UP, then goes clockwise.

                int runs = 0;

                while (true)
                {
                    runs++;
                    if (runs > 20)
                        break;

                    //Size of the crystal, going into the wall(s)
                    int dist = WorldGen.genRand.Next(8, 14);
                    Vector2 placePosition = randomWallLocation - (Vector2.Normalize(randomWallLocation - tC.ToVector2()) * (dist * 2.5f)); //Position used for placement base

                    Point nearestWallPositionDifference = new Point(-20, -20);
                    Vector2 wallPos = placePosition + nearestWallPositionDifference.ToVector2();
                    int max = 50;
                    for (int j = -max; j < max; ++j)
                    {
                        if (Main.tile[(int)wallPos.X, (int)wallPos.Y].active() && Math.Abs(nearestWallPositionDifference.X) > Math.Abs(j) && Main.tile[(int)wallPos.X, (int)wallPos.Y].type != ModContent.TileType<Tiles.VitricGlassCrystal>())
                            nearestWallPositionDifference.X = j;
                        for (int k = -max; k < max; ++k)
                        {
                            if (Main.tile[(int)wallPos.X, (int)wallPos.Y].active() && Math.Abs(nearestWallPositionDifference.Y) > Math.Abs(k) && Main.tile[(int)wallPos.X, (int)wallPos.Y].type != ModContent.TileType<Tiles.VitricGlassCrystal>())
                                nearestWallPositionDifference.Y = k;
                        }
                        wallPos = placePosition + nearestWallPositionDifference.ToVector2();
                    }

                    Vector2 dir = (Vector2.Normalize(randomWallLocation - wallPos));//.RotatedBy(WorldGen.genRand.Next(-60, 70) * 0.005f);
                    if (WorldGen.genRand.Next(Math.Abs((int)placePosition.X - tC.X)) < Main.maxTilesX / 80) //Biases crystals towards the sides
                        continue;
                    for (int j = 0; j < dist * 3; ++j)
                    {
                        Vector2 negDir = Vector2.Normalize(new Vector2(1 / dir.X, 1 / -dir.Y));
                        Vector2 actualPlacePos = placePosition - (negDir * 2);
                        for (int k = -5; k < 5; ++k)
                        {
                            Main.tile[(int)actualPlacePos.X, (int)actualPlacePos.Y].active(false);
                            WorldGen.PlaceTile((int)actualPlacePos.X, (int)actualPlacePos.Y, ModContent.TileType<Tiles.VitricGlassCrystal>());
                            actualPlacePos += (negDir / 4);
                        }
                        placePosition += dir;
                    }
                    break;
                }

                //PlaceCrystal(tC, randomWallLocation, (Math.Abs((int)randomWallLocation.X - tC.X) / 6) + (WorldGen.genRand.Next(8, 23)));
            }
        }

        ///Deprecated!
        /// <summary> Moved to a seperate method in order to test something. Since moving it back is hard, I won't. It's staying.</summary>
        private void PlaceCrystal(Point origin, Vector2 position, int siz = 6)
        {
            float side = 6.28f / 4; //Helper variable

            float adjRot = Vector2.Normalize(origin.ToVector2() - position).ToRotation() + side; //Rotation direction of the direction of the crystals, starting from the wall.
            adjRot += WorldGen.genRand.Next((int)(-side * 18), (int)(side * 18)) * 0.01f; //Randomization of angle
            Vector2 direction = new Vector2(0, -1).RotatedBy(adjRot); //Angle velocity of the crystal

            int wid = WorldGen.genRand.Next(1, 3); //Partial width of the crystal
            int negWid = -WorldGen.genRand.Next(1, 3); //Partial width of the crystal - formatted in this way to make odd numbered widths possible

            for (int j = 0; j < siz; ++j) //Places crystal, replace PlaceTile with TileRunner or other method of choice
            {
                WorldGen.PlaceTile((int)position.X, (int)position.Y, ModContent.TileType<Tiles.VitricGlass>(), true, true, -1, 0);
                position += direction;

                Vector2 newDir = new Vector2(0, -1).RotatedBy(Vector2.Normalize(origin.ToVector2() - position).ToRotation() - (side * 2));
                Vector2 widthPos = position - (newDir * ((wid + negWid) / 2f));
                for (float k = negWid; k < wid; k += 0.5f) //Widens the crystal
                {
                    WorldGen.PlaceTile((int)widthPos.X, (int)widthPos.Y, ModContent.TileType<Tiles.VitricGlass>(), true, true, -1, 0);
                    widthPos += newDir / 2;
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
        
        private void EbonyGen(GenerationProgress progress)
        {
            progress.Message = "Making the World Impure...";

            for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * .0015); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next(0, (int)WorldGen.worldSurfaceHigh);

                if (Main.tile[x, y].type == TileID.Dirt  && Math.Abs(x - Main.maxTilesX / 2) >= Main.maxTilesX / 6)
                {
                    WorldGen.TileRunner(x, y, (double)WorldGen.genRand.Next(10, 11), 1, mod.TileType("OreEbony"), false, 0f, 0f, false, true);
                }
            }
        }

        private void VoidAltarGen(GenerationProgress progress)
        {
            progress.Message = "Opening the Gates...";

            // Top-Left Position
            Vector2 PureAltarSP = new Vector2(Main.spawnTileX - 50, Main.maxTilesY - 101);
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
                        case 20: placeType = (ushort)ModContent.TileType<Tiles.Void1>(); break;
                        case 30: placeType = (ushort)ModContent.TileType<Tiles.Void2>(); break;
                        case 40: placeType = (ushort)ModContent.TileType<Tiles.VoidDoorOn>(); break;
                    }
                    switch (rawData[x].B) //select wall
                    {
                        case 10: wallType = (ushort)ModContent.WallType<Tiles.VoidWall>(); break;
                        case 20: wallType = (ushort)ModContent.WallType<Tiles.VoidWallPillar>(); break;
                        case 30: wallType = (ushort)ModContent.WallType<Tiles.VoidWallPillarS>(); break;
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
                if (Main.rand.Next(4) == 0) // 1/5 chance to generate
                {
                    for (int y = 0; y < Main.maxTilesY; y++) // find the highest grass block
                    {
                        if (Main.tile[x, y].type == TileID.Grass && Math.Abs(x - Main.maxTilesX / 2) >= Main.maxTilesX / 6 && Main.tile[x+ 4,y].active() && Main.tile[x + 8, y].active())// valid placement
                        {
                            int variant = Main.rand.Next(5);

                            // Generation Block
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

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Overgrow Generation
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void GenerateOvergrowRoom(int i, int j)
        {
            Texture2D Rooms = ModContent.GetTexture("StarlightRiver/Structures/Ruins");
            int variant = Main.rand.Next(5);

            // Generation Block
            for (int y = 0; y < 80; y++) // for every row
            {
                Color[] rawData = new Color[8]; //array of colors
                Rectangle row = new Rectangle(80 * variant, y, 80, 1); //one row of the image
                Rooms.GetData<Color>(0, row, rawData, 0, 80); //put the color data from the image into the array

                for (int x = 0; x < 80; x++) //every entry in the row
                {
                    Main.tile[i + x, j + y].ClearEverything();
                    Main.tile[i + x, j + y].slope(0);

                    ushort placeType = 0;
                    ushort wallType = 0;
                    switch (rawData[x].R) //select block
                    {
                        case 10: placeType = TileID.GrayBrick; break;
                        case 20: placeType = TileID.LeafBlock; break;
                    }
                    switch (rawData[x].B) //select wall
                    {
                        case 10: wallType = WallID.GrayBrick; break;
                    }

                    if (placeType != 0) { WorldGen.PlaceTile(i + x, j + y, placeType, true, true); } //place block
                    if (wallType != 0) { WorldGen.PlaceWall(i + x, j + y, wallType, true); } //place wall
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
                Main.NewText("The Starlight River is Passing Through!",120, 241, 255);
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
                [nameof(vitricTopLeft)] = vitricTopLeft,

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
            vitricTopLeft = tag.Get<Vector2>(nameof(vitricTopLeft));
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

using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework.Graphics;
using System;
using StarlightRiver.Structures;
using StarlightRiver.Keys;

namespace StarlightRiver
{
    
    public partial class LegendWorld : ModWorld
    {
        public static Vector2 BookSP;
        public static Vector2 DashSP;
        public static Vector2 WispSP;
        public static Vector2 PureSP;
        public static Vector2 SmashSP;

        public static Vector2 RiftLocation;

        public static bool ForceStarfall = false; 

        //Boss Flags
        public static bool AnyBossDowned = false;
        public static bool GlassBossDowned = false;

        public static bool SealOpen = false;

        //Voidsmith
        public static int[] NPCUpgrades = new int[] { 0,0 };

        public static List<Vector2> PureTiles = new List<Vector2> { };

        public static Rectangle vitricBiome = new Rectangle();

        //Handling Keys
        public static List<Key> Keys = new List<Key>();
        public static List<Key> KeyInventory = new List<Key>();
                     
        
        private void EbonyGen(GenerationProgress progress)
        {
            progress.Message = "Making the World Impure...";

            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * .0015); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next(0, (int)WorldGen.worldSurfaceHigh);

                if (Main.tile[x, y].type == TileID.Dirt  && Math.Abs(x - Main.maxTilesX / 2) >= Main.maxTilesX / 6)
                {
                    WorldGen.TileRunner(x, y, (double)WorldGen.genRand.Next(10, 11), 1, ModContent.TileType<Tiles.OreEbony>(), false, 0f, 0f, false, true);
                }
            }
        }

        private void DolomiteGen(GenerationProgress progress)
        {
            progress.Message = "Shifting Tectonic Plates...";
            ushort Dolomite = (ushort)ModContent.TileType<Tiles.Dolomite.Dolomite>();

            for (int k = 0; k < (Main.rand.Next(4,8)); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY);

                for(int i = x - 200; i <= x + 200; i++)
                {
                    for (int j = y - 100; j <= y + 100; j++)
                    {
                        if(i > 20 && j > 20 && i < Main.maxTilesX - 20 && j < Main.maxTilesY - 20)
                        {
                            if (Main.tile[i, j].type is TileID.Stone) { Main.tile[i, j].type = Dolomite; }

                            if (!Main.tile[i, j + 2].active() && Main.tile[i, j].type == Dolomite) { Main.tile[i, j + 1].type = (ushort)ModContent.TileType<Tiles.Dolomite.DolomiteHanging>(); }

                            if (i % 15 == 0 && Main.tile[i, j].active() && Main.tile[i, j].type == Dolomite && !Main.tile[i, j - 1].active() && Main.rand.Next(3) == 0)
                            {
                                GenerateBeam(i, j);
                            }
                        }
                    }
                }
            }
        }

        private void GenerateBeam(int x, int y)
        {
            for (int j = y - 1; j >= y - 100; j--)
            {
                if (j % 10 == 0 && Main.rand.Next(2) == 0)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        if (!Main.tile[x + k, j].active()) { Main.tile[x + k, j].type = TileID.WoodenBeam; }
                    }
                }

                if (x > 0 && j > 0 && x < Main.maxTilesX && j < Main.maxTilesY)
                {
                    Main.tile[x, j].wall = WallID.Wood;
                    if (Main.tile[x, j].active() || j == y - 100)
                    {
                        for (int k = -2; k <= 2; k++)
                        {
                            Main.tile[x + k, j].type = TileID.WoodBlock;
                        }
                        return;
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
                Main.NewText("The Starlight River is Passing Through!",120, 241, 255);
            }

            if (starfall)
            {
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
            if (!Main.projectile.Any(proj => proj.type == ModContent.ProjectileType<Projectiles.Ability.Purifier>()) && PureTiles != null)
            {
                PureTiles.Clear();
            }

            if (!Main.npc.Any(n => n.type == ModContent.NPCType<NPCs.Pickups.Wind>() && n.active == true))
            {
                NPC.NewNPC((int)DashSP.X, (int)DashSP.Y, ModContent.NPCType<NPCs.Pickups.Wind>());
            }

            if (!Main.npc.Any(n => n.type == ModContent.NPCType<NPCs.Pickups.Lore>() && n.active == true))
            {
                NPC.NewNPC((int)BookSP.X, (int)BookSP.Y, ModContent.NPCType<NPCs.Pickups.Lore>());
            }

            if (!Main.npc.Any(n => n.type == ModContent.NPCType<NPCs.Pickups.Wisp>() && n.active == true))
            {
                NPC.NewNPC((int)WispSP.X, (int)WispSP.Y, ModContent.NPCType<NPCs.Pickups.Wisp>());
            }

            //Keys
            foreach(Key key in Keys)
            {
                key.Update();
            }
        }

        public override void Initialize()
        {
            vitricBiome.X = 0;
            vitricBiome.Y = 0;

            AnyBossDowned = false;
            GlassBossDowned = false;
            SealOpen = false;

            ForceStarfall = false;

            NPCUpgrades = new int[] { 0, 0 };
            PureTiles = new List<Vector2>();

            BookSP = Vector2.Zero;
            DashSP = Vector2.Zero;
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                ["VitricBiomePos"] = vitricBiome.TopLeft(),
                ["VitricBiomeSize"] = vitricBiome.Size(),

                [nameof(AnyBossDowned)] = AnyBossDowned,
                [nameof(GlassBossDowned)] = GlassBossDowned,
                [nameof(SealOpen)] = SealOpen,

                [nameof(ForceStarfall)] = ForceStarfall,

                [nameof(NPCUpgrades)] = NPCUpgrades,

                [nameof(PureTiles)] = PureTiles,

                [nameof(BookSP)] = BookSP,
                [nameof(DashSP)] = DashSP,  
                
                [nameof(RiftLocation)] = RiftLocation
            };
        }
        public override void Load(TagCompound tag)
        {
            vitricBiome.X = (int)tag.Get<Vector2>("VitricBiomePos").X;
            vitricBiome.Y = (int)tag.Get<Vector2>("VitricBiomePos").Y;

            vitricBiome.Width = (int)tag.Get<Vector2>("VitricBiomeSize").X;
            vitricBiome.Height = (int)tag.Get<Vector2>("VitricBiomeSize").Y;

            AnyBossDowned = tag.GetBool(nameof(AnyBossDowned));
            GlassBossDowned = tag.GetBool(nameof(GlassBossDowned));
            SealOpen = tag.GetBool(nameof(SealOpen));

            ForceStarfall = tag.GetBool(nameof(ForceStarfall));

            NPCUpgrades = tag.GetIntArray(nameof(NPCUpgrades));           

            PureTiles = (List<Vector2>)tag.GetList<Vector2>(nameof(PureTiles));

            BookSP = tag.Get<Vector2>(nameof(BookSP));
            DashSP = tag.Get<Vector2>(nameof(DashSP));

            RiftLocation = tag.Get<Vector2>(nameof(RiftLocation));


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

            foreach(Key key in KeyInventory)
            {
                GUI.KeyInventory.keys.Add(new GUI.KeyIcon(key, false));
            }
        }   
    }
}

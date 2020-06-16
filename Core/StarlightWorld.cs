using Microsoft.Xna.Framework;
using StarlightRiver.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace StarlightRiver
{
    public partial class StarlightWorld : ModWorld
    {
        public static Vector2 BookSP;
        public static Vector2 DashSP;
        public static Vector2 WispSP;
        public static Vector2 PureSP;
        public static Vector2 SmashSP;

        public static Vector2 RiftLocation;

        public static bool ForceStarfall = false;

        //Boss Flags
        public static bool DesertOpen = false;

        public static bool GlassBossOpen = false;
        public static bool GlassBossDowned = false;

        public static bool OvergrowBossOpen = false;
        public static bool OvergrowBossFree = false;
        public static bool OvergrowBossDowned = false;

        public static bool SealOpen = false;

        //Voidsmith
        public static int[] NPCUpgrades = new int[] { 0, 0 };

        public static List<Vector2> PureTiles = new List<Vector2> { };

        public static Rectangle VitricBiome = new Rectangle();

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

                if (Main.tile[x, y].type == TileID.Dirt && Math.Abs(x - Main.maxTilesX / 2) >= Main.maxTilesX / 6)
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(10, 11), 1, ModContent.TileType<Tiles.OreEbony>(), false, 0f, 0f, false, true);
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
            foreach (Key key in Keys)
            {
                key.Update();
            }
        }

        public override void Initialize()
        {
            VitricBiome.X = 0;
            VitricBiome.Y = 0;

            DesertOpen = false;
            GlassBossOpen = false;
            GlassBossDowned = false;

            OvergrowBossDowned = false;
            OvergrowBossFree = false;
            OvergrowBossOpen = false;

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
                ["VitricBiomePos"] = VitricBiome.TopLeft(),
                ["VitricBiomeSize"] = VitricBiome.Size(),

                [nameof(DesertOpen)] = DesertOpen,
                [nameof(GlassBossOpen)] = GlassBossOpen,
                [nameof(GlassBossDowned)] = GlassBossDowned,

                [nameof(OvergrowBossOpen)] = OvergrowBossOpen,
                [nameof(OvergrowBossFree)] = OvergrowBossFree,
                [nameof(OvergrowBossDowned)] = OvergrowBossDowned,

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
            VitricBiome.X = (int)tag.Get<Vector2>("VitricBiomePos").X;
            VitricBiome.Y = (int)tag.Get<Vector2>("VitricBiomePos").Y;

            VitricBiome.Width = (int)tag.Get<Vector2>("VitricBiomeSize").X;
            VitricBiome.Height = (int)tag.Get<Vector2>("VitricBiomeSize").Y;

            DesertOpen = tag.GetBool(nameof(DesertOpen));
            GlassBossOpen = tag.GetBool(nameof(GlassBossOpen));
            GlassBossDowned = tag.GetBool(nameof(GlassBossDowned));

            OvergrowBossOpen = tag.GetBool(nameof(OvergrowBossOpen));
            OvergrowBossFree = tag.GetBool(nameof(OvergrowBossFree));
            OvergrowBossDowned = tag.GetBool(nameof(OvergrowBossDowned));

            SealOpen = tag.GetBool(nameof(SealOpen));

            ForceStarfall = tag.GetBool(nameof(ForceStarfall));

            NPCUpgrades = tag.GetIntArray(nameof(NPCUpgrades));

            PureTiles = (List<Vector2>)tag.GetList<Vector2>(nameof(PureTiles));

            BookSP = tag.Get<Vector2>(nameof(BookSP));
            DashSP = tag.Get<Vector2>(nameof(DashSP));

            RiftLocation = tag.Get<Vector2>(nameof(RiftLocation));

            for (int k = 0; k <= PureTiles.Count - 1; k++)
            {
                for (int i = (int)PureTiles[k].X - 16; i <= (int)PureTiles[k].X + 16; i++)
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

            foreach (NPC npc in Main.npc)
            {
                if (npc.townNPC)
                {
                    npc.life = 250 + NPCUpgrades[0] * 50;
                }
            }
            PureTiles.Clear();
            PureTiles = new List<Vector2> { };

            foreach (Key key in KeyInventory)
            {
                GUI.KeyInventory.keys.Add(new GUI.KeyIcon(key, false));
            }
        }
    }
}
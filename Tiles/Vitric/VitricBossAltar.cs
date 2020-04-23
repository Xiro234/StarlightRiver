using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using StarlightRiver.GUI;
using StarlightRiver.NPCs.Boss.VitricBoss;

namespace StarlightRiver.Tiles.Vitric
{
    class VitricBossAltar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;

            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.Height = 7;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            dustType = DustID.Stone;
            disableSmartCursor = true;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Vitric Altar");
            AddMapEntry(new Color(113, 113, 113), name);
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            if (tile.frameX % 90 == 0 && tile.frameY == 0)
            {
                if (!Main.projectile.Any(n => n.type == ModContent.ProjectileType<Projectiles.Dummies.VitricAltarDummy>() && n.active && n.Hitbox.Contains(new Point(i * 16 + 8, j * 16 + 8))))
                {
                    Projectile.NewProjectile(new Vector2(i, j) * 16 + new Vector2(40, 56), Vector2.Zero, ModContent.ProjectileType<Projectiles.Dummies.VitricAltarDummy>(), 0, 0);
                }
                if (!Main.npc.Any(n => n.type == ModContent.NPCType<VitricBackdropLeft>() && n.active))
                {
                    Vector2 center = new Vector2(i * 16 + 40, j * 16 + 114);
                    int timerset = LegendWorld.GlassBossOpen ? 360 : 0;

                    int index = NPC.NewNPC((int)center.X + 120, (int)center.Y, ModContent.NPCType<VitricBackdropRight>(), 0, timerset);
                    if (LegendWorld.GlassBossOpen && Main.npc[index].modNPC is VitricBackdropRight) (Main.npc[index].modNPC as VitricBackdropRight).SpawnPlatforms(false);

                    index = NPC.NewNPC((int)center.X - 120 - 560, (int)center.Y, ModContent.NPCType<VitricBackdropLeft>(), 0, timerset);
                    if (LegendWorld.GlassBossOpen && Main.npc[index].modNPC is VitricBackdropLeft) (Main.npc[index].modNPC as VitricBackdropLeft).SpawnPlatforms(false);

                }
            }
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if(Main.tile[i,j].frameX == 90 && Main.tile[i,j].frameY == 0)
            {
                //draw the boss summon hologram here
            }
            return true;
        }
        public override bool NewRightClick(int i, int j)
        {
            if(Main.tile[i, j].frameX >= 90)
            {
                //boss spawning logic somewhere in here I guess
            }
            return true;
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Codex;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver
{
    public static class Helper
    {
        /// <summary>
        /// Kills the NPC.
        /// </summary>
        /// <param name="npc"></param>
        public static void Kill(this NPC npc)
        {
            bool modNPCDontDie = npc.modNPC != null && !npc.modNPC.CheckDead();
            if (modNPCDontDie)
                return;

            npc.life = 0;
            npc.checkDead();
            npc.HitEffect();
            npc.active = false;
        }

        public static void PlaceMultitile(int width, int height, int xPos, int yPos, int type)
        {
            for (int multiN = 0; multiN < width; multiN++)
            {
                for (int multiM = 0; multiM < height; multiM++)
                {
                    Tile tileAt = Main.tile[multiN + xPos, multiM + yPos];
                    //WorldGen.PlaceTile(multiN + xPos, multiM + yPos, type, false, true);
                    tileAt.type = (ushort)type;
                    tileAt.frameX = (short)(multiN * 18);
                    tileAt.frameY = (short)(multiM * 18);
                }
            }
        }

        public static bool AirScanUp(Vector2 start, int MaxScan)
        {
            if (start.Y - MaxScan < 0) { return false; }

            bool clear = true;

            for (int k = 0; k <= MaxScan; k++)
            {
                if (Main.tile[(int)start.X, (int)start.Y - k].active()) { clear = false; }
            }
            return clear;
        }

        public static void UnlockEntry<type>(Player player)
        {
            player.GetModPlayer<CodexHandler>().Entries.FirstOrDefault(entry => entry is type).Locked = false;
            GUI.Codex.NewEntry = true;
        }

        public static void SpawnGem(int ID, Vector2 position)
        {
            int item = Item.NewItem(position, ModContent.ItemType<Items.StarlightGem>());
            (Main.item[item].modItem as Items.StarlightGem).gemID = ID;
        }

        public static void DrawSymbol(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            Texture2D tex = ModContent.GetTexture("StarlightRiver/Symbol");
            float scale = 0.9f + (float)Math.Sin(LegendWorld.rottime) * 0.1f;
            spriteBatch.Draw(tex, position, tex.Frame(), color * 0.8f * scale, 0, tex.Size() * 0.5f, scale * 0.8f, 0, 0);

            Texture2D tex2 = ModContent.GetTexture("StarlightRiver/Tiles/Interactive/WispSwitchGlow2");
            float fade = LegendWorld.rottime / 6.28f;
            spriteBatch.Draw(tex2, position, tex2.Frame(), color * (1 - fade), 0, tex2.Size() / 2f, fade * 0.6f, 0, 0);
        }
    }
}

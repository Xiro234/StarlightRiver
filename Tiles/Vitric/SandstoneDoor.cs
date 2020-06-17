﻿using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Vitric
{
    internal class SandstoneDoor : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }

        public override void SetDefaults() => QuickBlock.QuickSetFurniture(this, 8, 2, DustID.Stone, SoundID.Tink, false, new Color(130, 85, 45));

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (StarlightWorld.DesertOpen && !Main.npc.Any(n => n.type == NPCType<NPCs.Boss.VitricBoss.VitricBoss>() && n.active)) Main.tileSolid[Type] = false;
            else Main.tileSolid[Type] = true;
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0)
            {
                Texture2D tex = GetTexture("StarlightRiver/Tiles/Vitric/SandstoneDoor");
                Vector2 basepos = (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition;
                int off = StarlightWorld.DesertOpen ? 46 : 0;
                spriteBatch.Draw(tex, basepos + new Vector2(-off, 0), tex.Frame(), drawColor, 0, Vector2.Zero, 1, 0, 0);
                spriteBatch.Draw(tex, basepos + new Vector2(tex.Width + off, 0), tex.Frame(), drawColor, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
            }
        }
    }
}
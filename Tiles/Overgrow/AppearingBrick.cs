using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace StarlightRiver.Tiles.Overgrow
{
    class AppearingBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            minPick = 999;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            if (Main.tile[i, j].frameX == 20 && Main.rand.Next(5) == 0)
            {
                Dust.NewDustPerfect(new Vector2(i + 0.5f, j + 0.5f) * 16, ModContent.DustType<Dusts.Gold2>(), new Vector2(0, 1));
            }
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (!Main.npc.Any(n => n.active && n.type == ModContent.NPCType<NPCs.Boss.OvergrowBoss.OvergrowBoss>()))
                Main.tile[i, j].frameX = 0;

            if(Main.tile[i, j].frameX == 20)
            {
                Main.tileSolid[Type] = true;
            }
            else Main.tileSolid[Type] = false;
        }
    }
}

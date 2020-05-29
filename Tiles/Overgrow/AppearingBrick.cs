using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Overgrow
{
    internal class AppearingBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            minPick = 999;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            if (Main.tile[i, j].frameX == 20 && Main.rand.Next(2) == 0)
            {
                Dust.NewDustPerfect(new Vector2(i + 0.5f, j + 0.5f) * 16, ModContent.DustType<Dusts.Gold2>(), new Vector2(0, 1));
            }
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (!Main.npc.Any(n => n.active && n.type == ModContent.NPCType<NPCs.Boss.OvergrowBoss.OvergrowBoss>() && n.ai[0] == (int)NPCs.Boss.OvergrowBoss.OvergrowBoss.OvergrowBossPhase.FirstGuard))
                Main.tile[i, j].frameX = 0;

            if (Main.tile[i, j].frameX == 20)
            {
                Main.tileSolid[Type] = true;
            }
            else Main.tileSolid[Type] = false;
        }
    }
}

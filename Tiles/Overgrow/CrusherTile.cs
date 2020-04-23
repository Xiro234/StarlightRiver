using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Overgrow
{
    class CrusherTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileMerge[Type][mod.GetTile("GrassOvergrow").Type] = true;
            Main.tileMerge[Type][mod.GetTile("BrickOvergrow").Type] = true;
            Main.tileFrameImportant[Type] = true;
            dustType = mod.DustType("Gold2");          
            AddMapEntry(new Color(81, 77, 71));
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Dust.NewDustPerfect(new Vector2(4 + i * 16, 4 + j * 16), ModContent.DustType<Dusts.Gold2>());
            Lighting.AddLight(new Vector2(i * 16, j * 16), new Vector3(255, 200, 110) * 0.001f);
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Vector2 pos = new Vector2(4 + i * 16, 4 + j * 16);
            if (!Main.npc.Any(npc => npc.type == ModContent.NPCType<NPCs.Traps.Crusher>() && (npc.modNPC as NPCs.Traps.Crusher).Parent == Main.tile[i, j] && npc.active))
            {
                int crusher = NPC.NewNPC((int)pos.X + 4, (int)pos.Y + 21, ModContent.NPCType<NPCs.Traps.Crusher>());
                (Main.npc[crusher].modNPC as NPCs.Traps.Crusher).Parent = Main.tile[i, j];
            }
        }
    }
}

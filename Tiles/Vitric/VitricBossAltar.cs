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
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);
            dustType = DustID.Stone;
            disableSmartCursor = true;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Vitric Altar");
            AddMapEntry(new Color(113, 113, 113), name);
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if(Main.tile[i,j].frameX == 0 && Main.tile[i,j].frameY == 0)
            {
                Color color = Main.npc.Any(npc => npc.type == ModContent.NPCType<VitricBoss>() && npc.active) ? new Color(255, 70, 70) : new Color(200, 235, 255);
                Helper.DrawSymbol(spriteBatch, (new Vector2(i + 12, j + 12) * 16 + new Vector2(24, -18)) - Main.screenPosition, color);
            }
            return true;
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            if (Main.tile[i, j].frameX == 0 && Main.tile[i, j].frameY == 0 && !Main.npc.Any(npc => npc.type == ModContent.NPCType<VitricBoss>() && npc.active))
            {
                float rot = Main.rand.NextFloat(6.28f);
                Dust.NewDustPerfect(new Vector2(i, j) * 16 + new Vector2(24, -18), ModContent.DustType<Dusts.Starlight>(), Vector2.One.RotatedBy(rot) * 6f);
            }
        }

        public override bool NewRightClick(int i, int j)
        {
            if(!Main.npc.Any(npc => npc.type == ModContent.NPCType<VitricBoss>() && npc.active))
            {
                NPC.NewNPC(i * 16, (j - 10) * 16, ModContent.NPCType<VitricBoss>());
                NPC.NewNPC(i * 16, (j - 10) * 16, ModContent.NPCType<VitricBossArenaManager>());
                for (int k = 0; k <= 150; k++)
                {
                    float scale = Main.rand.NextFloat(2);
                    Dust.NewDustPerfect(new Vector2(i * 16, (j - 14) * 16), ModContent.DustType<Dusts.Air>(), Vector2.One.RotatedByRandom(6.28f) * scale, 0, default, (2 - scale) * 2f);
                }
            }
            return true;
        }
    }
}
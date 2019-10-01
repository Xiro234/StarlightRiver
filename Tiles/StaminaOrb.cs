using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Ability;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles
{
    class StaminaOrb : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = true;

            drop = mod.ItemType("StaminaOrbItem");
            dustType = mod.DustType("Stamina");
            AddMapEntry(new Color(255, 186, 66));
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.236f / 1.1f;
            g = 0.144f / 1.1f;
            b = 0.071f / 1.1f;
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            if (Main.npc.Any(npc => npc.Hitbox.Intersects(new Rectangle(i * 16 + 4, j * 16 + 4, 1, 1)) && npc.type == mod.NPCType("Stamina2") && npc.active && npc.localAI[0] != 0))
            {
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/StaminaOrbBack"), new Vector2((i + 12) * 16, (j + 12) * 16) - Main.screenPosition, Lighting.GetColor(i,j));
            }
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Vector2 pos = new Vector2(i * 16, j * 16) - new Vector2(-8, -19);
            if (!Main.npc.Any(npc => npc.Hitbox.Intersects(new Rectangle(i * 16 + 4, j * 16 + 4, 1, 1)) && npc.type == mod.NPCType("Stamina2") && npc.active))
            {
                NPC.NewNPC((int)pos.X, (int)pos.Y, mod.NPCType("Stamina2"));
            }
        }
    }
}

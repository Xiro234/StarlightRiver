using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles
{
    class Bounce : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;

            drop = mod.ItemType("Bounce");
            dustType = mod.DustType("Air");
            AddMapEntry(new Color(115, 182, 158));
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Vector2 pos = new Vector2(i * 16, j * 16) - new Vector2(-8, -19);
            if (!Main.npc.Any(npc => npc.Hitbox.Intersects(new Rectangle(i*16 + 4, j*16 + 4, 1,1)) && npc.type == mod.NPCType("Bouncer") && npc.active ))
            {
                NPC.NewNPC((int)pos.X, (int)pos.Y, mod.NPCType("Bouncer"));
            }
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color color = new Color(255, 255, 255) * (float)Math.Sin(LegendWorld.rottime);
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/BounceGlow"), new Vector2((i+ 12) * 16 -1, (j+12) * 16 - 1) - Main.screenPosition, color);
        }
    }
}

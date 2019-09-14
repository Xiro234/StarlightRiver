using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Ability;
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
            Main.tileSolid[Type] = true;

            drop = mod.ItemType("Bounce");
            dustType = mod.DustType("Air");
            AddMapEntry(new Color(115, 182, 158));
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Vector2 pos = new Vector2(i * 16, j * 16) - new Vector2(-8, -20);
            if (!Main.npc.Any(npc => npc.position == new Vector2((int)pos.X, (int)pos.Y) && npc.type == mod.NPCType("Bouncer") ))
            {
                NPC.NewNPC((int)pos.X, (int)pos.Y, mod.NPCType("Bouncer"));
            }
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color color = new Color(255, 255, 255) * (float)Math.Sin(LegendWorld.rottime);
            spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/Tiles/BounceGlow"), new Vector2(i * 16, j * 16) - Main.screenPosition - new Vector2(30,30), color);
        }
    }
}

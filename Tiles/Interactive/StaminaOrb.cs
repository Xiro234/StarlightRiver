using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Interactive
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
            Vector2 pos = new Vector2(i * 16, j * 16) - new Vector2(-8, -11);
            if (!Main.projectile.Any(proj => proj.Hitbox.Intersects(new Rectangle(i * 16 + 4, j * 16 + 4, 1, 1)) && proj.type == ModContent.ProjectileType<Projectiles.Dummies.StaminaOrbDummy>() && proj.active))
            {
                Projectile.NewProjectile(pos, Vector2.Zero, ModContent.ProjectileType<Projectiles.Dummies.StaminaOrbDummy>(), 0, 0);
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

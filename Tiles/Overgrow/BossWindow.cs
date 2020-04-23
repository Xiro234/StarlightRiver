using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;

namespace StarlightRiver.Tiles.Overgrow
{
    class BossWindow : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (!Main.npc.Any(n => n.active && n.type == ModContent.NPCType<Projectiles.Dummies.OvergrowBossWindowDummy>() && n.Hitbox.Contains(new Point(i * 16, j * 16))))
            {
                NPC.NewNPC(i * 16 + 8, j * 16 + 72, ModContent.NPCType<Projectiles.Dummies.OvergrowBossWindowDummy>(), 0, LegendWorld.OvergrowBossOpen ? 360 : 0);
            }
        }
    }
}

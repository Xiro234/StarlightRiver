using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Tiles.Overgrow
{
    internal class BossWindow : ModTile
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
                NPC.NewNPC(i * 16 + 8, j * 16 + 72, ModContent.NPCType<Projectiles.Dummies.OvergrowBossWindowDummy>(), 0, StarlightWorld.OvergrowBossOpen ? 360 : 0);
            }
        }
    }
}
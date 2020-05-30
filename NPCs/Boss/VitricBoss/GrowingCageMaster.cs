using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    internal class GrowingCageMaster : ModProjectile
    {
        public override string Texture => "StarlightRiver/Invisible";
        public VitricBoss Parent;

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.timeLeft = 2;
        }

        public override void AI()
        {
            if (Parent == null) { projectile.Kill(); return; } //This projectile's AI relies on it's parent boss - if its not present, this projectile will kill itself.

            NPC parent = Parent.npc; //the npc of the parent
            if (parent.ai[3] < 3000) //gives the player 5 seconds for the cage to spread out
            {
                projectile.velocity = projectile.velocity.RotatedBy(6.28f / 3000); //make one full rotation by the time were done
            }
        }
    }
}